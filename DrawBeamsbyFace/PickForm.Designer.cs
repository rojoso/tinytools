namespace DrawBeamsbyFace
{
    partial class PickForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bt_pickcurve = new System.Windows.Forms.Button();
            this.bt_pickface = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_Beamsymbols = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbox_levels = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // bt_pickcurve
            // 
            this.bt_pickcurve.Location = new System.Drawing.Point(60, 25);
            this.bt_pickcurve.Name = "bt_pickcurve";
            this.bt_pickcurve.Size = new System.Drawing.Size(101, 36);
            this.bt_pickcurve.TabIndex = 0;
            this.bt_pickcurve.Text = "拾取模型线";
            this.bt_pickcurve.UseVisualStyleBackColor = true;
            this.bt_pickcurve.Click += new System.EventHandler(this.bt_pickcurve_Click);
            // 
            // bt_pickface
            // 
            this.bt_pickface.Location = new System.Drawing.Point(60, 82);
            this.bt_pickface.Name = "bt_pickface";
            this.bt_pickface.Size = new System.Drawing.Size(113, 32);
            this.bt_pickface.TabIndex = 1;
            this.bt_pickface.Text = "拾取楼板表面";
            this.bt_pickface.UseVisualStyleBackColor = true;
            this.bt_pickface.Click += new System.EventHandler(this.bt_pickface_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(375, 229);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 33);
            this.button3.TabIndex = 2;
            this.button3.Text = "提交生成";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "梁族类型";
            // 
            // cbox_Beamsymbols
            // 
            this.cbox_Beamsymbols.FormattingEnabled = true;
            this.cbox_Beamsymbols.Location = new System.Drawing.Point(176, 140);
            this.cbox_Beamsymbols.Name = "cbox_Beamsymbols";
            this.cbox_Beamsymbols.Size = new System.Drawing.Size(180, 23);
            this.cbox_Beamsymbols.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "标高";
            // 
            // cbox_levels
            // 
            this.cbox_levels.FormattingEnabled = true;
            this.cbox_levels.Location = new System.Drawing.Point(176, 184);
            this.cbox_levels.Name = "cbox_levels";
            this.cbox_levels.Size = new System.Drawing.Size(180, 23);
            this.cbox_levels.TabIndex = 6;
            // 
            // PickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 286);
            this.Controls.Add(this.cbox_levels);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbox_Beamsymbols);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.bt_pickface);
            this.Controls.Add(this.bt_pickcurve);
            this.Name = "PickForm";
            this.Text = "面投影-绘制梁";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PickForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_pickcurve;
        private System.Windows.Forms.Button bt_pickface;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbox_Beamsymbols;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cbox_levels;
    }
}