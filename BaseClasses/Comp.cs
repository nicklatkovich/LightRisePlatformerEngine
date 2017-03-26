using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightRise.BaseClasses {
    public abstract class Comp : Instance {

        public static bool inUse { get; set; }
        public Comp(Point position, GraphicsDevice device) : base(position, device) {
        }

        public virtual void Connect( ) {
            inUse = true;
        }
        public void Close( ) {
            inUse = false;
        }

        public abstract void Complete( );

    }
}
