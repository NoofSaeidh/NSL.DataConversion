// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public class Cell : ICell, ICell<object>, IEquatable<Cell>
    {
        public Cell(object value)
        {
            Value = value;
        }

        public object Value { get; }

        public bool Equals(Cell other)
        {
            if (other == null) return false;

            if (Value == other?.Value)
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        public override int GetHashCode()
        {
            return (Value?.GetHashCode() ?? 7389126) ^ 1238764;
        }

        public override string ToString()
        {
            return Value?.ToString();
        }
    }

    public class Cell<T> : Cell, ICell<T>, IEquatable<Cell<T>>
    {
        public Cell(T value) : base(value)
        {
        }

        new public T Value => (T)base.Value;

        public bool Equals(Cell<T> other)
        {
            return base.Equals(other);
        }
    }
}