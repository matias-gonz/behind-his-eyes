using System.Collections.Generic;
using UnityEngine;

namespace utils
{
    public struct PathPoint
    {
        public Vector3 Position;
        public float WaitTime;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.points;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] points;
        }
    }

    public class PathPointsReader
    {
        [System.Serializable]
        private class PathPointRaw
        {
            public float x;
            public float z;
            public float waitTime;
        }

        public static List<PathPoint> ReadPathPoints(string pathName)
        {
            string path = $"Assets/!/Resources/Paths/{pathName}.json";
            List<PathPoint> pathPoints = new List<PathPoint>();
            string text = System.IO.File.ReadAllText(path);
            PathPointRaw[] pathPointRaws = JsonHelper.FromJson<PathPointRaw>(text);
            foreach (var pathPointRaw in pathPointRaws)
            {
                pathPoints.Add(new PathPoint
                {
                    Position = new Vector3(pathPointRaw.x, 0, pathPointRaw.z),
                    WaitTime = pathPointRaw.waitTime
                });
            }

            return pathPoints;
        }
    }
}