// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Tests.Mocks
{
    public static class MockCellConstructor
    {
        public static MockCell[,] ToArray(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new MockCell[imax, jmax];
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    result[i, j] = new MockCell(value[i, j]);
                }
            }
            return result;
        }

        public static MockCell[] ToArray(params object[] values)
        {
            var result = new MockCell[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = new MockCell(values[i]);
            }
            return result;
        }

        public static List<List<MockCell>> ToLists(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new List<List<MockCell>>(imax);
            for (int i = 0; i < imax; i++)
            {
                result.Add(new List<MockCell>(jmax));
                for (int j = 0; j < jmax; j++)
                {
                    result[i].Add(new MockCell(value[i, j]));
                }
            }
            return result;
        }

        public static List<MockCell> ToList(params object[] values)
        {
            return values.Select(x => new MockCell(x)).ToList();
        }
    }
}