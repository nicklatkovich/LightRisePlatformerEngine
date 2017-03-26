using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightRise.BaseClasses {
    public abstract class Instance {

        public Point GridPosition { get; protected set; }
        protected GraphicsDevice GraphicsDevice;

        public abstract void Update(StepState state);

        public Instance(Point position, GraphicsDevice device) {
            GridPosition = position;
            GraphicsDevice = device;
        }

        public abstract void Draw(SpriteBatch spriteBatch, Camera cam);
    }
}
