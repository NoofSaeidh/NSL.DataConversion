// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit
{
    public class CellTests
    {
        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        public void Value_ReturnInitializedValue(object value)
        {
            // Arrange
            var cell = new Cell(value);
            // Act
            var result = cell.Value;
            // Assert
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        public void Equals_TrueForEqualValues(object value)
        {
            // Arrange
            var cell1 = new Cell(value);
            var cell2 = new Cell(value);
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
            var cell1 = new Cell(value1);
            var cell2 = new Cell(value2);
            // Act
            var result = cell1.Equals(cell2);
            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        public void GetHashCode_EqualForEqualValues(object value)
        {
            // Arrange
            var cell1 = new Cell(value);
            var cell2 = new Cell(value);
            // Act
            var hash1 = cell1.GetHashCode();
            var hash2 = cell2.GetHashCode();
            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Theory]
        [InlineData(5, 4), InlineData("string", "int"), InlineData(typeof(Cell), false)]
        public void GetHashCode_NotEqualForNotEqualValues(object value1, object value2)
        {
            if (value1 == value2) throw new InvalidOperationException("Inline data for value1 and value2 must not be equal");
            // Arrange
            var cell1 = new Cell(value1);
            var cell2 = new Cell(value2);
            // Act
            var hash1 = cell1.GetHashCode();
            var hash2 = cell2.GetHashCode();
            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        // Arrange

        // Act

        // Assert
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
        public void Value_EqualToBaseClassValue(object value)
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