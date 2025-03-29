namespace BtcCzkRates
{
    partial class MasterForm
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            button1 = new Button();
            liveDataGridView = new DataGridView();
            tabPage2 = new TabPage();
            savedDataGridView = new DataGridView();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)liveDataGridView).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)savedDataGridView).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 24);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(776, 414);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(liveDataGridView);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(768, 386);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Live Data";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(602, 13);
            button1.Name = "button1";
            button1.Size = new Size(147, 23);
            button1.TabIndex = 1;
            button1.Text = "Uložit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // liveDataGridView
            // 
            liveDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            liveDataGridView.Location = new Point(6, 48);
            liveDataGridView.Name = "liveDataGridView";
            liveDataGridView.Size = new Size(756, 332);
            liveDataGridView.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(savedDataGridView);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(768, 386);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Saved Data";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // savedDataGridView
            // 
            savedDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            savedDataGridView.Location = new Point(6, 6);
            savedDataGridView.Name = "savedDataGridView";
            savedDataGridView.Size = new Size(756, 374);
            savedDataGridView.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Interval = 5000;
            timer1.Tick += timer1_Tick;
            // 
            // MasterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Name = "MasterForm";
            Text = "Form1";
            Load += MasterForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)liveDataGridView).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)savedDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button1;
        private DataGridView liveDataGridView;
        private DataGridView savedDataGridView;
        private System.Windows.Forms.Timer timer1;
    }
}
