// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    public class XlsxCell : Cell, ICell, IXlsxCell, IEquatable<XlsxCell>
    {
        private object _newValue;

        public XlsxCell(object value) : base(value)
        {
        }

        public XlsxCell(object value, XlsxCellType cellType, string format = null) : base(value)
        {
            CellType = cellType;
            Format = format;
        }

        public static XlsxCell DateTimeCell(object dateTimeValue, string dateTimeFormat = null)
        {
            return new XlsxCell(dateTimeValue, XlsxCellType.DateTime, dateTimeFormat);
        }

        public override object Value
        {
            get
            {
                if (_newValue != null || _value == null)
                    return _value;

                switch (CellType)
                {
                    case XlsxCellType.General:
                        return _newValue = OriginalValue;

                    case XlsxCellType.DateTime:
                        // handle datetime as datetime
                        var code = Convert.GetTypeCode(OriginalValue);
                        if (code == TypeCode.DateTime)
                        {
                            return _newValue = Convert.ToDateTime(OriginalValue);
                        }

                        // handle datetime as decimal (OADate)
                        if (code == TypeCode.Decimal
                            || code == TypeCode.Double
                            || code == TypeCode.Int16
                            || code == TypeCode.Int32
                            || code == TypeCode.Int64
                            || code == TypeCode.UInt16
                            || code == TypeCode.UInt32
                            || code == TypeCode.UInt64)
                        {
                            var dt = Convert.ToDouble(OriginalValue);
                            return _newValue = DateTime.FromOADate(dt);
                        }

                        return _newValue = OriginalValue;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public object OriginalValue => _value;
        public string Format { get; }
        public XlsxCellType CellType { get; }

        public bool Equals(XlsxCell other)
        {
            if (other == null) return false;

            if (OriginalValue == other.OriginalValue
                && Format == other.Format
                && CellType == other.CellType)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as XlsxCell);
        }

        public override int GetHashCode()
        {
            return (OriginalValue?.GetHashCode() ?? 2341234)
                ^ (Format?.GetHashCode() ?? 5234231)
                ^ CellType.GetHashCode()
                ^ 797654;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? "null";
        }
    }
}