//using System;
//using System.Linq;
//using SleepHack.DataStructures;

//namespace SleepHack.Helpers
//{
//    public static class AccelerometerToMovementParser
//    {
//        public static Movement AnalyzeToMovement(double curX, double curY, double curZ, double prevX, double prevY,
//                                                 double prevZ)
//        {
//            //if (curX > 1 || curX < -1 || curY > 1 || curY < -1 || curZ > 1 || curZ < -1)
//            //    return Movement.Noise;
//            //if (prevX > 1 || prevX < -1 || prevY > 1 || prevY < -1 || prevZ > 1 || prevZ < -1)
//            //    return Movement.Noise;

//            var xD = Math.Abs(prevX - curX);
//            var yD = Math.Abs(prevY - curY);
//            var zD = Math.Abs(prevZ - curZ);

//            var dArr = new[] { xD, yD, zD };
//            if (dArr.Any(f => f > 2 || f < -2))
//                return Movement.Noise;

//            if (dArr.Any(f => f > 1.1))
//                return Movement.Highest;

//            if (dArr.Any(f => f > 0.66))
//                return Movement.Higher;

//            if (dArr.Any(f => f > 0.34))
//                return Movement.High;

//            if (dArr.Any(f => f > 0.18))
//                return Movement.Med;

//            if (dArr.Any(f => f > 0.1))
//                return Movement.Low;

//            if (dArr.Any(f => f > 0.06))
//                return Movement.Lower;

//            if (dArr.Any(f => f > 0.04))
//                return Movement.Lowest;

//            return Movement.None;
//        }


//        public static float AnalyzeToFloat(float curX, float curY, float curZ, float prevX, float prevY, float prevZ)
//        {
//            if (curX > 1 || curX < -1 || curY > 1 || curY < -1 || curZ > 1 || curZ < -1)
//                return -1;
//            if (prevX > 1 || prevX < -1 || prevY > 1 || prevY < -1 || prevZ > 1 || prevZ < -1)
//                return -1;

//            var xD = Math.Abs(prevX - curX);
//            var yD = Math.Abs(prevY - curY);
//            var zD = Math.Abs(prevZ - curZ);

//            var dArr = new[] { xD, yD, zD };
//            return dArr.Max();
//        }
//    }
//}