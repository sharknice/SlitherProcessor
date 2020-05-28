using SlitherModel.Processed;

namespace SlitherBrain
{
    public class SliceMatchAnalyzer
    {
        public CollisionListMatchAnalyzer CollisionListMatchAnalyzer;
        private const double BaseSliceValue = 100.0;

        public SliceMatchAnalyzer(CollisionListMatchAnalyzer collisionListMatchAnalyzer)
        {
            CollisionListMatchAnalyzer = collisionListMatchAnalyzer;
        }

        public double GetMatchConfidence(CollisionSlice sourceSlice, CollisionSlice targetSlice)
        {
            var bad = CollisionListMatchAnalyzer.GetMatchConfidence(sourceSlice.BadCollisions, targetSlice.BadCollisions, 1.0);
            var food = CollisionListMatchAnalyzer.GetMatchConfidence(sourceSlice.FoodCollisions, targetSlice.FoodCollisions, 0.001);
            var self = CollisionListMatchAnalyzer.GetMatchConfidence(sourceSlice.SelfCollisions, targetSlice.SelfCollisions, 0.01);

            return BaseSliceValue + bad + food + self;
        }

        public double GetMaximumMatchValue(CollisionSlice slice)
        {
            var bad = CollisionListMatchAnalyzer.GetMaximumMatchValue(slice.BadCollisions, 1.0);
            var food = CollisionListMatchAnalyzer.GetMaximumMatchValue(slice.FoodCollisions, 0.001);
            var self = CollisionListMatchAnalyzer.GetMaximumMatchValue(slice.SelfCollisions, 0.01);

            return BaseSliceValue + bad + food + self;
        }
    }
}
