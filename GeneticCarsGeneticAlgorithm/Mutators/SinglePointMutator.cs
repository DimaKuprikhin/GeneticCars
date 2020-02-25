using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Mutators
{
    class SinglePointMutator : IMutator
    {
        private Random rnd = new Random();

        public SinglePointMutator() { }

        /// <summary>
        /// У каждого гена одинаковая вероятность измениться.
        /// </summary>
        /// <param name="individ"> Мутирующая особь. </param>
        /// <param name="mutationRate"> Вероятность мутации. </param>
        public void Mutate(Individ individ, double mutationRate)
        {
            for(int i = 0; i < individ.GeneSize; i++)
            {
                if(rnd.NextDouble() < mutationRate)
                {
                    // Переводим нулевой бит в единичный и наоборот.
                    individ.SetGene(i, -individ.GetGene(i) + 1);
                }
            }
        }
    }
}
