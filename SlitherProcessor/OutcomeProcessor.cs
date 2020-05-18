using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public interface IOutcomeProcessor
    {
        Outcome ProcessOutcome(List<SlitherFrame> frames, int index);
    }

    public class OutcomeProcessor : IOutcomeProcessor
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
                ImmediateTerm = _outcomeScoreProcessor.ProcessOutcomeScore(frames, index, 1000),
                ShortTerm = _outcomeScoreProcessor.ProcessOutcomeScore(frames, index, 10000),
                LongTerm = _outcomeScoreProcessor.ProcessOutcomeScore(frames, index, 60000),
                LifeTerm = _outcomeScoreProcessor.ProcessFinalOutcomeScore(frames, index)
            };
        }
    }
}