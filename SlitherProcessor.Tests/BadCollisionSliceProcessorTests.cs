using SlitherModel.Processed.BadCollision;
using SlitherModel.Source;
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
    }
}
