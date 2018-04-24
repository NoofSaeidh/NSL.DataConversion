using NSL.DataConversion.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Tests.Mocks
{
    public class MockData : IDataList
    {
        private readonly Dictionary<string, ITable> _items;

        public MockData()
        {
            _items = new Dictionary<string, ITable>();
        }

        public MockData(IDictionary<string, ITable> items)
        {
            _items = new Dictionary<string, ITable>(items);
        }

        public ITable this[string key]
        {
            get => _items[key];
            set => _items[key] = value;
        }
        public ITable this[int index] => _items.Values.ElementAt(index);

        public int Count => _items.Count;

        public void Add(string key, ITable table)
        {
            _items.Add(key, table);
        }

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public IEnumerator<ITable> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out ITable table)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
