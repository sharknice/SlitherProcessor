using Newtonsoft.Json;
using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;
using System.IO;
using Xunit;

namespace SlitherBrain.Tests
{
    public class SlitherPlayerTests
    {
        private readonly SlitherPlayer SlitherPlayer;
        private readonly Game SourceGame;

        public SlitherPlayerTests()
        {
            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\test\\processed";
            GameDatabase.LoadGames();

            var jsonString = File.ReadAllText("C:\\SlitherShark\\test\\source\\test.json");
            SourceGame = JsonConvert.DeserializeObject<Game>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });

            SlitherPlayer = new SlitherPlayer(new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer())), new ProcessedFrameMatchAnalyzer(new CollisionMapMatchAnalyzer(new SliceMatchAnalyzer(new CollisionListMatchAnalyzer()))));

            var frameProcessor = new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer()));
            var gameProcessor = new GameProcessor(frameProcessor);
            var processedGame = gameProcessor.ProcessGame(SourceGame);
            GameDatabase.AddGame(processedGame);
        }

        [Fact]
        public void OutCome()
        {
            var frame = SourceGame.Frames[5];
            var matchingFrame = GameDatabase.Games[0].Frames[5];

            var result = SlitherPlayer.PlayGame("test", frame, 250);
            Assert.Equal(matchingFrame.Outcome, result.PredictedOutCome);
        }

        [Fact]
        public void Match()
        {
            var frame = SourceGame.Frames[5];

            var result = SlitherPlayer.PlayGame("test", frame, 250);
            Assert.Equal(1, result.MatchConfidence);
        }
    }
}
