using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Selectors
{
    interface ISelector
    {
        List<Individ> Select(List<Individ> population, Crossovers.ICrossover crossover,
            int childrenNumber);
    }
}
