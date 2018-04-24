using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace NSL.DataConversion.Core.Tests.Helpers
{
    public static class DataAssert
    {
        // return true if both are null
        public static bool AssertBothNullOrBothNotNull(object expected, object actual)
        {
            if (expected != null)
            {
                Assert.NotNull(actual);
                return false;
            }
            else
            {
                Assert.Null(actual);
                return true;
            }
        }

        public static void AssertIDataEqual(IData expected, IData actual)
        {
            if (DataAssert.AssertBothNullOrBothNotNull(expected, actual)) return;

            Assert.Equal(expected.Count, actual.Count);

            var aggregator = new ExceptionAggregator();


            for (int i = 0; i < actual.Count; i++)
            {
                aggregator.Run(() => DataAssert.AssertITableEqual(expected[i], actual[i]));
            }

            if (aggregator.HasExceptions)
            {
                throw aggregator.ToException();
            }
        }

        public static void AssertITableEqual(ITable expected, ITable actual)
        {
            if (DataAssert.AssertBothNullOrBothNotNull(expected, actual)) return;
            Assert.Equal(expected.Length, actual.Length);
            Assert.Equal(expected.RowsCount, actual.RowsCount);
            Assert.Equal(expected.ColumnsCount, actual.ColumnsCount);

            var aggregator = new ExceptionAggregator();

            for (int i = 0, k = 0; i < actual.RowsCount; i++)
            {
                for (int j = 0; j < actual.ColumnsCount; j++, k++)
                {
                    aggregator.Run(() => DataAssert.AssertICellEqual(expected[i, j], actual[i, j]));
                }
            }

            if (aggregator.HasExceptions) throw aggregator.ToException();
        }

        public static void AssertICellEqual(ICell expected, ICell actual)
        {
            if (DataAssert.AssertBothNullOrBothNotNull(expected, actual)) return;

            Assert.Equal(expected.Value, actual.Value);
        }
    }
}
