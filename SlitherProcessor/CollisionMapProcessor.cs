using SlitherModel.Processed;
using SlitherModel.Source;

namespace SlitherProcessor
{
    public interface ICollisionMapProcessor
    {
        CollisionMap ProcessCollision(SlitherFrame slitherFrame);
    }

    public class CollisionMapProcessor: ICollisionMapProcessor
    {
        private readonly CollisionMapResolutionProcessor _collisionMapResolutionProcessor;
        private readonly SlitherFrameNormalizer _slitherFrameNormalizer;

        public CollisionMapProcessor(CollisionMapResolutionProcessor collisionMapResolutionProcessor, SlitherFrameNormalizer slitherFrameNormalizer)
        {
            _collisionMapResolutionProcessor = collisionMapResolutionProcessor;
            _slitherFrameNormalizer = slitherFrameNormalizer;
        }

        public CollisionMap ProcessCollision(SlitherFrame slitherFrame)
        {
            // TODO: get end of the world collision then normalize it (can't get it after normalization)
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(slitherFrame);

            return new CollisionMap
            {
                LowestResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(normalizedFrame, 8, 1000),
                LowResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(normalizedFrame, 16, 100),
                MediumResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(normalizedFrame, 32, 10),
                HighResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(normalizedFrame, 128, 1),
                HighestResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(normalizedFrame, 512, 1)
            };
        }
    }
}