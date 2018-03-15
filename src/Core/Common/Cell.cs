// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public class Cell : ICell, ICell<object>, IEquatable<Cell>, IEquatable<ICell>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly object _value;

        public Cell(object value)
        {
            _value = value;
        }

        public virtual object Value => _value;

        public bool Equals(Cell other)
        {
            if (other == null) return false;

            if (Value == other.Value)
                return true;

            return false;
        }

        public virtual bool Equals(ICell other)
        {
            return Equals((object)other);
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
            return Value?.ToString() ?? "";
        }
    }

    public class Cell<T> : Cell, ICell<T>, IEquatable<Cell<T>>
    {
        public Cell(T value) : base(value)
        {
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        new public T Value => (T)base.Value;

        public bool Equals(Cell<T> other)
        {
            return base.Equals(other);
        }

        public override bool Equals(ICell cell)
        {
            var result = base.Equals(cell);
            if (!result)
                return false;
            if (cell.GetType() == GetType())
                return true;
            return false;
        }

        public static implicit operator Cell<T>(T value) => new Cell<T>(value);
    }
}