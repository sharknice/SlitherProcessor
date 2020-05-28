using SlitherDatabase;
using SlitherModel.Processed;
using SlitherModel.Source;
using SlitherProcessor;
using System.Threading.Tasks;

namespace SlitherBrain
{
    public class SlitherPlayer
    {
        private readonly FrameProcessor FrameProcessor;
        private readonly ProcessedFrameMatchAnalyzer ProcessedFrameMatchAnalyzer;
        private readonly SlitherFrameNormalizer SlitherFrameNormalizer;

        public SlitherPlayer(FrameProcessor frameProcessor, ProcessedFrameMatchAnalyzer processedFrameMatchAnalyzer, SlitherFrameNormalizer slitherFrameNormalizer)
        {
            FrameProcessor = frameProcessor;
            ProcessedFrameMatchAnalyzer = processedFrameMatchAnalyzer;
            SlitherFrameNormalizer = slitherFrameNormalizer;
        }

        public GameDecision PlayGame(string id, SlitherFrame slitherFrame, int millisecondsToAction)
        {
            var normalizedFrame = SlitherFrameNormalizer.NormalizeFrame(slitherFrame);
            var processedFrame = FrameProcessor.ProcessSingleFrame(normalizedFrame);

            var decision = new GameDecision { MatchConfidence = 0 };
            var killDecision = new GameDecision { MatchConfidence = 0 };

            foreach (var game in GameDatabase.Games)
            {

                Parallel.ForEach(game.Frames, (frame, pls, frameIndex) =>
                {
                    if (frame.Outcome.ShortTerm.Alive)
                    {
                        ActionResult actionResult = null;
                        var confidence = ProcessedFrameMatchAnalyzer.GetMatchConfidence(processedFrame, frame);
                        if (confidence > decision.MatchConfidence)
                        {
                            actionResult = GetActionResult(millisecondsToAction, game, (int)frameIndex);

                            decision.MatchConfidence = confidence;
                            decision.PredictedOutcome = frame.Outcome;
                            decision.TargetAngle = actionResult.Angle;
                            decision.Sprint = actionResult.Sprinting;
                        }
                        if (confidence > killDecision.MatchConfidence && frame.Outcome.ShortTerm.Kills > 0)
                        {
                            if (actionResult == null)
                            {
                                actionResult = GetActionResult(millisecondsToAction, game, (int)frameIndex);
                            }

                            killDecision.MatchConfidence = confidence;
                            killDecision.PredictedOutcome = frame.Outcome;
                            killDecision.TargetAngle = actionResult.Angle;
                            killDecision.Sprint = actionResult.Sprinting;
                        }
                    }
                });

            }

            return decision;
        }

        private ActionResult GetActionResult(int millisecondsToAction, ProcessedGame game, int frameIndex)
        {
            int framesAhead = 1;
            var angle = game.Frames[frameIndex + framesAhead].SnakeAngle;
            var sprint = game.Frames[frameIndex + framesAhead].SnakeSprinting;
            while (game.Frames.Count > frameIndex + framesAhead
                && game.Frames[frameIndex + framesAhead].Time - game.Frames[frameIndex].Time < millisecondsToAction)
            {
                angle = game.Frames[frameIndex + framesAhead].SnakeAngle;
                sprint = game.Frames[frameIndex + framesAhead].SnakeSprinting;
                framesAhead++;
            }

            return new ActionResult { Angle = game.Frames[frameIndex].SnakeAngle - angle, Sprinting = sprint };
        }
    }

    public class ActionResult
    {
        public double Angle { get; set; }
        public bool Sprinting { get; set; }
    }
}
