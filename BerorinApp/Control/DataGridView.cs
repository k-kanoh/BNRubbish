namespace BerorinApp
{
    public class DataGridView : System.Windows.Forms.DataGridView
    {
        private bool isEditingTextBoxInitialized;
        private bool isEditingComboBoxInitialized;

        public DataGridView()
        {
            TabStop = false;
            ShowCellToolTips = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataMember = null;
            DoubleBuffered = true;
            AutoGenerateColumns = false;
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            if (DataSource is GridRecords gridRecords)
            {
                var recipes = (Recipes)Tag;
                var tei = ColumnCount;

                foreach (var recipe in recipes)
                    Columns.Add(Service.GenerateDataGridViewColumn(recipe));

                foreach (var row in Rows.Cast<DataGridViewRow>())
                {
                    var record = (GridRecord)row.DataBoundItem;

                    for (int i = 0; i < record.Pieces.Count; i++)
                    {
                        var piece = record.Pieces[i];
                        row.Cells[tei + i].Tag = piece;
                        row.Cells[tei + i].Value = piece.Recipe.Disp == DispEnum.Hex ? piece.Hex : piece.Int;
                    }
                }

                if (gridRecords.All(x => !x.Hex.Val()))
                    Columns[0].Visible = false;

                if (gridRecords.All(x => !x.Name.Val()))
                    Columns[1].Visible = false;
            }
        }

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs eArgs)
        {
            if (!isEditingTextBoxInitialized && eArgs.Control is TextBox textBox)
            {
                textBox.KeyPress += (s, e) =>
                {
                    e.Handled = (char.IsControl(e.KeyChar) && e.KeyChar != '\b');
                    base.OnKeyPress(e);
                };

                var menuTemplate = new MenuStripTemplate()
                {
                    new MenuStripTemplate("全て選択", textBox.SelectAll, () => (textBox.TextLength > 0), (Keys.Control | Keys.A)),
                    new MenuStripTemplate("-"),
                    new MenuStripTemplate("切り取り", textBox.Cut, () => (textBox.SelectionLength > 0), (Keys.Control | Keys.X)),
                    new MenuStripTemplate("コピー", textBox.Copy, () => (textBox.SelectionLength > 0), (Keys.Control | Keys.C)),
                    new MenuStripTemplate("貼り付け", textBox.Paste, () => Clipboard.GetText().Val(), (Keys.Control | Keys.V)),
                };

                var menu = new ContextMenuStrip();
                menu.Items.AddRange(menuTemplate.ConvertToStripItem());
                menu.Opened += (s, e) => menuTemplate.OnOpenedMethod(menu.Items);
                menu.Closed += (s, e) => menuTemplate.OnClosedMethod(menu.Items);
                textBox.ContextMenuStrip = menu;

                isEditingTextBoxInitialized = true;
            }

            if (!isEditingComboBoxInitialized && eArgs.Control is DataGridViewComboBoxEditingControl comboBox)
            {
                isEditingComboBoxInitialized = true;
            }

            base.OnEditingControlShowing(eArgs);
        }
    }
}
