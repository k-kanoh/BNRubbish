using System.ComponentModel;

namespace BerorinApp
{
    public class BaseForm : Form
    {
        private Task _titleMsgEraseTask;
        private bool _titleMsgCancelRequest;

        private string __title;
        private bool __isDirty;
        private bool __reloadReqFlg = true;

        [Category("オリジナル")]
        [Description("この画面に含まれるコントロールの変更を監視します。")]
        public bool DirtyMonitoring { get; set; }

        [Category("表示")]
        [Description("画面のタイトルを設定します。")]
        public string Title
        {
            get => __title;
            set
            {
                __title = value;
                Text = __title;
            }
        }

        protected bool IsDirty
        {
            get => __isDirty;
            set
            {
                __isDirty = value;
                Text = __isDirty ? $"{Title} *" : Title;
            }
        }

        [Browsable(false)]
        [DefaultValue(false)]
        protected bool ReloadRequestFlg
        {
            get
            {
                if (__reloadReqFlg)
                {
                    __reloadReqFlg = false;
                    return true;
                }

                return false;
            }
            set => __reloadReqFlg = value;
        }

        /// <summary>
        /// エラープロバイダ
        /// </summary>
        protected ErrorProvider ErrorProvider { get; set; } = new ErrorProvider() { BlinkStyle = ErrorBlinkStyle.NeverBlink };

        [Browsable(false)]
        public override string Text { get => base.Text; set => base.Text = value; }

        [DefaultValue(typeof(Font), "メイリオ, 9.75pt")]
        public override Font Font { get => base.Font; set => base.Font = value; }

        public BaseForm()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            Font = new Font("メイリオ", 9.75F);
            StartPosition = FormStartPosition.CenterScreen;

            ErrorProvider = new ErrorProvider { BlinkStyle = ErrorBlinkStyle.NeverBlink };
        }

        /// <summary>
        /// ショートカットキーの実装
        /// </summary>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            var key = Util.KeyParse(keyData, out var ctrl, out var alt, out var shift);

            if (ctrl && !alt && !shift)
            {
                switch (key)
                {
                    case Keys.S:
                        PressCtrlS();
                        return true;

                    case Keys.R:    // テキストボックス上で不可?
                        PressCtrlR();
                        return true;

                    case Keys.W:
                        Close();
                        return true;
                }
            }

            if (!ctrl && !alt && !shift)
            {
                switch (key)
                {
                    case Keys.F5:
                        PressF5();
                        return true;
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        protected virtual void PressCtrlS()
        {
        }

        protected virtual void PressCtrlR()
        {
        }

        protected virtual void PressF5()
        {
        }

        /// <summary>
        /// 子画面をモーダレスで出します。
        /// </summary>
        public void ShowModeless<T>(object data) where T : BaseForm, new()
        {
            new T() { Tag = data }.Show();
        }

        public void ActionInvoke(Action action)
        {
            Invoke((MethodInvoker)(() => action.Invoke()));
        }

        protected async Task DispMessageInTitleBarAsync(string msg, int milliseconds = 3000)
        {
            _titleMsgCancelRequest = true;

            if (_titleMsgEraseTask != null)
                await _titleMsgEraseTask;

            _titleMsgCancelRequest = false;

            if (msg != null)
            {
                _titleMsgEraseTask = Task.Run(async () =>
                {
                    ActionInvoke(() => Text = $"{Title}  ({msg})");

                    var eraseTime = DateTime.Now.AddMilliseconds(milliseconds);
                    while (eraseTime > DateTime.Now && !_titleMsgCancelRequest)
                        await Task.Delay(100);

                    ActionInvoke(() => Text = Title);
                });
            }
        }

        /// <summary>
        /// ファイル選択ダイアログを表示します。
        /// </summary>
        public bool PickFileDialog(out FileInfo finfo)
        {
            finfo = null;
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "全てのファイル(*.*)|*.*";

                if (dlg.ShowDialog() != DialogResult.OK)
                    return false;

                finfo = new FileInfo(dlg.FileName);
                return true;
            }
        }

        /// <summary>
        /// フォルダ選択ダイアログを表示します。
        /// </summary>
        public bool PickFolderDialog(out DirectoryInfo dinfo)
        {
            dinfo = null;
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return false;

                dinfo = new DirectoryInfo(dlg.SelectedPath);
                return true;
            }
        }

        /// <summary>
        /// 「OK」だけのメッセージダイアログを表示します。
        /// </summary>
        //public void InformationMessageBox(string msg)
        //{
        //    using (var dlg = new TaskDialog())
        //    {
        //        dlg.Caption = "確認";
        //        dlg.Icon = TaskDialogStandardIcon.Information;
        //        dlg.StandardButtons = TaskDialogStandardButtons.Ok;
        //        dlg.Text = msg;
        //        var res = dlg.Show();
        //    }
        //}

        /// <summary>
        /// 「OK」だけのエラーメッセージボックスを表示します。
        /// </summary>
        //public void ErrorMessageBox(string msg)
        //{
        //    using (var dlg = new TaskDialog())
        //    {
        //        dlg.Caption = "確認";
        //        dlg.Icon = TaskDialogStandardIcon.Error;
        //        dlg.StandardButtons = TaskDialogStandardButtons.Ok;
        //        dlg.Text = msg;
        //        var res = dlg.Show();
        //    }
        //}

        /// <summary>
        /// 「OK」「キャンセル」のメッセージボックスを表示します。
        /// </summary>
        public bool InfoMessageBoxOKCancel(string msg)
        {
            var option = new TaskDialogPage()
            {
                Caption = "確認",
                Icon = TaskDialogIcon.Information,
                Buttons = { TaskDialogButton.OK, TaskDialogButton.Cancel },
                Text = msg
            };

            return TaskDialog.ShowDialog(this, option) == TaskDialogButton.OK;
        }

        /// <summary>
        /// 「はい」「いいえ」「キャンセル」のメッセージボックスを表示します。
        /// </summary>
        //public bool InfoMessageBoxYesNoCancel(string msg, out bool isYes)
        //{
        //    isYes = false;

        //    using (var dlg = new TaskDialog())
        //    {
        //        dlg.Caption = "確認";
        //        dlg.Icon = TaskDialogStandardIcon.Information;
        //        dlg.StandardButtons = TaskDialogStandardButtons.Yes | TaskDialogStandardButtons.No | TaskDialogStandardButtons.Cancel;
        //        dlg.Text = msg;
        //        var res = dlg.Show();

        //        switch (res)
        //        {
        //            case TaskDialogResult.Yes:
        //                isYes = true;
        //                return true;

        //            case TaskDialogResult.No:
        //                return true;

        //            default:
        //                return false;
        //        }
        //    }
        //}
    }
}
