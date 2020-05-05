using System.Collections.Generic;

namespace SlitherModel.Processed
{
    public class CollisionSlice
    {
        public List<FoodCollision> FoodCollisions { get; set; }
        public List<Collision> BadCollisions { get; set; }
        public List<Collision> SelfCollisions { get; set; }
    }
}