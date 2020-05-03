using SlitherModel.Source;
using System.Linq;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class CollisionMapResolutionProcessorTests
    {
        private readonly CollisionMapResolutionProcessor _collisionMapResolutionProcessor;
        private readonly SlitherFrame _slitherFrame;

        public CollisionMapResolutionProcessorTests()
        {
            _collisionMapResolutionProcessor = new CollisionMapResolutionProcessor(new CollisionSliceProcessor());

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void sliceCount()
        {
            var collisionMap = _collisionMapResolutionProcessor.ProcessCollisionMap(_slitherFrame, 5, 10);

            Assert.Equal(5, collisionMap.Count);
        }

        [Fact]
        void distanceStep()
        {
            var collisionMap = _collisionMapResolutionProcessor.ProcessCollisionMap(_slitherFrame, 5, 10);

            Assert.True(collisionMap.All(slice => slice.BadCollisions.All(collision => collision.Distance % 10 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 10 == 0)));
        }
    }
}
