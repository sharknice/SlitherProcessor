using SlitherModel.Processed;
using SlitherModel.Processed.BadCollision;
using SlitherModel.Source;
using System.Collections.Generic;
using System.Linq;

namespace SlitherProcessor
{
    public class SelfSliceProcessor
    {
        private readonly CollisionService _collisionService;

        public SelfSliceProcessor(CollisionService collisionService)
        {
            _collisionService = collisionService;
        }

        public List<Collision> ProcessSlice(SlitherFrame slitherFrame, double angleStart, double angleEnd, int distanceStep)
        {
            var collision = new List<Collision>();

            var points = slitherFrame.Snake.Pts.Where(p => p.Dying == false);
            for (int index = 0; index < points.Count(); index++)
            {
                var distance = _collisionService.GetDistance(slitherFrame.Snake.Pts[index].Xx, slitherFrame.Snake.Pts[index].Yy, 0, angleStart, angleEnd, distanceStep);
                if (distance != null)
                {
                    collision.Add(new SnakeBody { Distance = distance.Value, Size = slitherFrame.Snake.Sc * 14.5, StartIndex = index, EndIndex = points.Count() - index });
                }
            }

            return collision;
        }
    }
}