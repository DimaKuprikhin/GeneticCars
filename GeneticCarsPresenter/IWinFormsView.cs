using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GeneticCarsPhysicsEngine;

namespace GeneticCarsPresenter
{
    public interface IWinFormsView
    {
        void ShowPolygon(Vector2[] vertices, Color color, int alpha);

        void ShowCircle(Vector2 center, float radius, Color color, Vector2 pointOnCircle);

        void ShowGraph(List<float> bestResults, List<float> averageResults);

        void ShowLine(Vector2 begin, Vector2 end, Color color);

        void ShowMessage(string text, string title);

        int GetWidth { get; }
        int GetHeight { get; }
        bool IsPaused { get; }
        bool IsHide { get; }

        event EventHandler<EventArgs> paint;

        event EventHandler<EventArgs> graphPaint;

        float Interval { get; }
        event EventHandler<EventArgs> step;

        event EventHandler<EventArgs> createPopulation;
        event EventHandler<EventArgs> createGround;
        event EventHandler<EventArgs> finishSimulation;

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

        float SimulationSpeed { get; }
        event EventHandler<EventArgs> simulationSpeedChanged;

        float FuelPerSquareMeter { get; }
        event EventHandler<EventArgs> fuelPerSquareMeterSet;

        event EventHandler<EventArgs> populatioinSaveButtonClick;
        void SavePopulation(int populationSize, int bytesPerIndivid, byte[][] genes);

        event EventHandler<EventArgs> populatioinLoadButtonClick;
        byte[][] LoadPopulation(ref int populationSize, ref int bytesPerIndivid);
    }
}
