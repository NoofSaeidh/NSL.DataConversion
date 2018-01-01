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

        #region Initialization

        public ModifiableTable()
        {
            _items = new List<List<ICell>>();
        }

        public ModifiableTable(IEnumerable<IEnumerable<ICell>> items)
        {
            _items = items.Select(x => x.ToList()).ToList();
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
        }

        #endregion

        public ICell this[int row, int column]
        {
            get => _items[row][column];
            set => _items[row][column] = value;
        }

        //todo: need ensure that table always is rectange

        public int RowsCount => _items.Count;
        public int ColumnsCount => _items.FirstOrDefault()?.Count ?? 0;
        public int Length => RowsCount * ColumnsCount;

        public void AddColumn(IEnumerable<ICell> column)
        {
            if (column == null)
                throw new ArgumentNullException(nameof(column));

            var columnlist = column as IList<ICell> ?? column.ToArray();
            for (int i = 0; i < columnlist.Count; i++)
            {
                _items[i].Add(columnlist[i]);
            }
        }

        public void AddRow(IEnumerable<ICell> row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            //todo: ensure in legnth!!
            _items.Add(row.ToList());
        }

        public IEnumerable<ICell> GetColumn(int index)
        {
            return _items.Select(list => list[index]);
        }

        public IEnumerable<ICell> GetRow(int index)
        {
            return _items[index].Select(x => x);
        }

        public void InsertColumn(int index, IEnumerable<ICell> column)
        {
            throw new NotImplementedException();
        }

        public void InsertRow(int index, IEnumerable<ICell> row)
        {
            throw new NotImplementedException();
        }

        public void RemoveColumn(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveRow(int index)
        {
            throw new NotImplementedException();
        }

        ICell<T> IGenericCellsTable.GetCell<T>(int row, int column)
        {
            if (this[row, column]?.Value is T t)
                return new Cell<T>(t);
            return null;
        }

        IEnumerator<ICell> IEnumerable<ICell>.GetEnumerator() => _items.SelectMany(x => x).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ICell>)this).GetEnumerator();
    }
}