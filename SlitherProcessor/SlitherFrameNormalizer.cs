using SlitherModel;
using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class SlitherFrameNormalizer
    {
        private readonly int worldRadius = 21600;

        /// <summary>
        /// normalize the map so the snake angle is 0 and the snake point is 0,0
        /// </summary>
        /// <param name="slitherFrame"></param>
        /// <returns></returns>
        public SlitherFrame NormalizeFrame(SlitherFrame slitherFrame)
        {
            var angleChange = -slitherFrame.Snake.Ang;
            var centerShiftedPoint = new Coordinates { X = worldRadius - slitherFrame.Snake.Xx, Y = worldRadius - slitherFrame.Snake.Yy };
            var pointChange = new Coordinates { X = worldRadius - centerShiftedPoint.X, Y = worldRadius - centerShiftedPoint.Y };

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
                var normalizedCoordinates = NormalizeCoordinates(new Coordinates { X = snakes[index].Xx, Y = snakes[index].Yy }, anchorPoint, angleChange);
                snakes[index].Xx = normalizedCoordinates.X;
                snakes[index].Yy = normalizedCoordinates.Y;
                snakes[index].Ang += angleChange;
                var twoPi = Math.PI * 2;
                if (snakes[index].Ang > twoPi)
                {
                    snakes[index].Ang -= twoPi;
                }
                if(snakes[index].Ang < 0)
                {
                    snakes[index].Ang += twoPi;
                }

                for (int pointIndex = 0; pointIndex < snakes[index].Pts.Count; pointIndex++)
                {
                    var normalizedPointCoordinates = NormalizeCoordinates(new Coordinates { X = snakes[index].Pts[pointIndex].Xx, Y = snakes[index].Pts[pointIndex].Yy }, anchorPoint, angleChange);
                    snakes[index].Pts[pointIndex].Xx = normalizedPointCoordinates.X;
                    snakes[index].Pts[pointIndex].Yy = normalizedPointCoordinates.Y;
                }
            }

            return snakes;
        }

        private Snake GetNormalizedSnake(Snake snake, Coordinates pointChange, double angleChange)
        {
            //snake.Ang = 0;
            snake.Xx = 0;
            snake.Yy = 0;

            for(int index = 0; index < snake.Pts.Count; index++)
            {
                var normalizedCoordinates = NormalizeCoordinates(new Coordinates { X = snake.Pts[index].Xx, Y = snake.Pts[index].Yy }, pointChange, angleChange);
                snake.Pts[index].Xx = normalizedCoordinates.X;
                snake.Pts[index].Yy = normalizedCoordinates.Y;
            }

            return snake;
        }

        private List<Food> GetNormalizedFoods(List<Food> foods, Coordinates pointChange, double angleChange)
        {
            for (int index = 0; index < foods.Count; index++)
            {
                if (foods[index] != null)
                {
                    var normalizedCoordinates = NormalizeCoordinates(new Coordinates { X = foods[index].Xx, Y = foods[index].Yy }, pointChange, angleChange);
                    foods[index].Xx = normalizedCoordinates.X;
                    foods[index].Yy = normalizedCoordinates.Y;
                }
            }
            
            return foods;
        }

        private Coordinates NormalizeCoordinates(Coordinates sourcePoint, Coordinates pointChange, double angleChange)
        {
            var shiftedCoordinates = new Coordinates { X = sourcePoint.X - pointChange.X, Y = sourcePoint.Y - pointChange.Y };
            return RotateCoordinates(shiftedCoordinates, angleChange);
        }

        private Coordinates RotateCoordinates(Coordinates shiftedCoordinates, double angleChange)
        {
            var x = (shiftedCoordinates.X * Math.Cos(angleChange)) - (shiftedCoordinates.Y * Math.Sin(angleChange));
            var y = (shiftedCoordinates.Y * Math.Cos(angleChange)) + (shiftedCoordinates.X * Math.Sin(angleChange));
            return new Coordinates { X = Math.Round(x), Y = Math.Round(y) };
        }
    }
}