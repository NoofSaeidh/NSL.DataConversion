// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public abstract class IModifiableTableTests<T> : IIntersectionTableTests<T> where T : IModifiableTable
    {
        [Theory]
        [InlineData("string")]
        [InlineData(12, null)]
        [InlineData("string", true, null, 'a', 'b')]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void AddRow_Works(params object[] values)
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
        public virtual void AddColumn_Works(params object[] values)
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
        public virtual void AddRow_ExtendColumnsCount(int initialRows, int initialColumns)
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
        public virtual void AddColumn_ExtendRowsCount(int initialRows, int initialColumns)
        {
            // Arrange
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(new object[initialRows, initialColumns]));
            var list = MockCellConstructor.ToList(new object[initialRows + 1]);
            // Act
            table.AddColumn(list);
            // Assert
            Assert.Equal(initialRows + 1, table.RowsCount);
        }

        [Fact]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void GetRow_Works()
        {
            // Arrange
            object[,] array =
            {
                { null, null, null, null },
                { "string", true, null, typeof(IModifiableTable) },
                { null, null, null, null },
                { null, null, null, null },
            };
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(array));
            // Act
            var result = table.GetRow(1);
            // Assert
            Assert.Collection(result
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal(null, item.Value)
                , item => Assert.Equal(typeof(IModifiableTable), item.Value));
        }

        [Fact]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void GetColumn_Works()
        {
            // Arrange
            object[,] array =
            {
                { null, "string", null, null },
                { null, true, null, null },
                { null, null, null, null },
                { null, typeof(IModifiableTable), null, null },
            };
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(array));
            // Act
            var result = table.GetColumn(1);
            // Assert
            Assert.Collection(result
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal(null, item.Value)
                , item => Assert.Equal(typeof(IModifiableTable), item.Value));
        }

        [Fact]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void RemoveRow_Works()
        {
            // Arrange
            object[,] array =
            {
                { null, null, null, null },
                { "string", true, 5, typeof(IModifiableTable) },
                { null, null, null, null },
                { null, null, null, null },
            };
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(array));
            // Act
            table.RemoveRow(1);
            var row = table.GetRow(1);
            // Assert
            Assert.Equal(3, table.RowsCount);
            Assert.Collection(row
                , item => Assert.NotEqual("string", item.Value)
                , item => Assert.NotEqual(true, item.Value)
                , item => Assert.NotEqual(5, item.Value)
                , item => Assert.NotEqual(typeof(IModifiableTable), item.Value));
        }

        [Fact]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void RemoveColumn_Works()
        {
            // Arrange
            object[,] array =
            {
                { null, "string", null, null },
                { null, true, null, null },
                { null, 5, null, null },
                { null, typeof(IModifiableTable), null, null },
            };
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(array));
            // Act
            table.RemoveColumn(1);
            var column = table.GetColumn(1);
            // Assert
            Assert.Equal(3, table.ColumnsCount);
            Assert.Collection(column
               , item => Assert.NotEqual("string", item.Value)
               , item => Assert.NotEqual(true, item.Value)
               , item => Assert.NotEqual(5, item.Value)
               , item => Assert.NotEqual(typeof(IModifiableTable), item.Value));
        }

        [Theory]
        [InlineData("string")]
        [InlineData(12, null)]
        [InlineData("string", true, null, 'a', 'b')]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void InsertRow_Works(params object[] values)
        {
            // Arrange
            var empty = MockCellConstructor.ToArray(new object[4, values.Length]);
            IModifiableTable table = GetInstance(empty);
            // Act
            table.InsertRow(1, MockCellConstructor.ToList(values));
            // Assert
            Assert.Equal(5, table.RowsCount);
            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], table[1, i].Value);
            }
        }

        [Theory]
        [InlineData("string")]
        [InlineData(12, null)]
        [InlineData("string", true, null, 'a', 'b')]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void InsertColumn_Works(params object[] values)
        {
            // Arrange
            var empty = MockCellConstructor.ToArray(new object[values.Length, 4]);
            IModifiableTable table = GetInstance(empty);
            // Act
            table.InsertColumn(1, MockCellConstructor.ToList(values));
            // Assert
            Assert.Equal(5, table.ColumnsCount);
            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], table[i, 1].Value);
            }
        }

        [Theory]
        [InlineData(1, 1), InlineData(3, 2)]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void InsertRow_ExtendColumnsCount(int initialRows, int initialColumns)
        {
            // Arrange
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(new object[initialRows, initialColumns]));
            var list = MockCellConstructor.ToList(new object[initialColumns + 1]);
            // Act
            table.InsertRow(initialRows - 1, list);
            // Assert
            Assert.Equal(initialColumns + 1, table.ColumnsCount);
        }

        [Theory]
        [InlineData(1, 1), InlineData(3, 2)]
        [Trait("interface", nameof(IModifiableTable))]
        public virtual void InsertColumn_ExtendRowsCount(int initialRows, int initialColumns)
        {
            // Arrange
            IModifiableTable table = GetInstance(MockCellConstructor.ToArray(new object[initialRows, initialColumns]));
            var list = MockCellConstructor.ToList(new object[initialRows + 1]);
            // Act
            table.InsertColumn(initialColumns - 1, list);
            // Assert
            Assert.Equal(initialRows + 1, table.RowsCount);
        }
    }
}