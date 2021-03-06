﻿// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core
{
    public class Table : ITable, IReadOnlyTable, IGenericCellsTable
    {
        private readonly ICell[,] _items;

        public Table(ICell[,] items)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public ICell this[int i, int j]
        {
            get => _items[i, j];
            set => _items[i, j] = value;
        }

        public IEnumerator<ICell> GetEnumerator()
        {
            return _items.Cast<ICell>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        ICell<T> IGenericCellsTable.GetCell<T>(int i, int j)
        {
            if (this[i, j]?.Value is T t)
                return new Cell<T>(t);
            return null;
        }
    }
}