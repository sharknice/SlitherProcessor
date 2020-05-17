using Newtonsoft.Json;
using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SlitherProcessorApi
{
    public class GameManager
    {
        private readonly string DatabaseSourceFolder;
        private readonly GameProcessor GameProcessor;

        public GameManager(GameProcessor gameProcessor, string databaseFolder)
        {
            GameProcessor = gameProcessor;
            DatabaseSourceFolder = databaseFolder;
            GameProcessor = new GameProcessor(new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer())));
        }

        public string StartGame(string source)
        {
            var id = $"{source}-{DateTime.Now.Ticks.ToString()}";
            var game = new Game { Id = id, Frames = new List<SlitherFrame>() };
            ActiveGameDatabase.ActiveGames.Add(game);

            return id;
        }

        public bool EndGame(string id)
        {
            var game = ActiveGameDatabase.ActiveGames.First(game => game.Id == id);

            var processedGame = GameProcessor.ProcessGame(game);
            GameDatabase.AddGame(processedGame);

            string json = JsonConvert.SerializeObject(game);
            File.WriteAllText(DatabaseSourceFolder + "\\" + game.Id + ".json", json, Encoding.UTF8);

            ActiveGameDatabase.ActiveGames.RemoveAll(game => game.Id == id);

            return true;
        }
    }
}
