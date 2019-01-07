using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class Score
    {

        public static void Main(string[] args)
        {
            new Score().Load();
            Console.Read();
        }

        private List<Point> _scores;

        public Score()
        {
            this._scores = new List<Point>();

        }

        private void Load()
        {
            var a = JsonConvert.DeserializeObject<Score>(File.ReadAllText("../../Config/score.json"));
            foreach (var aScore in a._scores)
            {
                Console.WriteLine(aScore.id + " : " + aScore.nbPoint);
            }
        }

        public void Save()
        {
            _scores.Add(new Point(1, 1));
            _scores.Add(new Point(2, 1));
            _scores.Add(new Point(3, 6));
            _scores.Add(new Point(4, 1));
            _scores.Add(new Point(5, 1));
            File.WriteAllText("../../score.json", JsonConvert.SerializeObject(this._scores));
        }

        public class Point
        {
            public ulong id { get; set; }
            public int nbPoint { get; set; }

            public Point(ulong id, int nbPoint)
            {
                this.id = id;
                this.nbPoint = nbPoint;
            }
        }
    }


  

