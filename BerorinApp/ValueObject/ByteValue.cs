using YamlDotNet.Serialization;

namespace BerorinApp
{
    public class ByteValue
    {
        private ByteObject[] ByteObjects { get; set; } = Array.Empty<ByteObject>();

        public int? Size { get; private set; }

        [YamlIgnore]
        public byte[] Bytes
        {
            get => ByteObjects.Select(x => x.Byte).ToArray();
            set
            {
                if (Size.HasValue)
                {
                    ByteObjects = value.Select(x => new ByteObject() { Byte = x }).Take(Size.Value).ToArray();
                }
                else
                {
                    ByteObjects = value.Select(x => new ByteObject() { Byte = x }).ToArray();
                }
            }
        }

        [YamlIgnore]
        public int IntLittleEndian
        {
            get => Bytes.Select((x, i) => x * (1 << 8 * i)).Sum();
            set
            {
                if (Size.HasValue)
                {
                    Bytes = BitConverter.GetBytes(value).Take(Size.Value).ToArray();
                }
                else
                {
                    Bytes = BitConverter.GetBytes(value).TrimEnd();
                }
            }
        }

        [YamlIgnore]
        public int Int
        {
            get => Bytes.Reverse().Select((x, i) => x * (1 << 8 * i)).Sum();
            set
            {
                if (Size.HasValue)
                {
                    Bytes = BitConverter.GetBytes(value).Take(Size.Value).Reverse().ToArray();
                }
                else
                {
                    Bytes = BitConverter.GetBytes(value).TrimEnd().Reverse().ToArray();
                }
            }
        }

        public string Hex
        {
            get => string.Join("", ByteObjects.Select(x => x.Hex));
            set => Bytes = value.HexToByteArray();
        }

        public ByteValue()
        { }

        public ByteValue(int size)
        {
            Size = size;
        }
    }
}
