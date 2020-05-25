using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;

namespace SlitherBrain
{
    public class SlitherPlayer
    {
        private readonly FrameProcessor FrameProcessor;
        private readonly ProcessedFrameMatchAnalyzer ProcessedFrameMatchAnalyzer;

        public SlitherPlayer(FrameProcessor frameProcessor, ProcessedFrameMatchAnalyzer processedFrameMatchAnalyzer)
        {
            FrameProcessor = frameProcessor;
            ProcessedFrameMatchAnalyzer = processedFrameMatchAnalyzer;
        }

        public GameDecision PlayGame(string id, SlitherFrame slitherFrame, int millisecondsToAction)
        {
            var processedFrame = FrameProcessor.ProcessSingleFrame(slitherFrame);

            var decision = new GameDecision { MatchConfidence = 0 };
            var killDecision = new GameDecision { MatchConfidence = 0 };

            for (var gameIndex = 0; gameIndex < GameDatabase.Games.Count; gameIndex++)
            {
                for (var frameIndex = 0; frameIndex < GameDatabase.Games[gameIndex].Frames.Count; frameIndex++)
                {
                    var frame = GameDatabase.Games[gameIndex].Frames[frameIndex];

                    if (frame.Outcome.ShortTerm.Alive)
                    {
                        double? actionAngle = null;
                        var confidence = ProcessedFrameMatchAnalyzer.GetMatchConfidence(processedFrame, frame);
                        if(confidence > decision.MatchConfidence)
                        {
                            actionAngle = GetActionAngle(millisecondsToAction, gameIndex, frameIndex);

                            decision.MatchConfidence = confidence;
                            decision.PredictedOutCome = frame.Outcome;
                            decision.TargetAngle = actionAngle.Value;
                        }
                        if(confidence > killDecision.MatchConfidence && frame.Outcome.ShortTerm.Kills > 0)
                        {
                            if(actionAngle == null)
                            {
                                actionAngle = GetActionAngle(millisecondsToAction, gameIndex, frameIndex);
                            }

                            killDecision.MatchConfidence = confidence;
                            killDecision.PredictedOutCome = frame.Outcome;
                            killDecision.TargetAngle = actionAngle.Value;
                        }
                    }
                }
                
            }

            // loop through every frame in the database
            // first look at the outcome score, if it is negative, skip this frame
            // calculate how close the frame matches
            // weight outcome score parameters versus match closeness

            // alive most important, then kills, then growth, but need to decide how much

            // find the best matched frame combined with the best result, then get the next frame from that point based on frameTime, make new project for that processor
            // decision has the angle to go (need to adjust based on the original angle and the processed, normalized angle), decision also has the OutCome from the best match and how close the match is to this game

            // show the state value function (how much reward going to get in the future) and action value function graphs (how much reward for each action) on the UI?

            return decision;
        }

        private double GetActionAngle(int millisecondsToAction, int gameIndex, int frameIndex)
        {
            var angle = GameDatabase.Games[gameIndex].Frames[frameIndex].SnakeAngle;
            int framesAhead = 1;
            while(GameDatabase.Games[gameIndex].Frames.Count > frameIndex + framesAhead 
                && GameDatabase.Games[gameIndex].Frames[frameIndex + framesAhead].Time - GameDatabase.Games[gameIndex].Frames[frameIndex].Time < millisecondsToAction)
            {
                angle = GameDatabase.Games[gameIndex].Frames[frameIndex + framesAhead].SnakeAngle;
                framesAhead++;
            }

            return GameDatabase.Games[gameIndex].Frames[frameIndex].SnakeAngle - angle; // TODO: should this be reversed?
        }
    }
}
