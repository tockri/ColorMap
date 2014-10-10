using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ColorMap {
    /// <summary>
    /// カラーマップを生成するクラス
    /// </summary>
    public class ColorMapMaker {
        private const int MAX = 255;
        private const int LENGTH = 256;
        /// <summary>
        /// カラーマップ種別
        /// </summary>
        public enum FocusType {
            /// <summary>
            /// 通常
            /// </summary>
            None,
            /// <summary>
            /// 低温域強調
            /// </summary>
            Low,
            /// <summary>
            /// 中温域強調
            /// </summary>
            Mid,
            /// <summary>
            /// 高温域強調
            /// </summary>
            High
        }
        /// <summary>
        /// 256階調のカラーマップを１つ生成する
        /// </summary>
        /// <param name="focusType">種別</param>
        /// <returns></returns>
        public static Color[] Create(FocusType focusType) {
            var ret = new Color[LENGTH];
            var pts = iPoints(focusType);
            int i1 = pts[1];
            int i2 = pts[2];
            int i3 = pts[3];
            int i4 = pts[4];
            int i5 = pts[5];
            for (int i = 0; i < LENGTH; i++) {
                // 6段階に分けて計算する
                int r = 0;
                int g = 0;
                int b = 0;
                if (i < i1) {
                    b = increase(i, 0, i1);
                } else if (i < i2) {
                    b = MAX;
                    g = increase(i, i1, i2);
                } else if (i < i3) {
                    r = increase(i, i2, i4);
                    b = decrease(i, i2, i4);
                    g = MAX;
                } else if (i < i4) {
                    g = MAX;
                    r = increase(i, i2, i4);
                    b = decrease(i, i2, i4);
                } else if (i < i5) {
                    r = MAX;
                    g = decrease(i, i4, i5);
                } else {
                    r = MAX;
                    g = increase(i, i5, LENGTH);
                    b = increase(i, i5, LENGTH);
                }
                // Debug.WriteLine("r=" + r + "/g=" + g + "/b=" + b);
                ret[i] = Color.FromArgb(r, g, b);
            }
            return ret;
        }
        /// <summary>
        /// i1～i5を返す
        /// </summary>
        /// <param name="focusType"></param>
        /// <returns></returns>
        private static int[] iPoints(FocusType focusType) {
            var ret = new int[6];
            ret[0] = 0;
            if (focusType == FocusType.None) {
                // まんべんなく
                var i1 = LENGTH / 6;
                ret[1] = i1;
                ret[2] = i1 * 2;
                ret[3] = i1 * 3;
                ret[4] = i1 * 4;
                ret[5] = i1 * 5;
            } else if (focusType == FocusType.Low) {
                // 低い方を強調
                var i1 = LENGTH / 12;
                ret[1] = i1 * 2;
                ret[2] = i1 * 3;
                ret[3] = i1 * 4;
                ret[4] = i1 * 6;
                ret[5] = i1 * 9;
            } else if (focusType == FocusType.Mid) {
                // 真ん中を強調
                var i1 = LENGTH / 12;
                ret[1] = i1 * 3;
                ret[2] = i1 * 5;
                ret[3] = i1 * 6;
                ret[4] = i1 * 7;
                ret[5] = i1 * 9;
            } else if (focusType == FocusType.High) {
                var i1 = LENGTH / 12;
                ret[1] = i1 * 4;
                ret[2] = i1 * 7;
                ret[3] = i1 * 8;
                ret[4] = i1 * 9;
                ret[5] = i1 * 10;
            }
            return ret;
        }


        /// <summary>
        /// 上り
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        private static int increase(int x, int x1, int x2) {
            double y = (Math.Cos(Math.PI / (x2 - x1) * (x - x2)) + 1) * MAX / 2;
            if (y > MAX) {
                return MAX;
            } else if (y < 0) {
                return 0;
            } else {
                return (int)Math.Round(y);
            }
        }
        /// <summary>
        /// 下り
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        private static int decrease(int x, int x1, int x2) {
            double y = (Math.Cos(Math.PI / (x2 - x1) * (x - x1)) + 1) * MAX / 2;
            if (y > MAX) {
                return MAX;
            } else if (y < 0) {
                return 0;
            } else {
                return (int)Math.Round(y);
            }
        }

        /// <summary>
        /// 幅256pxのBitmapを生成して返す
        /// </summary>
        /// <param name="map">色配列</param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmap(Color[] map, int height) {
            Bitmap bmp = new Bitmap(map.Length, height);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            unsafe {
                byte* p = (byte*)data.Scan0;
                for (int y = 0; y < bmp.Height; y++) {
                    for (int x = 0; x < map.Length; x++) {
                        var c = map[x];
                        p[0] = c.B;
                        p[1] = c.G;
                        p[2] = c.R;
                        p += 3;
                    }
                }
            }
            bmp.UnlockBits(data);
            return bmp;

        }
    }
}
