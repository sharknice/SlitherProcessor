using Newtonsoft.Json;
using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;
using System;
using System.IO;

namespace DataReprocessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\database\\processed";
            GameDatabase.LoadGames();   

            var frameProcessor = new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer()));
            var gameProcessor = new GameProcessor(frameProcessor);

            var sourceFolder = "C:\\SlitherShark\\database\\source";
            foreach (var fileName in Directory.GetFiles(sourceFolder))
            {
                Console.WriteLine(fileName);
                var jsonString = File.ReadAllText(fileName);
                var sourceGame = JsonConvert.DeserializeObject<Game>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                });

                var processedGame = gameProcessor.ProcessGame(sourceGame);
                GameDatabase.AddGame(processedGame);
            }

            Console.WriteLine("Done");
        }
    }
}
