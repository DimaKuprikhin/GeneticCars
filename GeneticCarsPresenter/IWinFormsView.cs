using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticCarsPresenter
{
    public interface IWinFormsView
    {
        void ShowPolygon(float[][] vertices, int[] color);

        void ShowCircle(float x, float y, float radius, int[] color, float onCircleX,
            float onCircleY);

        int GetWidth { get; }
        int GetHeight { get; }

        event EventHandler<EventArgs> paint;

        float Interval { get; }
        event EventHandler<EventArgs> step;

        event EventHandler<EventArgs> start;

        int PopulationSize { get; }
        event EventHandler<EventArgs> populationSizeChanged;

        int EliteClones { get; }
        event EventHandler<EventArgs> eliteClonesChanged;

        int CrossoverType { get; }
        event EventHandler<EventArgs> crossoverTypeChanged;

        int MutationRate { get; }
        event EventHandler<EventArgs> mutationRateChanged;

        double GenerationLifeTime { get;  }
        event EventHandler<EventArgs> generationLifeTimeChanged;

        void SetCurrentGenerationTime(double value);
    }
}
