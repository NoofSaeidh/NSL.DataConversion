// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.XLSX
{
    public interface IXLSXCell : ICell
    {
        string Format { get; }
        XLSXCellType CellType { get; }
        object OriginalValue { get; }
    }

    public interface IXLSXCellResolver : ICellResolver
        , IObjectResolver<IXLSXCell>
        , IResolver<object, IXLSXCell>
        , IResolver<object[,], IXLSXCell[,]>
        , IResolver<IEnumerable<IEnumerable<object>>, IEnumerable<IEnumerable<IXLSXCell>>>
    {
        new IXLSXCell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value);

        new IXLSXCell[,] ResolveToArray(object[,] value);

        new IList<IList<IXLSXCell>> ResolveToList(IEnumerable<IEnumerable<object>> value);

        new IList<IList<IXLSXCell>> ResolveToList(object[,] value);
    }
}