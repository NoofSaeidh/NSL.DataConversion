// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NSL.DataConversion.Core.Common
{
    public class ModifiableTable : IModifiableTable, ITable, IGenericCellsTable
    {
        private readonly List<List<ICell>> _items;
        private int rowsCount;
        private int columnsCount;

        #region Initialization

        public ModifiableTable()
        {
            _items = new List<List<ICell>>();
            rowsCount = 0;
            columnsCount = 0;
        }

        public ModifiableTable(IEnumerable<IEnumerable<ICell>> items)
        {
            _items = items.Select(x => x.ToList()).ToList();
            rowsCount = _items.Count;
            //to fix count:
            ColumnsCount = _items.Count == 0 ? 0 : _items.Max(x => x.Count);
        }

        public ModifiableTable(ICell[,] items)
        {
            //todo: create and use TableResolver?
            var imax = items.GetLength(0);
            var jmax = items.GetLength(1);
            _items = new List<List<ICell>>(imax);
            for (int i = 0; i < imax; i++)
            {
                _items.Add(new List<ICell>(jmax));
                for (int j = 0; j < jmax; j++)
                {
                    _items[i].Add(items[i, j]);
                }
            }
            rowsCount = imax;
            columnsCount = jmax;
        }

        #endregion

        public ICell this[int row, int column]
        {
            get
            {
                CheckRowIndex(row, nameof(row));
                CheckColumnIndex(column, nameof(column));

                return _items[row][column];
            }

            set
            {
                CheckRowIndex(row, nameof(row));
                CheckColumnIndex(column, nameof(column));

                _items[row][column] = value;
            }
        }

        public int RowsCount
        {
            get
            {
                return rowsCount;
            }
            private set
            {
                // increase or decrease size of _items list
                if (value > rowsCount) IncreaseRows(value);
                else if (value < rowsCount) DecreaseRows(value);
            }
        }

        public int ColumnsCount
        {
            get
            {
                return columnsCount;
            }
            private set
            {
                // increase or decrease size of inner lists of _items

                if (value > columnsCount) IncreaseColumns(value);
                else if (value < columnsCount) DecreaseColumns(value);
            }
        }

        public int Length => RowsCount * ColumnsCount;

        public void AddColumn(IEnumerable<ICell> column)
        {
            if (column == null)
            {
                ChangeColumnsBy(+1);
                return;
            }

            var columnlist = column as IList<ICell> ?? column.ToArray();
            IncreaseRows(columnlist.Count);
            for (int i = 0; i < columnlist.Count; i++)
            {
                _items[i].Add(columnlist[i]);
            }
            ChangeColumnsBy(+1);
        }

        public void AddRow(IEnumerable<ICell> row)
        {
            if (row == null)
            {
                ChangeRowsBy(+1);
                EnsureColumnsCount();
                return;
            }

            //todo: ensure in legnth!!
            _items.Add(row.ToList());
            ChangeRowsBy(+1);
            EnsureColumnsCount();
        }

        public IEnumerable<ICell> GetColumn(int index)
        {
            CheckColumnIndex(index, nameof(index));
            return _items.Select(list => list[index]);
        }

        public IEnumerable<ICell> GetRow(int index)
        {
            CheckRowIndex(index, nameof(index));
            return _items[index].Select(x => x);
        }

        public void InsertColumn(int index, IEnumerable<ICell> column)
        {
            CheckColumnIndex(index, nameof(index));
            throw new NotImplementedException();
        }

        public void InsertRow(int index, IEnumerable<ICell> row)
        {
            CheckRowIndex(index, nameof(index));
            throw new NotImplementedException();
        }

        public void RemoveColumn(int index)
        {
            CheckColumnIndex(index, nameof(index));
            throw new NotImplementedException();
        }

        public void RemoveRow(int index)
        {
            CheckRowIndex(index, nameof(index));
            throw new NotImplementedException();
        }

        ICell<T> IGenericCellsTable.GetCell<T>(int row, int column)
        {
            CheckRowIndex(row, nameof(row));
            CheckColumnIndex(column, nameof(column));

            if (this[row, column]?.Value is T t)
                return new Cell<T>(t);
            return null;
        }

        IEnumerator<ICell> IEnumerable<ICell>.GetEnumerator() => _items.SelectMany(x => x).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICell>)this).GetEnumerator();

        public override string ToString() => $"Rows: {RowsCount}, Columns: {ColumnsCount}, Length: {Length}";

        #region Service Methods

        private void ChangeColumnsBy(int count)
        {
            if (count > 0)
                IncreaseColumns(ColumnsCount + count);
            else if (count < 0)
                DecreaseColumns(ColumnsCount + count);
        }

        private void ChangeRowsBy(int count)
        {
            if (count > 0)
                IncreaseRows(RowsCount + count);
            else if (count < 0)
                DecreaseRows(RowsCount + count);
        }

        private void IncreaseColumns(int count)
        {
            foreach (var item in _items)
            {
                if (count > item.Count)
                {
                    var diff = count - item.Count;
                    item.AddRange(Enumerable.Repeat<ICell>(null, diff));
                    continue;
                }
            }
            columnsCount = count;
        }

        private void IncreaseRows(int count)
        {
            if (count > _items.Count)
            {
                var diff = count - _items.Count;
                var range = new List<List<ICell>>(diff);
                for (int i = 0; i < diff; i++)
                {
                    range.Add(new List<ICell>(Enumerable.Repeat<ICell>(null, ColumnsCount)));
                }
                _items.AddRange(range);
            }
            rowsCount = count;
        }

        private void DecreaseColumns(int count)
        {
            foreach (var item in _items)
            {
                if (count < item.Count)
                {
                    var diff = item.Count - count;
                    item.RemoveRange(item.Count - diff, diff);
                }
            }
            columnsCount = count;
        }

        private void DecreaseRows(int count)
        {
            if (count < _items.Count)
            {
                var diff = _items.Count - count;
                _items.RemoveRange(_items.Count - diff, diff);
            }
            rowsCount = count;
        }

        private void EnsureColumnsCount()
        {
            if (_items.Count == 0)
            {
                columnsCount = 0;
                return;
            }

            var columns = _items.Max(x => x.Count);
            foreach (var item in _items)
            {
                if (item.Count < columns)
                {
                    item.AddRange(Enumerable.Repeat<ICell>(null, columns - item.Count));
                }
            }
            columnsCount = columns;
        }

        // If argumentName is null - IndexOutOfRangeException
        // otherwize ArgumentOutOfRangeException
        private void CheckRowIndex(int index, string argumentName = null)
        {
            const string message = "Row index was out of range. Must be non-negative and less than the count of rows.";
            if (index < 0 || index >= RowsCount)
            {
                if (argumentName == null)
                    throw new IndexOutOfRangeException(message);
                throw new ArgumentOutOfRangeException(argumentName, index, message);
            }
        }

        // If argumentName is null - IndexOutOfRangeException
        // otherwize ArgumentOutOfRangeException
        private void CheckColumnIndex(int index, string argumentName = null)
        {
            const string message = "Column index was out of range. Must be non-negative and less than the count of columns.";
            if (index < 0 || index >= ColumnsCount)
            {
                if (argumentName == null)
                    throw new IndexOutOfRangeException(message);
                throw new ArgumentOutOfRangeException(argumentName, index, message);
            }
        }

        #endregion
    }
}