using System.Collections;
using System.ComponentModel;

namespace BerorinApp
{
    public class Masters : IReadOnlyList<Master>, IListSource
    {
        private IList<Master> _masters;

        public Masters(IList<Master> masters)
        {
            _masters = masters;
        }

        public Master this[int index] => _masters[index];

        public int Count => _masters.Count;

        public IEnumerator<Master> GetEnumerator()
        {
            return _masters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _masters.GetEnumerator();
        }

        public bool ContainsListCollection => _masters is IList;

        public IList GetList()
        {
            return (IList)_masters;
        }
    }
}
