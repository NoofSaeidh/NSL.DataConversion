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
    }

    public class ModifiableTableTests_GenericCells : IGenericCellsTableTests<ModifiableTable>
    {
        protected override ModifiableTable GetInstance(ICell[,] value) => new ModifiableTable(value);
    }
}