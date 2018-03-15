// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenXmlCell = DocumentFormat.OpenXml.Spreadsheet.Cell;
using NSL.DataConversion.Core.Common;

namespace NSL.DataConversion.Core.Xlsx
{
    public class XlsxFileResolver : IXlsxFileResolver
    {
        public IData Read(string path)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    fileStream.CopyTo(memoryStream);
                }
                using (var document = SpreadsheetDocument.Open(memoryStream, false))
                {
                    return Resolve(document);
                }
            }
        }

        public IData Resolve(SpreadsheetDocument value)
        {
            var workbookPart = value.WorkbookPart;
            //todo: size optimization?
            var data = new Data();
            foreach (var worksheetPart in workbookPart.WorksheetParts)
            {
                var table = new ModifiableTable();
                var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                foreach (var r in sheetData.Elements<Row>())
                {
                    var row = new List<ICell>();
                    foreach (var c in r.Elements<OpenXmlCell>())
                    {
                        object cellValue;
                        XlsxCellType cellType = XlsxCellType.General;
                        if (c.DataType != null)
                        {
                            switch ((CellValues)c.DataType)
                            {
                                case CellValues.Boolean:
                                    cellValue = c.CellValue.Text; //todo: to bool
                                    break;

                                case CellValues.Number:
                                    cellValue = c.CellValue.Text; //todo: to int
                                    break;

                                case CellValues.Error:
                                    cellValue = null; //todo: what should be?
                                    break;

                                case CellValues.SharedString:
                                    //todo: error checking
                                    var stringId = Convert.ToInt32(c.InnerText);
                                    cellValue = workbookPart
                                        .SharedStringTablePart
                                        .SharedStringTable
                                        .Elements<SharedStringItem>()
                                        .ElementAt(stringId)
                                        .InnerText;
                                    break;

                                case CellValues.String:
                                    cellValue = c.CellValue.Text;
                                    break;

                                case CellValues.InlineString:
                                    cellValue = c.CellValue.Text;
                                    break;

                                case CellValues.Date:
                                    cellValue = c.CellValue.Text; //todo: to DateTime
                                    cellType = XlsxCellType.DateTime;
                                    break;

                                default:
                                    cellValue = null;
                                    break;
                            }
                        }
                        else
                        {
                            //todo: or not?
                            cellValue = null;
                        }
                        row.Add(new XlsxCell(cellValue, cellType));
                    }
                    table.AddRow(row);
                }
                //todo: sheetname
                //var name = worksheetPart.Worksheet.

                var partRelationshipId = workbookPart.GetIdOfPart(worksheetPart);
                var correspondingSheet = workbookPart.Workbook
                    .Sheets
                    .Cast<Sheet>()
                    .FirstOrDefault(s => s.Id.HasValue
                                      && s.Id.Value == partRelationshipId);

                data.Add(correspondingSheet.Name, table);
            }

            return data;
        }

        public SpreadsheetDocument Resolve(IData value)
        {
            var memoryStream = new MemoryStream();
            var result = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook);

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
            catch (Exception)
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
    }
}