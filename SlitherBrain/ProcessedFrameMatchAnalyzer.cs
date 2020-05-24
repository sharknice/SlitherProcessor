using SlitherModel.Processed;

namespace SlitherBrain
{
    public class ProcessedFrameMatchAnalyzer
    {
        CollisionMapMatchAnalyzer CollisionMapMatchAnalyzer;

        public ProcessedFrameMatchAnalyzer(CollisionMapMatchAnalyzer collisionMapMatchAnalyzer)
        {
            CollisionMapMatchAnalyzer = collisionMapMatchAnalyzer;
        }

        public double GetMatchConfidence(ProcessedFrame source, ProcessedFrame target)
        {
            var lowest = CollisionMapMatchAnalyzer.GetMatchConfidence(source.CollisionMap.LowestResolution, target.CollisionMap.LowestResolution);
            var low = CollisionMapMatchAnalyzer.GetMatchConfidence(source.CollisionMap.LowResolution, target.CollisionMap.LowResolution);
            var medium = CollisionMapMatchAnalyzer.GetMatchConfidence(source.CollisionMap.MediumResolution, target.CollisionMap.MediumResolution);
            var high = CollisionMapMatchAnalyzer.GetMatchConfidence(source.CollisionMap.HighResolution, target.CollisionMap.HighResolution);
            var highest = CollisionMapMatchAnalyzer.GetMatchConfidence(source.CollisionMap.HighestResolution, target.CollisionMap.HighestResolution);

            var confidence = (0.5 * lowest) + (0.25 * low) + (0.125 * medium) + (0.0625 * high) + (0.03125 * highest);

            if(source.SnakeLength > target.SnakeLength)
            {
                confidence += 0.03125 * (target.SnakeLength / source.SnakeLength);
            }
            else
            {
                confidence += 0.03125 * (source.SnakeLength / target.SnakeLength);
            }
            
            return confidence;
        }
    }
}
