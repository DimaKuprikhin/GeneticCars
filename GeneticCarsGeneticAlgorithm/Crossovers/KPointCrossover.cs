using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Crossovers
{
    class KPointCrossover : ICrossover
    {
        public int CrossoverPoints { get; set; }

        public KPointCrossover(int crossoverPoints)
        {
            CrossoverPoints = crossoverPoints;
        }

        public Individ Cross(Individ first, Individ second)
        {
            Individ child = new Individ(first.GeneSize);

            List<int> crossoverPoints = new List<int>(CrossoverPoints);
            for(int i = 0; i < CrossoverPoints; i++)
            {
                crossoverPoints.Add(0);
            }

            Random rnd = new Random();
            for(int i = 0; i < CrossoverPoints; )
            {
                crossoverPoints[i] = rnd.Next(0, child.GeneSize);
                bool valid = true;
                for(int j = 0; j < i; j++)
                {
                    if(crossoverPoints[i] == crossoverPoints[j])
                    {
                        valid = false;
                    }
                }
                if(valid)
                {
                    i++;
                }
            }
            crossoverPoints.Sort();

            bool genesFromFirstParent = true;
            int pointsPointer = 0;
            for(int i = 0; i < child.GeneSize; i++)
            {
                if(genesFromFirstParent)
                {
                    child.SetGene(i, first.GetGene(i));
                }
                else
                {
                    child.SetGene(i, second.GetGene(i));
                }
                if((pointsPointer < crossoverPoints.Count) &&
                    (i == crossoverPoints[pointsPointer]))
                {
                    genesFromFirstParent = !genesFromFirstParent;
                    pointsPointer++;
                }
            }

            return child;
        }
    }
}
