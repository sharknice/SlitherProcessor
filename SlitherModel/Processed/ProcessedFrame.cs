using System;

namespace SlitherModel.Processed
{
    public class ProcessedFrame
    {
        public DateTime Time { get; set; }
        public int SnakeLength { get; set; }
        public Outcome Outcome { get; set; }
        public CollisionMap CollisionMap { get; set; }
    }
}
