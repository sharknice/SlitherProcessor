using SlitherModel.Processed;
using System.Collections.Generic;

namespace SlitherBrain
{
    public class CollisionListMatchAnalyzer
    {
        // collision is worth 21,600 - distance
        private const double MaximumCollisionDistance = 21600.0;
        private const double MinimumDistance = 0.001;

        public double GetMatchConfidence(List<Collision> sourceCollisions, List<Collision> targetCollisions, double multiplier)
        {
            if(sourceCollisions.Count == 0 || targetCollisions.Count == 0)
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
                    source = MinimumDistance;
                }
                if (target == 0)
                {
                    target = MinimumDistance;
                }

                var percentMatch = source / target;
                if (source > target)
                {
                    percentMatch = target / source;
                }

                confidence += (MaximumCollisionDistance - source) * percentMatch * multiplier;
            }

            return confidence;
        }

        internal double GetMatchConfidence(List<FoodCollision> sourceCollisions, List<FoodCollision> targetCollisions, double multiplier)
        {
            if (sourceCollisions.Count == 0 || targetCollisions.Count == 0)
            {
                return 0;
            }

            var confidence = 0.0;

            for (var index = 0; index < sourceCollisions.Count && index < targetCollisions.Count; index++)
            {
                var source = sourceCollisions[index].Distance;
                var target = targetCollisions[index].Distance;
                if (source == 0)
                {
                    source = MinimumDistance;
                }
                if (target == 0)
                {
                    target = MinimumDistance;
                }

                var percentMatch = source / target;
                if (source > target)
                {
                    percentMatch = target / source;
                }

                confidence += (MaximumCollisionDistance - source) * percentMatch * multiplier;
            }

            return confidence;
        }

        internal double GetMaximumMatchValue(List<FoodCollision> collisions, double multiplier)
        {
            double max = 0.0;

            foreach (var collision in collisions)
            {
                var distance = collision.Distance;
                if (distance == 0)
                {
                    distance = MinimumDistance;
                }
                max += (MaximumCollisionDistance - distance) * multiplier;
            }

            return max;
        }

        public double GetMaximumMatchValue(List<Collision> collisions, double multiplier)
        {
            double max = 0.0;

            foreach(var collision in collisions)
            {
                var distance = collision.Distance;
                if (distance == 0)
                {
                    distance = MinimumDistance;
                }
                max += (MaximumCollisionDistance - distance) * multiplier;
            }

            return max;
        }
    }
}
