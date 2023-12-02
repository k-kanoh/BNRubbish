namespace BerorinApp
{
    public class GridRecord
    {
        public int Seq { get; set; }

        public ByteValue Id { get; set; } = new();

        public string Hex => Id.Hex;

        public string Name { get; set; }

        public List<DataPiece> Pieces { get; } = new();
    }
}
