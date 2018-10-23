using System;
using System.Collections.Generic;
using System.Xml;

namespace BotDiscord.Tuto
{
    public abstract class Aliment
    {
        public string saveur;

        public Aliment(string saveur)
        {
            this.saveur = saveur;
        }

        public override string ToString()
        {
            return "Aliment de saveur";
        }
    }

    public class TestCorenpute
    {
        public static void Main(string[] args)
        {
            List<Aliment> Aliments = new List<Aliment>();
            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                if (rand.Next() % 2 == 1)
                {
                    Aliments.Add(new Cookie("space",rand.Next(0, 10), rand.Next(0, 20)));
                }
                else
                {
                    Aliments.Add(new Muffin("space",rand.Next(0,10)));
                }
            }


            foreach (var aliment in Aliments.FindAll(l => ((Cookie) l)?.nbPepite > 10))
            {
                Console.WriteLine(aliment);
            }
        }
    }

    public class Cookie : Aliment
    {
        public int nbPepite;
        public int rayon;

        public Cookie(string saveur, int nbPepite, int rayon) : base(saveur)
        {
            this.nbPepite = nbPepite;
            this.rayon = rayon;
        }


        public override string ToString()
        {
            return base.ToString() + " de type cookie de " + rayon + " cm de rayon avec " + nbPepite + " pepite";
        }
    }

    public class Muffin : Aliment
    {
        public int hauteur;
        public List<Muffin> Muffins = new List<Muffin>();


        public Muffin(string saveur, int hauteur) : base(saveur)
        {
            this.hauteur = hauteur;
            Muffins.Add(this);

            for (int i = 0; i < 500; i++)
            {
                Muffins.AddRange(Muffins);
            }
        }

        public override string ToString()
        {
            return base.ToString() + " de type muffin de " + hauteur + " cm de hauteur";
        }
    }
}