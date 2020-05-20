using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsPhysicsEngine
{
    public struct Color
    {
        /// <summary>
        /// Значение красного канала.
        /// </summary>
        public int R;
        /// <summary>
        /// Значение зеленого канала.
        /// </summary>
        public int G;
        /// <summary>
        /// Значение синего канала.
        /// </summary>
        public int B;

        /// <summary>
        /// Конструктор цвета.
        /// </summary>
        /// <param name="r"> Значение красного канала. </param>
        /// <param name="g"> Значение зеленого канала. </param>
        /// <param name="b"> Значение синего канала. </param>
        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
