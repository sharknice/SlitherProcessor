using SlitherModel.Source;
using System.Collections.Generic;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class CollisionSliceProcessorTests
    {
        private readonly CollisionSliceProcessor _collisionSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public CollisionSliceProcessorTests()
        {
            _collisionSliceProcessor = new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()));

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void BadCollisions()
        {
            var slice = _collisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.NotNull(slice.BadCollisions);
        }

        [Fact]
        void FoodCollisions()
        {
            _slitherFrame.Foods = new List<Food>();
            _slitherFrame.Foods.Add(new Food { Xx = 112, Yy = 0 });
            _slitherFrame.Foods.Add(new Food { Xx = 9, Yy = 0 });
            _slitherFrame.Foods.Add(new Food { Xx = -100, Yy = 0 });

            var slice = _collisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(2, slice.FoodCollisions.Count);
        }
    }
}
