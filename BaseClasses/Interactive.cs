using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightRise.BaseClasses {
    public class Interactive {

        public readonly string ID;
        public Point GridPosition;

        public Interactive(Point position, string id) {
            GridPosition = position;
            ID = id;
        }

    }
}
