using System;

namespace SlitherProcessor
{
    public class CollisionService
    {
        public double? GetDistance(double xx, double yy, double radius, double angleStart, double angleEnd, int distanceStep)
        {
            var targetAngle = Math.Atan2(yy, xx);
            if(TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx, yy, radius, distanceStep);
            }

            // TODO: checking collision for the point with radius of circle here, probably not the right way to do it
            targetAngle = Math.Atan2(yy, xx + radius);
            if (TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx + radius, yy, radius, distanceStep);
            }

            targetAngle = Math.Atan2(yy, xx - radius);
            if(TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx - radius, yy, radius, distanceStep);
            }

            targetAngle = Math.Atan2(yy + radius, xx);
            if(TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx, yy + radius, radius, distanceStep);
            }

            targetAngle = Math.Atan2(yy + radius, xx + radius);
            if (TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx + radius, yy + radius, radius, distanceStep);
            } 

            targetAngle = Math.Atan2(yy + radius, xx - radius);
            if (TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx - radius, yy + radius, radius, distanceStep);
            }

            targetAngle = Math.Atan2(yy - radius, xx);
            if (TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx, yy - radius, radius, distanceStep);
            }

            targetAngle = Math.Atan2(yy - radius, xx + radius);
            if (TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx + radius, yy - radius, radius, distanceStep);
            }

            targetAngle = Math.Atan2(yy - radius, xx - radius);
            if (TargetWithinSlice(targetAngle, angleStart, angleEnd))
            {
                return GetDistance(xx - radius, yy - radius, radius, distanceStep);
            }

            return null;
        }

        bool TargetWithinSlice(double targetAngle, double angleStart, double angleEnd)
        {
            return angleStart <= targetAngle && targetAngle <= angleEnd;
        }

        double GetDistance(double x, double y, double radius, int distanceStep)
        {
            var distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)) - radius;

            return Math.Round(distance / distanceStep, 0) * distanceStep;
        }
    }
}