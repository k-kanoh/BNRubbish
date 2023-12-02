using System.ComponentModel;

namespace BerorinApp
{
    public class SplitContainer : System.Windows.Forms.SplitContainer
    {
        private double __horizontalDistance = 0.5;
        private double __verticalDistance = 0.5;

        [DefaultValue(0.5)]
        [Category("オリジナル")]
        [Description("水平方向に伸びる分割線の高さを設定します。")]
        public double HorizontalDistance
        {
            get => __horizontalDistance;
            set
            {
                __horizontalDistance = value;

                if (Orientation == Orientation.Horizontal)
                    SplitterDistance = (int)(ParentForm.Height * value);
            }
        }

        [DefaultValue(0.5)]
        [Category("オリジナル")]
        [Description("垂直方向に伸びる分割線の左端からの位置を設定します。")]
        public double VerticalDistance
        {
            get => __verticalDistance;
            set
            {
                __verticalDistance = value;

                if (Orientation == Orientation.Vertical)
                    SplitterDistance = (int)(ParentForm.Width * value);
            }
        }

        /// <summary>
        /// 分割線の縦横を切り替えます。
        /// </summary>
        public void ChangeOrientation()
        {
            if (Orientation == Orientation.Horizontal)
            {
                HorizontalDistance = (double)SplitterDistance / ParentForm.Height;
                Orientation = Orientation.Vertical;
                SplitterDistance = (int)(VerticalDistance * ParentForm.Width);
            }
            else
            {
                VerticalDistance = (double)SplitterDistance / ParentForm.Width;
                Orientation = Orientation.Horizontal;
                SplitterDistance = (int)(HorizontalDistance * ParentForm.Height);
            }
        }
    }
}
