// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Excel;
using NSL.DataConversion.Core.Tests.Unit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Excel
{
    public class ExcelCellTests : ICellTests<ExcelCell>
    {
        protected override ExcelCell GetInstance(object value)
        {
            return new ExcelCell(value);
        }

        [Theory]
        [InlineData(214521), InlineData(12344.123412), InlineData(-53623.43123)]
        [InlineData(1231.4231, TypeCode.Decimal), InlineData(34675, TypeCode.UInt32)]
        public void Value_DateTime_ConvertedFromOADate(IConvertible doubleConvertible, TypeCode? type = null)
        {
            // Arrange
            var cell = new ExcelCell(doubleConvertible, ExcelCellType.DateTime);
            var dbl = Convert.ToDouble(doubleConvertible);
            var datetime = DateTime.FromOADate(dbl);
            // Act
            var result = cell.Value;
            // Assert
            Assert.Equal(datetime, result);
        }
    }
}