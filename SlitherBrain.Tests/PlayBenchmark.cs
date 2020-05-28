using Newtonsoft.Json;
using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace SlitherBrain.Tests
{
    public class PlayBenchmark
    {
        private readonly SlitherPlayer SlitherPlayer;
        private readonly Game SourceGame;

        public PlayBenchmark()
        {
            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\test\\benchmark\\10";
            GameDatabase.LoadGames();

            var jsonString = File.ReadAllText("C:\\SlitherShark\\test\\source\\test.json");
            SourceGame = JsonConvert.DeserializeObject<Game>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });

            SlitherPlayer = new SlitherPlayer(new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer())), new ProcessedFrameMatchAnalyzer(new CollisionMapMatchAnalyzer(new SliceMatchAnalyzer(new CollisionListMatchAnalyzer()))), new SlitherFrameNormalizer());
        }

        [Fact]
        public void Benchmark()
        {
            var frame = SourceGame.Frames[5];

            Stopwatch stopwatch = Stopwatch.StartNew();

            var result = SlitherPlayer.PlayGame("test", frame, 250);

            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
