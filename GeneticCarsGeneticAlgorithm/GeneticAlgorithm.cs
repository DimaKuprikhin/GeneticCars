using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        private Random rnd = new Random();

        private List<Individ> population = new List<Individ>();

        private Crossovers.ICrossover Crossover;

        private Selectors.ISelector Selector;

        private Mutators.IMutator Mutator;

        public int PopulationSize
        {
            get { return population.Count; }
            set
            {
                while(population.Count > value)
                {
                    population.Remove(population[population.Count - 1]);
                }
                AddRandomIndivid(Math.Max(0, value - population.Count));
            }
        }

        public readonly int GeneSize;

        public int GenerationCount { get; private set; }

        /// <summary>
        /// Количество особей
        /// </summary>
        public int EliteClones { get; set; }

        public double MutationRate { get; set; }

        public GeneticAlgorithm(int populationSize, int geneSize, byte[][] genes = null)
        {
            GeneSize = geneSize;
            if(genes != null)
            {
                for(int i = 0; i < populationSize; ++i)
                {
                    population.Add(new Individ(geneSize, genes[i]));
                }
            }
            else
            {
                PopulationSize = populationSize;
            }
            GenerationCount = 0;
            EliteClones = 1;
            MutationRate = 0.01;
            Crossover = new Crossovers.KPointCrossover(1);
            Selector = new Selectors.FitnessProportionateSelector();
            Mutator = new Mutators.SinglePointMutator();
        }

        /// <summary>
        /// Добавляет в популяцию случаных особей.
        /// </summary>
        private void AddRandomIndivid(int number)
        {
            byte[] genes = new byte[GeneSize];
            for(int i = 0; i < number; i++)
            {
                for(int j = 0; j < GeneSize; j++)
                {
                    genes[j] = (byte)rnd.Next(0, 256);
                }
                Individ newIndivid = new Individ(GeneSize, genes);
                
                population.Add(newIndivid);
            }
        }

        /// <summary>
        /// Возвращает массив генов популяции.
        /// </summary>
        /// <returns> Массив генов. </returns>
        public byte[][] GetPopulationInfo()
        {
            byte[][] result = new byte[PopulationSize][];
            for(int i = 0; i < PopulationSize; i++)
            {
                result[i] = population[i].Genes;
            }
            return result;
        }

        /// <summary>
        /// Задает значения фитнесс-функций каждой особи.
        /// </summary>
        /// <param name="fitnessFunctionResults"> Массив значений. </param>
        public void SetFitnessFunctionResults(double[] fitnessFunctionResults)
        {
            for(int i = 0; i < fitnessFunctionResults.Length && i < population.Count; i++)
            {
                population[i].FitnessValue = fitnessFunctionResults[i];
            }
        }

        /// <summary>
        /// Генерирует новое поколение на основе резултьтатов фитнесс-функции.
        /// </summary>
        public void GenerateNextGeneration()
        {
            // Отбираем особи для скрещивания и получаем массив особей 
            // для нового поколения.
            List<Individ> children = Selector.Select(population, Crossover,
                PopulationSize - EliteClones);

            // Замена особей из предыдущего поколения на новых.
            population.Sort();
            for(int i = 0; i < children.Count; i++)
            {
                population[i] = children[i];
            }
            for(int i = 0; i < PopulationSize; i++)
            {
                Mutator.Mutate(population[i], MutationRate);
            }
            GenerationCount++;
        }

        /// <summary>
        /// Задает тип скрещивания особей.
        /// </summary>
        /// <param name="typeIndex"> Индекс типа (1, 2, 3 - скрещивание с 
        /// 1, 2 или 3-мя точками разрыва, 4 - каждый ген из случайного
        /// родителя). </param>
        public void SetCrossover(int typeIndex)
        {
            if((0 < typeIndex) && (typeIndex < 4))
            {
                Crossover = new Crossovers.KPointCrossover(typeIndex);
            }
            if(typeIndex == 4)
            {
                Crossover = new Crossovers.UniformCrossover();
            }
        }
    }
}
