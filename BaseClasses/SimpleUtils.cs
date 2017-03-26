using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace LightRise.BaseClasses {
    public static class SimpleUtils {

        private static Random _rand = new Random( );
        public static Texture2D WhiteRect { get; private set; }

        // ATTENTION: Use this method before using textures
        // TODO: Fix it... Like anything. And remove this function to hell.
        public static void Init(GraphicsDevice GraphicsDevice) {
            WhiteRect = new Texture2D(GraphicsDevice, 1, 1);
            WhiteRect.SetData(new Color[ ] { Color.White });
        }

        public static readonly int[ ] DX = new int[ ] { 1, 0, -1, 0 };
        public static readonly int[ ] DY = new int[ ] { 0, -1, 0, 1 };
        public static readonly uint[ ] DS = new uint[ ] { 2, 3, 5, 7 };

        static SimpleUtils( ) {
        }

        public static T[ ][ ] Create2DArray<T>(UInt32 width, UInt32 height, T @default) {
            T[ ][ ] result = new T[width][ ];
            for (UInt32 i = 0; i < width; i++) {
                result[i] = new T[height];
                for (UInt32 j = 0; j < height; j++) {
                    result[i][j] = @default;
                }
            }
            return result;
        }

        public static T Choose<T>(T[ ] variants) {
            return variants[_rand.Next( ) % variants.Length];
        }

        public static T Choose<T>(Tuple<T, float>[ ] variants) {
            float max_probability = 0f;
            foreach (var elem in variants) {
                max_probability += elem.Item2;
            }
            float result = Random * max_probability;
            float current_probability = 0f;
            foreach (var elem in variants) {
                current_probability += elem.Item2;
                if (result < current_probability) {
                    return elem.Item1;
                }
            }
            throw new Exception("Check this function for logical errors");
        }

        public static float Random { get { return (float)_rand.NextDouble( ); } }

        public static int Rand { get { return _rand.Next( ); } }

        public static Vector2 Vector2(this Point point) {
            return new Vector2(point.X, point.Y);
        }

        public static Point RoundToPoint(this Vector2 vector) {
            return new Point((int)Math.Round(vector.X), (int)Math.Round(vector.Y));
        }

        public static Point FloorToPoint(this Vector2 vector) {
            return new Point((int)Math.Floor(vector.X), (int)Math.Floor(vector.Y));
        }

        public static Point CeilingToPoint(this Vector2 vector) {
            return new Point((int)Math.Ceiling(vector.X), (int)Math.Ceiling(vector.Y));
        }

        public static Point Mod(this Point point1, Point point2) {
            return new Point(point1.X % point2.X, point1.Y % point2.Y);
        }

        public static Vector2 Add(this Vector2 vector, float value) {
            return new Vector2(vector.X + value, vector.Y + value);
        }
    }
}
