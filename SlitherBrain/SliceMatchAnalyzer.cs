using SlitherModel.Processed;

namespace SlitherBrain
{
    public class SliceMatchAnalyzer
    {
        public CollisionListMatchAnalyzer CollisionListMatchAnalyzer;

        public SliceMatchAnalyzer(CollisionListMatchAnalyzer collisionListMatchAnalyzer)
        {
            CollisionListMatchAnalyzer = collisionListMatchAnalyzer;
        }

        public double GetMatchConfidence(CollisionSlice sourceSlice, CollisionSlice targetSlice)
        {
            var bad = CollisionListMatchAnalyzer.GetMatchConfidence(sourceSlice.BadCollisions, targetSlice.BadCollisions);
            var food = CollisionListMatchAnalyzer.GetMatchConfidence(sourceSlice.FoodCollisions, targetSlice.FoodCollisions);
            var self = CollisionListMatchAnalyzer.GetMatchConfidence(sourceSlice.SelfCollisions, targetSlice.SelfCollisions);

            return (bad * 0.75) + (food * 0.2) + (self * 0.5);
        }
    }
}
