using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsGeneticAlgorithm.Mutators
{
    interface IMutator
    {
        void Mutate(Individ individ, double mutationRate);
    }
}
