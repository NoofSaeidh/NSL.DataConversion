// This file is licensed under the MIT license.
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
    public abstract class ICellTests<T> where T : ICell
    {
        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        [Trait("interface", nameof(ICell))]
        public virtual void Value_ReturnInitializedValue(object value)
        {
            // Arrange
            ICell cell = GetInstance(value);
            // Act
            var result = cell.Value;
            // Assert
            Assert.Equal(value, result);
        }

        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        [Trait("interface", nameof(ICell))]
        public virtual void Equals_TrueForEqualValues(object value)
        {
            // Arrange
            ICell cell1 = GetInstance(value);
            ICell cell2 = GetInstance(value);
            // Act
            var result = cell1.Equals(cell2);
            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(5, 4), InlineData("string", "int"), InlineData(typeof(Cell), false)]
        [Trait("interface", nameof(ICell))]
        public virtual void Equals_FalseForNotEqualValues(object value1, object value2)
        {
            if (value1 == value2) throw new InvalidOperationException("Inline data for value1 and value2 must not be equal");
            // Arrange
            ICell cell1 = GetInstance(value1);
            ICell cell2 = GetInstance(value2);
            // Act
            var result = cell1.Equals(cell2);
            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(5), InlineData("string"), InlineData(typeof(Cell))]
        [Trait("interface", nameof(ICell))]
        public virtual void GetHashCode_EqualForEqualValues(object value)
        {
            // Arrange
            ICell cell1 = GetInstance(value);
            ICell cell2 = GetInstance(value);
            // Act
            var hash1 = cell1.GetHashCode();
            var hash2 = cell2.GetHashCode();
            // Assert
            Assert.Equal(hash1, hash2);
        }

        [Theory]
        [InlineData(5, 4), InlineData("string", "int"), InlineData(typeof(Cell), false)]
        [Trait("interface", nameof(ICell))]
        public virtual void GetHashCode_NotEqualForNotEqualValues(object value1, object value2)
        {
            if (value1 == value2) throw new InvalidOperationException("Inline data for value1 and value2 must not be equal");
            // Arrange
            ICell cell1 = GetInstance(value1);
            ICell cell2 = GetInstance(value2);
            // Act
            var hash1 = cell1.GetHashCode();
            var hash2 = cell2.GetHashCode();
            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        protected abstract T GetInstance(object value);
    }
}