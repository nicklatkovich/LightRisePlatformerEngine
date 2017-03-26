using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightRise.BaseClasses {
    public abstract class Comp : Instance {

        public Comp(Point position, GraphicsDevice device) : base(position, device) {
        }

        public abstract void Connect( );
        public abstract void Complete( );

    }
}
