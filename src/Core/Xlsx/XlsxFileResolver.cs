// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using ClosedXML;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSL.DataConversion.Core.Common;

namespace NSL.DataConversion.Core.Xlsx
{
    public class XlsxFileResolver : IXlsxFileResolver
    {
        public IData Read(string path)
        {
            using (var memoryStream = CopyFileToMemory(path))
            {
                using (var workbook = new XLWorkbook(memoryStream, XLEventTracking.Disabled))
                {
                    return Resolve(workbook);
                }
            }
        }

        public IData Resolve(IXLWorkbook value)
        {
            //todo: size optimization?
            var data = new Data();

            foreach (var worksheet in value.Worksheets)
            {
                var range = worksheet.RangeUsed();
                if (range == null)
                {
                    data.Add(worksheet.Name, new SimpleTable(new IXlsxCell[0, 0]));
                    continue;
                }
                var rowsCount = range.RowCount();
                var columnsCount = range.ColumnCount();
                var xlsxCells = new IXlsxCell[rowsCount, columnsCount];

                for (int i = 0; i < rowsCount; i++)
                {
                    for (int j = 0; j < columnsCount; j++)
                    {
                        var cell = range.Cell(i + 1, j + 1);
                        xlsxCells[i, j] = new XlsxCell(cell.Value, cell.Style.NumberFormat.NumberFormatId, cell.Style.NumberFormat.Format);
                    }
                }

                data.Add(worksheet.Name, new SimpleTable(xlsxCells));
            }


            return data;
        }

        public IXLWorkbook Resolve(IData value)
        {
            var memoryStream = new MemoryStream();
            //var result = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

            throw new NotImplementedException();
        }

        public void Save(string path, IData content)
        {
            using (var document = Resolve(content))
            {
                document.SaveAs(path);
            }
        }

        public bool TryRead(string path, out IData result)
        {
            if (!File.Exists(path))
            {
                result = null;
                return false;
            }
            try
            {
                result = Read(path);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public bool TrySave(string path, IData content)
        {
            //todo: some checks before try catch
            try
            {
                Save(path, content);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //todo: move to helper??
        public static Stream CopyFileToMemory(string path)
        {
            var memoryStream = new MemoryStream();

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                fileStream.CopyTo(memoryStream);
            }

            return memoryStream;
        }
    }
}