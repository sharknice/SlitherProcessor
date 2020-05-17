using Newtonsoft.Json;
using SlitherModel.Processed;
using SlitherProcessor;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SlitherDatabase
{
    public static class GameDatabase
    {
        public static string DatabaseFolder { get; set; }
        public static List<ProcessedGame> Games { get; private set; }

        private static readonly GameProcessor GameProcessor;

        static GameDatabase()
        {
            GameProcessor = new GameProcessor(new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor( new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer())));
        }

        public static void LoadGames()
        {
            Games = new List<ProcessedGame>();

            foreach (var fileName in Directory.GetFiles(DatabaseFolder))
            {
                var jsonString = File.ReadAllText(fileName);
                var game = JsonConvert.DeserializeObject<ProcessedGame>(jsonString);
                Games.Add(game);
            }
        }

        public static void AddGame(ProcessedGame processedGame)
        {
            Games.Add(processedGame);

            string json = JsonConvert.SerializeObject(processedGame);
            File.WriteAllText(DatabaseFolder + "\\" + processedGame.SourceId + ".json", json, Encoding.UTF8);
        }
    }
}
