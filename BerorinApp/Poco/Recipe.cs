using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace BerorinApp
{
    public class Recipe
    {
        public string RecipeFile { get; set; }

        public ByteValue Address { get; set; }

        public string Name { get; set; }

        public int ByteSize { get; set; }

        public int BitPos { get; set; }

        public int? StepByte { get; set; }

        public int StepBit { get; set; }

        public int? Row { get; set; }

        public DispEnum Disp { get; set; }

        public int? Min { get; set; }

        public int? Max { get; set; }

        public ByteObject Filter { get; set; }

        public string[] ListItems { get; set; }

        public int? ColWidth { get; set; }

        [YamlMember(ScalarStyle = ScalarStyle.Literal)]
        public string Memo { get; set; }

        public Masters TrueListItems { get; private set; }

        public void ExpandListItems()
        {
            if (ListItems != null)
                TrueListItems = Service.ExpandListItems(this);
        }
    }
}
