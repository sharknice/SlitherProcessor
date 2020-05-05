using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class SlitherFrameNormalizer
    {
        /// <summary>
        /// normalize the map so the snake angle is 0
        /// </summary>
        /// <param name="slitherFrame"></param>
        /// <returns></returns>
        public SlitherFrame NormalizeFrame(SlitherFrame slitherFrame)
        {
            var angleChange = -slitherFrame.Snake.Ang;
            var anchorPoint = new Coordinates { x = slitherFrame.Snake.Xx, y = slitherFrame.Snake.Yy };

            var normalizedFrame = new SlitherFrame();

            normalizedFrame.Snake = GetNormalizedSnake(slitherFrame.Snake, anchorPoint, angleChange);
            normalizedFrame.Snakes = GetNormalizedSnakes(slitherFrame.Snakes, anchorPoint, angleChange);
            normalizedFrame.Foods = GetNormalizedFoods(slitherFrame.Foods, anchorPoint, angleChange);

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

        private Snake GetNormalizedSnake(Snake snake, Coordinates anchorPoint, double angleChange)
        {
            snake.Ang = 0;
            snake.Xx = 0;
            snake.Yy = 0;

            for(int index = 0; index < snake.Pts.Count; index++)
            {
                var normalizedCoordinates = NormalizeCoordinates(new Coordinates { x = snake.Pts[index].Xx, y = snake.Pts[index].Yy }, anchorPoint, angleChange);
                snake.Pts[index].Xx = normalizedCoordinates.x;
                snake.Pts[index].Yy = normalizedCoordinates.y;
            }

            return snake;
        }

        private List<Food> GetNormalizedFoods(List<Food> foods, Coordinates anchorPoint, double angleChange)
        {
            for (int index = 0; index < foods.Count; index++)
            {
                if (foods[index] != null)
                {
                    var normalizedCoordinates = NormalizeCoordinates(new Coordinates { x = foods[index].Xx, y = foods[index].Yy }, anchorPoint, angleChange);
                    foods[index].Xx = normalizedCoordinates.x;
                    foods[index].Yy = normalizedCoordinates.y;
                }
            }
            
            return foods;
        }

        private Coordinates NormalizeCoordinates(Coordinates sourcePoint, Coordinates anchorPoint, double angleChange)
        {
            return sourcePoint;
        }
    }
}