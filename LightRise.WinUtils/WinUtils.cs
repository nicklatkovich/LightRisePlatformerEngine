using LightRise.BaseClasses;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightRise.WinUtilsLib {

    public static class WinUtils {

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
                using (FileStream sw = new FileStream(sfd.FileName, FileMode.Create))
                using (BinaryWriter bw = new BinaryWriter(sw)) {
                    map.Save(bw);
                }
            }
        }

        public static void Save(this Map map, BinaryWriter sw) {
            sw.Write(map.Width);
            sw.Write(map.Height);
            for (uint i = 0; i < map.Width; i++) {
                for (uint j = 0; j < map.Height; j++) {
                    sw.Write(map[i, j]);
                }
            }
        }

        public static Map LoadMap( ) {
            Map result = null;
            OpenFileDialog ofd = new OpenFileDialog( );
            ofd.Filter = "map files (*.lrmap)|*.lrmap";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog( ) == DialogResult.OK) {
                using (FileStream sw = new FileStream(ofd.FileName, FileMode.Open))
                using (BinaryReader br = new BinaryReader(sw)) {
                    result = new Map(br.ReadUInt32( ), br.ReadUInt32( ));
                    for (uint i = 0; i < result.Width; i++) {
                        for (uint j = 0; j < result.Height; j++) {
                            result[i, j] = br.ReadUInt32( );
                        }
                    }
                }
            }
            return result;
        }
    }
}
