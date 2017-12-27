// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public class CellResolver : IResolver<ICell>, IGenericResolver<ICell>
    {
        private static readonly Lazy<CellResolver> lazy = new Lazy<CellResolver>();

        public static CellResolver Instance => lazy.Value;

        public ICell<T> Resolve<T>(T value)
        {
            return new Cell<T>(value);
        }

        ICell IResolver<ICell>.Resolve(object value)
        {
            if (value == null) return new Cell(value);
            return (ICell)Activator.CreateInstance(typeof(Cell<>).MakeGenericType(value.GetType()), value);
        }

        ICell IGenericResolver<ICell>.Resolve<U>(U value)
        {
            return Resolve(value);
        }
    }
}