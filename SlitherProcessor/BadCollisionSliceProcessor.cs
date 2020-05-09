using SlitherModel;
using SlitherModel.Processed;
using SlitherModel.Processed.BadCollision;
using SlitherModel.Source;
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

            collision.Add(new Boundry { Distance = GetBoundryDistance(slitherFrame.WorldCenter, selfRadius) });

            return collision;
        }

        private double GetSnakeRadius(Snake snake)
        {
            return snake.Sc * 14.5;
        }

        private double GetBoundryDistance(Coordinates worldCenter, double selfRadius)
        {
            // TODO: collision with circle from world center with radius worldRadius
            return 0;
        }
    }
}