using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Selectors
{
    class FitnessProportionateSelector : ISelector
    {
        private Random rnd = new Random();

        public FitnessProportionateSelector() { }

        public List<Individ> Select(List<Individ> population, 
            Crossovers.ICrossover crossover, int childrenNumber)
        {
            // Нормализация значений функций приспособленности особей и 
            // заполнение массива префиксных сумм этих значений.
            double fitnessSum = 0.0;
            for(int i = 0; i < population.Count; i++)
            {
                fitnessSum += population[i].FitnessValue;
            }
            double[] fitnessValues = new double[population.Count];
            for(int i = 0; i < population.Count; i++)
            {
                fitnessValues[i] = population[i].FitnessValue / fitnessSum;
            }
            double[] prefixSum = new double[population.Count];
            prefixSum[0] = fitnessValues[0];
            for(int i = 1; i < population.Count; i++)
            {
                prefixSum[i] = prefixSum[i - 1] + fitnessValues[i];
            }

            // Скрещивание на основе пропорционального отбора.
            List<Individ> children = new List<Individ>(childrenNumber);
            for(int i = 0; i < childrenNumber; i++)
            {
                double randomValue = rnd.NextDouble();
                int firstParentIndex = 0;
                while(randomValue > prefixSum[firstParentIndex])
                {
                    firstParentIndex++;
                }
                randomValue = rnd.NextDouble();
                int secondParentIndex = 0;
                while(randomValue > prefixSum[secondParentIndex])
                {
                    secondParentIndex++;
                }
                if(secondParentIndex == firstParentIndex)
                {
                    secondParentIndex = (secondParentIndex + 1) % population.Count;
                }
                children.Add(crossover.Cross(population[firstParentIndex],
                    population[secondParentIndex]));
            }
            return children;
        }
    }
}
