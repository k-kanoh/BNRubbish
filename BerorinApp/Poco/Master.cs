using System.Text.RegularExpressions;

namespace BerorinApp
{
    public partial class Master
    {
        public int? Seq { get; set; }

        public string Name { get; set; }

        private ByteValue __hex = new();

        public string Hex
        {
            get => __hex.Hex;
            set => __hex.Hex = value;
        }

        public string DispName => $"{Hex} {Name}";

        public int? Order { get; set; }

        public int? Group { get; set; }

        public override string ToString()
        {
            var text = string.Join(" ", new[] { Hex, Name });

            if (Order.HasValue || Group.HasValue)
                text = text.Trim() + $" {Order}|{Group}";

            return text.Trim();
        }

        [GeneratedRegex(@"^([0-9A-F]+)(?:\s+|$)")]
        private static partial Regex ForwardMatch();

        [GeneratedRegex(@"([0-9]*)\|([0-9]*)$")]
        private static partial Regex BackwardMatch();

        public static Master RestoreByText(string text)
        {
            var instance = new Master();

            var matchResult = BackwardMatch().Match(text);

            if (matchResult.Success)
            {
                instance.Order = matchResult.Groups[1].Value.ToInt();
                instance.Group = matchResult.Groups[2].Value.ToInt();
                text = BackwardMatch().Replace(text, "");
            }

            matchResult = ForwardMatch().Match(text);

            if (matchResult.Success)
            {
                instance.Hex = matchResult.Groups[1].Value;
                text = ForwardMatch().Replace(text, "");
            }

            instance.Name = text.Trim();

            return instance;
        }
    }
}
