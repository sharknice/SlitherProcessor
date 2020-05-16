using SlitherModel.Processed.BadCollision;
using SlitherModel.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class BadCollisionSliceProcessorTests
    {
        private readonly BadCollisionSliceProcessor _badCollisionSliceProcessor;
        private readonly SlitherFrame _slitherFrame;

        public BadCollisionSliceProcessorTests()
        {
            _badCollisionSliceProcessor = new BadCollisionSliceProcessor(new CollisionService());

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void EnemySnakeHead()
        {
            _slitherFrame.Snakes = new List<Snake>();
            _slitherFrame.Snakes.Add(new Snake { Xx = 100, Yy = 0, Sc = 2, Pts = new List<Lnp>(), DeadAmt = 0 });
            _slitherFrame.Snakes.Add(new Snake { Xx = 150, Yy = 0, Sc = 2, Pts = new List<Lnp>(), DeadAmt = 0 });
            _slitherFrame.Snakes.Add(new Snake { Xx = -100, Yy = 0, Sc = 2, Pts = new List<Lnp>(), DeadAmt = 0 });
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);
            var snakeCollisions = slice.Where(c => c.GetType() == typeof(SnakeHead));
            Assert.Equal(2, snakeCollisions.Count());
        }

        [Fact]
        void EnemySnakeBody()
        {
            _slitherFrame.Snakes = new List<Snake>();
            _slitherFrame.Snakes.Add(new Snake { Xx = 100, Yy = 0, Sc = 2, Pts = new List<Lnp> { new Lnp { Dying = false, Xx = 50, Yy = 0 }, new Lnp { Dying = false, Xx = 100, Yy = 0 }, new Lnp { Dying = false, Xx = 150, Yy = 0 }, new Lnp { Dying = false, Xx = -150, Yy = 0 } }, DeadAmt = 0 });

            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);
            var bodyCollisions = slice.Where(c => c.GetType() == typeof(SnakeBody));
            Assert.Equal(3, bodyCollisions.Count());
        }

        [Fact]
        void EndOfTheWorld()
        {
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .1, 10);

            Assert.Single(slice);
        }

        [Fact]
        void EndOfTheWorldCenter()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = 0;
            _slitherFrame.WorldCenter.Y = 0;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, .001, 100);

            Assert.Equal(21600, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }

        [Fact]
        void EndOfTheWorldEdge()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = -21599;
            _slitherFrame.WorldCenter.Y = 1;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, Math.PI / 2, Math.PI / 2, 100);

            Assert.Equal(0, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }

        [Fact]
        void EndOfTheWorldStart()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = 21599;
            _slitherFrame.WorldCenter.Y = 1;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, Math.PI / 2, Math.PI / 2, 100);

            Assert.Equal(43200, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }

        [Fact]
        void EndOfTheWorldDistanceX()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = -1600;
            _slitherFrame.WorldCenter.Y = 1;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, Math.PI / 2, Math.PI / 2, 100);

            Assert.Equal(20000, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }

        [Fact]
        void EndOfTheWorldDistanceY()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = 1;
            _slitherFrame.WorldCenter.Y = -1600;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, 0, 100);

            Assert.Equal(20000, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }

        [Fact]
        void EndOfTheWorldDistanceCloseX()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = -20000;
            _slitherFrame.WorldCenter.Y = 1;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, Math.PI / 2, Math.PI / 2, 100);

            Assert.Equal(1600, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }

        [Fact]
        void EndOfTheWorldDistanceCloseY()
        {
            _slitherFrame.Snake.Ang = 0;
            _slitherFrame.Snake.Xx = 0;
            _slitherFrame.Snake.Yy = 0;
            _slitherFrame.Snake.Sc = 0;
            _slitherFrame.WorldCenter.X = 1;
            _slitherFrame.WorldCenter.Y = -20000;
            var slice = _badCollisionSliceProcessor.ProcessSlice(_slitherFrame, 0, 0, 100);

            Assert.Equal(1600, slice.Where(c => c.GetType() == typeof(Boundry)).First().Distance);
        }
    }
}
