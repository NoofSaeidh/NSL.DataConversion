// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    [Trait("type", "interface")]
    public abstract class IModifiableTableTests<T> : IIntersectionTableTests<T> where T : IModifiableTable
    {
        [Theory]
        [InlineData("string")]
        [InlineData(12, null)]
        [InlineData("string", true, null, 'a', 'b')]
        [Trait("interface", nameof(IModifiableTable))]
        public void AddRow_Works(params object[] values)
        {
            // Arrange
            var empty = MockCellConstructor.ToArray(new object[0, values.Length]);
            IModifiableTable table = GetInstance(empty);
            // Act
            table.AddRow(MockCellConstructor.ToList(values));
            // Assert
            Assert.Equal(1, table.RowsCount);
            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], table[0, i].Value);
            }
        }

        [Theory]
        [InlineData("string")]
        [InlineData(12, null)]
        [InlineData("string", true, null, 'a', 'b')]
        [Trait("interface", nameof(IModifiableTable))]
        public void AddColumn_Works(params object[] values)
        {
            // Arrange
            var empty = MockCellConstructor.ToArray(new object[values.Length, 0]);
            IModifiableTable table = GetInstance(empty);
            // Act
            table.AddColumn(MockCellConstructor.ToList(values));
            // Assert
            Assert.Equal(1, table.ColumnsCount);
            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], table[i, 0].Value);
            }
        }

        [Theory]
        [InlineData(0, 0), InlineData(1, 0), InlineData(0, 1), InlineData(3, 2)]
        [Trait("interface", nameof(IModifiableTable))]
        public void AddRow_ExtendColumnsCount(int initialRows, int initialColumns)
        {
            // Arrange
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(new object[initialRows, initialColumns]));
            var list = MockCellConstructor.ToList(new object[initialColumns + 1]);
            // Act
            table.AddRow(list);
            // Assert
            Assert.Equal(initialColumns + 1, table.ColumnsCount);
        }

        [Theory]
        [InlineData(0, 0), InlineData(1, 0), InlineData(0, 1), InlineData(3, 2)]
        [Trait("interface", nameof(IModifiableTable))]
        public void AddColumn_ExtendRowsCount(int initialRows, int initialColumns)
        {
            // Arrange
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(new object[initialRows, initialColumns]));
            var list = MockCellConstructor.ToList(new object[initialRows + 1]);
            // Act
            table.AddColumn(list);
            // Assert
            Assert.Equal(initialRows + 1, table.RowsCount);
        }
    }
}