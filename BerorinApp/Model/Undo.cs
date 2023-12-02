namespace BerorinApp
{
    public class Undo
    {
        public int Seq { get; }

        public ByteValue Before { get; } = new ByteValue();

        public ByteValue After { get; } = new ByteValue();

        public Undo(int seq, ByteValue value)
        {
            Seq = seq;
            Before.Hex = value.Hex;
        }

        public void SetChangedValue(ByteValue value)
        {
            After.Hex = value.Hex;
        }
    }
}
