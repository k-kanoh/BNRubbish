namespace BerorinApp
{
    public partial class PartsView : BaseForm
    {
        public new PartsViewViewModel DataContext => (PartsViewViewModel)base.DataContext;

        public PartsView()
        {
            InitializeComponent();
            base.DataContext = new PartsViewViewModel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataContext.Data = (DataPart)Tag;
            DataContext.Data.Initialize();
            DataContext.Data.LoadBinary();

            gv.Tag = DataContext.Data.Recipes;
            gv.DataSource = DataContext.Data.GridRecords;

            SetupMenuStrip();
        }

        private void SetupMenuStrip()
        {
            var menuTemplate = new MenuStripTemplate { "ファイル(&F)", "編集(&E)", "表示(&V)", "設定(&S)" };
            menuTemplate[0].Add("この画面だけ上書き保存", shortcut: Keys.Control | Keys.S);
            menuTemplate[0].Add("この画面だけリロード", shortcut: Keys.Control | Keys.R);
            menuTemplate[0].Add("-");
            menuTemplate[0].Add("まとめて上書き保存", shortcut: Keys.Control | Keys.Shift | Keys.S);
            menuTemplate[0].Add("まとめてリロード", shortcut: Keys.Control | Keys.Shift | Keys.R);
            menuTemplate[0].Add("-");
            menuTemplate[0].Add("閉じる", Close);
            menuTemplate[1].Add("元に戻す", shortcut: Keys.Control | Keys.Z);
            menuTemplate[1].Add("やり直し", shortcut: Keys.Control | Keys.U);
            menuTemplate[1].Add("-");
            menuTemplate[1].Add("列情報の編集");
            menuTemplate[2].Add("16進数で表示", shortcut: Keys.Alt | Keys.D1);
            menuTemplate[2].Add("10進数で表示", shortcut: Keys.Alt | Keys.D2);
            menuTemplate[2].Add("符号付き10進数で表示", shortcut: Keys.Alt | Keys.D3);
            menuTemplate[2].Add("リストで表示", shortcut: Keys.Alt | Keys.D4);
            menuTemplate[2].Add("-");
            menuTemplate[2].Add("分割を縦横切り替え", SplitContainer.ChangeOrientation, shortcut: Keys.F5);
            menuTemplate[3].Add("[x]常に手前に表示", () => TopMost = !TopMost, null, () => TopMost);

            MenuStrip.Items.AddRange(menuTemplate.ConvertToStripItem());
        }
    }
}
