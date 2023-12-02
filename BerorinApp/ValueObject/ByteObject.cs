using YamlDotNet.Serialization;

namespace BerorinApp
{
    public class ByteObject
    {
        [YamlIgnore]
        public byte Byte { get; set; }

        [YamlIgnore]
        public int Int
        {
            get => Byte;
            set => Byte = (byte)value;
        }

        public string Hex
        {
            get => Byte.ToHex();
            set => Byte = value.HexToByte();
        }
    }
}
