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
    class Car
    {
        public World CarWorld { get; private set; }
        public Body CarBody { get; set; }
        public Body FirstWheel { get; set; }
        public Body SecondWheel { get; set; }
        public float WheelAngularSpeed { get; set; }

        public Car(World world, float speed)
        {
            this.CarWorld = world;
            WheelAngularSpeed = speed;
        }

        public void GoForward()
        {
            FirstWheel.AngularVelocity = -WheelAngularSpeed;
            SecondWheel.AngularVelocity = -WheelAngularSpeed;
        }
    }
}
