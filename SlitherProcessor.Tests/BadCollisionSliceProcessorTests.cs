using SlitherModel.Source;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class BadCollisionSliceProcessorTests
    {
        private readonly BadCollisionSliceProcessor _badCollisionSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public BadCollisionSliceProcessorTests()
        {
            _badCollisionSliceProcessor = new BadCollisionSliceProcessor(new CollisionService());

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void EnemySnakeHead()
        {
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(8, slice.Count);
        }

        [Fact]
        void EnemySnakeBody()
        {
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(8, slice.Count);
        }

        [Fact]
        void EndOfTheWorld()
        {
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Single(slice);
        }
    }
}
