using System;
using System.Collections.Generic;

namespace SlitherModel.Source
{
    public class SlitherFrame
    {
        public DateTime Time { get; set; }
        public Snake Snake { get; set; }
        public List<Snake> Snakes { get; set; }
        public List<Food> Foods { get; set; }
        public int Kills { get; set; }
        public int SnakeLength { get; set; }
    }
}
