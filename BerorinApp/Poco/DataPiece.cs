namespace BerorinApp
{
    public class DataPiece
    {
        public DataPart Parent { get; }

        public Recipe Recipe { get; }

        public ByteValue Address { get; } = new();

        public List<Undo> UndoHistory { get; } = new();

        private readonly ByteValue __value;

        public int Int
        {
            get => __value.IntLittleEndian;
            set
            {
                var undo = new Undo(++Parent.UndoCount, __value);
                __value.IntLittleEndian = value;
                undo.SetChangedValue(__value);
                UndoHistory.Add(undo);
            }
        }

        public string Hex
        {
            get => __value.Hex;
            set
            {
                var undo = new Undo(++Parent.UndoCount, __value);
                __value.Hex = value;
                undo.SetChangedValue(__value);
                UndoHistory.Add(undo);
            }
        }

        public DataPiece(DataPart dataPart, Recipe recipe, int seq)
        {
            Parent = dataPart;
            Recipe = recipe;
            Address.Int = recipe.Address.Int + (recipe.StepByte ?? recipe.ByteSize) * seq;
            __value = new ByteValue(recipe.ByteSize);
        }

        public void InitializeValue(byte[] bytes)
        {
            __value.Bytes = bytes;
        }
    }
}
