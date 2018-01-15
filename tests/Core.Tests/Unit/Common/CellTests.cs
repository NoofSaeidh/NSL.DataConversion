﻿// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public class CellTests : ICellTests<Cell>
    {
        protected override Cell GetInstance(object value)
        {
            return new Cell(value);
        }
    }

    public class CellGenericTests
    {
        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        public void Value_ReturnInitializedValue(object value)
        {
            // Arrange
            var cell = new Cell<object>(value);
            // Act
            var result = cell.Value;
            // Assert
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        public void Value_EqualsToBaseClassValue(object value)
        {
            // Arrange
            var cell = new Cell<object>(value);
            // Act
            var result = cell.Value;
            var baseClassResult = ((Cell)cell).Value;
            // Assert
            Assert.Equal(baseClassResult, result);
        }

        [Fact]
        public void Value_ForStructsWorksTheSameWay()
        {
            // Arrange
            var cell = new Cell<int>(5);
            // Act
            var result = cell.Value;
            // Assert
            Assert.Equal(5, result);
        }

        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        public void Equals_TrueForEqualValues(object value)
        {
            // Arrange
            var cell1 = new Cell<object>(value);
            var cell2 = new Cell<object>(value);
            // Act
            var result = cell1.Equals(cell2);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(5, 4), InlineData("string", "int"), InlineData(typeof(Cell), false)]
        public void Equals_FalseForNotEqualValues(object value1, object value2)
        {
            if (value1 == value2) throw new InvalidOperationException("Inline data for value1 and value2 must not be equal");
            // Arrange
            var cell1 = new Cell<object>(value1);
            var cell2 = new Cell<object>(value2);
            // Act
            var result = cell1.Equals(cell2);
            // Assert
            Assert.False(result);
        }
    }
}