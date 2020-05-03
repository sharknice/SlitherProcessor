using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class CollisionSliceProcessor
    {
        public CollisionSlice ProcessSlice(SlitherFrame slitherFrame, double angle, int distanceStep)
        {
            var slice = new CollisionSlice();
            slice.BadCollisions = new List<Collision>();
            slice.FoodCollisions = new List<FoodCollision>();
            return slice;
        }
    }
}