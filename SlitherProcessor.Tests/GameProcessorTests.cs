using SlitherModel.Processed;
using SlitherModel.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class GameProcessorTests
    {
        private readonly GameProcessor _gameProcessor;
        private readonly Game _sourceGame;
        private readonly ProcessedGame _processedGame;

        public GameProcessorTests()
        {
            _gameProcessor = new GameProcessor(new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor()))));
            _sourceGame = new Game();
            _sourceGame.Id = "test123";

            _sourceGame.Frames = new List<SlitherFrame>();
            _sourceGame.Frames.Add(new SlitherFrame { Kills = 0, SnakeLength = 10, Time = DateTime.Now - TimeSpan.FromSeconds(60) });
            _sourceGame.Frames.Add(new SlitherFrame { Kills = 0, SnakeLength = 50, Time = DateTime.Now - TimeSpan.FromSeconds(30) });
            _sourceGame.Frames.Add(new SlitherFrame { Kills = 2, SnakeLength = 105, Time = DateTime.Now });

            _processedGame = _gameProcessor.ProcessGame(_sourceGame);
        }

        [Fact]
        void ProcessesEveryFrame()
        {
            Assert.Equal(_sourceGame.Frames.Count, _processedGame.Frames.Count);
        }

        [Fact]
        void SourceIdMatches()
        {
            Assert.Equal(_sourceGame.Id, _processedGame.SourceId);
        }

        [Fact]
        void GameLength()
        {
            var gameLength = _sourceGame.Frames.Last().Time - _sourceGame.Frames.First().Time;

            Assert.Equal(gameLength, _processedGame.GameLength);
        }

        [Fact]
        void SnakeLength()
        {
            var snakeLength = _sourceGame.Frames.Last().SnakeLength;

            Assert.Equal(snakeLength, _processedGame.SnakeLength);
        }

        [Fact]
        void Kills()
        {
            var snakeKills = _sourceGame.Frames.Last().Kills;

            Assert.Equal(snakeKills, _processedGame.SnakeKills);
        }
    }
}
