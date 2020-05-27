using SlitherModel.Processed;

namespace SlitherBrain
{
    public class GameDecision
    {
        public double TargetAngle { get; set; }
        public bool Sprint { get; set; }
        public Outcome PredictedOutcome { get; set; }
        public double MatchConfidence { get; set; }
    }
}
