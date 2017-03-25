using LightRise.BaseClasses;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Windows.Forms;

namespace LightRise.WinUtils {

    static class WinUtils {

        public static Map ConvertToBigMap(Map[ ][ ] maps) {
            uint width = maps[0][0].Width;
            uint height = maps[0][0].Height;

            Map result = new Map((uint)maps.Length * width, (uint)maps[0].Length * height);
            for (uint i = 0; i < maps.Length; i++) {
                if (maps[i].Length != maps[0].Length) {
                    throw new Exception("Wrong Map[][] format");
                }
                for (uint j = 0; j < maps[i].Length; j++) {
                    result.Add(maps[i][j], new Point((int)(i * width), (int)(j * height)));
                }
            }
            return result;
        }

        public static void Save(this Map map) {
            SaveFileDialog sfd = new SaveFileDialog( );
            sfd.Filter = "map files (*.lrmap)|*.lrmap";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog( ) == DialogResult.OK) {
                using (Stream stream = sfd.OpenFile( )) {
                    using (StreamWriter sw = new StreamWriter(stream)) {
                        map.Save(sw);
                    }
                }
            }
        }

        public static void Save(this Map map, StreamWriter sw) {
            sw.Write(map.Width);
            sw.Write(map.Height);
            for (uint i = 0; i < map.Width; i++) {
                for (uint j = 0; j < map.Height; j++) {
                    sw.Write(map[i, j]);
                }
            }
        }

    }

}
