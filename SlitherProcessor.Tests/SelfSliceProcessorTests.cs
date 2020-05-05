using SlitherModel.Source;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class SelfSliceProcessorTests
    {
        private readonly SelfSliceProcessor _selfSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public SelfSliceProcessorTests()
        {
            _selfSliceProcessor = new SelfSliceProcessor();

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void HasSelfPoints()
        {
            var slice = _selfSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(8, slice.Count);
        }
    }
}
