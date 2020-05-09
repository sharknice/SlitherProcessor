using SlitherModel.Source;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class CollisionSliceProcessorTests
    {
        private readonly CollisionSliceProcessor _collisionSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public CollisionSliceProcessorTests()
        {
            _collisionSliceProcessor = new CollisionSliceProcessor(new FoodSliceProcessor(), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()));

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
            var slice = _collisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.NotNull(slice.FoodCollisions);
        }
    }
}
