using SlitherModel.Source;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class FrameProcessorTests
    {
        private readonly FrameProcessor _frameProcessor;

        public FrameProcessorTests()
        {
            _frameProcessor = new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor())));
        }

        [Fact]
        void time()
        {
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = now });
            frames.Add(new SlitherFrame { Kills = 1, Time = now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { Kills = 3, Time = now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Kills = 4, Time = now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Kills = 5, Time = now + TimeSpan.FromSeconds(5) });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.Equal(now + TimeSpan.FromSeconds(2), processedFrame.Time);
        }

        [Fact]
        void snakeLength()
        {
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5) });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.Equal(2, processedFrame.SnakeLength);
        }

        void collisionMap()
        {
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5) });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.NotNull(processedFrame.CollisionMap);
        }

        void outcome()
        {
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5) });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.NotNull(processedFrame.Outcome);
        }
    }
}
