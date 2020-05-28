using SlitherDatabase;
using SlitherModel.Processed;
using Xunit;

namespace SlitherBrain.Tests
{
    public class ProcessedFrameMatchAnalyzerTests
    {
        private readonly ProcessedFrameMatchAnalyzer ProcessedFrameMatchAnalyzer;

        public ProcessedFrameMatchAnalyzerTests()
        {
            ProcessedFrameMatchAnalyzer = new ProcessedFrameMatchAnalyzer(new CollisionMapMatchAnalyzer(new SliceMatchAnalyzer(new CollisionListMatchAnalyzer())));

            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\test\\processed";
            GameDatabase.LoadGames();
        }

        [Fact]
        public void ExactFrameMatchIsOne()
        {
            ProcessedGame game;
            GameDatabase.Games.TryPeek(out game);
            var matchingFrame = game.Frames[5];

            var result = ProcessedFrameMatchAnalyzer.GetMatchConfidence(matchingFrame, matchingFrame);
            Assert.Equal(1, result);
        }
    }
}
