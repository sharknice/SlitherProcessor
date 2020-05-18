using Newtonsoft.Json;
using SlitherModel.Processed;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SlitherDatabase
{
    public static class GameDatabase
    {
        public static string DatabaseFolder { get; set; }
        public static List<ProcessedGame> Games { get; private set; }

        public static void LoadGames()
        {
            Games = new List<ProcessedGame>();

            foreach (var fileName in Directory.GetFiles(DatabaseFolder))
            {
                var jsonString = File.ReadAllText(fileName);
                var game = JsonConvert.DeserializeObject<ProcessedGame>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Ignore,
                });
                Games.Add(game);
            }
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
