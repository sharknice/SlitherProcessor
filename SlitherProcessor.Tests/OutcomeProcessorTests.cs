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
        void immediateKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(2) });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTime.Now + TimeSpan.FromSeconds(3) });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTime.Now + TimeSpan.FromSeconds(4) });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 1);

            Assert.Equal(1, outcome.ImmediateTerm.Kills);
        }

        [Fact]
        void shortTermKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTime.Now + TimeSpan.FromSeconds(10) });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTime.Now + TimeSpan.FromSeconds(15) });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTime.Now + TimeSpan.FromSeconds(20) });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 2);

            Assert.Equal(2, outcome.ShortTerm.Kills);
        }

        [Fact]
        void longTermKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTime.Now + TimeSpan.FromSeconds(10) });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTime.Now + TimeSpan.FromSeconds(65) });
            frames.Add(new SlitherFrame { Kills = 6, Time = DateTime.Now + TimeSpan.FromSeconds(120) });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 2);

            Assert.Equal(3, outcome.LongTerm.Kills);
        }

        [Fact]
        void lifeTermKills()
        {
            var frames = new List<SlitherFrame>();
            frames.Add(new SlitherFrame { Kills = 0, Time = DateTime.Now });
            frames.Add(new SlitherFrame { Kills = 1, Time = DateTime.Now + TimeSpan.FromSeconds(1) });
            frames.Add(new SlitherFrame { Kills = 2, Time = DateTime.Now + TimeSpan.FromSeconds(5) });
            frames.Add(new SlitherFrame { Kills = 3, Time = DateTime.Now + TimeSpan.FromSeconds(10) });
            frames.Add(new SlitherFrame { Kills = 4, Time = DateTime.Now + TimeSpan.FromSeconds(60) });
            frames.Add(new SlitherFrame { Kills = 5, Time = DateTime.Now + TimeSpan.FromSeconds(120) });
            var outcome = _outcomeProcessor.ProcessOutcome(frames, 1);

            Assert.Equal(4, outcome.LifeTerm.Kills);
        }
    }
}
