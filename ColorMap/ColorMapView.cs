using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ColorMap {
    public partial class ColorMapView : UserControl {
        private Bitmap bmp;
        private ColorMapMaker.FocusType focusType = ColorMapMaker.FocusType.None;
        /// <summary>
        /// カラーマップの種類
        /// </summary>
        public ColorMapMaker.FocusType FocusType {
            get {
                return focusType;
            }
            set {
                if (focusType != value) {
                    focusType = value;
                    UpdateColormap();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ColorMapView() {
            InitializeComponent();
            UpdateColormap();
        }
        /// <summary>
        /// Bitmapを更新する
        /// </summary>
        private void UpdateColormap() {
            var map = ColorMapMaker.Create(focusType);
            bmp = ColorMapMaker.CreateBitmap(map, 20);
        }
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            if (bmp != null) {
                g.DrawImage(bmp, 0, 0, Width, Height);
            }
        }
        /// <summary>
        /// Bitmapを破棄する
        /// </summary>
        protected override void DestroyHandle() {
            base.DestroyHandle();
            if (bmp != null) {
                bmp.Dispose();
                bmp = null;
            }
        }
    }
}
