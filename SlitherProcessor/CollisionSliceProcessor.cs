using SlitherModel.Processed;
using SlitherModel.Source;

namespace SlitherProcessor
{
    public class CollisionSliceProcessor
    {
        private readonly FoodSliceProcessor _foodSliceProcessor;
        private readonly BadCollisionSliceProcessor _badCollisionSliceProcessor;
        private readonly SelfSliceProcessor _selfSliceProcessor;

        public CollisionSliceProcessor(FoodSliceProcessor foodSliceProcessor, BadCollisionSliceProcessor badCollisionSliceProcessor, SelfSliceProcessor selfSliceProcessor)
        {
            _foodSliceProcessor = foodSliceProcessor;
            _badCollisionSliceProcessor = badCollisionSliceProcessor;
            _selfSliceProcessor = selfSliceProcessor;
        }

        public CollisionSlice ProcessSlice(SlitherFrame slitherFrame, double angleStart, double angleEnd, int distanceStep)
        {
            var slice = new CollisionSlice();
            slice.BadCollisions = _badCollisionSliceProcessor.ProcessSlice(slitherFrame, angleStart, angleEnd, distanceStep);
            slice.FoodCollisions = _foodSliceProcessor.ProcessSlice(slitherFrame, angleStart, angleEnd, distanceStep);
            slice.SelfCollisions = _selfSliceProcessor.ProcessSlice(slitherFrame, angleStart, angleEnd, distanceStep);
            return slice;
        }
    }
}