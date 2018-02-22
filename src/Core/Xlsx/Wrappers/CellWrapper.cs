// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx.Wrappers
{
    public class CellWrapper : ICellWrapper
    {
        private readonly Cell _cell;
        private readonly string _sharedString;

        public CellWrapper(Cell cell)
        {
            _cell = cell ?? throw new ArgumentNullException(nameof(cell));
        }

        public CellWrapper(Cell cell, string sharedString) : this(cell)
        {
            _sharedString = sharedString;
        }

        public string Text => _sharedString ?? _cell.CellValue?.Text;

        public override string ToString() => Text ?? "";
    }
}