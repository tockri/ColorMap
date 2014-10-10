namespace ColorMapTest {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.colorMapView = new ColorMap.ColorMapView();
            this.typeSelectBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // colorMapView
            // 
            this.colorMapView.BackColor = System.Drawing.Color.Transparent;
            this.colorMapView.FocusType = ColorMap.ColorMapMaker.FocusType.None;
            this.colorMapView.Location = new System.Drawing.Point(12, 12);
            this.colorMapView.MinimumSize = new System.Drawing.Size(320, 40);
            this.colorMapView.Name = "colorMapView";
            this.colorMapView.Size = new System.Drawing.Size(320, 40);
            this.colorMapView.TabIndex = 0;
            // 
            // typeSelectBox
            // 
            this.typeSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeSelectBox.FormattingEnabled = true;
            this.typeSelectBox.Location = new System.Drawing.Point(13, 59);
            this.typeSelectBox.Name = "typeSelectBox";
            this.typeSelectBox.Size = new System.Drawing.Size(121, 20);
            this.typeSelectBox.TabIndex = 1;
            this.typeSelectBox.SelectedIndexChanged += new System.EventHandler(this.typeSelectBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 96);
            this.Controls.Add(this.typeSelectBox);
            this.Controls.Add(this.colorMapView);
            this.Name = "MainForm";
            this.Text = "ColorMapTest";
            this.ResumeLayout(false);

        }

        #endregion

        private ColorMap.ColorMapView colorMapView;
        private System.Windows.Forms.ComboBox typeSelectBox;
    }
}

