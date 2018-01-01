﻿// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public abstract class ITableTests<T> where T : ITable
    {
        [Fact]
        public virtual void Indexer_GetWorks()
        {
            // Arrange
            ITable table = GetInstance(MockCellConstructor.ToArray(new object[,]
            {
                {"string", true },
                {5, typeof(SimpleTable) }
            }));
            // Act
            var v1 = table[0, 0].Value;
            var v2 = table[0, 1].Value;
            var v3 = table[1, 0].Value;
            var v4 = table[1, 1].Value;
            // Assert
            Assert.Equal("string", v1);
            Assert.Equal(true, v2);
            Assert.Equal(5, v3);
            Assert.Equal(typeof(SimpleTable), v4);
        }

        [Fact]
        public virtual void Indexer_SetWorks()
        {
            // Arrange
            ITable table = GetInstance(new MockCell[2, 2]);
            // Act
            table[1, 0] = new MockCell("string");
            table[0, 1] = new MockCell(typeof(MockCell));
            // Assert
            Assert.Equal("string", table[1, 0].Value);
            Assert.Equal(typeof(MockCell), table[0, 1].Value);
        }

        [Fact]
        public virtual void GetEnumerator_Works()
        {
            // Arrange
            ITable table = GetInstance(MockCellConstructor.ToArray(new object[,]
            {
                {"string", true },
                {typeof(MockCell), 5 }
            }));
            // Act
            foreach (ICell cell in table)
            {
                // Assert
                Assert.NotNull(cell);
            }
        }

        [Theory]
        [InlineData(2, 3), InlineData(4, 5), InlineData(1, 1)]
        public virtual void RowsCount_ColumnsCount_Lenght_CountsRight(int rows, int columns)
        {
            // Arrange
            var array = MockCellConstructor.ToArray(new object[rows, columns]);
            ITable table = GetInstance(array);
            // Act
            var rowsCount = table.RowsCount;
            var columnsCount = table.ColumnsCount;
            var length = table.Length;
            // Assert
            Assert.Equal(rows, rowsCount);
            Assert.Equal(columns, columnsCount);
            Assert.Equal(rows * columns, length);
        }

        protected abstract T GetInstance(ICell[,] value);
    }

    public abstract class IGenericCellsTableTests<T> where T : IGenericCellsTable
    {
        [Fact]
        public virtual void IGenericCellsTable_GetCell_ReturnsRightTypeValue()
        {
            // Arrange
            IGenericCellsTable table = GetInstance(MockCellConstructor.ToArray(new object[,]
            {
                {"string", true },
                {typeof(MockCell), 5 }
            }));
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

        protected abstract T GetInstance(ICell[,] value);
    }

    public abstract class IIntersectionTableTests<T> : ITableTests<T> where T : IIntersectionTable
    {
    }

    public abstract class IModifiableTableTests<T> : ITableTests<T> where T : IModifiableTable
    {
    }
}