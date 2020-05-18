using SlitherModel.Source;
using System;
using System.Collections.Generic;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class OutcomeProcessorTests
    {
        private readonly OutcomeProcessor _outcomeProcessor;

        public OutcomeProcessorTests()
        {
            _outcomeProcessor = new OutcomeProcessor(new OutcomeScoreProcessor());
        }

        [Fact]
        void ImmediateKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 2000 });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 3000 });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 4000 });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 1);

            Assert.Equal(1, outcome.ImmediateTerm.Kills);
        }

        [Fact]
        void ShortTermKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 10000 });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 15000 });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 120000 });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 2);

            Assert.Equal(2, outcome.ShortTerm.Kills);
        }

        [Fact]
        void LongTermKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 10000 });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 65000 });
            frames.Add(new SlitherFrame { Kills = 6, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 120000 });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 2);

            Assert.Equal(3, outcome.LongTerm.Kills);
        }

        [Fact]
        void LifeTermKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1000 });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 5000 });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 10000 });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 60000 });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + 120000 });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 1);

            Assert.Equal(4, outcome.LifeTerm.Kills);
        }
    }
}
