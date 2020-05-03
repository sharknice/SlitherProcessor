using SlitherModel.Processed;
using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class CollisionMapResolutionProcessor
    {
        private readonly CollisionSliceProcessor _collisionSliceProcessor;

        public CollisionMapResolutionProcessor(CollisionSliceProcessor collisionSliceProcessor)
        {
            _collisionSliceProcessor = collisionSliceProcessor;
        }

        public List<CollisionSlice> ProcessCollisionMap(SlitherFrame slitherFrame, int sliceCount, int distanceStep)
        {
            var fullCircle = Math.PI * 2;
            var sliceSize = fullCircle / sliceCount;

            var slices = new List<CollisionSlice>();

            var angle = 0.0;
            while(angle + (sliceSize/2) < fullCircle)
            {
                angle += sliceSize;
                slices.Add(_collisionSliceProcessor.ProcessSlice(slitherFrame, angle, distanceStep));
            }

            return slices;
        }
    }
}