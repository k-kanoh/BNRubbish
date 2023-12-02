using System.ComponentModel;

namespace BerorinApp
{
    public partial class Main : BaseForm
    {
        public new MainViewModel DataContext => (MainViewModel)base.DataContext;

        public Main()
        {
            InitializeComponent();
            base.DataContext = new MainViewModel();
        }

        /// <summary>
        /// 新しいViewModelがセットされた時
        /// </summary>
        private void Form3_DataContextChanged(object sender, EventArgs e)
        {
            DataContext.PropertyChanged += MainView_PropertyChanged;
        }

        /// <summary>
        /// ViewModelのプロパティが変更された時
        /// </summary>
        private void MainView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Data")
                TabControl.DataContext = DataContext.Data;
        }

        /// <summary>
        /// TabControlのDataContextが変更された時
        /// </summary>
        private void TabControl_DataContextChanged(object sender, EventArgs e)
        {
            TabControl.Controls.Clear();

            foreach (var (name, parts) in DataContext.Data)
            {
                var flowPanel = new FlowLayoutPanel()
                {
                    AutoScroll = true,
                    Dock = DockStyle.Fill,
                    Padding = new Padding(5)
                };

                foreach (var part in parts)
                {
                    var button = new Button()
                    {
                        Size = new Size(180, 75),
                        Text = part.Name
                    };

                    button.Click += (s, e) => ShowModeless<PartsView>(part);

                    flowPanel.Controls.Add(button);
                }

                TabControl.Controls.Add(new TabPage() { Text = name, Controls = { flowPanel } });
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            SetupMenuStrip();
        }

        private void SetupMenuStrip()
        {
            var isBinaryPicked = Setting.Current.Binary.Val;

            var menuTemplate = new MenuStripTemplate { "ファイル(&F)", "設定(&S)", "出力(&O)" };
            menuTemplate[0].Add("開く", PickBinary);
            menuTemplate[0].Add("-");
            menuTemplate[0].Add("まとめて上書き保存", canExecute: isBinaryPicked, shortcut: Keys.Control | Keys.Shift | Keys.S);
            menuTemplate[0].Add("まとめてリロード", canExecute: isBinaryPicked, shortcut: Keys.Control | Keys.Shift | Keys.R);
            menuTemplate[0].Add("-");

            foreach (var his in Setting.History.Take(10))
                menuTemplate[0].Add(his.Binary, () => PickBinary(his.Binary));

            menuTemplate[0].Add("-");
            menuTemplate[0].Add("終了", Close);
            menuTemplate[1].Add("レシピフォルダを選択", PickRecipeDir, isBinaryPicked);
            menuTemplate[1].Add("BNE2の設定フォルダを取り込む", ConvertBne2Setting);
            menuTemplate[1].Add("[x]常に手前に表示", () => TopMost = !TopMost, null, () => TopMost);
            menuTemplate[2].Add("全レシピのダンプを出力する", OutputDump, isBinaryPicked);

            Menu.Items.Clear();
            Menu.Items.AddRange(menuTemplate.ConvertToStripItem());
        }

        protected override void PressF5()
        {
            DataContext.GetRecipeList();
        }

        private void PickBinary()
        {
            if (PickFileDialog(out var binary))
                PickBinary(binary.FullName);
        }

        private void PickBinary(string binary)
        {
            Setting.ChangeCurrent(binary);
            base.DataContext = new MainViewModel();
            Title = binary;
            SetupMenuStrip();
        }

        private void PickRecipeDir()
        {
            if (PickFolderDialog(out var dinfo) && !Setting.Current.RecipeFolders.Any(x => dinfo.FullName == x))
                Setting.Current.RecipeFolders.Add(dinfo.FullName);
        }

        private void ConvertBne2Setting()
        {
            if (!PickFolderDialog(out var source))
                return;

            var dest = Util.Desktop.SubDirectory(source.Name);

            if (dest.Exists && !InfoMessageBoxOKCancel($"{dest.FullName}を上書きしますか?"))
                return;

            new BNE2Converter().ConvertBne2SettingFiles(source);

            _ = DispMessageInTitleBarAsync($"{dest}に出力しました", 10000);
        }

        private void OutputDump()
        {
        }
    }
}
