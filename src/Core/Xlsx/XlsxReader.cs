// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    //todo: what happens if file changed?
    public class XlsxReader : IDisposable
    {
        private readonly SpreadsheetDocument _document;
        private Dictionary<string, WorksheetWrapper> _worksheets;
        private readonly string _tempFile;
        private bool _disposed;

        public XlsxReader(string path)
        {
            try
            {
                _document = SpreadsheetDocument.Open(path, false);
            }
            catch (Exception ex)
            {
                //todo: add more clear message
                throw new XlsxAccessException($"Cannot read file {path}.", ex);
            }
        }

        protected XlsxReader(string path, bool deleteOnDispose) : this(path)
        {
            _tempFile = deleteOnDispose ? path : null;
        }

        public Dictionary<string, WorksheetWrapper> Worksheets
        {
            get
            {
                if (_worksheets != null) return _worksheets;

                var result = new List<KeyValuePair<string, WorksheetWrapper>>();

                foreach (Sheet sheet in _document.WorkbookPart.Workbook.Sheets)
                {
                    var item = (WorksheetPart)_document.WorkbookPart.GetPartById(sheet.Id);
                    result.Add(new KeyValuePair<string, WorksheetWrapper>(sheet.Name
                        , new WorksheetWrapper(item.Worksheet, _document.WorkbookPart.SharedStringTablePart.SharedStringTable)));
                }

                return _worksheets = new Dictionary<string, WorksheetWrapper>(result);
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _document.Dispose();
            if (_tempFile != null)
            {
                try
                {
                    File.Delete(_tempFile);
                }
#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
                catch
                {
                    // Disposing without exceptions
                }
#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body
            }
            _disposed = true;
        }

        //Open copy insted of real file
        public static XlsxReader OpenCopy(string path)
        {
            var newpath = Path.Combine(Path.GetTempPath()
                , Path.GetTempFileName() + Consts.XlsxExtension);
            try
            {
                File.Copy(path, newpath);
            }
            catch (IOException ex)
            {
                //todo: message
                throw new XlsxAccessException($"Cannot copy file {path}.", ex);
            }
            return new XlsxReader(newpath, true);
        }
    }
}