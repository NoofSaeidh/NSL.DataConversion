// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Tests.Mocks
{
    public class MockTable : ITable
    {
        private readonly ICell[,] _items;

        public MockTable(ICell[,] items)
        {
            _items = items;
            RowsCount = _items.GetLength(0);
            ColumnsCount = _items.GetLength(1);
            Length = _items.Length;
        }

        public ICell this[int row, int column]
        {
            get
            {
                return _items[row, column];
            }
            set
            {
                _items[row, column] = value;
            }
        }

        public int RowsCount { get; }
        public int ColumnsCount { get; }
        public int Length { get; }

        public IEnumerator<ICell> GetEnumerator()
        {
            return _items.Cast<ICell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}