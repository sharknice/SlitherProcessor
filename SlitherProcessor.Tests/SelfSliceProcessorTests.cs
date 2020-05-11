using SlitherModel.Source;
using System.Collections.Generic;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class SelfSliceProcessorTests
    {
        private readonly SelfSliceProcessor _selfSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public SelfSliceProcessorTests()
        {
            _selfSliceProcessor = new SelfSliceProcessor(new CollisionService());

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void HasSelfPoints()
        {
            _slitherFrame.Snake.Pts = new List<Lnp> { new Lnp { Xx = 10, Yy = 0, Dying = false }, new Lnp { Xx = 15, Yy = 0, Dying = false }, new Lnp { Xx = -15, Yy = 0, Dying = false } };
            var slice = _selfSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Equal(2, slice.Count);
        }
    }
}
