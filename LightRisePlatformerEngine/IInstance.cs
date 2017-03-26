using LightRise.BaseClasses;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightRise.Main {
    interface IInstance {
        void Draw(SpriteBatch surface, Camera camera);
        void Step(StepState state);
    }
}
