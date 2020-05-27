using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class FrameProcessor
    {
        private readonly IOutcomeProcessor _outcomeProcessor;
        private readonly ICollisionMapProcessor _collisionMapProcessor;

        public FrameProcessor(IOutcomeProcessor outcomeProcessor, ICollisionMapProcessor collisionMapProcessor)
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
            processedFrame.SnakeAngle = frames[index].Snake.Ang;
            processedFrame.SnakeSprinting = frames[index].Snake.Sp > 10;

            return processedFrame;
        }

        public ProcessedFrame ProcessSingleFrame(SlitherFrame slitherFrame)
        {
            var processedFrame = new ProcessedFrame();
            processedFrame.CollisionMap = _collisionMapProcessor.ProcessCollision(slitherFrame);
            processedFrame.SnakeLength = slitherFrame.SnakeLength;
            processedFrame.Time = slitherFrame.Time;
            processedFrame.SnakeAngle = slitherFrame.Snake.Ang;

            return processedFrame;
        }
    }
}