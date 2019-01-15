using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using GameManager.Env.Extentions;
using Newtonsoft.Json;

namespace GameManager.Env
{
    public class Score
    {

        [JsonProperty("scores")]
        private Dictionary<string, int> _scores { get; set; }

        public void ModifyPoint(ulong id, int nb)
        {
            try
            {
                Console.WriteLine("Try to modify");
                var strId = id.ToString();
                _scores[strId] += nb;

                if (_scores[strId] < 0)
                    _scores[strId] = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }

        public bool AddPlayer(ulong id)
        {
            Console.WriteLine("Try to addPlayer");
            return _scores.TryAdd(id.ToString(), 0);
        }

        public int GetScore(ulong id)
        {
            return _scores[id.ToString()];
        }

        public override string ToString()
        {
            var str = "Scores des joueurs (par id)\n";
            foreach (var (key, value) in _scores)
            {
                str += $"**{key}** : {value}\n";
            }

            return str;
        }


        public static Score Load()
        {
            try
            {
                var path = Path.Combine(PathExtension.RootBot(), "Scores", "score.json");
                Console.WriteLine($"Try to load on {path}");
                return JsonConvert.DeserializeObject<Score>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Console.WriteLine(1 + e.Message);
                //return null;
            }

            return null;
        }


        public void Save()
        {
            try
            {
                Console.WriteLine("Try to save");
                var path = Path.Combine(PathExtension.RootBot(), "Scores", "score.json");

                File.WriteAllText(path, JsonConvert.SerializeObject(this));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
