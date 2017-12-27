// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    #region ICell

    public interface ICell
    {
        object Value { get; }
    }

    public interface ICell<out T> : ICell
    {
        new T Value { get; }
    }

    #endregion

    #region ITable

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

    #endregion

    #region IData

    public interface IData : IEnumerable<ITable>
    {
        ITable this[int index] { get; }
        ITable this[string index] { get; }
        int Count { get; }
    }

    public interface IDataList : IData
    {
        new ITable this[int index] { get; set; }
        new ITable this[string key] { get; set; }

        void Add(string key, ITable table);

        bool Remove(string key);

        void RemoveAt(int index);

        bool ContainsKey(string key);

        bool TryGetValue(string key, out ITable table);
    }

    #endregion

    #region IResolver

    public interface IResolver<T>
    {
        T Resolve(object value);
    }

    public interface IGenericResolver<T>
    {
        T Resolve<U>(U value);
    }

    #endregion
}