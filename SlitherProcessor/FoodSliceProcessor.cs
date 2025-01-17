﻿using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;
using System.Linq;

namespace SlitherProcessor
{
    public class FoodSliceProcessor
    {
        private readonly CollisionService _collisionService;

        public FoodSliceProcessor(CollisionService collisionService)
        {
            _collisionService = collisionService;
        }

        public List<FoodCollision> ProcessSlice(SlitherFrame slitherFrame, double angleStart, double angleEnd, int distanceStep)
        {
            var foods = new List<FoodCollision>();

            foreach(var food in slitherFrame.Foods)
            {
                if (food != null)
                {
                    var distance = _collisionService.GetDistance(food.Xx, food.Yy, 1, angleStart, angleEnd, distanceStep);
                    if (distance != null)
                    {
                        foods.Add(new FoodCollision { Distance = distance.Value, Size = 1 });
                    }
                }
            }

            return foods.OrderBy(c => c.Distance).ToList();
        }
    }
}