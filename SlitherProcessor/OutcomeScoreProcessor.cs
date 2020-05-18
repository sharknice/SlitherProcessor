using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;
using System.Linq;

namespace SlitherProcessor
{
    public class OutcomeScoreProcessor
    {
        public OutcomeScore ProcessOutcomeScore(List<SlitherFrame> sourceFrames, int sourceFrameIndex, long timeSpan)
        {
            var sourceFrame = sourceFrames[sourceFrameIndex];
            var outcomeFrame = GetOutcomeFrame(sourceFrames, sourceFrameIndex, timeSpan);

            if(outcomeFrame == null)
            {
                return ProcessFinalOutcomeScore(sourceFrames, sourceFrameIndex);
            }

            return GetScore(sourceFrame, outcomeFrame);
        }

        public OutcomeScore ProcessFinalOutcomeScore(List<SlitherFrame> sourceFrames, int sourceFrameIndex)
        {
            var sourceFrame = sourceFrames[sourceFrameIndex];
            var outcomeFrame = sourceFrames.Last();

            var score = GetScore(sourceFrame, outcomeFrame);
            score.Alive = false;
            return score;
        }

        private OutcomeScore GetScore(SlitherFrame sourceFrame, SlitherFrame outcomeFrame)
        {
            return new OutcomeScore
            {
                Alive = true,
                Growth = outcomeFrame.SnakeLength - sourceFrame.SnakeLength,
                Kills = outcomeFrame.Kills - sourceFrame.Kills
            };
        }

        // TODO: might want to optimize this search
        private SlitherFrame GetOutcomeFrame(List<SlitherFrame> sourceFrames, int sourceFrameIndex, long timeSpan)
        {
            var targetTime = sourceFrames[sourceFrameIndex].Time + timeSpan;

            if(sourceFrames.Last().Time < targetTime)
            {
                return null;
            }

            for (int index = sourceFrameIndex; index < sourceFrames.Count; index++)
            {
                if(sourceFrames[index].Time >= targetTime)
                {
                    return sourceFrames[index];
                }
            }
            return null;
        }
    }
}
