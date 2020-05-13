using SlitherModel.Source;
using System;
using System.Collections.Generic;
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
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Pts = new List<Lnp> { new Lnp { Xx = 21600, Yy = 21600 }, new Lnp { Xx = 21700, Yy = 21600 } };
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(100, normalizedFrame.Snake.Pts[1].Xx);
        }

        [Fact]
        void SnakePointsYyTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Pts = new List<Lnp> { new Lnp { Xx = 21600, Yy = 21600 }, new Lnp { Xx = 21600, Yy = 21700 } };
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(100, normalizedFrame.Snake.Pts[1].Yy);
        }

        [Fact]
        void SnakesPointsXxTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snakes[2].Pts = new List<Lnp> { new Lnp { Xx = 21600, Yy = 21600 }, new Lnp { Xx = 21700, Yy = 21600 } };
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(100, normalizedFrame.Snakes[2].Pts[1].Xx);
        }

        [Fact]
        void SnakesPointsYyTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snakes[2].Pts = new List<Lnp> { new Lnp { Xx = 21600, Yy = 21600 }, new Lnp { Xx = 21600, Yy = 21700 } };
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(100, normalizedFrame.Snakes[2].Pts[1].Yy);
        }

        [Fact]
        void SnakesAnglesTranslated()
        {
            _slitherFrame.Snake.Ang = .5;
            _slitherFrame.Snakes[2].Ang = .5;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.Snakes[2].Ang);
        }

        [Fact]
        void FoodXxTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Foods = new List<Food> { new Food { Xx = 21600, Yy = 21600 }, new Food { Xx = 21700, Yy = 21600 } };
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(100, normalizedFrame.Foods[1].Xx);
        }

        [Fact]
        void FoodYyTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Foods = new List<Food> { new Food { Xx = 21600, Yy = 21600 }, new Food { Xx = 21600, Yy = 21700 } };
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(100, normalizedFrame.Foods[1].Yy);
        }

        [Fact]
        void WorldMinXTranslated()
        {
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(21600, normalizedFrame.WorldCenter.X);
        }

        [Fact]
        void WorldMinYTranslated()
        {
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(21600, normalizedFrame.WorldCenter.Y);
        }

        [Fact]
        void WorldXMiddleTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.WorldCenter.X);
        }

        [Fact]
        void WorldYMiddleTranslated()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.WorldCenter.Y);
        }

        [Fact]
        void WorldXMaxTranslated()
        {
            _slitherFrame.Snake.Xx = 43200;
            _slitherFrame.Snake.Yy = 43200;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(-21600, normalizedFrame.WorldCenter.X);
        }

        [Fact]
        void WorldYMaxTranslated()
        {
            _slitherFrame.Snake.Xx = 43200;
            _slitherFrame.Snake.Yy = 43200;
            _slitherFrame.Snake.Ang = 0;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(-21600, normalizedFrame.WorldCenter.Y);
        }

        [Fact]
        void WorldXTranslatedWithAngle()
        {
            _slitherFrame.Snake.Xx = 21600;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Ang = Math.PI;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.WorldCenter.X);
        }

        [Fact]
        void WorldYTranslatedWithAngle()
        {
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 21600;
            _slitherFrame.Snake.Ang = Math.PI;
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(0, normalizedFrame.WorldCenter.Y);
        }

        [Fact]
        void Kills()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.Kills, normalizedFrame.Kills);
        }

        [Fact]
        void SnakeLength()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.SnakeLength, normalizedFrame.SnakeLength);
        }

        [Fact]
        void Time()
        {
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(_slitherFrame);

            Assert.Equal(_slitherFrame.Time, normalizedFrame.Time);
        }
    }
}
