namespace BerorinApp
{
    public class DataPart
    {
        public string RecipeFile { get; private set; }

        public string MasterFile { get; private set; }

        public string Name { get; private set; }

        public string Binary { get; private set; }

        public DataPart(FileInfo recipe)
        {
            RecipeFile = recipe.FullName;
            Name = recipe.GetFileNameWithoutExtension();
            MasterFile = recipe.Directory.Combine($"{Name}.idn");
            Binary = Setting.Current.Binary;
        }

        public Recipes Recipes { get; private set; }

        public Masters Masters { get; private set; }

        public GridRecords GridRecords { get; private set; }

        public int UndoCount { get; set; }

        public void Initialize()
        {
            LoadRecipes();
            LoadMasters();
            CreateGridRecords();
        }

        private void LoadRecipes()
        {
            Recipes = Service.LoadRecipes(RecipeFile);

            foreach (var recipe in Recipes)
                recipe.ExpandListItems();
        }

        private void LoadMasters()
        {
            Masters = Service.LoadMasters(MasterFile);
        }

        public void CreateGridRecords()
        {
            GridRecords = Service.CreateGridRecords(this);
        }

        public void LoadBinary()
        {
            var pieces = GridRecords.SelectMany(x => x.Pieces);

            using (var stream = new FileInfo(Binary).OpenReadNoLock())
            {
                foreach (var piece in pieces)
                {
                    var bytes = new byte[piece.Recipe.ByteSize];
                    stream.Seek(piece.Address.Int, SeekOrigin.Begin);
                    stream.Read(bytes, 0, bytes.Length);
                    piece.InitializeValue(bytes);
                }
            }
        }
    }
}
