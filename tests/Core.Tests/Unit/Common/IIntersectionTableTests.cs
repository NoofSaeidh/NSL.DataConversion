// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public abstract class IIntersectionTableTests<T> : ITableTests<T> where T : IIntersectionTable
    {
        [Fact]
        [Trait("interface", nameof(IIntersectionTable))]
        public virtual void GetColumn_ReturnColumnEnumeration()
        {
            // Arrange
            object[,] array =
            {
                {"bool", null, typeof(IIntersectionTable) },
                { 5, true, 'c' },
            };
            IIntersectionTable table = GetInstance(MockCellConstructor.ToArray(array));
            // Act
            var c0 = table.GetColumn(0);
            var c1 = table.GetColumn(1);
            var c2 = table.GetColumn(2);
            // Assert
            Assert.Collection(c0
                , item => Assert.Equal("bool", item.Value)
                , item => Assert.Equal(5, item.Value));
            Assert.Collection(c1
                , item => Assert.Equal(null, item.Value)
                , item => Assert.Equal(true, item.Value));
            Assert.Collection(c2
                , item => Assert.Equal(typeof(IIntersectionTable), item.Value)
                , item => Assert.Equal('c', item.Value));
        }

        [Fact]
        [Trait("interface", nameof(IIntersectionTable))]
        public virtual void GetRow_ReturnRowEnumeration()
        {
            // Arrange
            object[,] array =
            {
                {"bool", null},
                { typeof(IIntersectionTable), 5 },
                { true, 'c' },
            }; IIntersectionTable table = GetInstance(MockCellConstructor.ToArray(array));
            // Act
            var c0 = table.GetRow(0);
            var c1 = table.GetRow(1);
            var c2 = table.GetRow(2);
            // Assert
            Assert.Collection(c0
                , item => Assert.Equal("bool", item.Value)
                , item => Assert.Equal(null, item.Value));
            Assert.Collection(c1
                , item => Assert.Equal(typeof(IIntersectionTable), item.Value)
                , item => Assert.Equal(5, item.Value));
            Assert.Collection(c2
                , item => Assert.Equal(true, item.Value)
                , item => Assert.Equal('c', item.Value));
        }
    }
}