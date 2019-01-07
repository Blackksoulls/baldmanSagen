using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using GameManager.Locale;
using Newtonsoft.Json;

namespace GameManager.Env
{
    public class Score
    {

   

        private List<Point> _scores;

        public Score()
        {
            this._scores = new List<Point>();

        }

        private void Load()
        {
            JsonConvert.DeserializeObject<Language>(
                File.ReadAllText
                (Path.Combine(Program.GetPath(-2), "Config", "scores.json"),
                    Encoding.UTF8));

        }

        public void Save()
        {
            _scores.Add(new Point(1, 1));
            _scores.Add(new Point(2, 1));
            _scores.Add(new Point(3, 6));
            _scores.Add(new Point(4, 1));
            _scores.Add(new Point(5, 1));
            Console.WriteLine(JsonConvert.SerializeObject(this._scores));
        }

        private class Point
        {
            private ulong id;
            private int nbPoint;

            public Point(ulong id, int nbPoint)
            {
                this.id = id;
                this.nbPoint = nbPoint;
            }
        }
    }


  

}