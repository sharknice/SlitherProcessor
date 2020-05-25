using SlitherModel.Processed;
using System.Collections.Generic;

namespace SlitherBrain
{
    public class CollisionListMatchAnalyzer
    {
        public double GetMatchConfidence(List<Collision> sourceCollisions, List<Collision> targetCollisions)
        {
            if(sourceCollisions.Count == 0 && targetCollisions.Count == 0)
            {
                return 1;
            } 
            else if(sourceCollisions.Count == 0 || targetCollisions.Count == 0)
            {
                return 0;
            }

            var confidence = 0.0;

            for(var index = 0; index < sourceCollisions.Count && index < targetCollisions.Count; index++)
            {
                var source = sourceCollisions[index].Distance;
                var target = targetCollisions[index].Distance;
                if (source == 0)
                {
                    source = 0.001;
                }
                if (target == 0)
                {
                    target = .001;
                }
                if (source > target)
                {
                    confidence += target / source;
                }
                else
                {
                    confidence += source / target;
                }
            }

            return confidence / sourceCollisions.Count;
        }

        public double GetMatchConfidence(List<FoodCollision> sourceCollisions, List<FoodCollision> targetCollisions)
        {
            if (sourceCollisions.Count == 0 && targetCollisions.Count == 0)
            {
                return 1;
            }
            else if (sourceCollisions.Count == 0 || targetCollisions.Count == 0)
            {
                return 0;
            }

            var confidence = 0.0;

            for (var index = 0; index < sourceCollisions.Count && index < targetCollisions.Count; index++)
            {
                var source = sourceCollisions[index].Distance;
                var target = targetCollisions[index].Distance;
                if(source == 0)
                {
                    source = 0.001;
                }
                if(target == 0)
                {
                    target = .001;
                }
                if (source > target)
                {
                    confidence += target / source;
                }
                else
                {
                    confidence += source / target;
                }
            }

            return confidence / sourceCollisions.Count;
        }
    }
}
