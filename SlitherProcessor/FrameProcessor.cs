using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class FrameProcessor
    {
        private readonly OutcomeProcessor _outcomeProcessor;
        private readonly CollisionMapProcessor _collisionMapProcessor;

        public FrameProcessor(OutcomeProcessor outcomeProcessor, CollisionMapProcessor collisionMapProcessor)
        {
            _collisionMapProcessor = collisionMapProcessor;
            _outcomeProcessor = outcomeProcessor;
        }

        public ProcessedFrame ProcessFrame(List<SlitherFrame> frames, int index)
        {
            var processedFrame = new ProcessedFrame();
            processedFrame.Outcome = _outcomeProcessor.ProcessOutcome(frames, index);
            processedFrame.CollisionMap = _collisionMapProcessor.ProcessCollision(frames[index]);
            processedFrame.SnakeLength = frames[index].SnakeLength;
            processedFrame.Time = frames[index].Time;

            return processedFrame;
        }
    }
}