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
    public class SimpleTableTests
    {
        [Fact]
        public void Indexer_GetWorks()
        {
            // Arrange
            var table = InitializeTable(2, 2, "a", true, 5, typeof(SimpleTable));
            // Act
            var v1 = table[0, 0].Value;
            var v2 = table[0, 1].Value;
            var v3 = table[1, 0].Value;
            var v4 = table[1, 1].Value;
            // Assert
            Assert.Equal("a", v1);
#pragma warning disable xUnit2004 // Do not use equality check to test for boolean conditions
            Assert.Equal(true, v2);
#pragma warning restore xUnit2004 // Do not use equality check to test for boolean conditions
            Assert.Equal(5, v3);
            Assert.Equal(typeof(SimpleTable), v4);
        }

        [Fact]
        public void Indexer_SetWorks()
        {
            // Arrange
            var table = new SimpleTable(new MockCell[2, 2]);
            // Act
            table[1, 0] = new MockCell("string");
            table[0, 1] = new MockCell(typeof(MockCell));
            // Assert
            Assert.Equal("string", table[1, 0].Value);
            Assert.Equal(typeof(MockCell), table[0, 1].Value);
        }

        [Fact]
        public void GetEnumerator_Works()
        {
            // Arrange
            var table = InitializeTable(2, 2, "string", true, typeof(MockCell), 5);
            // Act
            foreach (ICell cell in table)
            {
                // Assert
                Assert.NotNull(cell);
            }
        }

        [Fact]
        public void IGenericCellsTable_GetCell_ReturnsRightTypeValue()
        {
            // Arrange
            IGenericCellsTable table = InitializeTable(2, 2, "string", true, typeof(MockCell), 5);
            // Act
            var v1 = table.GetCell<string>(0, 0);
            var v2 = table.GetCell<bool>(0, 1);
            var v3 = table.GetCell<Type>(1, 0);
            var v4 = table.GetCell<int>(1, 1);
            // Assert
            Assert.IsAssignableFrom<ICell<string>>(v1);
            Assert.IsAssignableFrom<ICell<bool>>(v2);
            Assert.IsAssignableFrom<ICell<Type>>(v3);
            Assert.IsAssignableFrom<ICell<int>>(v4);
        }

        [Fact]
        public void GenericCellsTable_GetCell_ReturnsNullForWrongTypeValue()
        {
            // Arrange
            var table = InitializeTable(2, 2, "string", true, typeof(MockCell), 5);
            // Act
            table[1, 0] = new MockCell("string");
            table[0, 1] = new MockCell(typeof(MockCell));
            // Assert
            Assert.Equal("string", table[1, 0].Value);
            Assert.Equal(typeof(MockCell), table[0, 1].Value);
        }

        [Theory]
        [InlineData(2, 3), InlineData(4, 5), InlineData(1, 1)]
        public void RowsCount_ColumnsCount_Lenght_CountsRight(int rows, int columns)
        {
            // Arrange
            var array = MockCellConstructor.ToArray(new object[rows, columns]);
            var table = new SimpleTable(array);
            // Act
            var rowsCount = table.RowsCount;
            var columnsCount = table.ColumnsCount;
            var length = table.Length;
            // Assert
            Assert.Equal(rows, rowsCount);
            Assert.Equal(columns, columnsCount);
            Assert.Equal(rows * columns, length);
        }

        private SimpleTable InitializeTable(int dim1, int dim2, params object[] values)
        {
            var cells = new MockCell[dim1, dim2];
            for (int i = 0, k = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2; j++, k++)
                {
                    cells[i, j] = new MockCell(values[k]);
                }
            }
            return new SimpleTable(cells);
        }
    }
}