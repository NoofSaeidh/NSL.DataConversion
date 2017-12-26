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
    public class TableTests
    {
        [Fact]
        public void Indexer_Work()
        {
            // Arrange
            var table = InitializeTable(2, 2, "a", true, 5, typeof(Table));
            // Act
            var v1 = table[0, 0].Value;
            var v2 = table[0, 1].Value;
            var v3 = table[1, 0].Value;
            var v4 = table[1, 1].Value;
            // Assert
            Assert.Equal("a", v1);
#pragma warning disable xUnit2004 // Do not use equality check to test for boolean conditions
            Assert.Equal(true, v2);
#pragma warning restore xUnit2004 // Do not use equality check to test for boolean conditions
            Assert.Equal(5, v3);
            Assert.Equal(typeof(Table), v4);
        }

        private Table InitializeTable(int dim1, int dim2, params object[] values)
        {
            var cells = new Cell[dim1, dim2];
            for (int i = 0, k = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2; j++, k++)
                {
                    cells[i, j] = new Cell(values[k]);
                }
            }
            return new Table(cells);
        }
    }
}