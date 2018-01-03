// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NSL.DataConversion.Core.Tests.Unit.Common
{
    public class ModifiableTableTests : IModifiableTableTests<ModifiableTable>
    {
        protected override ModifiableTable GetInstance(ICell[,] value) => new ModifiableTable(value);

        [Fact]
        public void Ctor_EmptyArray_Works()
        {
            // Arrange & Act
            var table = new ModifiableTable(MockCellConstructor.ToArray(new object[0, 0]));
            // Assert
            Assert.Equal(0, table.Length);
            Assert.Equal(0, table.RowsCount);
            Assert.Equal(0, table.ColumnsCount);
        }

        [Fact]
        public void Ctor_EmptyList_Works()
        {
            // Arrange & Act
            var table = new ModifiableTable(MockCellConstructor.ToLists(new object[0, 0]));
            // Assert
            Assert.Equal(0, table.Length);
            Assert.Equal(0, table.RowsCount);
            Assert.Equal(0, table.ColumnsCount);
        }
    }

    public class ModifiableTableTests_GenericCells : IGenericCellsTableTests<ModifiableTable>
    {
        protected override ModifiableTable GetInstance(ICell[,] value) => new ModifiableTable(value);
    }
}