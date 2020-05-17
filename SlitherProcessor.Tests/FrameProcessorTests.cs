using Moq;
using SlitherModel.Source;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class FrameProcessorTests
    {
        private readonly FrameProcessor _frameProcessor;
        private readonly Mock<ICollisionMapProcessor> _collisionMapProcessor;
        private readonly Mock<IOutcomeProcessor> _outcomeProcessor;

        public FrameProcessorTests()
        {
            _outcomeProcessor = new Mock<IOutcomeProcessor>();
            _collisionMapProcessor = new Mock<ICollisionMapProcessor>();
            _frameProcessor = new FrameProcessor(_outcomeProcessor.Object, _collisionMapProcessor.Object);
        }

        [Fact]
        void Time()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = now });
            frames.Add(new SlitherFrame { Kills = 1, Time = now + TimeSpan.FromSeconds(1), Snake = snake });
            frames.Add(new SlitherFrame { Kills = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake });
            frames.Add(new SlitherFrame { Kills = 3, Time = now + TimeSpan.FromSeconds(3), Snake = snake });
            frames.Add(new SlitherFrame { Kills = 4, Time = now + TimeSpan.FromSeconds(4), Snake = snake });
            frames.Add(new SlitherFrame { Kills = 5, Time = now + TimeSpan.FromSeconds(5), Snake = snake });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.Equal(now + TimeSpan.FromSeconds(2), processedFrame.Time);
        }

        [Fact]
        void SnakeLength()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now, Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5), Snake = snake });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.Equal(2, processedFrame.SnakeLength);
        }

        [Fact]
        void SnakeAngle()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now, Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5), Snake = snake });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            Assert.Equal(snake.Ang, processedFrame.SnakeAngle);
        }

        [Fact]
        void CollisionMap()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5), Snake = snake });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            _collisionMapProcessor.Verify(m => m.ProcessCollision(frames[2]), Times.Once(), "Collision processed once per frame");
        }

        [Fact]
        void Outcome()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = now + TimeSpan.FromSeconds(1), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(3), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = now + TimeSpan.FromSeconds(4), Snake = snake });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = now + TimeSpan.FromSeconds(5), Snake = snake });
            var processedFrame = _frameProcessor.ProcessFrame(frames, 2);

            _outcomeProcessor.Verify(m => m.ProcessOutcome(frames, 2), Times.Once(), "Outcome processed once per frame");
        }

        [Fact]
        void SingleFrameTime()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frame = new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake };
            var processedFrame = _frameProcessor.ProcessSingleFrame(frame);

            Assert.Equal(now + TimeSpan.FromSeconds(2), processedFrame.Time);
        }

        [Fact]
        void SingleFrameSnakeLength()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frame = new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake };
            var processedFrame = _frameProcessor.ProcessSingleFrame(frame);

            Assert.Equal(2, processedFrame.SnakeLength);
        }

        [Fact]
        void SingleFrameSnakeAngle()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frame = new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake };
            var processedFrame = _frameProcessor.ProcessSingleFrame(frame);

            Assert.Equal(snake.Ang, processedFrame.SnakeAngle);
        }

        [Fact]
        void SingleFrameCollisionMap()
        {
            var snake = new Snake { Ang = 0 };
            var now = DateTime.Now;
            var frame = new SlitherFrame { SnakeLength = 2, Time = now + TimeSpan.FromSeconds(2), Snake = snake };
            var processedFrame = _frameProcessor.ProcessSingleFrame(frame);

            _collisionMapProcessor.Verify(m => m.ProcessCollision(frame), Times.Once(), "Collision processed once per frame");
        }
    }
}
