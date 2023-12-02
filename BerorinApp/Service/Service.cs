namespace BerorinApp
{
    public static class Service
    {
        public static Recipes LoadRecipes(string path)
        {
            var recipes = Util.YamlLoadOrNew<List<Recipe>>(path);

            recipes.ForEach(x => x.RecipeFile = path);

            return new Recipes(recipes);
        }

        private static Dictionary<string, Masters> _cache_masters = new();

        public static Masters LoadMasters(string path)
        {
            var nchHash = new FileInfo(path).GetNoChangeHash();

            if (!_cache_masters.TryGetValue(nchHash, out var masters))
            {
                var list = new List<Master>();

                foreach (var line in File.ReadAllLines(path))
                    list.Add(Master.RestoreByText(line));

                masters = new Masters(list);

                _cache_masters.Add(nchHash, masters);
            }

            return masters;
        }

        public static GridRecords CreateGridRecords(DataPart dataPart)
        {
            var gridRecords = new List<GridRecord>();

            for (int i = 0; i < dataPart.Masters.Count; i++)
            {
                var record = new GridRecord();
                record.Seq = i;
                record.Id.Hex = dataPart.Masters[i].Hex;
                record.Name = dataPart.Masters[i].Name;

                foreach (var recipe in dataPart.Recipes)
                    record.Pieces.Add(new DataPiece(dataPart, recipe, i));

                gridRecords.Add(record);
            }

            return new GridRecords(gridRecords);
        }

        public static Masters ExpandListItems(Recipe recipe)
        {
            var masters = new List<Master>();

            foreach (var line in recipe.ListItems)
            {
                var asitis = Master.RestoreByText(line);

                if (asitis.Hex.Val())
                {
                    masters.Add(asitis);
                }
                else
                {
                    var link = new FileInfo(recipe.RecipeFile).Directory.File(line);

                    if (link.Exists)
                    {
                        var attached = LoadMasters(link.FullName);

                        foreach (var item in attached.Where(x => x.Hex.Val()))
                            masters.Add(item);
                    }
                }
            }

            return new Masters(masters);
        }

        public static DataGridViewColumn GenerateDataGridViewColumn(Recipe recipe)
        {
            DataGridViewColumn column;

            switch (recipe.Disp)
            {
                case DispEnum.List:
                    column = new DataGridViewComboBoxColumn()
                    {
                        DefaultCellStyle = new DataGridViewCellStyle() { Font = new Font("MS UI Gothic", 9.0F) },
                        DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                        DataSource = recipe.TrueListItems,
                        DisplayMember = "Name",
                        ValueMember = "Hex",
                        HeaderText = recipe.Name
                    };
                    break;

                default:
                    column = new DataGridViewTextBoxColumn()
                    {
                        DefaultCellStyle = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleRight },
                        HeaderText = recipe.Name
                    };
                    break;
            }

            column.Tag = recipe;

            return column;
        }
    }
}
