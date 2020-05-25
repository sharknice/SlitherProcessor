using Newtonsoft.Json;
using SlitherDatabase;
using SlitherModel.Source;
using System.IO;
using Xunit;

namespace SlitherBrain.Tests
{
    public class ProcessedFrameMatchAnalyzerTests
    {
        private readonly ProcessedFrameMatchAnalyzer ProcessedFrameMatchAnalyzer;
        private readonly Game SourceGame;

        public ProcessedFrameMatchAnalyzerTests()
        {
            ProcessedFrameMatchAnalyzer = new ProcessedFrameMatchAnalyzer(new CollisionMapMatchAnalyzer(new SliceMatchAnalyzer(new CollisionListMatchAnalyzer())));

            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\test\\processed";
            GameDatabase.LoadGames();

            var jsonString = File.ReadAllText("C:\\SlitherShark\\test\\source\\test.json");
            SourceGame = JsonConvert.DeserializeObject<Game>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        [Fact]
        public void ExactFrameMatchIsOne()
        {
            var matchingFrame = GameDatabase.Games[0].Frames[5];

            var result = ProcessedFrameMatchAnalyzer.GetMatchConfidence(matchingFrame, matchingFrame);
            Assert.Equal(1, result);
        }
    }
}
