using SlitherModel;
using SlitherModel.Processed;
using SlitherModel.Processed.BadCollision;
using SlitherModel.Source;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SlitherProcessor
{
    public class BadCollisionSliceProcessor
    {
        private readonly CollisionService _collisionService;
        int worldRadius = 21600;

        public BadCollisionSliceProcessor(CollisionService collisionService)
        {
            _collisionService = collisionService;
        }

        public List<Collision> ProcessSlice(SlitherFrame slitherFrame, double angleStart, double angleEnd, int distanceStep)
        {
            var collision = new List<Collision>();
            var selfRadius = GetSnakeRadius(slitherFrame.Snake);

            foreach (var snake in slitherFrame.Snakes)
            {
                if(snake.DeadAmt == 0)
                {
                    var snakeRadius = GetSnakeRadius(snake);
                    var radius = selfRadius + snakeRadius;
                    var distance = _collisionService.GetDistance(snake.Xx, snake.Yy, radius, angleStart, angleEnd, distanceStep);
                    if (distance != null)
                    {
                        collision.Add(new SnakeHead { Distance = distance.Value, RelativeAngle = snake.Ang, Size = snake.Pts.Count });
                    }

                    var points = snake.Pts.Where(p => p.Dying == false);
                    for (int index = 0; index < points.Count(); index++)
                    {
                        distance = _collisionService.GetDistance(snake.Pts[index].Xx, snake.Pts[index].Yy, radius, angleStart, angleEnd, distanceStep);
                        if (distance != null)
                        {
                            collision.Add(new SnakeBody { Distance = distance.Value, Size = snakeRadius, StartIndex = index, EndIndex = points.Count() - index });
                        }
                    }
                }
            }

            collision.Add(new Boundry { Distance = GetBoundryDistance(slitherFrame.WorldCenter, selfRadius, angleStart, angleEnd, distanceStep) });

            return collision.OrderBy(c => c.Distance).ToList();
        }

        private double GetSnakeRadius(Snake snake)
        {
            return snake.Sc * 14.5;
        }

        private double GetBoundryDistance(Coordinates worldCenter, double selfRadius, double angleStart, double angleEnd, int distanceStep)
        {
            var start = GetWorldEndPoint(worldCenter, angleStart);
            var distance = Math.Sqrt(Math.Pow(start.X, 2) + Math.Pow(start.Y, 2));
            var end = GetWorldEndPoint(worldCenter, angleEnd);
            var endDistance = Math.Sqrt(Math.Pow(end.X, 2) + Math.Pow(end.Y, 2));
            if (endDistance < distance)
            {
                distance = endDistance;
            }
            distance -= selfRadius;

            return Math.Round(distance / distanceStep, 0) * distanceStep;
        }

        private Coordinates GetWorldEndPoint(Coordinates worldCenter, double angle)
        {
            double dx = Math.Sin(angle) * 100.0;
            double dy = Math.Cos(angle) * 100.0;

            double A = dx * dx + dy * dy;
            double B = 2 * (dx * (- worldCenter.X) + dy * (- worldCenter.Y));
            double C = (-worldCenter.X) * (-worldCenter.X) +
                (-worldCenter.Y) * (-worldCenter.Y) -
                worldRadius * worldRadius;

            double det = B * B - 4 * A * C;

            double t = (float)((-B + Math.Sqrt(Math.Abs(det))) / (2 * A));
            var intersection1 = new Coordinates { X = t * dx, Y = t * dy };
            t = (float)((-B - Math.Sqrt(det)) / (2 * A));
            var intersection2 = new Coordinates { X = t * dx, Y = t * dy };

            if((dx < 0 && intersection2.X < 0) || (dx > 0 && intersection2.X > 0) || (dy < 0 && intersection2.Y < 0) || (dy > 0 && intersection2.Y > 0))
            {
                return intersection2;
            }

            return intersection1;
        }
    }
}