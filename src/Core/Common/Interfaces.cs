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

    public interface ITable : IReadOnlyTable
    {
        /// <summary>
        ///     Get or set value of cell on intercetion of specified column and row.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns>Cell.</returns>
        new ICell this[int row, int column] { get; set; }
    }

    public interface IReadOnlyTable : IEnumerable<ICell>
    {
        /// <summary>
        ///     Get value of cell on intercetion of specified column and row.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns>Cell.</returns>
        ICell this[int row, int column] { get; }

        int RowsCount { get; }
        int ColumnsCount { get; }
        int Length { get; }
    }

    public interface IGenericCellsTable : ITable
    {
        ICell<T> GetCell<T>(int row, int column);
    }

    public interface IIntersectionTable : ITable
    {
        IEnumerable<ICell> GetColumn(int index);

        IEnumerable<ICell> GetRow(int index);
    }

    public interface IModifiableTable : IIntersectionTable
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
        new ITable this[string key] { get; set; }

        void Add(string key, ITable table);

        bool Remove(string key);

        void RemoveAt(int index);

        bool ContainsKey(string key);

        bool TryGetValue(string key, out ITable table);
    }

    #endregion

    #region IResolver

    public interface IResolver<TIn, TOut>
    {
        TOut Resolve(TIn value);
    }

    public interface IObjectResolver<T>
    {
        T ResolveObject(object value);
    }

    public interface IGenericResolver<T>
    {
        T ResolveGeneric<U>(U value);
    }

    public interface ICellResolver : IObjectResolver<ICell>
        , IGenericResolver<ICell>
        , IResolver<object, ICell>
        , IResolver<object[,], ICell[,]>
        , IResolver<IEnumerable<IEnumerable<object>>, ICell[,]>
        , IResolver<object[,], IList<IList<ICell>>>
        , IResolver<IEnumerable<IEnumerable<object>>, IList<IList<ICell>>>
    {
        ICell<T> Resolve<T>(T value);

        ICell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value);

        ICell[,] ResolveToArray(object[,] value);

        IList<IList<ICell>> ResolveToList(IEnumerable<IEnumerable<object>> value);

        IList<IList<ICell>> ResolveToList(object[,] value);
    }

    public interface ITableResolver : IResolver<object[,], ITable>, IResolver<IEnumerable<IEnumerable<object>>, ITable>
        , IResolver<object[,], IModifiableTable>, IResolver<IEnumerable<IEnumerable<object>>, IModifiableTable>
    {
        IModifiableTable ResolveModifiableTable(IEnumerable<IEnumerable<object>> value);

        IModifiableTable ResolveModifiableTable(object[,] value);

        ITable ResolveTable(IEnumerable<IEnumerable<object>> value);

        ITable ResolveTable(object[,] value);
    }

    #endregion
}