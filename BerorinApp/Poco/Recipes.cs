using System.Collections;

namespace BerorinApp
{
    public class Recipes : IReadOnlyList<Recipe>
    {
        private IList<Recipe> _recipes;

        public Recipes(IList<Recipe> recipes)
        {
            _recipes = recipes;
        }

        public Recipe this[int index] => _recipes[index];

        public int Count => _recipes.Count;

        public IEnumerator<Recipe> GetEnumerator()
        {
            return _recipes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _recipes.GetEnumerator();
        }
    }
}
