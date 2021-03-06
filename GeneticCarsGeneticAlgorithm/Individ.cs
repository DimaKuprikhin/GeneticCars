﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm
{
    class Individ : IComparable<Individ>
    {
        /// <summary>
        /// Гены особи.
        /// </summary>
        public readonly byte[] Genes;

        /// <summary>
        /// Длина генотипа особи.
        /// </summary>
        public readonly int GeneSize;

        /// <summary>
        /// Значение функции приспособленности.
        /// </summary>
        public double FitnessValue { get; set; }

        public Individ(int geneSize, byte[] genes = null)
        {
            GeneSize = geneSize;
            Genes = new byte[GeneSize / 8 + (GeneSize % 8 == 0 ? 0 : 1)];
            if(genes != null)
                for(int i = 0; i < geneSize / 8; ++i)
                    Genes[i] = genes[i];
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
        /// Устанавливает гену значение value.
        /// </summary>
        /// <param name="index"> Номер гена. </param>
        /// <param name="value"> Значние гена 0 или 1. </param>
        public void SetGene(int index, int value)
        {
            if(GetGene(index) != value)
            {
                Genes[index / 8] ^= (byte)(1 << (index % 8));
            }
        }

        /// <summary>
        /// Функция сравнения двух особей на основе значения их функций 
        /// приспособленности.
        /// </summary>
        /// <param name="other"> Особь, с которой сравниваем. </param>
        /// <returns> Возвращает 1, если значение функции приспособленности 
        /// другой особи меньше; 0, если они равны; -1, если у другой особи
        /// она больше. </returns>
        public int CompareTo(Individ other)
        {
            if(this.FitnessValue == other.FitnessValue)
                return 0;
            return this.FitnessValue > other.FitnessValue ? 1 : -1;
        }
    }
}
