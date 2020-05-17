using Moq;
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
            var collisionMapProcessor = new Mock<ICollisionMapProcessor>();
            _gameProcessor = new GameProcessor(new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), collisionMapProcessor.Object));
            _sourceGame = new Game();
            _sourceGame.Id = "test123";

            var snake = new Snake { Ang = 0 };
            _sourceGame.Frames = new List<SlitherFrame>();
            _sourceGame.Frames.Add(new SlitherFrame { Kills = 0, SnakeLength = 10, Time = DateTime.Now - TimeSpan.FromSeconds(60), Snake = snake });
            _sourceGame.Frames.Add(new SlitherFrame { Kills = 0, SnakeLength = 50, Time = DateTime.Now - TimeSpan.FromSeconds(30), Snake = snake });
            _sourceGame.Frames.Add(new SlitherFrame { Kills = 2, SnakeLength = 105, Time = DateTime.Now, Snake = snake });

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
