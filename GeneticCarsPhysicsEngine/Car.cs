using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace GeneticCarsPhysicsEngine
{
    public class Car
    {
        /// <summary>
        /// Мир, в который добавлена машинка.
        /// </summary>
        public World CarWorld { get; private set; }

        /// <summary>
        /// Корпус машинки.
        /// </summary>
        public Body CarBody { get; set; }
        
        /// <summary>
        /// Первое колесо.
        /// </summary>
        public Body FirstWheel { get; set; }

        /// <summary>
        /// Второе колесо.
        /// </summary>
        public Body SecondWheel { get; set; }

        /// <summary>
        /// Угловая скорость колес.
        /// </summary>
        public float WheelAngularSpeed { get; set; }

        /// <summary>
        /// Количество пополнений топлива.
        /// </summary>
        public int FuelRefillCount { get; private set; } = 0;

        /// <summary>
        /// Текущее количество топлива.
        /// </summary>
        private float fuel;
        /// <summary>
        /// Максимальное количество топлива.
        /// </summary>
        public readonly float MaxFuel;
        /// <summary>
        /// Свойство доступа к количеству топлива.
        /// </summary>
        public float Fuel {
            get { return fuel; }
            set
            {
                fuel = Math.Max(0, Math.Min(MaxFuel, value));
            }
        }

        /// <summary>
        /// конструктор машинки.
        /// </summary>
        /// <param name="world"> Мир физическая симуляция. </param>
        /// <param name="speed"> Угловая скорость колес. </param>
        /// <param name="fuel"> Максимальное количество топлива. </param>
        public Car(World world, float speed, float fuel)
        {
            CarWorld = world;
            WheelAngularSpeed = speed;
            this.fuel = MaxFuel = fuel;
        }

        /// <summary>
        /// пополняет топливо.
        /// </summary>
        public void RefillFuel()
        {
            Fuel = MaxFuel;
            ++FuelRefillCount;
        }

        /// <summary>
        /// Вращает колеса.
        /// </summary>
        /// <param name="delTime"> Количество времени, прошедшее с прошлого 
        /// шага симуляции. </param>
        public void GoForward(float delTime)
        {
            if(fuel > 0)
            {
                FirstWheel.AngularVelocity = -WheelAngularSpeed;
                SecondWheel.AngularVelocity = -WheelAngularSpeed;
                Fuel -= WheelAngularSpeed * delTime;
            }
            else
            {
                FirstWheel.AngularVelocity = SecondWheel.AngularVelocity = 0;
            }
        }
    }
}
