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
        void killScoreExactTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(60) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 0, TimeSpan.FromSeconds(3));

            Assert.Equal(1, _outcomeScore.Kills);
        }

        [Fact]
        void killScoreAfterTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(60) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 0, TimeSpan.FromSeconds(3));

            Assert.Equal(1, _outcomeScore.Kills);
        }

        [Fact]
        void killScoreMiddleIndex()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 1, TimeSpan.FromSeconds(3));

            Assert.Equal(3, _outcomeScore.Kills);
        }

        [Fact]
        void killScoreExceedsTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 2, TimeSpan.FromSeconds(10));

            Assert.Equal(3, _outcomeScore.Kills);
        }

        [Fact]
        void growthScore()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { SnakeLength = 3, Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 1, TimeSpan.FromSeconds(3));

            Assert.Equal(3, _outcomeScore.Growth);
        }

        [Fact]
        void growthScoreExceedsTime()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { SnakeLength = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { SnakeLength = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { SnakeLength = 2, Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { SnakeLength = 3, Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { SnakeLength = 4, Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { SnakeLength = 5, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 2, TimeSpan.FromSeconds(10));

            Assert.Equal(3, _outcomeScore.Growth);
        }

        [Fact]
        void aliveInMiddle()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Time = DateTime.Now });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 1, TimeSpan.FromSeconds(3));

            Assert.True(_outcomeScore.Alive);
        }

        [Fact]
        void deadAtEnd()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Time = DateTime.Now });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            _outcomeScore = _outcomeScoreProcessor.ProcessOutcomeScore(frames, 2, TimeSpan.FromSeconds(10));

            Assert.False(_outcomeScore.Alive);
        }
    }
}
