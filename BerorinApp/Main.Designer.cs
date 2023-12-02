namespace BerorinApp
{
    partial class Main
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
            Menu = new MenuStrip();
            TabControl = new TabControl();
            SuspendLayout();
            // 
            // Menu
            // 
            Menu.Location = new Point(2, 0);
            Menu.Name = "Menu";
            Menu.Padding = new Padding(0, 2, 0, 2);
            Menu.Size = new Size(982, 24);
            Menu.TabIndex = 0;
            // 
            // TabControl
            // 
            TabControl.Dock = DockStyle.Fill;
            TabControl.Location = new Point(2, 24);
            TabControl.Multiline = true;
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(982, 436);
            TabControl.TabIndex = 1;
            TabControl.DataContextChanged += TabControl_DataContextChanged;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(984, 461);
            Controls.Add(TabControl);
            Controls.Add(Menu);
            Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Main";
            Padding = new Padding(2, 0, 0, 1);
            StartPosition = FormStartPosition.WindowsDefaultLocation;
            Text = "Form3";
            Load += Form3_Load;
            DataContextChanged += Form3_DataContextChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip Menu;
        private TabControl TabControl;
    }
}