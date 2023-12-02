namespace BerorinApp
{
    partial class PartsView
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            SplitContainer = new SplitContainer();
            gv = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            MenuStrip = new MenuStrip();
            ((System.ComponentModel.ISupportInitialize)SplitContainer).BeginInit();
            SplitContainer.Panel1.SuspendLayout();
            SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gv).BeginInit();
            SuspendLayout();
            // 
            // SplitContainer
            // 
            SplitContainer.Dock = DockStyle.Fill;
            SplitContainer.HorizontalDistance = 0.7D;
            SplitContainer.Location = new Point(0, 24);
            SplitContainer.Name = "SplitContainer";
            SplitContainer.Orientation = Orientation.Horizontal;
            // 
            // SplitContainer.Panel1
            // 
            SplitContainer.Panel1.Controls.Add(gv);
            SplitContainer.Panel1.Padding = new Padding(2, 2, 2, 1);
            SplitContainer.Panel1.RightToLeft = RightToLeft.No;
            // 
            // SplitContainer.Panel2
            // 
            SplitContainer.Panel2.Padding = new Padding(2, 1, 2, 2);
            SplitContainer.Panel2.RightToLeft = RightToLeft.No;
            SplitContainer.Panel2MinSize = 0;
            SplitContainer.Size = new Size(984, 537);
            SplitContainer.SplitterDistance = 420;
            SplitContainer.TabIndex = 0;
            SplitContainer.VerticalDistance = 0.8D;
            // 
            // gv
            // 
            gv.AllowUserToAddRows = false;
            gv.AllowUserToDeleteRows = false;
            gv.AllowUserToResizeRows = false;
            gv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gv.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            gv.Dock = DockStyle.Fill;
            gv.EditMode = DataGridViewEditMode.EditOnEnter;
            gv.Location = new Point(2, 2);
            gv.Name = "gv";
            gv.RowHeadersWidth = 20;
            gv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            gv.RowTemplate.Height = 21;
            gv.ShowCellToolTips = false;
            gv.Size = new Size(980, 417);
            gv.TabIndex = 0;
            gv.TabStop = false;
            // 
            // Column1
            // 
            Column1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column1.DataPropertyName = "Hex";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.Padding = new Padding(5, 0, 5, 0);
            dataGridViewCellStyle1.SelectionBackColor = Color.Yellow;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            Column1.DefaultCellStyle = dataGridViewCellStyle1;
            Column1.Frozen = true;
            Column1.HeaderText = "";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column1.Width = 5;
            // 
            // Column2
            // 
            Column2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Column2.DataPropertyName = "Name";
            dataGridViewCellStyle2.BackColor = Color.Gray;
            dataGridViewCellStyle2.Font = new Font("メイリオ", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.Padding = new Padding(0, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            Column2.DefaultCellStyle = dataGridViewCellStyle2;
            Column2.Frozen = true;
            Column2.HeaderText = "";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.Width = 5;
            // 
            // MenuStrip
            // 
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Padding = new Padding(0, 2, 0, 2);
            MenuStrip.Size = new Size(984, 24);
            MenuStrip.TabIndex = 1;
            // 
            // PartsView
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(984, 561);
            Controls.Add(SplitContainer);
            Controls.Add(MenuStrip);
            Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point);
            MainMenuStrip = MenuStrip;
            Name = "PartsView";
            Text = "Form1";
            Load += Form1_Load;
            SplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SplitContainer).EndInit();
            SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gv).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer SplitContainer;
        private DataGridView gv;
        private MenuStrip MenuStrip;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}