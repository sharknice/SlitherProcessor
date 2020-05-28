﻿using SlitherDatabase;
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
                //for (var frameIndex = 0; frameIndex < game.Frames.Count; frameIndex++)
                //{
                //    var frame = game.Frames[frameIndex];

                //    if (frame.Outcome.ShortTerm.Alive)
                //    {
                //        ActionResult actionResult = null;
                //        var confidence = ProcessedFrameMatchAnalyzer.GetMatchConfidence(processedFrame, frame);
                //        if (confidence > decision.MatchConfidence)
                //        {
                //            actionResult = GetActionResult(millisecondsToAction, game, frameIndex);

                //            decision.MatchConfidence = confidence;
                //            decision.PredictedOutcome = frame.Outcome;
                //            decision.TargetAngle = actionResult.Angle;
                //            decision.Sprint = actionResult.Sprinting;
                //        }
                //        if (confidence > killDecision.MatchConfidence && frame.Outcome.ShortTerm.Kills > 0)
                //        {
                //            if (actionResult == null)
                //            {
                //                actionResult = GetActionResult(millisecondsToAction, game, frameIndex);
                //            }

                //            killDecision.MatchConfidence = confidence;
                //            killDecision.PredictedOutcome = frame.Outcome;
                //            killDecision.TargetAngle = actionResult.Angle;
                //            killDecision.Sprint = actionResult.Sprinting;
                //        }
                //    }
                //}

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

            return new ActionResult { Angle = game.Frames[frameIndex].SnakeAngle - angle, Sprinting = sprint }; // TODO: should angle be reversed? the angle is always 0, need to fix that
        }
    }

    public class ActionResult
    {
        public double Angle { get; set; }
        public bool Sprinting { get; set; }
    }
}
