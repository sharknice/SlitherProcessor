using Newtonsoft.Json;
using SlitherModel.Processed;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SlitherDatabase
{
    public static class GameDatabase
    {
        public static string DatabaseFolder { get; set; }
        public static ConcurrentBag<ProcessedGame> Games { get; private set; }

        public static void LoadGames()
        {
            Games = new ConcurrentBag<ProcessedGame>();
            var filePaths = Directory.GetFiles(DatabaseFolder);
            Parallel.ForEach(filePaths, (fileName) => {
                using (StreamReader file = File.OpenText(fileName))
                {
                    var serializer = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };
                    var game = (ProcessedGame)serializer.Deserialize(file, typeof(ProcessedGame));
                    Games.Add(game);
                }
            });
        }

        public static void AddGame(ProcessedGame processedGame)
        {
            Games.Add(processedGame);

            string json = JsonConvert.SerializeObject(processedGame, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            });
            File.WriteAllText(DatabaseFolder + "\\" + processedGame.SourceId + ".json", json, Encoding.UTF8);
        }
    }
}
