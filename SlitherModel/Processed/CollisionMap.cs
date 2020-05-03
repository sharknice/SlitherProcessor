using System.Collections.Generic;

namespace SlitherModel.Processed
{
    /// <summary>
    /// 360 degree collision map, 0 degrees is the angle the snake is facing
    /// </summary>
    public class CollisionMap
    {
        /// <summary>
        /// 8 slices, round distances to 1000, 
        /// </summary>
        public List<CollisionSlice> LowestResolution { get; set; }

        /// <summary>
        /// 16 slices, round to 100,
        /// </summary>
        public List<CollisionSlice> LowResolution { get; set; }

        /// <summary>
        /// 32 slices, round to 10,
        /// </summary>
        public List<CollisionSlice> MediumResolution { get; set; }

        /// <summary>
        /// 128 slices, round to 1
        /// </summary>
        public List<CollisionSlice> HighResolution { get; set; }

        /// <summary>
        /// 512 slices, round to 1
        /// </summary>
        public List<CollisionSlice> HighestResolution { get; set; }
    }
}