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
        public World CarWorld { get; private set; }
        public Body CarBody { get; set; }
        public Body FirstWheel { get; set; }
        public Body SecondWheel { get; set; }
        public float WheelAngularSpeed { get; set; }
        public int FuelRefillCount { get; private set; } = 0;

        private float fuel;
        public readonly float MaxFuel;
        public float Fuel {
            get { return fuel; }
            set
            {
                fuel = Math.Max(0, Math.Min(MaxFuel, value));
            }
        }

        public Car(World world, float speed, float fuel)
        {
            CarWorld = world;
            WheelAngularSpeed = speed;
            this.fuel = MaxFuel = fuel;
        }

        public void RefillFuel()
        {
            Fuel = MaxFuel;
            ++FuelRefillCount;
        }

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
