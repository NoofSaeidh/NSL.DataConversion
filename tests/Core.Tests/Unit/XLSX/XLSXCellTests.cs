﻿// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.XLSX;
using NSL.DataConversion.Core.Tests.Unit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.XLSX
{
    public class XLSXCellTests : ICellTests<XLSXCell>
    {
        protected override XLSXCell GetInstance(object value)
        {
            return new XLSXCell(value);
        }

        [Theory]
        [InlineData(214521), InlineData(12344.123412), InlineData(-53623.43123)]
        [InlineData(1231.4231, TypeCode.Decimal), InlineData(34675, TypeCode.UInt32)]
        public void Value_DateTime_ConvertedFromOADate(IConvertible input, TypeCode? type = null)
        {
            // Arrange
            var dbl = Convert.ToDouble(input);
            var datetime = DateTime.FromOADate(dbl);

            var value = type == null ? input : Convert.ChangeType(input, type.Value);
            var cell = new XLSXCell(value, XLSXCellType.DateTime);

            // Act
            var result = cell.Value;
            // Assert
            Assert.Equal(datetime, result);
        }
    }
}