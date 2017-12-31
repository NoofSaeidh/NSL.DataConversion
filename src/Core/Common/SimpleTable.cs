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
    public class SimpleTable : ITable, IReadOnlyTable, IGenericCellsTable
    {
        private readonly ICell[,] _items;

        public int RowsCount { get; }
        public int ColumnsCount { get; }
        public int Length { get; }

        public SimpleTable(ICell[,] items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
            RowsCount = _items.GetLength(0);
            ColumnsCount = _items.GetLength(1);
            Length = _items.Length;
        }

        public ICell this[int row, int column]
        {
            get => _items[row, column];
            set => _items[row, column] = value;
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return _items.Cast<ICell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        ICell<T> IGenericCellsTable.GetCell<T>(int row, int column)
        {
            if (this[row, column]?.Value is T t)
                return new Cell<T>(t);
            return null;
        }

        public override string ToString() => $"{nameof(ICell)}[{_items.GetLength(0)}, {_items.GetLength(1)}]";
    }
}