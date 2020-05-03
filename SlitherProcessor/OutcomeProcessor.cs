using SlitherModel.Processed;
using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class OutcomeProcessor
    {
        OutcomeScoreProcessor _outcomeScoreProcessor;

        public OutcomeProcessor(OutcomeScoreProcessor outcomeScoreProcessor)
        {
            _outcomeScoreProcessor = outcomeScoreProcessor;
        }

        public Outcome ProcessOutcome(List<SlitherFrame> frames, int index)
        {
            return new Outcome
            {
                ImmediateTerm = _outcomeScoreProcessor.ProcessOutcomeScore(frames, index, TimeSpan.FromSeconds(1)),
                ShortTerm = _outcomeScoreProcessor.ProcessOutcomeScore(frames, index, TimeSpan.FromSeconds(10)),
                LongTerm = _outcomeScoreProcessor.ProcessOutcomeScore(frames, index, TimeSpan.FromSeconds(60)),
                LifeTerm = _outcomeScoreProcessor.ProcessFinalOutcomeScore(frames, index)
            };
        }
    }
}