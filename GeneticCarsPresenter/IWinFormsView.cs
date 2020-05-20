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
        /// <summary>
        /// Отрисовывает многоугольник.
        /// </summary>
        /// <param name="vertices"> Массив координат вершин. </param>
        /// <param name="color"> Цвет. </param>
        /// <param name="alpha"> Прозрачность. </param>
        void ShowPolygon(Vector2[] vertices, Color color, int alpha);

        /// <summary>
        /// Отрисовывает поверхность.
        /// </summary>
        /// <param name="vertices"> Массив координат вершин. </param>
        /// <param name="delta"> Сдвиг по горизонтали. </param>
        void ShowGround(Vector2[] vertices, int delta);

        /// <summary>
        /// Рисует круг.
        /// </summary>
        /// <param name="center"> Координата центра. </param>
        /// <param name="radius"> Радиус. </param>
        /// <param name="color"> Цвет. </param>
        /// <param name="pointOnCircle"> Координата точки на круге для 
        /// отрисовки линии, соединяющей центр и эту точку, для наглядности
        /// движения колес. </param>
        void ShowCircle(Vector2 center, float radius, Color color, Vector2 pointOnCircle);

        /// <summary>
        /// Отрисовка графиков.
        /// </summary>
        /// <param name="bestResults"> Массив лучших результатов. </param>
        /// <param name="averageResults"> Массив средних результатов. </param>
        void ShowGraph(List<int> bestResults, List<int> averageResults);

        /// <summary>
        /// Отрисовка линии.
        /// </summary>
        /// <param name="begin"> Координата начала. </param>
        /// <param name="end"> Координата конца. </param>
        /// <param name="color"> Цвет. </param>
        void ShowLine(Vector2 begin, Vector2 end, Color color);

        /// <summary>
        /// Показывает окно с сообщением.
        /// </summary>
        /// <param name="text"> Текст. </param>
        /// <param name="title"> Загаловок. </param>
        void ShowMessage(string text, string title);

        /// <summary>
        /// Возвращает ширину PictureBox для отрисовки симуляции алгоритма.
        /// </summary>
        int GetWidth { get; }
        /// <summary>
        /// Возвращает высоту PictureBox для отрисовки симуляции алгоритма.
        /// </summary>
        int GetHeight { get; }
        /// <summary>
        /// Возвращает true, если симуляции приостановлена, иначе false.
        /// </summary>
        bool IsPaused { get; }
        /// <summary>
        /// Возвращает true, если скрыта отрисовка симуляции, иначе false.
        /// </summary>
        bool IsHide { get; }

        /// <summary>
        /// Событие отрисовки нового кадра симуляции.
        /// </summary>
        event EventHandler<EventArgs> paint;

        /// <summary>
        /// Событие отрисовки графика результатов популяции.
        /// </summary>
        event EventHandler<EventArgs> graphPaint;

        /// <summary>
        /// Время в секундах с прошлого кадра.
        /// </summary>
        float Interval { get; }
        /// <summary>
        /// Событие обработки симуляции нового шага симуляции.
        /// </summary>
        event EventHandler<EventArgs> step;

        /// <summary>
        /// Событие создания новой популяции.
        /// </summary>
        event EventHandler<EventArgs> createPopulation;
        /// <summary>
        /// Событие создание новой поверхности.
        /// </summary>
        event EventHandler<EventArgs> createGround;
        /// <summary>
        /// Событие завершения симуляции.
        /// </summary>
        event EventHandler<EventArgs> finishSimulation;

        /// <summary>
        /// Возвращает установленный размер популяции.
        /// </summary>
        int PopulationSize { get; }
        /// <summary>
        /// Событие изменения размера популяции.
        /// </summary>
        event EventHandler<EventArgs> populationSizeChanged;

        /// <summary>
        /// Возвращает количество элитных особей.
        /// </summary>
        int EliteClones { get; }
        /// <summary>
        /// Событие изменения количества элитных особей.
        /// </summary>
        event EventHandler<EventArgs> eliteClonesChanged;

        /// <summary>
        /// Возвращает тип скрещивания.
        /// </summary>
        int CrossoverType { get; }
        /// <summary>
        /// Событие изменения типа скрещивания.
        /// </summary>
        event EventHandler<EventArgs> crossoverTypeChanged;

        /// <summary>
        /// Возвращает вероятность мутации в процентах.
        /// </summary>
        int MutationRate { get; }
        /// <summary>
        /// Событие изменения вероятности мутации.
        /// </summary>
        event EventHandler<EventArgs> mutationRateChanged;

        /// <summary>
        /// Возвращает время жизни поколения.
        /// </summary>
        double GenerationLifeTime { get;  }
        /// <summary>
        /// Событие изменения времени жизни поколения.
        /// </summary>
        event EventHandler<EventArgs> generationLifeTimeChanged;

        /// <summary>
        /// Установка текста текущего времени поколения.
        /// </summary>
        /// <param name="value"> Время в секундах. </param>
        void SetCurrentGenerationTime(double value);

        /// <summary>
        /// Установка текста общего времени жизни популяции.
        /// </summary>
        /// <param name="value"></param>
        void SetTotalSimulationTime(double value);

        /// <summary>
        /// Возвращает количество топлива на квадратный метр.
        /// </summary>
        float FuelPerSquareMeter { get; }
        /// <summary>
        /// Событие изменения количества топлива на квадратный метр.
        /// </summary>
        event EventHandler<EventArgs> fuelPerSquareMeterSet;

        /// <summary>
        /// Событие сохранения популяции.
        /// </summary>
        event EventHandler<EventArgs> populatioinSaveButtonClick;
        /// <summary>
        /// Сохраняет популяцию в txt файл.
        /// </summary>
        /// <param name="populationSize"> Размер популяции. </param>
        /// <param name="bytesPerIndivid"> Количество байтов, кодирующих особь.
        /// </param>
        /// <param name="genes"> Массив массивов генов каждой особи. </param>
        /// <param name="averageResults"> Массив средних результатов поколений.
        /// </param>
        /// <param name="bestResults"> Массив лучщих результатов поколений. 
        /// </param>
        /// <param name="simulationTime"> Общее время жизни популяции. </param>
        void SavePopulation(int populationSize, int bytesPerIndivid, byte[][] genes,
            List<int> averageResults, List<int> bestResults, 
            double simulationTime);

        /// <summary>
        /// Событие загрузки популяции.
        /// </summary>
        event EventHandler<EventArgs> populatioinLoadButtonClick;
        /// <summary>
        /// Загружает информацию о популяции из txt файла.
        /// </summary>
        /// <param name="populationSize"> Размер популяции. </param>
        /// <param name="bytesPerIndivid"> Количество байтов, кодирующих особь.
        /// </param>
        /// <param name="averageResults"> Массив средних результатов поколений.
        /// </param>
        /// <param name="bestResults"> Массив лучщих результатов поколений. 
        /// </param>
        /// <param name="simulationTime"> Общее время жизни популяции. </param>
        /// <returns></returns>
        byte[][] LoadPopulation(ref int populationSize, ref int bytesPerIndivid,
            ref List<int> averageResults, ref List<int> bestResults, 
            ref double simulationTime);

        /// <summary>
        /// Показывает сообщение пользователю.
        /// </summary>
        /// <param name="text"> Текст. </param>
        void ShowPauseMessage(string text);
    }
}
