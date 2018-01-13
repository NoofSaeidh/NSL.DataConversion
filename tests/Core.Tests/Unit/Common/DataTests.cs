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
    public class DataTests
    {
        // perhaps it is not good to test get and set in one method
        // but i don't want to split it
        [Fact]
        public void Indexer_GetSet_String_Works()
        {
            // Arrange
            var data = new Data();
            var table = new MockTable(new ICell[0, 0]);
            // Act
            data["table"] = table;
            var result = data["table"];
            // Assert
            Assert.Equal(table, result);
        }

        [Fact]
        public void Add_Works()
        {
            // Arrange
            var data = new Data();
            var table = new MockTable(new ICell[0, 0]);
            // Act
            data.Add("table", table);
            // Assert
            Assert.Equal(table, data["table"]);
        }

        [Theory]
        [InlineData(0), InlineData(4)]
        public void Indexer_Get_Int_Works(int index)
        {
            // Arrange
            var data = new Data();
            var table = new MockTable(new ICell[0, 0]);
            var otherTable = new MockTable(new ICell[0, 0]);
            for (int i = 0; i < index + 1; i++)
            {
                if (i == index)
                    data.Add(i.ToString(), table);
                else
                    data.Add(i.ToString(), otherTable);
            }
            // Act
            var result = data[index];
            // Assert
            Assert.Equal(table, result);
        }

        [Theory]
        [InlineData("key", true, "key", "notkey")]
        [InlineData("key", false, "notkey", "anotherNotKey")]
        public void ContainsKey_Works(string keyToCheck, bool expectedResult, params string[] keys)
        {
            // Arrange
            var data = new Data();
            var table = new MockTable(new ICell[0, 0]);
            foreach (var key in keys)
            {
                data.Add(key, table);
            }
            // Act
            var result = data.ContainsKey(keyToCheck);
            // Assert
            Assert.Equal(expectedResult, result);
        }

        //todo: other methods tests
    }
}