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
    public class CellResolverTests
    {
        [Fact]
        public void Resolve_CreateCell()
        {
            // Arrange
            var resolver = new CellResolver();
            var value = new object();
            // Act
            var result = resolver.Resolve(value);
            // Assert
            Assert.Equal(value, result.Value);
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
            Assert.IsAssignableFrom<ICell<object>>(result1);
            Assert.IsAssignableFrom<ICell<int>>(result2);
            Assert.IsAssignableFrom<ICell<bool>>(result3);
            Assert.IsAssignableFrom<ICell<string>>(result4);
        }

        [Fact]
        public void ResolveObject_ResolveNullAsCellOfObject()
        {
            // Arrange
            var resolver = new CellResolver();
            // Act
            var result = resolver.ResolveObject(null);
            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
        }

        [Fact]
        public void ResolveObject_ResolveObjectToGenericCellType()
        {
            // Arrange
            var resolver = new CellResolver();
            // Act
            var result1 = resolver.ResolveObject(new object());
            var result2 = resolver.ResolveObject(5);
            var result3 = resolver.ResolveObject(true);
            var result4 = resolver.ResolveObject("string");
            // Assert
            Assert.IsAssignableFrom<ICell<object>>(result1);
            Assert.IsAssignableFrom<ICell<int>>(result2);
            Assert.IsAssignableFrom<ICell<bool>>(result3);
            Assert.IsAssignableFrom<ICell<string>>(result4);
        }

        [Fact]
        public void ResolveGeneric_WorksTheSameAsResolveObject()
        {
            // Arrange
            IGenericResolver<ICell> resolver = new CellResolver();
            // Act
            var result1 = resolver.ResolveGeneric(new object());
            var result2 = resolver.ResolveGeneric(5);
            var result3 = resolver.ResolveGeneric(true);
            var result4 = resolver.ResolveGeneric("string");
            // Assert
            Assert.IsAssignableFrom<ICell<object>>(result1);
            Assert.IsAssignableFrom<ICell<int>>(result2);
            Assert.IsAssignableFrom<ICell<bool>>(result3);
            Assert.IsAssignableFrom<ICell<string>>(result4);
        }

        [Fact]
        public void ResolveToArray_ResolvesWithRightValues()
        {
            // Arrange
            var resolver = new CellResolver();
            object[,] array =
            {
                {"string", 0 },
                {true, typeof(CellResolver) }
            };
            // Act
            var result = resolver.ResolveToArray(array);
            // Assert
            Assert.Collection(result.Cast<ICell>()
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(0, item.Value)
#pragma warning disable xUnit2004 // Do not use equality check to test for boolean conditions
                , item => Assert.Equal(true, item.Value)
#pragma warning restore xUnit2004 // Do not use equality check to test for boolean conditions
                , item => Assert.Equal(typeof(CellResolver), item.Value));
        }

        [Fact]
        public void ResolveToArray_ResolvesWithRightGenericTypes()
        {
            // Arrange
            var resolver = new CellResolver();
            object[,] array =
            {
                {"string", 0 },
                {true, typeof(CellResolver) }
            };
            // Act
            var result = resolver.ResolveToArray(array);
            // Assert
            Assert.Collection(result.Cast<ICell>()
                , item => Assert.IsAssignableFrom<ICell<string>>(item)
                , item => Assert.IsAssignableFrom<ICell<int>>(item)
                , item => Assert.IsAssignableFrom<ICell<bool>>(item)
                , item => Assert.IsAssignableFrom<ICell<Type>>(item));
        }

        [Fact]
        public void ResolveToArray_ResolvesNullAsCellsWithNullValue()
        {
            // Arrange
            var resolver = new CellResolver();
            object[,] array =
            {
                {"string", null },
                {null, typeof(CellResolver) }
            };
            // Act
            var result = resolver.ResolveToArray(array);
            // Assert
            Assert.NotNull(result[1, 0]);
            Assert.NotNull(result[0, 1]);
            Assert.Null(result[1, 0].Value);
            Assert.Null(result[0, 1].Value);
        }

        [Fact]
        public void ResolveToArray_Enumerable_ResolvesWithRightValues()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", 0 },
                new List<object>{true, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToArray(list);
            // Assert
            Assert.Collection(result.Cast<ICell>()
                , item => Assert.Equal("string", item.Value)
                , item => Assert.Equal(0, item.Value)
#pragma warning disable xUnit2004 // Do not use equality check to test for boolean conditions
                , item => Assert.Equal(true, item.Value)
#pragma warning restore xUnit2004 // Do not use equality check to test for boolean conditions
                , item => Assert.Equal(typeof(CellResolver), item.Value));
        }

        [Fact]
        public void ResolveToArray_Enumerable_ResolvesWithRightGenericTypes()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", 0 },
                new List<object>{true, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToArray(list);
            // Assert
            Assert.Collection(result.Cast<ICell>()
                , item => Assert.IsAssignableFrom<ICell<string>>(item)
                , item => Assert.IsAssignableFrom<ICell<int>>(item)
                , item => Assert.IsAssignableFrom<ICell<bool>>(item)
                , item => Assert.IsAssignableFrom<ICell<Type>>(item));
        }

        [Fact]
        public void ResolveToArray_Enumerable_ResolvesNullAsCellsWithNullValue()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", null },
                new List<object>{null, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToArray(list);
            // Assert
            Assert.NotNull(result[1, 0]);
            Assert.NotNull(result[0, 1]);
            Assert.Null(result[1, 0].Value);
            Assert.Null(result[0, 1].Value);
        }

        [Fact]
        public void ResolveToList_ResolvesWithRightValues()
        {
            // Arrange
            var resolver = new CellResolver();
            object[,] array =
            {
                {"string", 0 },
                {true, typeof(CellResolver) }
            };
            // Act
            var result = resolver.ResolveToList(array);
            // Assert
            Assert.Collection(result
                , list => Assert.Collection(list
                    , item => Assert.Equal("string", item.Value)
                    , item => Assert.Equal(0, item.Value))
                , list => Assert.Collection(list
#pragma warning disable xUnit2004 // Do not use equality check to test for boolean conditions
                    , item => Assert.Equal(true, item.Value)
#pragma warning restore xUnit2004 // Do not use equality check to test for boolean conditions
                    , item => Assert.Equal(typeof(CellResolver), item.Value)));
        }

        [Fact]
        public void ResolveToList_ResolvesWithRightGenericTypes()
        {
            // Arrange
            var resolver = new CellResolver();
            object[,] array =
            {
                {"string", 0 },
                {true, typeof(CellResolver) }
            };
            // Act
            var result = resolver.ResolveToList(array);
            // Assert
            Assert.Collection(result
                , list => Assert.Collection(list
                    , item => Assert.IsAssignableFrom<ICell<string>>(item)
                    , item => Assert.IsAssignableFrom<ICell<int>>(item))
                , list => Assert.Collection(list
                    , item => Assert.IsAssignableFrom<ICell<bool>>(item)
                    , item => Assert.IsAssignableFrom<ICell<Type>>(item)));
        }

        [Fact]
        public void ResolveToList_ResolvesNullAsCellsWithNullValue()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", null },
                new List<object>{null, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToList(list);
            // Assert
            Assert.NotNull(result[1][0]);
            Assert.NotNull(result[0][1]);
            Assert.Null(result[1][0].Value);
            Assert.Null(result[0][1].Value);
        }

        [Fact]
        public void ResolveToList_Enumerable_ResolvesWithRightValues()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", 0 },
                new List<object>{true, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToList(list);
            // Assert
            Assert.Collection(result
               , _list => Assert.Collection(_list
                   , item => Assert.Equal("string", item.Value)
                   , item => Assert.Equal(0, item.Value))
               , _list => Assert.Collection(_list
#pragma warning disable xUnit2004 // Do not use equality check to test for boolean conditions
                    , item => Assert.Equal(true, item.Value)
#pragma warning restore xUnit2004 // Do not use equality check to test for boolean conditions
                    , item => Assert.Equal(typeof(CellResolver), item.Value)));
        }

        [Fact]
        public void ResolveToList_Enumerable_ResolvesWithRightGenericTypes()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", 0 },
                new List<object>{true, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToList(list);
            // Assert
            Assert.Collection(result
                , _list => Assert.Collection(_list
                    , item => Assert.IsAssignableFrom<ICell<string>>(item)
                    , item => Assert.IsAssignableFrom<ICell<int>>(item))
                , _list => Assert.Collection(_list
                    , item => Assert.IsAssignableFrom<ICell<bool>>(item)
                    , item => Assert.IsAssignableFrom<ICell<Type>>(item)));
        }

        [Fact]
        public void ResolveToList_Enumerable_ResolvesNullAsCellsWithNullValue()
        {
            // Arrange
            var resolver = new CellResolver();
            IEnumerable<IEnumerable<object>> list = new List<List<object>>
            {
                new List<object>{"string", null },
                new List<object>{null, typeof(CellResolver)}
            };
            // Act
            var result = resolver.ResolveToList(list);
            // Assert
            Assert.NotNull(result[1][0]);
            Assert.NotNull(result[0][1]);
            Assert.Null(result[1][0].Value);
            Assert.Null(result[0][1].Value);
        }
    }
}