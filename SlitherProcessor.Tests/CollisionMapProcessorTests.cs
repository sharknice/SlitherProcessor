﻿using SlitherModel.Source;
using System.Linq;
using Xunit;

namespace SlitherProcessor.Tests
{
    public class CollisionMapProcessorTests
    {
        private readonly CollisionMapProcessor _collisionMapProcessor;
        private readonly SlitherFrame _slitherFrame;

        public CollisionMapProcessorTests()
        {
            _collisionMapProcessor = new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor()));

            var testFrame = new TestFrame();
            _slitherFrame = testFrame.GetTestFrame();
        }

        [Fact]
        void lowestResolutionSlices()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.Equal(8, processedFrame.LowestResolution.Count);
        }

        [Fact]
        void lowResolutionSlices()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.Equal(16, processedFrame.LowResolution.Count);
        }

        [Fact]
        void mediumResolutionSlices()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.Equal(32, processedFrame.MediumResolution.Count);
        }

        [Fact]
        void highResolutionSlices()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.Equal(128, processedFrame.HighResolution.Count);
        }

        [Fact]
        void highestResolutionSlices()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.Equal(512, processedFrame.HighestResolution.Count);
        }

        [Fact]
        void lowestResolutionStep()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.True(processedFrame.LowestResolution.All(slice => slice.BadCollisions.All(collision => collision.Distance % 1000 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 1000 == 0)));
        }

        [Fact]
        void lowResolutionStep()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.True(processedFrame.LowResolution.All(slice => slice.BadCollisions.All(collision => collision.Distance % 100 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 100 == 0)));
        }

        [Fact]
        void mediumResolutionStep()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.True(processedFrame.MediumResolution.All(slice => slice.BadCollisions.All(collision => collision.Distance % 10 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 10 == 0)));
        }

        [Fact]
        void highResolutionStep()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.True(processedFrame.HighResolution.All(slice => slice.BadCollisions.All(collision => collision.Distance % 1 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 1 == 0)));
        }

        [Fact]
        void highestResolutionStep()
        {
            var processedFrame = _collisionMapProcessor.ProcessCollision(_slitherFrame);

            Assert.True(processedFrame.HighestResolution.All(slice => slice.BadCollisions.All(collision => collision.Distance % 1 == 0) && slice.FoodCollisions.All(collision => collision.Distance % 1 == 0)));
        }
    }
}