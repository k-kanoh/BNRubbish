using System.Collections;
using System.ComponentModel;

namespace BerorinApp
{
    public class GridRecords : IReadOnlyList<GridRecord>, IListSource
    {
        private IList<GridRecord> _gridRecords;

        public GridRecords(IList<GridRecord> gridRecords)
        {
            _gridRecords = gridRecords;
        }

        public GridRecord this[int index] => _gridRecords[index];

        public int Count => _gridRecords.Count;

        public IEnumerator<GridRecord> GetEnumerator()
        {
            return _gridRecords.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _gridRecords.GetEnumerator();
        }

        public bool ContainsListCollection => _gridRecords is IList;

        public IList GetList()
        {
            return (IList)_gridRecords;
        }
    }
}
