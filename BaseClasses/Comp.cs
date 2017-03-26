using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightRise.BaseClasses {
    public abstract class Comp : Instance {
        bool allowed = false;
        public bool Allowed {
            get { return allowed; }
            set { if (value) AccessAllowed(); else AccessDenied(); allowed = value; }
        }
        public delegate void Select();
        public Texture2D texture;
        public event Select AccessAllowed;
        public event Select AccessDenied;
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
