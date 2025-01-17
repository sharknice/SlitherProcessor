﻿using Newtonsoft.Json;
using SlitherModel;
using SlitherModel.Source;
using System;
using System.Collections.Generic;
using System.IO;

namespace SlitherProcessor.Tests
{
    public class TestFrame
    {
        public SlitherFrame GetTestFrame()
        {
            var frame = new SlitherFrame();
            frame.Time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            frame.Kills = 3;
            frame.SnakeLength = 2500;

            var foodString = File.ReadAllText("food.json");
            frame.Foods = JsonConvert.DeserializeObject<List<Food>>(foodString);

            var snakeString = File.ReadAllText("snake.json");
            frame.Snake = JsonConvert.DeserializeObject<Snake>(snakeString);

            var snakesString = File.ReadAllText("snakes.json");
            frame.Snakes = JsonConvert.DeserializeObject<List<Snake>>(snakesString);

            frame.WorldCenter = new Coordinates { X = 21600, Y = 21600 };

            return frame;
        }
    }
}
