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
    //is it unit or integration tests?
    public class CellResolverTests
    {
        [Fact]
        public void Resolve_CreateCell()
        {
            // Arrange
            var resolver = new CellResolver();
            // Act
            var result = resolver.Resolve(new object());
            // Assert
            Assert.IsAssignableFrom<Cell>(result);
        }

        [Fact]
        public void Resolve_CreateGenericCell()
        {
            // Arrange
            var resolver = new CellResolver();
            // Act
            var result1 = resolver.Resolve(new object());
            var result2 = resolver.Resolve(5);
            var result3 = resolver.Resolve(true);
            var result4 = resolver.Resolve("string");
            // Assert
            Assert.IsAssignableFrom<Cell<object>>(result1);
            Assert.IsAssignableFrom<Cell<int>>(result2);
            Assert.IsAssignableFrom<Cell<bool>>(result3);
            Assert.IsAssignableFrom<Cell<string>>(result4);
        }

        [Fact]
        public void Resolve_ResolveNullAsCellOfObject()
        {
            // Arrange
            IResolver<ICell> resolver = new CellResolver();
            // Act
            var result = resolver.Resolve(null);
            // Assert
            Assert.IsAssignableFrom<Cell>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void Resolve_ResolveObjectToGenericCellType()
        {
            // Arrange
            IResolver<ICell> resolver = new CellResolver();
            // Act
            var result1 = resolver.Resolve(new object());
            var result2 = resolver.Resolve(5);
            var result3 = resolver.Resolve(true);
            var result4 = resolver.Resolve("string");
            // Assert
            Assert.IsAssignableFrom<Cell<object>>(result1);
            Assert.IsAssignableFrom<Cell<int>>(result2);
            Assert.IsAssignableFrom<Cell<bool>>(result3);
            Assert.IsAssignableFrom<Cell<string>>(result4);
        }
    }
}