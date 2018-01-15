// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Excel
{
    public class ExcelCell : ICell, IEquatable<ExcelCell>
    {
        private object _value;

        public ExcelCell(object value)
        {
            OriginalValue = value;
        }

        public ExcelCell(object value, ExcelCellType cellType, string format = null)
        {
            OriginalValue = value;
            CellType = cellType;
            Format = format;
        }

        public static ExcelCell DateTimeCell(object dateTimeValue, string dateTimeFormat = null)
        {
            return new ExcelCell(dateTimeValue, ExcelCellType.DateTime, dateTimeFormat);
        }

        public virtual object Value
        {
            get
            {
                if (_value != null || OriginalValue == null)
                    return _value;

                switch (CellType)
                {
                    case ExcelCellType.General:
                        return _value = OriginalValue;

                    case ExcelCellType.DateTime:
                        // handle datetime as datetime
                        var code = Convert.GetTypeCode(OriginalValue);
                        if (code == TypeCode.DateTime)
                        {
                            return _value = Convert.ToDateTime(OriginalValue);
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
                            return _value = DateTime.FromOADate(dt);
                        }

                        return _value = OriginalValue;

                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public object OriginalValue { get; }
        public string Format { get; }
        public ExcelCellType CellType { get; }

        public bool Equals(ExcelCell other)
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
            return Equals(obj as ExcelCell);
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