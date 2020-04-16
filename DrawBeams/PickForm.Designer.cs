namespace DrawBeams
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
            this.tbox_curve = new System.Windows.Forms.TextBox();
            this.bt_floor = new System.Windows.Forms.Button();
            this.tbox_floor = new System.Windows.Forms.TextBox();
            this.bt_Commit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_Beamsymbols = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_pickcurve
            // 
            this.bt_pickcurve.Location = new System.Drawing.Point(115, 43);
            this.bt_pickcurve.Name = "bt_pickcurve";
            this.bt_pickcurve.Size = new System.Drawing.Size(103, 23);
            this.bt_pickcurve.TabIndex = 0;
            this.bt_pickcurve.Text = "拾取模型线";
            this.bt_pickcurve.UseVisualStyleBackColor = true;
            this.bt_pickcurve.Click += new System.EventHandler(this.bt_pickcurve_Click);
            // 
            // tbox_curve
            // 
            this.tbox_curve.Location = new System.Drawing.Point(255, 43);
            this.tbox_curve.Name = "tbox_curve";
            this.tbox_curve.Size = new System.Drawing.Size(100, 25);
            this.tbox_curve.TabIndex = 1;
            // 
            // bt_floor
            // 
            this.bt_floor.Location = new System.Drawing.Point(115, 108);
            this.bt_floor.Name = "bt_floor";
            this.bt_floor.Size = new System.Drawing.Size(103, 23);
            this.bt_floor.TabIndex = 2;
            this.bt_floor.Text = "拾取楼板";
            this.bt_floor.UseVisualStyleBackColor = true;
            this.bt_floor.Click += new System.EventHandler(this.bt_floor_Click);
            // 
            // tbox_floor
            // 
            this.tbox_floor.Location = new System.Drawing.Point(255, 106);
            this.tbox_floor.Name = "tbox_floor";
            this.tbox_floor.Size = new System.Drawing.Size(100, 25);
            this.tbox_floor.TabIndex = 3;
            // 
            // bt_Commit
            // 
            this.bt_Commit.Location = new System.Drawing.Point(403, 218);
            this.bt_Commit.Name = "bt_Commit";
            this.bt_Commit.Size = new System.Drawing.Size(103, 23);
            this.bt_Commit.TabIndex = 4;
            this.bt_Commit.Text = "提交";
            this.bt_Commit.UseVisualStyleBackColor = true;
            this.bt_Commit.Click += new System.EventHandler(this.bt_Commit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(125, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "梁的族类型";
            // 
            // cbox_Beamsymbols
            // 
            this.cbox_Beamsymbols.FormattingEnabled = true;
            this.cbox_Beamsymbols.Location = new System.Drawing.Point(255, 160);
            this.cbox_Beamsymbols.Name = "cbox_Beamsymbols";
            this.cbox_Beamsymbols.Size = new System.Drawing.Size(192, 23);
            this.cbox_Beamsymbols.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(416, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "拾取面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 285);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbox_Beamsymbols);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_Commit);
            this.Controls.Add(this.tbox_floor);
            this.Controls.Add(this.bt_floor);
            this.Controls.Add(this.tbox_curve);
            this.Controls.Add(this.bt_pickcurve);
            this.Name = "PickForm";
            this.Text = "投影梁";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PickForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_pickcurve;
        private System.Windows.Forms.TextBox tbox_curve;
        private System.Windows.Forms.Button bt_floor;
        private System.Windows.Forms.TextBox tbox_floor;
        private System.Windows.Forms.Button bt_Commit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox cbox_Beamsymbols;
    }
}