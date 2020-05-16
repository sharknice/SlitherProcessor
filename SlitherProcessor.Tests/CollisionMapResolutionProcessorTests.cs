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
            _collisionMapResolutionProcessor = new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService())));

            var testFrame = new TestFrame();
            var slitherTestFrame = testFrame.GetTestFrame();
            var normalizer = new SlitherFrameNormalizer();
            _slitherFrame = normalizer.NormalizeFrame(slitherTestFrame);
        }

        [Fact]
        void SliceCount()
        {
            var collisionMap = _collisionMapResolutionProcessor.ProcessCollisionMap(_slitherFrame, 5, 10);

            Assert.Equal(5, collisionMap.Count);
        }

        [Fact]
        void DistanceStep()
        {
            var collisionMap = _collisionMapResolutionProcessor.ProcessCollisionMap(_slitherFrame, 5, 10);

            Assert.True(collisionMap.All(slice => slice.BadCollisions.All(collision => collision.Distance % 10 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 10 == 0)));
        }
    }
}
