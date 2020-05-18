using SlitherModel.Processed;
using SlitherModel.Source;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class OutcomeScoreProcessorTests
    {
        private readonly OutcomeScoreProcessor _outcomeScoreProcessor;
        private OutcomeScore _outcomeScore;

        public OutcomeScoreProcessorTests()
        {
            _outcomeScoreProcessor = new OutcomeScoreProcessor();

        }

        [Fact]
        void KillScoreExactTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 60000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 0, 3000);

            Assert.Equal(1, _outcomeScore.Kills);
        }

        [Fact]
        void KillScoreAfterTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 60000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 0, 3000);

            Assert.Equal(1, _outcomeScore.Kills);
        }

        [Fact]
        void KillScoreMiddleIndex()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 1, 3000);

            Assert.Equal(3, _outcomeScore.Kills);
        }

        [Fact]
        void KillScoreExceedsTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 2, 10000);

            Assert.Equal(3, _outcomeScore.Kills);
        }

        [Fact]
        void GrowthScore()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { SnakeLength = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 1, 3000);

            Assert.Equal(3, _outcomeScore.Growth);
        }

        [Fact]
        void GrowthScoreExceedsTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { SnakeLength = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 2, 10000);

            Assert.Equal(3, _outcomeScore.Growth);
        }

        [Fact]
        void AliveInMiddle()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 1, 3000);

            Assert.True(_outcomeScore.Alive);
        }

        [Fact]
        void DeadAtEnd()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 2, 10000);

            Assert.False(_outcomeScore.Alive);
        }
    }
}
