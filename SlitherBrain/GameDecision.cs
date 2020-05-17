using SlitherModel.Processed;

namespace SlitherBrain
{
    public class GameDecision
    {
        public double TargetAngle { get; set; }
        public Outcome PredictedOutCome { get; set; }
        public double MatchConfidence { get; set; }
    }
}
