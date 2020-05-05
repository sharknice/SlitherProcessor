using SlitherModel.Processed;
using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class CollisionMapResolutionProcessor
    {
        private readonly CollisionSliceProcessor _collisionSliceProcessor;
        private readonly SlitherFrameNormalizer _slitherFrameNormalizer;

        public CollisionMapResolutionProcessor(CollisionSliceProcessor collisionSliceProcessor, SlitherFrameNormalizer slitherFrameNormalizer)
        {
            _collisionSliceProcessor = collisionSliceProcessor;
            _slitherFrameNormalizer = slitherFrameNormalizer;
        }

        public List<CollisionSlice> ProcessCollisionMap(SlitherFrame slitherFrame, int sliceCount, int distanceStep)
        {
            // TODO: get end of the world collision then normalize it (can't get it after normalization)
            var normalizedFrame = _slitherFrameNormalizer.NormalizeFrame(slitherFrame);

            var fullCircle = Math.PI * 2;
            var sliceSize = fullCircle / sliceCount;

            var slices = new List<CollisionSlice>();

            var angle = 0.0;
            while(angle + (sliceSize/2) < fullCircle)
            {
                angle += sliceSize;
                slices.Add(_collisionSliceProcessor.ProcessSlice(normalizedFrame, angle, angle + sliceSize, distanceStep));
            }

            return slices;
        }
    }
}