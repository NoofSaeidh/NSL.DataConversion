// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NSL.DataConversion.Core.Common
{
    public class ModifiableTable : IIntersectionModifiableTable, ITable, IGenericCellsTable
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

        //todo: move from here
        public ModifiableTable(object[,] items, IObjectResolver<ICell> cellResolver = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (cellResolver == null) cellResolver = CellResolver.Instance;

            var imax = items.GetLength(0);
            var jmax = items.GetLength(1);
            _items = new List<List<ICell>>(imax);
            for (int i = 0; i < imax; i++)
            {
                _items[i] = new List<ICell>(jmax);
                for (int j = 0; j < jmax; j++)
                {
                    _items[i][j] = cellResolver.ResolveObject(items[i, j]);
                }
            }
        }

        #endregion

        /// <summary>
        ///     Get or set value of cell on intercetion of specified column and row.
        /// </summary>
        /// <param name="i">Column number.</param>
        /// <param name="j">Row number.</param>
        /// <returns>Cell.</returns>
        public ICell this[int i, int j]
        {
            get => _items[i][j];
            set => _items[i][j] = value;
        }

        public void AddColumn(IEnumerable<ICell> column)
        {
            if (column == null)
                throw new ArgumentNullException(nameof(column));

            _items.Add(column.ToList());
        }

        public void AddRow(IEnumerable<ICell> row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            var rowlist = row as IList<ICell> ?? row.ToArray();
            for (int i = 0; i < rowlist.Count; i++)
            {
                _items[i].Add(rowlist[i]);
            }
        }

        public ICell<T> GetCell<T>(int i, int j)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICell> GetColumn(int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICell> GetRow(int index)
        {
            throw new NotImplementedException();
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}