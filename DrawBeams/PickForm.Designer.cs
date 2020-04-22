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
            this.bt_floor = new System.Windows.Forms.Button();
            this.bt_Commit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_Beamsymbols = new System.Windows.Forms.ComboBox();
            this.lbl_modelcurve = new System.Windows.Forms.Label();
            this.lbl_floor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bt_pickcurve
            // 
            this.bt_pickcurve.Location = new System.Drawing.Point(33, 25);
            this.bt_pickcurve.Name = "bt_pickcurve";
            this.bt_pickcurve.Size = new System.Drawing.Size(103, 28);
            this.bt_pickcurve.TabIndex = 0;
            this.bt_pickcurve.Text = "拾取模型线";
            this.bt_pickcurve.UseVisualStyleBackColor = true;
            this.bt_pickcurve.Click += new System.EventHandler(this.bt_pickcurve_Click);
            // 
            // bt_floor
            // 
            this.bt_floor.Location = new System.Drawing.Point(33, 90);
            this.bt_floor.Name = "bt_floor";
            this.bt_floor.Size = new System.Drawing.Size(103, 28);
            this.bt_floor.TabIndex = 2;
            this.bt_floor.Text = "拾取楼板";
            this.bt_floor.UseVisualStyleBackColor = true;
            this.bt_floor.Click += new System.EventHandler(this.bt_floor_Click);
            // 
            // bt_Commit
            // 
            this.bt_Commit.Location = new System.Drawing.Point(33, 200);
            this.bt_Commit.Name = "bt_Commit";
            this.bt_Commit.Size = new System.Drawing.Size(103, 28);
            this.bt_Commit.TabIndex = 4;
            this.bt_Commit.Text = "提交";
            this.bt_Commit.UseVisualStyleBackColor = true;
            this.bt_Commit.Click += new System.EventHandler(this.bt_Commit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "梁的族类型";
            // 
            // cbox_Beamsymbols
            // 
            this.cbox_Beamsymbols.FormattingEnabled = true;
            this.cbox_Beamsymbols.Location = new System.Drawing.Point(173, 146);
            this.cbox_Beamsymbols.Name = "cbox_Beamsymbols";
            this.cbox_Beamsymbols.Size = new System.Drawing.Size(192, 23);
            this.cbox_Beamsymbols.TabIndex = 6;
            // 
            // lbl_modelcurve
            // 
            this.lbl_modelcurve.AutoSize = true;
            this.lbl_modelcurve.Location = new System.Drawing.Point(195, 27);
            this.lbl_modelcurve.Name = "lbl_modelcurve";
            this.lbl_modelcurve.Size = new System.Drawing.Size(38, 15);
            this.lbl_modelcurve.TabIndex = 7;
            this.lbl_modelcurve.Text = "ID值";
            // 
            // lbl_floor
            // 
            this.lbl_floor.AutoSize = true;
            this.lbl_floor.Location = new System.Drawing.Point(195, 93);
            this.lbl_floor.Name = "lbl_floor";
            this.lbl_floor.Size = new System.Drawing.Size(38, 15);
            this.lbl_floor.TabIndex = 8;
            this.lbl_floor.Text = "ID值";
            // 
            // PickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 247);
            this.Controls.Add(this.lbl_floor);
            this.Controls.Add(this.lbl_modelcurve);
            this.Controls.Add(this.cbox_Beamsymbols);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_Commit);
            this.Controls.Add(this.bt_floor);
            this.Controls.Add(this.bt_pickcurve);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PickForm";
            this.Text = "板投影-绘制梁";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PickForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_pickcurve;
        private System.Windows.Forms.Button bt_floor;
        private System.Windows.Forms.Button bt_Commit;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cbox_Beamsymbols;
        public System.Windows.Forms.Label lbl_modelcurve;
        public System.Windows.Forms.Label lbl_floor;
    }
}