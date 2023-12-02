namespace BerorinApp
{
    public class IniConverter
    {
        [CsvField(1)]
        public string Address { get; set; }

        [CsvField(2)]
        public string Name { get; set; }

        [CsvField(3)]
        public string Size { get; set; }

        [CsvField(4)]
        public string Step { get; set; }

        [CsvField(5)]
        public string Row { get; set; }

        [CsvField(6)]
        public string Disp { get; set; }

        [CsvField(7)]
        public string Filter { get; set; }

        [CsvField(8)]
        public string ColWidth { get; set; }

        [CsvField(9)]
        public string Memo { get; set; }

        [CsvField(10)]
        public string Items { get; set; }

        public Recipe ConvertRecipe()
        {
            var recipe = new Recipe();

            recipe.Address.Hex = Address;
            recipe.Name = Name;

            if (Size.StartsWith("."))
            {
                recipe.BitPos = Size.Substring(1).ToInt() ?? throw new FormatException($"{Size}が不正です。");
            }
            else
            {
                recipe.ByteSize = Size.ToInt() ?? 1;
            }

            if (Step.StartsWith("."))
            {
                recipe.StepBit = Size.Substring(1).ToInt() ?? throw new FormatException($"{Step}が不正です。");
            }
            else if (Step.Val())
            {
                recipe.StepByte = Step.HexToInt();
            }

            recipe.Row = Row.ToInt();

            switch (Disp)
            {
                case "16":
                    recipe.Disp = DispEnum.Hex;
                    break;

                case "10":
                    recipe.Disp = DispEnum.Dec;
                    break;

                case "-10":
                    recipe.Disp = DispEnum.SDec;
                    break;

                default:
                    recipe.Disp = DispEnum.List;
                    break;
            }

            if (Filter.StartsWith("."))
            {
                recipe.Filter.Hex = Filter.Substring(1);
            }
            else if (Filter.Match(@"^([0-9A-Fa-f]+)\|([0-9A-Fa-f]+)$", out var min, out var max))
            {
                recipe.Min = min.HexToInt();
                recipe.Max = max.HexToInt();
            }

            recipe.Memo = Memo.Replace("\\n", "\r\n");

            if (Items.Val())
                recipe.ListItems = Items.Split('|').Select(x => x.Trim()).ToArray();

            return recipe;
        }
    }
}
