using SlitherModel.Source;
using System;
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

        [Fact]
        void WorldMinXTranslated()
        {
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(21600, normalizedFrame.WorldCenter.x);
        }

        [Fact]
        void WorldMinYTranslated()
        {
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(21600, normalizedFrame.WorldCenter.y);
        }

        [Fact]
        void WorldXMiddleTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.WorldCenter.x);
        }

        [Fact]
        void WorldYMiddleTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.WorldCenter.y);
        }

        [Fact]
        void WorldXMaxTranslated()
        {
            _slitherFrame.Snake.Xx = 43200;
            _slitherFrame.Snake.Yy = 43200;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(-21600, normalizedFrame.WorldCenter.x);
        }

        [Fact]
        void WorldYMaxTranslated()
        {
            _slitherFrame.Snake.Xx = 43200;
            _slitherFrame.Snake.Yy = 43200;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(-21600, normalizedFrame.WorldCenter.y);
        }

        [Fact]
        void WorldXTranslatedWithAngle()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Ang = Math.PI;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(-21600, normalizedFrame.WorldCenter.x); // TODO: not sure what this should be
        }

        [Fact]
        void WorldYTranslatedWithAngle()
        {
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = Math.PI;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(-21600, normalizedFrame.WorldCenter.y); // TODO: not sure what this should be
        }
    }
}
