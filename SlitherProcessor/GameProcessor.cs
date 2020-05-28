using SlitherModel.Processed;
using SlitherModel.Source;
using System.Collections.Generic;
using System.Linq;

namespace SlitherProcessor
{
    public class GameProcessor
    {
        private readonly FrameProcessor _frameProcessor;

        public GameProcessor(FrameProcessor frameProcessor)
        {
            _frameProcessor = frameProcessor;
        }

        public ProcessedGame ProcessGame(Game game)
        {
            var processedGame = new ProcessedGame();
            processedGame.SourceId = game.Id;

            var lastFrame = game.Frames.Last();
            processedGame.SnakeKills = lastFrame.Kills;
            processedGame.SnakeLength = lastFrame.SnakeLength;
            processedGame.GameLength = lastFrame.Time - game.Frames.First().Time;

            processedGame.Frames = new List<ProcessedFrame>();

            for(int index = 0; index < game.Frames.Count; index++)
            {
                processedGame.Frames.Add(_frameProcessor.ProcessFrame(game.Frames, index));
            }

            return processedGame;
        }
    }
}