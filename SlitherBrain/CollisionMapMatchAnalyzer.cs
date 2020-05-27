using SlitherModel.Processed;
using System.Collections.Generic;

namespace SlitherBrain
{
    public class CollisionMapMatchAnalyzer
    {
        private readonly SliceMatchAnalyzer SliceMatchAnalyzer;

        public CollisionMapMatchAnalyzer(SliceMatchAnalyzer sliceMatchAnalyzer)
        {
            SliceMatchAnalyzer = sliceMatchAnalyzer;
        }

        public double GetMatchConfidence(List<CollisionSlice> sourceSlices, List<CollisionSlice> targetSlices)
        {
            var maximumMatchValue = 0.0;
            var matchValue = 0.0;
            for(var index = 0; index < sourceSlices.Count; index++)
            {
                maximumMatchValue += SliceMatchAnalyzer.GetMaximumMatchValue(sourceSlices[index]);
                matchValue += SliceMatchAnalyzer.GetMatchConfidence(sourceSlices[index], targetSlices[index]); 
            }
            return matchValue / maximumMatchValue;
        }
    }
}
