// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public class CellResolver : IObjectResolver<ICell>, IGenericResolver<ICell>
        , IResolver<object, ICell>
        , IResolver<object[,], ICell[,]>, IResolver<IEnumerable<IEnumerable<object>>, ICell[,]>
    {
        private static readonly Lazy<CellResolver> lazy = new Lazy<CellResolver>();

        public static CellResolver Instance => lazy.Value;

        public ICell<T> Resolve<T>(T value)
        {
            return new Cell<T>(value);
        }

        public ICell ResolveObject(object value)
        {
            if (value == null) return new Cell(value);
            return (ICell)Activator.CreateInstance(typeof(Cell<>).MakeGenericType(value.GetType()), value);
        }

        ICell IResolver<object, ICell>.Resolve(object value) => ResolveObject(value);

        public ICell ResolveGeneric<U>(U value) => Resolve(value);

        public ICell[,] Resolve(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new ICell[imax, jmax];
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    result[i, j] = ((IObjectResolver<ICell>)this).ResolveObject(value[i, j]);
                }
            }
            return result;
        }

        public ICell[,] Resolve(IEnumerable<IEnumerable<object>> value)
        {
            var array = value.Select(x => x.ToArray()).ToArray();
            var jmax = array.Max(x => x.Length);
            var result = new ICell[array.Length, jmax];
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    result[i, j] = ResolveObject(array[i][j]);
                }
                for (int j = array[i].Length; j < jmax; j++)
                {
                    result[i, j] = ResolveObject(null);
                }
            }
            return result;
        }
    }
}