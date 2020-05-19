using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;

namespace SlitherBrain
{
    public class SlitherPlayer
    {
        private readonly FrameProcessor FrameProcessor;

        public SlitherPlayer(FrameProcessor frameProcessor)
        {
            FrameProcessor = frameProcessor;
        }

        public GameDecision PlayGame(string id, SlitherFrame slitherFrame, int millisecondsToAction)
        {
            var processedFrame = FrameProcessor.ProcessSingleFrame(slitherFrame);

            //GameDatabase.Games

            // loop through every frame in the database
            // first look at the outcome score, if it is negative, skip this frame
            // calculate how close the frame matches
            // weight outcome score parameters versus match closeness

            // alive most important, then kills, then growth, but need to decide how much

            // TODO: find the best matched frame combined with the best result, then get the next frame from that point based on frameTime, make new project for that processor
            // decision has the angle to go (need to adjust based on the original angle and the processed, normalized angle), decision also has the OutCome from the best match and how close the match is to this game

            // show the state value function (how much reward going to get in the future) and action value function graphs (how much reward for each action) on the UI?

            return new GameDecision();
        }
    }
}
