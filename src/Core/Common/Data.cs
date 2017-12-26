// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public class Data : IData, IDataList
    {
        private readonly Dictionary<string, ITable> _items;

        public Data()
        {
            _items = new Dictionary<string, ITable>();
        }

        public Data(Dictionary<string, ITable> items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public ITable this[int index]
        {
            get => _items.ElementAt(index).Value;
            set => _items[_items.ElementAt(index).Key] = value;
        }

        public ITable this[string key]
        {
            get => _items[key];
            set => _items[key] = value;
        }

        public int Count => _items.Count;

        public void Add(string key, ITable table)
        {
            _items.Add(key, table);
        }

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public void RemoveAt(int index)
        {
            _items.Remove(_items.ElementAt(index).Key);
        }

        public bool TryGetValue(string key, out ITable table)
        {
            return _items.TryGetValue(key, out table);
        }

        public IEnumerator<ITable> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}