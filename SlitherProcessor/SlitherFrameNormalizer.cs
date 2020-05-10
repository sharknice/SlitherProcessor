using SlitherModel;
using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class SlitherFrameNormalizer
    {
        int worldRadius = 21600;

        /// <summary>
        /// normalize the map so the snake angle is 0 and the snake point is 0,0
        /// </summary>
        /// <param name="slitherFrame"></param>
        /// <returns></returns>
        public SlitherFrame NormalizeFrame(SlitherFrame slitherFrame)
        {
            var angleChange = -slitherFrame.Snake.Ang;
            var centerShiftedPoint = new Coordinates { x = worldRadius - slitherFrame.Snake.Xx, y = worldRadius - slitherFrame.Snake.Yy };
            var pointChange = new Coordinates { x = worldRadius - centerShiftedPoint.x, y = worldRadius - centerShiftedPoint.y };

            var normalizedFrame = new SlitherFrame();

            normalizedFrame.Snake = GetNormalizedSnake(slitherFrame.Snake, pointChange, angleChange);
            normalizedFrame.Snakes = GetNormalizedSnakes(slitherFrame.Snakes, pointChange, angleChange);
            normalizedFrame.Foods = GetNormalizedFoods(slitherFrame.Foods, pointChange, angleChange);
            normalizedFrame.WorldCenter = RotateCoordinates(centerShiftedPoint, angleChange);

            normalizedFrame.Kills = slitherFrame.Kills;
            normalizedFrame.SnakeLength = slitherFrame.SnakeLength;
            normalizedFrame.Time = slitherFrame.Time;

            return normalizedFrame;
        }

        private List<Snake> GetNormalizedSnakes(List<Snake> snakes, Coordinates anchorPoint, double angleChange)
        {
            for (int index = 0; index < snakes.Count; index++)
            {
                var normalizedCoordinates = NormalizeCoordinates(new Coordinates { x = snakes[index].Xx, y = snakes[index].Yy }, anchorPoint, angleChange);
                snakes[index].Xx = normalizedCoordinates.x;
                snakes[index].Yy = normalizedCoordinates.y;
                // TODO: snakes[index].Ang = ??

                for (int pointIndex = 0; pointIndex < snakes[index].Pts.Count; pointIndex++)
                {
                    var normalizedPointCoordinates = NormalizeCoordinates(new Coordinates { x = snakes[index].Pts[pointIndex].Xx, y = snakes[index].Pts[pointIndex].Yy }, anchorPoint, angleChange);
                    snakes[index].Pts[pointIndex].Xx = normalizedPointCoordinates.x;
                    snakes[index].Pts[pointIndex].Yy = normalizedPointCoordinates.y;
                }
            }

            return snakes;
        }

        private Snake GetNormalizedSnake(Snake snake, Coordinates pointChange, double angleChange)
        {
            snake.Ang = 0;
            snake.Xx = 0;
            snake.Yy = 0;

            for(int index = 0; index < snake.Pts.Count; index++)
            {
                var normalizedCoordinates = NormalizeCoordinates(new Coordinates { x = snake.Pts[index].Xx, y = snake.Pts[index].Yy }, pointChange, angleChange);
                snake.Pts[index].Xx = normalizedCoordinates.x;
                snake.Pts[index].Yy = normalizedCoordinates.y;
            }

            return snake;
        }

        private List<Food> GetNormalizedFoods(List<Food> foods, Coordinates pointChange, double angleChange)
        {
            for (int index = 0; index < foods.Count; index++)
            {
                if (foods[index] != null)
                {
                    var normalizedCoordinates = NormalizeCoordinates(new Coordinates { x = foods[index].Xx, y = foods[index].Yy }, pointChange, angleChange);
                    foods[index].Xx = normalizedCoordinates.x;
                    foods[index].Yy = normalizedCoordinates.y;
                }
            }
            
            return foods;
        }

        private Coordinates NormalizeCoordinates(Coordinates sourcePoint, Coordinates pointChange, double angleChange)
        {
            var shiftedCoordinates = new Coordinates { x = sourcePoint.x - pointChange.x, y = sourcePoint.y - pointChange.y };
            return RotateCoordinates(shiftedCoordinates, angleChange);
        }

        private Coordinates RotateCoordinates(Coordinates shiftedCoordinates, double angleChange)
        {
            var x = (shiftedCoordinates.x * Math.Cos(angleChange)) - (shiftedCoordinates.y * Math.Sin(angleChange));
            var y = (shiftedCoordinates.y * Math.Cos(angleChange)) + (shiftedCoordinates.x * Math.Sin(angleChange));
            return new Coordinates { x = Math.Round(x), y = Math.Round(y) };
        }
    }
}