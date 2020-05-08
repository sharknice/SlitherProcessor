using SlitherModel.Source;
using System.Collections.Generic;
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
            _slitherFrame.Foods = new List<Food>();
            _slitherFrame.Foods.Add(new Food { Xx = 0, Yy = 111 });
            _slitherFrame.Foods.Add(new Food { Xx = 0, Yy = 9 });
            _slitherFrame.Foods.Add(new Food { Xx = 0, Yy = -100 });

            var slice = _foodSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(2, slice.Count);
        }

        [Fact]
        void FoodDistanceRoundDown()
        {
            _slitherFrame.Foods = new List<Food>();
            _slitherFrame.Foods.Add(new Food { Xx = 0, Yy = 111 });

            var slice = _foodSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(110, slice[0].Distance);
        }

        [Fact]
        void FoodDistanceRoundUp()
        {
            _slitherFrame.Foods = new List<Food>();
            _slitherFrame.Foods.Add(new Food { Xx = 0, Yy = 165 });

            var slice = _foodSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 100);

            Assert.Equal(200, slice[0].Distance);
        }
    }
}
