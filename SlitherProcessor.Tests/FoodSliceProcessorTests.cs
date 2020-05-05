using SlitherModel.Source;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class FoodSliceProcessorTests
    {
        private readonly FoodSliceProcessor _foodSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public FoodSliceProcessorTests()
        {
            _foodSliceProcessor = new FoodSliceProcessor();

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void HasFood()
        {
            var slice = _foodSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(8, slice.Count);
        }
    }
}
