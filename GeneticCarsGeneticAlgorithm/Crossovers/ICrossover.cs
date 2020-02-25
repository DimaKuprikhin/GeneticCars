using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Crossovers
{
    interface ICrossover
    {
        Individ Cross(Individ first, Individ second);
    }
}
