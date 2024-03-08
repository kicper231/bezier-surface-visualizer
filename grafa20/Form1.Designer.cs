using System.Reflection;
using System.Resources;
using System.Runtime.Versioning;

namespace grafa20
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            groupBox1 = new GroupBox();
            checkBox7 = new CheckBox();
            label15 = new Label();
            label14 = new Label();
            trackBar10 = new TrackBar();
            trackBar9 = new TrackBar();
            checkBox6 = new CheckBox();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            zlabel = new Label();
            label10 = new Label();
            trackBar8 = new TrackBar();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            label9 = new Label();
            trackBar7 = new TrackBar();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            checkBox3 = new CheckBox();
            trackBar6 = new TrackBar();
            trackBar5 = new TrackBar();
            trackBar4 = new TrackBar();
            checkBox2 = new CheckBox();
            label5 = new Label();
            checkBox1 = new CheckBox();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            label4 = new Label();
            label3 = new Label();
            trackBar3 = new TrackBar();
            trackBar2 = new TrackBar();
            label2 = new Label();
            label1 = new Label();
            trackBar1 = new TrackBar();
            colorDialog1 = new ColorDialog();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            pictureBox1.Location = new Point(51, 58);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(500, 500);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ActiveCaption;
            groupBox1.Controls.Add(checkBox7);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(label14);
            groupBox1.Controls.Add(trackBar10);
            groupBox1.Controls.Add(trackBar9);
            groupBox1.Controls.Add(checkBox6);
            groupBox1.Controls.Add(label13);
            groupBox1.Controls.Add(label12);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(zlabel);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(trackBar8);
            groupBox1.Controls.Add(checkBox5);
            groupBox1.Controls.Add(checkBox4);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(trackBar7);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(trackBar6);
            groupBox1.Controls.Add(trackBar5);
            groupBox1.Controls.Add(trackBar4);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(trackBar3);
            groupBox1.Controls.Add(trackBar2);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(trackBar1);
            groupBox1.Location = new Point(614, 0);
            groupBox1.Margin = new Padding(4, 4, 4, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 4, 4, 4);
            groupBox1.Size = new Size(516, 625);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(259, 509);
            checkBox7.Margin = new Padding(4, 4, 4, 4);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(194, 29);
            checkBox7.TabIndex = 36;
            checkBox7.Text = "włacz transformacje";
            checkBox7.UseVisualStyleBackColor = true;
            checkBox7.CheckedChanged += checkBox7_CheckedChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(366, 590);
            label15.Margin = new Padding(4, 0, 4, 0);
            label15.Name = "label15";
            label15.Size = new Size(47, 25);
            label15.TabIndex = 35;
            label15.Text = "beta";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(169, 586);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(40, 25);
            label14.TabIndex = 34;
            label14.Text = "alfa";
            // 
            // trackBar10
            // 
            trackBar10.Location = new Point(136, 548);
            trackBar10.Margin = new Padding(4, 4, 4, 4);
            trackBar10.Name = "trackBar10";
            trackBar10.Size = new Size(162, 69);
            trackBar10.TabIndex = 33;
            trackBar10.Scroll += trackBar10_Scroll;
            // 
            // trackBar9
            // 
            trackBar9.Location = new Point(310, 548);
            trackBar9.Margin = new Padding(4, 4, 4, 4);
            trackBar9.Name = "trackBar9";
            trackBar9.Size = new Size(162, 69);
            trackBar9.TabIndex = 32;
            trackBar9.Scroll += trackBar9_Scroll;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(16, 509);
            checkBox6.Margin = new Padding(4, 4, 4, 4);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(118, 29);
            checkBox6.TabIndex = 31;
            checkBox6.Text = "Kula wlacz";
            checkBox6.UseVisualStyleBackColor = true;
            checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = Color.Transparent;
            label13.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(38, 102);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(0, 15);
            label13.TabIndex = 30;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(369, 102);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(15, 15);
            label12.TabIndex = 29;
            label12.Text = "z:";
            label12.Click += label12_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(99, 29);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(0, 15);
            label11.TabIndex = 28;
            label11.Click += label11_Click;
            // 
            // zlabel
            // 
            zlabel.AutoSize = true;
            zlabel.BackColor = Color.Transparent;
            zlabel.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            zlabel.Location = new Point(369, 32);
            zlabel.Margin = new Padding(4, 0, 4, 0);
            zlabel.Name = "zlabel";
            zlabel.Size = new Size(17, 15);
            zlabel.TabIndex = 27;
            zlabel.Text = "Z:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(315, 71);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(133, 25);
            label10.TabIndex = 26;
            label10.Text = "zmien z punktu";
            label10.Click += label10_Click;
            // 
            // trackBar8
            // 
            trackBar8.Location = new Point(281, 40);
            trackBar8.Margin = new Padding(4, 4, 4, 4);
            trackBar8.Maximum = 20;
            trackBar8.Name = "trackBar8";
            trackBar8.Size = new Size(191, 69);
            trackBar8.TabIndex = 25;
            trackBar8.Scroll += trackBar8_Scroll;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(342, 199);
            checkBox5.Margin = new Padding(4, 4, 4, 4);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(125, 29);
            checkBox5.TabIndex = 24;
            checkBox5.Text = "siatka bazy";
            checkBox5.UseVisualStyleBackColor = true;
            checkBox5.CheckedChanged += checkBox5_CheckedChanged;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(171, 199);
            checkBox4.Margin = new Padding(4, 4, 4, 4);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(160, 29);
            checkBox4.TabIndex = 23;
            checkBox4.Text = "Siatka trojkatna";
            checkBox4.UseVisualStyleBackColor = true;
            checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(26, 296);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(130, 25);
            label9.TabIndex = 22;
            label9.Text = "wpólczynnik m";
            // 
            // trackBar7
            // 
            trackBar7.Location = new Point(14, 265);
            trackBar7.Margin = new Padding(4, 4, 4, 4);
            trackBar7.Minimum = 1;
            trackBar7.Name = "trackBar7";
            trackBar7.Size = new Size(162, 69);
            trackBar7.TabIndex = 21;
            trackBar7.Value = 1;
            trackBar7.Scroll += trackBar7_Scroll;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(222, 296);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(29, 25);
            label8.TabIndex = 20;
            label8.Text = "ks";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(369, 296);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(32, 25);
            label7.TabIndex = 19;
            label7.Text = "kd";
            label7.Click += label7_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(369, 142);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(78, 25);
            label6.TabIndex = 18;
            label6.Text = "z swiatla";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(26, 199);
            checkBox3.Margin = new Padding(4, 4, 4, 4);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(110, 29);
            checkBox3.TabIndex = 17;
            checkBox3.Text = "Animacja";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // trackBar6
            // 
            trackBar6.Location = new Point(318, 110);
            trackBar6.Margin = new Padding(4, 4, 4, 4);
            trackBar6.Name = "trackBar6";
            trackBar6.Size = new Size(162, 69);
            trackBar6.TabIndex = 16;
            trackBar6.Value = 5;
            trackBar6.Scroll += trackBar6_Scroll;
            // 
            // trackBar5
            // 
            trackBar5.Location = new Point(324, 265);
            trackBar5.Margin = new Padding(4, 4, 4, 4);
            trackBar5.Name = "trackBar5";
            trackBar5.Size = new Size(162, 69);
            trackBar5.TabIndex = 15;
            trackBar5.Value = 5;
            trackBar5.Scroll += trackBar5_Scroll;
            // 
            // trackBar4
            // 
            trackBar4.Location = new Point(168, 265);
            trackBar4.Margin = new Padding(4, 4, 4, 4);
            trackBar4.Name = "trackBar4";
            trackBar4.Size = new Size(162, 69);
            trackBar4.TabIndex = 14;
            trackBar4.Value = 5;
            trackBar4.Scroll += trackBar4_Scroll;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox2.Location = new Point(281, 436);
            checkBox2.Margin = new Padding(4, 4, 4, 4);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(166, 25);
            checkBox2.TabIndex = 13;
            checkBox2.Text = "Domyslna tekstura";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 555);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(117, 25);
            label5.TabIndex = 12;
            label5.Text = "performance:";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            checkBox1.Location = new Point(26, 436);
            checkBox1.Margin = new Padding(4, 4, 4, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(203, 25);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Domyślna normal mapa";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            button4.Location = new Point(335, 342);
            button4.Margin = new Padding(4, 4, 4, 4);
            button4.Name = "button4";
            button4.Size = new Size(129, 86);
            button4.TabIndex = 10;
            button4.Text = "Wczytaj normalmape";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.Location = new Point(186, 342);
            button3.Margin = new Padding(4, 4, 4, 4);
            button3.Name = "button3";
            button3.Size = new Size(129, 86);
            button3.TabIndex = 9;
            button3.Text = "Wczytaj teksture koloru";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(19, 342);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new Size(141, 36);
            button2.TabIndex = 8;
            button2.Text = "kolor swiatla";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // button1
            // 
            button1.Location = new Point(19, 386);
            button1.Margin = new Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new Size(139, 42);
            button1.TabIndex = 7;
            button1.Text = "kolor tla";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(199, 142);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(82, 25);
            label4.TabIndex = 6;
            label4.Text = "swiatlo Y";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 142);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(83, 25);
            label3.TabIndex = 5;
            label3.Text = "swiatlo X";
            // 
            // trackBar3
            // 
            trackBar3.Location = new Point(156, 110);
            trackBar3.Margin = new Padding(4, 4, 4, 4);
            trackBar3.Name = "trackBar3";
            trackBar3.Size = new Size(162, 69);
            trackBar3.TabIndex = 4;
            trackBar3.Value = 5;
            trackBar3.Scroll += trackBar3_Scroll;
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(14, 110);
            trackBar2.Margin = new Padding(4, 4, 4, 4);
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(162, 69);
            trackBar2.TabIndex = 3;
            trackBar2.Value = 5;
            trackBar2.Scroll += trackBar2_Scroll;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 580);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(59, 25);
            label2.TabIndex = 2;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 7F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(42, 71);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(148, 19);
            label1.TabIndex = 1;
            label1.Text = "dokladnosc triangulacji";
            // 
            // trackBar1
            // 
            trackBar1.LargeChange = 2;
            trackBar1.Location = new Point(8, 32);
            trackBar1.Margin = new Padding(4, 4, 4, 4);
            trackBar1.Minimum = 2;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(240, 69);
            trackBar1.SmallChange = 2;
            trackBar1.TabIndex = 0;
            trackBar1.Value = 8;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // timer1
            // 
            timer1.Interval = 50;
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1126, 615);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Margin = new Padding(4, 4, 4, 4);
            MaximumSize = new Size(1148, 671);
            MinimumSize = new Size(1148, 671);
            Name = "Form1";
            Text = "GRAFIKA2";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar10).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar9).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar8).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar7).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar6).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar5).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar4).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private Label label1;
        private TrackBar trackBar1;
        private Label label2;
        private TrackBar trackBar2;
        private Label label4;
        private Label label3;
        private TrackBar trackBar3;
        private ColorDialog colorDialog1;
        private Button button2;
        private Button button1;
        private Button button3;
        private Button button4;
        private CheckBox checkBox1;
        private Label label5;
        private CheckBox checkBox2;
        private TrackBar trackBar5;
        private TrackBar trackBar4;
        private CheckBox checkBox3;
        private TrackBar trackBar6;
        private Label label7;
        private Label label6;
        private Label label8;
        private Label label9;
        private TrackBar trackBar7;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private System.Windows.Forms.Timer timer1;
        private TrackBar trackBar8;
        private Label label10;
        private Label zlabel;
        private Label label12;
        private Label label11;
        private Label label13;
        private CheckBox checkBox6;
        private TrackBar trackBar10;
        private TrackBar trackBar9;
        private CheckBox checkBox7;
        private Label label15;
        private Label label14;
    }
}