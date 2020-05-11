using Xunit;

namespace SlitherProcessor.Tests
{
    public class CollisionServiceTests
    {
        private readonly CollisionService _collisionService;

        public CollisionServiceTests()
        {
            _collisionService = new CollisionService();
        }

        [Fact]
        void RoundsDown()
        {
            var distance = _collisionService.GetDistance(112, 0, 1, 0, .5, 10);

            Assert.Equal(110, distance);
        }

        [Fact]
        void RoundsUp()
        {
            var distance = _collisionService.GetDistance(165, 0, 1, 0, .5, 100);

            Assert.Equal(200, distance);
        }

        [Fact]
        void NullForNothing()
        {
            var distance = _collisionService.GetDistance(-165, 0, 1, 0, .5, 100);

            Assert.Null(distance);
        }
    }
}
