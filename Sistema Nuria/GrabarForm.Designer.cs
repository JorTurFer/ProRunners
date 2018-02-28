namespace Sistema_Nuria
{
    partial class GrabarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrabarForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pict_Cam1 = new System.Windows.Forms.PictureBox();
            this.pict_Cam0 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pict_Photo = new System.Windows.Forms.PictureBox();
            this.pict_Grab = new System.Windows.Forms.PictureBox();
            this.rB_B = new System.Windows.Forms.RadioButton();
            this.rb_A = new System.Windows.Forms.RadioButton();
            this.rB_2 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Cam1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Cam0)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Grab)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pict_Cam1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pict_Cam0, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1171, 614);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pict_Cam1
            // 
            this.pict_Cam1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pict_Cam1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pict_Cam1.Location = new System.Drawing.Point(588, 125);
            this.pict_Cam1.Name = "pict_Cam1";
            this.pict_Cam1.Size = new System.Drawing.Size(580, 486);
            this.pict_Cam1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_Cam1.TabIndex = 1;
            this.pict_Cam1.TabStop = false;
            // 
            // pict_Cam0
            // 
            this.pict_Cam0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pict_Cam0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pict_Cam0.Location = new System.Drawing.Point(3, 125);
            this.pict_Cam0.Name = "pict_Cam0";
            this.pict_Cam0.Size = new System.Drawing.Size(579, 486);
            this.pict_Cam0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_Cam0.TabIndex = 0;
            this.pict_Cam0.TabStop = false;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.pict_Photo);
            this.groupBox1.Controls.Add(this.pict_Grab);
            this.groupBox1.Controls.Add(this.rB_B);
            this.groupBox1.Controls.Add(this.rb_A);
            this.groupBox1.Controls.Add(this.rB_2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1165, 116);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // pict_Photo
            // 
            this.pict_Photo.Image = ((System.Drawing.Image)(resources.GetObject("pict_Photo.Image")));
            this.pict_Photo.Location = new System.Drawing.Point(637, 17);
            this.pict_Photo.Name = "pict_Photo";
            this.pict_Photo.Size = new System.Drawing.Size(105, 91);
            this.pict_Photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_Photo.TabIndex = 6;
            this.pict_Photo.TabStop = false;
            this.pict_Photo.Click += new System.EventHandler(this.pict_Photo_Click);
            // 
            // pict_Grab
            // 
            this.pict_Grab.Image = ((System.Drawing.Image)(resources.GetObject("pict_Grab.Image")));
            this.pict_Grab.Location = new System.Drawing.Point(414, 17);
            this.pict_Grab.Name = "pict_Grab";
            this.pict_Grab.Size = new System.Drawing.Size(105, 91);
            this.pict_Grab.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict_Grab.TabIndex = 5;
            this.pict_Grab.TabStop = false;
            this.pict_Grab.Click += new System.EventHandler(this.pict_Grab_Click);
            // 
            // rB_B
            // 
            this.rB_B.AutoSize = true;
            this.rB_B.Location = new System.Drawing.Point(149, 77);
            this.rB_B.Name = "rB_B";
            this.rB_B.Size = new System.Drawing.Size(70, 17);
            this.rB_B.TabIndex = 4;
            this.rB_B.Text = "Cámara 2";
            this.rB_B.UseVisualStyleBackColor = true;
            // 
            // rb_A
            // 
            this.rb_A.AutoSize = true;
            this.rb_A.Location = new System.Drawing.Point(149, 54);
            this.rb_A.Name = "rb_A";
            this.rb_A.Size = new System.Drawing.Size(70, 17);
            this.rb_A.TabIndex = 3;
            this.rb_A.Text = "Cámara 1";
            this.rb_A.UseVisualStyleBackColor = true;
            // 
            // rB_2
            // 
            this.rB_2.AutoSize = true;
            this.rB_2.Checked = true;
            this.rB_2.Location = new System.Drawing.Point(149, 31);
            this.rB_2.Name = "rB_2";
            this.rB_2.Size = new System.Drawing.Size(87, 17);
            this.rB_2.TabIndex = 2;
            this.rB_2.TabStop = true;
            this.rB_2.Text = "Dos cámaras";
            this.rB_2.UseVisualStyleBackColor = true;
            // 
            // GrabarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 614);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GrabarForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Captura";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GrabarForm_FormClosing);
            this.Load += new System.EventHandler(this.GrabarForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pict_Cam1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Cam0)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict_Grab)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pict_Cam0;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pict_Cam1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rB_B;
        private System.Windows.Forms.RadioButton rb_A;
        private System.Windows.Forms.RadioButton rB_2;
        private System.Windows.Forms.PictureBox pict_Photo;
        private System.Windows.Forms.PictureBox pict_Grab;
    }
}