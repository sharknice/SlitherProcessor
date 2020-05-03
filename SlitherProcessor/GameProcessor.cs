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


// TODO: send data to api about rank, kills, length, server, best

// TODO: shark face smiling if eating, opening and closing mouth while talking, worried if running, like the dr. robotnik's mean bean machine enemies


// TODO: learning algorithm every update (60fps or whatever) send status with window.snake and window.snakes and window.foods
// find the best match for current status (based on good outcomes and most similar match)
// send command back to replicate that
// base match value on similar collision points, proximity to head more important
// base outcome rating on not dying as priority, then getting kills and growth, short term (10 seconds), and long term, 60 seconds
// when a game ends analyze the data and make it available to the database, do not make it available until the game ends
// do better open arc collision detection.  ex: 0 degrees to 5 degrees is blocked not just 0 degrees and 5 degrees are blocked.
// have source data and analyzed data

// take in data window.snake, window.snakes, window.foods
// build 360 degree collision map, 0 degrees is the angle the snake is facing
// check every N degrees
// each degree has details
// list of bad collisions, list of food collisions, list of snake heads
// bad collision properties - distance, type
// bad collision types - snake body (includes head), end of world
// snake body properties - size, index from the beginning, index from the end of the live points, snake id it belongs to
// snake head properties - size, angle relative to the bot snake's angle, snake id, is it spriting
// create several resolutions of the data, 
// only take PI/4 slices, round distances to 1000, 
// PI/8 slices, round to 100,
// PI/16 slices, round to 10,
// PI/32 slices, round to 1

// comparison algorithm
// 

// show the state value function (how much reward going to get in the future) and action value function graphs (how much reward for each action) on the UI?