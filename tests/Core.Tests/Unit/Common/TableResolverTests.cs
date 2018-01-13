// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public class TableResolverTests
    {
        [Fact]
        public void ResolveReadOnlyTable_Array_Works()
        {
            // Arrange
            var table = new TableResolver(new MockCellResolver());
            object[,] array =
            {
                { "string", 5 },
                { true, null}
            };
            // Act
            var result = table.ResolveReadOnlyTable(array);
            // Assert
            Assert.Equal(2, result.RowsCount);
            Assert.Equal(2, result.ColumnsCount);
            Assert.Collection(result
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(5, item.Value)
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal(null, item.Value));
        }

        [Fact]
        public void ResolveReadOnlyTable_Enumerable_Works()
        {
            // Arrange
            var table = new TableResolver(new MockCellResolver());
            var list = new List<List<object>>
            {
                new List<object> { "string", 5 },
                new List<object> { true, null}
            };
            // Act
            var result = table.ResolveReadOnlyTable(list);
            // Assert
            Assert.Equal(2, result.RowsCount);
            Assert.Equal(2, result.ColumnsCount);
            Assert.Collection(result
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(5, item.Value)
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal(null, item.Value));
        }

        [Fact]
        public void ResolveModifiableTable_Array_Works()
        {
            // Arrange
            var table = new TableResolver(new MockCellResolver());
            object[,] array =
            {
                { "string", 5 },
                { true, null}
            };
            // Act
            var result = table.ResolveModifiableTable(array);
            // Assert
            Assert.Equal(2, result.RowsCount);
            Assert.Equal(2, result.ColumnsCount);
            Assert.Collection(result
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(5, item.Value)
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal(null, item.Value));
        }

        [Fact]
        public void ResolveModifiableTable_Enumerable_Works()
        {
            // Arrange
            var table = new TableResolver(new MockCellResolver());
            var list = new List<List<object>>
            {
                new List<object> { "string", 5 },
                new List<object> { true, null}
            };
            // Act
            var result = table.ResolveModifiableTable(list);
            // Assert
            Assert.Equal(2, result.RowsCount);
            Assert.Equal(2, result.ColumnsCount);
            Assert.Collection(result
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(5, item.Value)
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal(null, item.Value));
        }
    }
}