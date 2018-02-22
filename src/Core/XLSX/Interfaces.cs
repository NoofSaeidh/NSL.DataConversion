// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Xlsx.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    public interface IXlsxCell : ICell
    {
        string Format { get; }
        XlsxCellType CellType { get; }
        object OriginalValue { get; }
    }

    public interface IXlsxCellResolver : ICellResolver
        , IObjectResolver<IXlsxCell>
        , IResolver<object, IXlsxCell>
        , IResolver<object[,], IXlsxCell[,]>
        , IResolver<IEnumerable<IEnumerable<object>>, IEnumerable<IEnumerable<IXlsxCell>>>
        , IResolver<ICellWrapper, IXlsxCell>
        , IResolver<ICellWrapper[,], IXlsxCell[,]>
    {
        new IXlsxCell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value);

        new IXlsxCell[,] ResolveToArray(object[,] value);

        new IList<IList<IXlsxCell>> ResolveToList(IEnumerable<IEnumerable<object>> value);

        new IList<IList<IXlsxCell>> ResolveToList(object[,] value);
    }
}