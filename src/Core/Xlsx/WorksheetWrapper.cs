// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    public class WorksheetWrapper
    {
        private readonly Worksheet _worksheet;
        private readonly SharedStringTable _sharedTable;
        private int? _cellsCount;
        private int? _rowsCount;
        private CellWrapper[][] _items;

        public WorksheetWrapper(Worksheet worksheet, SharedStringTable sharedStringTable)
        {
            _worksheet = worksheet ?? throw new ArgumentNullException(nameof(worksheet));
            _sharedTable = sharedStringTable ?? throw new ArgumentNullException(nameof(sharedStringTable));
        }

        public int RowsCount => _rowsCount ?? (_rowsCount = _worksheet.Descendants<Row>().Count()).Value;
        public int CellsCount => _cellsCount ?? (_cellsCount = _worksheet.Descendants<Cell>().Count()).Value;

        public CellWrapper[][] Items
        {
            get
            {
                if (_items != null) return _items;

                _items = new CellWrapper[RowsCount][];
                var rows = _worksheet.Descendants<Row>().ToArray();
                for (int i = 0; i < RowsCount; i++)
                {
                    var line = rows[i].Elements<Cell>().ToArray();
                    _items[i] = new CellWrapper[line.Length];
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j].DataType != null && line[j].DataType == CellValues.SharedString)
                        {
                            var sharedid = int.Parse(line[j].CellValue.Text);
                            var sharedstring = _sharedTable.ChildElements[sharedid].InnerText;
                            _items[i][j] = new CellWrapper(line[j], sharedstring);
                        }
                        else
                            _items[i][j] = new CellWrapper(line[j]);
                    }
                }
                return _items;
            }
        }
    }
}