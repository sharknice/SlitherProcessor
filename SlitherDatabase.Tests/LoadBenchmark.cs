using System;
using System.Diagnostics;
using Xunit;

namespace SlitherDatabase.Tests
{
    public class LoadBenchmark
    {
        [Fact]
        public void Load10()
        {
            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\test\\benchmark\\10";

            Stopwatch stopwatch = Stopwatch.StartNew();

            GameDatabase.LoadGames();

            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        [Fact]
        public void Load1()
        {
            GameDatabase.DatabaseFolder = "C:\\SlitherShark\\test\\benchmark\\1";

            Stopwatch stopwatch = Stopwatch.StartNew();

            GameDatabase.LoadGames();

            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
