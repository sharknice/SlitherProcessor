using SlitherModel.Source;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class SlitherFrameNormalizerTests
    {
        private readonly SlitherFrameNormalizer _slitherFrameNormalizer;
        private readonly SlitherFrame _slitherFrame;

        public SlitherFrameNormalizerTests()
        {
            _slitherFrameNormalizer = new SlitherFrameNormalizer();

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void SnakeAngleZero()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.Snake.Ang);
        }

        [Fact]
        void SnakePointsTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.Snake.Pts.Count, normalizedFrame.Snake.Pts.Count);
        }

        [Fact]
        void SnakesTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.Snakes.Count, normalizedFrame.Snakes.Count);
        }

        [Fact]
        void SnakesPointsTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.Snakes[0].Pts.Count, normalizedFrame.Snakes[0].Pts.Count);
        }

        [Fact]
        void FoodsTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.Foods.Count, normalizedFrame.Foods.Count);
        }

        [Fact]
        void SnakePointsXxTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Snake.Pts[2].Xx);
        }

        [Fact]
        void SnakePointsYyTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Snake.Pts[2].Yy);
        }

        [Fact]
        void SnakesPointsXxTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Snakes[2].Pts[2].Xx);
        }

        [Fact]
        void SnakesPointsYyTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Snakes[2].Pts[2].Yy);
        }

        [Fact]
        void SnakesAnglesTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Snakes[2].Ang);
        }

        [Fact]
        void FoodXxTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Foods[2].Xx);
        }

        [Fact]
        void FoodYyTranslated()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(3, normalizedFrame.Foods[2].Yy);
        }
    }
}
