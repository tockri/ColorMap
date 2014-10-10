using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ColorMap;

namespace ColorMapTest {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            // FocusTypeの選択ボックスを作る
            var items = new Dictionary<ColorMapMaker.FocusType, string>();
            var types = Enum.GetValues(typeof(ColorMapMaker.FocusType));
            foreach (var t in types) {
                items[(ColorMapMaker.FocusType)t] = t.ToString();
            }
            typeSelectBox.DataSource = new BindingSource(items, null);
            typeSelectBox.ValueMember = "Key";
            typeSelectBox.DisplayMember = "Value";
        }
        /// <summary>
        /// FocusType選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void typeSelectBox_SelectedIndexChanged(object sender, EventArgs e) {
            var item = (KeyValuePair<ColorMapMaker.FocusType, string>)typeSelectBox.SelectedItem;
            colorMapView.FocusType = item.Key;
            colorMapView.Invalidate();
        }
    }
}
