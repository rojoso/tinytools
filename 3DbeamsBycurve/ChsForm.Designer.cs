namespace _3DbeamsBycurve
{
    partial class ChsForm
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
            this.cbox_BeamSymbols = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_Commit = new System.Windows.Forms.Button();
            this.cbox_Levels = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbox_BeamSymbols
            // 
            this.cbox_BeamSymbols.FormattingEnabled = true;
            this.cbox_BeamSymbols.Location = new System.Drawing.Point(106, 39);
            this.cbox_BeamSymbols.Name = "cbox_BeamSymbols";
            this.cbox_BeamSymbols.Size = new System.Drawing.Size(177, 23);
            this.cbox_BeamSymbols.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "梁族类型";
            // 
            // bt_Commit
            // 
            this.bt_Commit.Location = new System.Drawing.Point(297, 148);
            this.bt_Commit.Name = "bt_Commit";
            this.bt_Commit.Size = new System.Drawing.Size(75, 23);
            this.bt_Commit.TabIndex = 2;
            this.bt_Commit.Text = "确定";
            this.bt_Commit.UseVisualStyleBackColor = true;
            this.bt_Commit.Click += new System.EventHandler(this.bt_Commit_Click);
            // 
            // cbox_Levels
            // 
            this.cbox_Levels.FormattingEnabled = true;
            this.cbox_Levels.Location = new System.Drawing.Point(106, 97);
            this.cbox_Levels.Name = "cbox_Levels";
            this.cbox_Levels.Size = new System.Drawing.Size(177, 23);
            this.cbox_Levels.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "标高";
            // 
            // ChsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 192);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbox_Levels);
            this.Controls.Add(this.bt_Commit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbox_BeamSymbols);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChsForm";
            this.Text = "拾取线生梁";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ChsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_Commit;
        public System.Windows.Forms.ComboBox cbox_BeamSymbols;
        public System.Windows.Forms.ComboBox cbox_Levels;
        private System.Windows.Forms.Label label2;
    }
}