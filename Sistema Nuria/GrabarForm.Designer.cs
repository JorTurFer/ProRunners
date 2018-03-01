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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrabarForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pict_Cam1 = new System.Windows.Forms.PictureBox();
            this.pict_Cam0 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_FPS_Display = new System.Windows.Forms.Label();
            this.lbl_FPS = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_Duracion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pb_HDD = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.pb_Memory = new System.Windows.Forms.ProgressBar();
            this.pict_Photo = new System.Windows.Forms.PictureBox();
            this.pict_Grab = new System.Windows.Forms.PictureBox();
            this.rB_B = new System.Windows.Forms.RadioButton();
            this.rb_A = new System.Windows.Forms.RadioButton();
            this.rB_2 = new System.Windows.Forms.RadioButton();
            this.MemoryTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_Counters = new System.Windows.Forms.Label();
            this.lbl_Images = new System.Windows.Forms.Label();
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
            this.groupBox1.Controls.Add(this.lbl_Counters);
            this.groupBox1.Controls.Add(this.lbl_Images);
            this.groupBox1.Controls.Add(this.lbl_FPS_Display);
            this.groupBox1.Controls.Add(this.lbl_FPS);
            this.groupBox1.Controls.Add(this.lbl_Time);
            this.groupBox1.Controls.Add(this.lbl_Duracion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pb_HDD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pb_Memory);
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
            // lbl_FPS_Display
            // 
            this.lbl_FPS_Display.AutoSize = true;
            this.lbl_FPS_Display.Location = new System.Drawing.Point(1084, 92);
            this.lbl_FPS_Display.Name = "lbl_FPS_Display";
            this.lbl_FPS_Display.Size = new System.Drawing.Size(0, 13);
            this.lbl_FPS_Display.TabIndex = 16;
            this.lbl_FPS_Display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_FPS_Display.Visible = false;
            // 
            // lbl_FPS
            // 
            this.lbl_FPS.AutoSize = true;
            this.lbl_FPS.Location = new System.Drawing.Point(1008, 92);
            this.lbl_FPS.Name = "lbl_FPS";
            this.lbl_FPS.Size = new System.Drawing.Size(24, 13);
            this.lbl_FPS.TabIndex = 15;
            this.lbl_FPS.Text = "Fps";
            this.lbl_FPS.Visible = false;
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.Location = new System.Drawing.Point(879, 69);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(0, 13);
            this.lbl_Time.TabIndex = 14;
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Time.Visible = false;
            // 
            // lbl_Duracion
            // 
            this.lbl_Duracion.AutoSize = true;
            this.lbl_Duracion.Location = new System.Drawing.Point(818, 69);
            this.lbl_Duracion.Name = "lbl_Duracion";
            this.lbl_Duracion.Size = new System.Drawing.Size(50, 13);
            this.lbl_Duracion.TabIndex = 13;
            this.lbl_Duracion.Text = "Duracion";
            this.lbl_Duracion.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(823, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Espacio";
            // 
            // pb_HDD
            // 
            this.pb_HDD.Location = new System.Drawing.Point(876, 44);
            this.pb_HDD.Name = "pb_HDD";
            this.pb_HDD.Size = new System.Drawing.Size(261, 17);
            this.pb_HDD.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_HDD.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(823, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Memoria";
            // 
            // pb_Memory
            // 
            this.pb_Memory.Location = new System.Drawing.Point(876, 21);
            this.pb_Memory.Name = "pb_Memory";
            this.pb_Memory.Size = new System.Drawing.Size(261, 17);
            this.pb_Memory.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_Memory.TabIndex = 7;
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
            // MemoryTimer
            // 
            this.MemoryTimer.Enabled = true;
            this.MemoryTimer.Interval = 500;
            this.MemoryTimer.Tick += new System.EventHandler(this.MemoryTimer_Tick);
            // 
            // lbl_Counters
            // 
            this.lbl_Counters.AutoSize = true;
            this.lbl_Counters.Location = new System.Drawing.Point(890, 92);
            this.lbl_Counters.Name = "lbl_Counters";
            this.lbl_Counters.Size = new System.Drawing.Size(0, 13);
            this.lbl_Counters.TabIndex = 18;
            this.lbl_Counters.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Counters.Visible = false;
            // 
            // lbl_Images
            // 
            this.lbl_Images.AutoSize = true;
            this.lbl_Images.Location = new System.Drawing.Point(841, 92);
            this.lbl_Images.Name = "lbl_Images";
            this.lbl_Images.Size = new System.Drawing.Size(27, 13);
            this.lbl_Images.TabIndex = 17;
            this.lbl_Images.Text = "Img:";
            this.lbl_Images.Visible = false;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pb_Memory;
        private System.Windows.Forms.Timer MemoryTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pb_HDD;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_Duracion;
        private System.Windows.Forms.Label lbl_FPS_Display;
        private System.Windows.Forms.Label lbl_FPS;
        private System.Windows.Forms.Label lbl_Counters;
        private System.Windows.Forms.Label lbl_Images;
    }
}