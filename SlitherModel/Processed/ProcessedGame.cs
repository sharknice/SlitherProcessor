using SlitherModel.Source;
using System;
using System.Collections.Generic;

namespace SlitherModel.Processed
{
    public class ProcessedGame
    {
        public string SourceId { get; set; }
        public List<ProcessedFrame> Frames { get; set; }
        public TimeSpan GameLength { get; set; }
        public int SnakeLength { get; set; }
        public int SnakeKills { get; set; }
    }
}
