using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;

namespace SlitherProcessor
{
    public class BadCollisionSliceProcessor
    {
        public List<Collision> ProcessSlice(SlitherFrame slitherFrame, double angleStart, double angleEnd, int distanceStep)
        {
            // TODO: some kind of collision service that takes in coordinates and size, and double angleStart, double angleEnd, int distanceStep, and returns distance or null if no collision
            // returns the distance
            return new List<Collision>();
        }
    }
}