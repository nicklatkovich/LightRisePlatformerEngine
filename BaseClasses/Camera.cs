using Microsoft.Xna.Framework;
using System;

namespace LightRise.BaseClasses {
    public class Camera {

        private Vector2 _position;
        private Vector2 _scale;

        public Vector2 Position {
            get { return _position; }
            set {
                if (value == null) {
                    throw new NullReferenceException( );
                }
                _position = value;
            }
        }
        public Vector2 Scale {
            get { return _scale; }
            set {
                if (value == null) {
                    throw new NullReferenceException( );
                }
                _scale = value;
            }
        }

        public Camera(Vector2 position, Vector2 scale) {
            Position = position;
            Scale = scale;
        }

        public Point WorldToWindow(Vector2 point) {
            return ((point - Position) * Scale).RoundToPoint( );
        }

    }
}
