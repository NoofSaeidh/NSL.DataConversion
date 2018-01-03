// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using System;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public abstract class IGenericCellsTableTests<T> where T : IGenericCellsTable
    {
        [Fact]
        [Trait("interface", nameof(IGenericCellsTable))]
        public virtual void GetCell_ReturnsRightTypeValue()
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
}