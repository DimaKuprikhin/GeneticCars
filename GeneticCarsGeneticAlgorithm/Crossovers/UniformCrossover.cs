using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Crossovers
{
    class UniformCrossover : ICrossover
    {
        public UniformCrossover() { }

        public Individ Cross(Individ first, Individ second)
        {
            Individ child = new Individ(first.GeneSize);

            Random rnd = new Random();
            for(int i = 0; i < child.GeneSize; i++)
            {
                if(rnd.Next(0, 2) == 0)
                {
                    child.SetGene(i, first.GetGene(i));
                }
                else
                {
                    child.SetGene(i, second.GetGene(i));
                }
            }

            return child;
        }
    }
}
