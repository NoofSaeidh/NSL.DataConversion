// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public interface ICell
    {
        object Value { get; }
    }

    public interface ICell<out T> : ICell
    {
        new T Value { get; }
    }

    public interface ITable : IEnumerable<ICell>
    {
        ICell this[int i, int j] { get; set; }
    }

    public interface IReadOnlyTable : IEnumerable<ICell>
    {
        ICell this[int i, int j] { get; }
    }

    public interface IGenericCellsTable : ITable
    {
        ICell<T> GetCell<T>(int i, int j);
    }

    public interface IIntersectionTable : ITable
    {
        IEnumerable<ICell> GetColumn(int index);

        IEnumerable<ICell> GetRow(int index);
    }

    public interface IIntersectionModifiableTable : IIntersectionTable
    {
        void AddColumn(IEnumerable<ICell> column);

        void AddRow(IEnumerable<ICell> row);

        void InsertColumn(int index, IEnumerable<ICell> column);

        void InsertRow(int index, IEnumerable<ICell> row);

        void RemoveColumn(int index);

        void RemoveRow(int index);
    }
}