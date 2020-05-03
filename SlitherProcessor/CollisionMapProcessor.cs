using SlitherModel.Processed;
using SlitherModel.Source;

namespace SlitherProcessor
{
    public class CollisionMapProcessor
    {
        private readonly CollisionMapResolutionProcessor _collisionMapResolutionProcessor;

        public CollisionMapProcessor(CollisionMapResolutionProcessor collisionMapResolutionProcessor)
        {
            _collisionMapResolutionProcessor = collisionMapResolutionProcessor;
        }

        public CollisionMap ProcessCollision(SlitherFrame slitherFrame)
        {
            return new CollisionMap
            {
                LowestResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(slitherFrame, 8, 1000),
                LowResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(slitherFrame, 16, 100),
                MediumResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(slitherFrame, 32, 10),
                HighResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(slitherFrame, 128, 1),
                HighestResolution = _collisionMapResolutionProcessor.ProcessCollisionMap(slitherFrame, 512, 1)
            };
        }
    }
}