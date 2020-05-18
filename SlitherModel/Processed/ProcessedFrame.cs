namespace SlitherModel.Processed
{
    public class ProcessedFrame
    {
        public long Time { get; set; }
        public int SnakeLength { get; set; }
        public Outcome Outcome { get; set; }
        public CollisionMap CollisionMap { get; set; }
        public double SnakeAngle { get; set; }
    }
}
