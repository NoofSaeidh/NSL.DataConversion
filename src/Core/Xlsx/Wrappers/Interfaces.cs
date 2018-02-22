// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx.Wrappers
{
    public interface ICellWrapper
    {
        string Text { get; }
    }

    public interface IWorksheetWrapper
    {
        ICellWrapper[][] Cells { get; }
        int RowsCount { get; }
        int CellsCount { get; }
    }

    public interface IWorkbookWrapper
    {
        Dictionary<string, IWorksheetWrapper> Worksheets { get; }
    }
}