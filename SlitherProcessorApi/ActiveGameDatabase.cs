using SlitherModel.Source;
using System.Collections.Concurrent;

namespace SlitherProcessorApi
{
    public class ActiveGameDatabase
    {
        public static ConcurrentDictionary<string, Game> ActiveGames { get; set; }
    }
}
