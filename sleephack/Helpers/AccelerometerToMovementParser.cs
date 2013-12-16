    using System;
    using System.Collections.Generic;
    using System.Linq;
using SleepHack.DataStructures;

namespace SleepHack.Helpers
{
    public static class AccelerometerToMovementParser
    {
        private const float Highest = 0.8f;
        private const float Higher = 0.40f;
        private const float High = 0.3f;
        private const float Medplus = 0.22f;
        private const float Med = 0.16f;
        private const float Medminus = 0.1f;
        private const float Low = 0.06f;
        private const float Lower = 0.02f;
        private const float Lowest = 0.01f;

        public static Movement AnalyzeToMovement(double curX, double curY, double curZ, double prevX, double prevY, double prevZ, Movement prevMovement)
        {
            var xD = Math.Abs(prevX - curX);
            var yD = Math.Abs(prevY - curY);
            var zD = Math.Abs(prevZ - curZ);

            var dArr = new[] { xD, yD, zD };

            var movement = Movement.Noise;

            if (dArr.Any(f => f > Highest))
                movement = Movement.Highest;

            else if (dArr.Any(f => f > Higher))
                movement = Movement.Higher;

            else if (dArr.Any(f => f > High))
                movement = Movement.High;

            else if (dArr.Any(f => f > Medplus))
                movement = Movement.Medplus;

            else if (dArr.Any(f => f > Med))
                movement = Movement.Med;

            else if (dArr.Any(f => f > Medminus))
                movement = Movement.Medminus;
            
            else if (dArr.Any(f => f > Low))
                movement = Movement.Low;

            else if (dArr.Any(f => f > Lower))
                movement = Movement.Lower;

            else if (dArr.Any(f => f > Lowest))
                movement = Movement.Lowest;

            else if (dArr.Any(f => f <= 0.01875))
                movement = Movement.None;

            if (movement > prevMovement)
                return prevMovement + 1;
            if (movement < prevMovement)
                return prevMovement - 1;

            return prevMovement;
        }

        public static Movement Analyze(IList<Movement> movements)
        {
            Movement movement;

            if (movements.Any(x => x > Movement.Higher))
                movement = Movement.Highest;
            else if (movements.Any(x => x > Movement.High))
                movement = Movement.High;
            else if (movements.Any(x => x > Movement.Med))
                movement = Movement.Med;
            else if (movements.Average(x => (double)x) > (double)Movement.Lowest)
                movement = Movement.Low;
            else
                movement = Movement.Lowest;
            return movement;
        }

        private static Movement Smooth(Movement movement, Movement prevMovement)
        {
            if (movement > prevMovement)
                return prevMovement + 1;
            if (movement < prevMovement)
                return prevMovement - 1;

            return prevMovement;
        }

        public static float AnalyzeToFloat(float curX, float curY, float curZ, float prevX, float prevY, float prevZ)
        {
            if (curX > 1 || curX < -1 || curY > 1 || curY < -1 || curZ > 1 || curZ < -1)
                return -1;
            if (prevX > 1 || prevX < -1 || prevY > 1 || prevY < -1 || prevZ > 1 || prevZ < -1)
                return -1;

            var xD = Math.Abs(prevX - curX);
            var yD = Math.Abs(prevY - curY);
            var zD = Math.Abs(prevZ - curZ);

            var dArr = new[] { xD, yD, zD };
            return dArr.Max();
        }
    }
}