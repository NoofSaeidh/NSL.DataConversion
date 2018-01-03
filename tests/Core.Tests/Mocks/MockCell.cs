// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Tests.Mocks
{
    public class MockCell : ICell
    {
        public MockCell(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public override string ToString() => Value?.ToString() ?? "null";
    }

    public class MockCell<T> : ICell<T>
    {
        public MockCell(T value)
        {
            Value = value;
        }

        public T Value { get; }
        object ICell.Value => Value;

        public static implicit operator MockCell<T>(T value) => new MockCell<T>(value);
    }
}