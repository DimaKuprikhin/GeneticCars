using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm
{
    class Individ : IComparable<Individ>
    {
        public readonly byte[] Genes;

        public readonly int GeneSize;

        public double FitnessValue { get; set; }

        public Individ(int geneSize)
        {
            GeneSize = geneSize;
            Genes = new byte[GeneSize / 8 + (GeneSize % 8 == 0 ? 0 : 1)];
        }

        /// <summary>
        /// Возвращает значение гена.
        /// </summary>
        /// <param name="index"> Номер гена. </param>
        /// <returns> Возвращает 1 или 0. </returns>
        public int GetGene(int index)
        {
            return (Genes[index / 8] >> (index % 8)) & 1;
        }

        /// <summary>
        /// Устанавливает гену значение 1.
        /// </summary>
        /// <param name="index"> Номер гена. </param>
        /// <param name="value"> Значние гена 0 или 1. </param>
        public void SetGene(int index, int value)
        {
            if(GetGene(index) != value)
            {
                Genes[index / 8] = (byte)(Genes[index / 8] ^ (1 << (index % 8)));
            }
        }

        public int CompareTo(Individ other)
        {
            if(this.FitnessValue == other.FitnessValue)
                return 0;
            return this.FitnessValue > other.FitnessValue ? 1 : -1;
        }
    }
}
