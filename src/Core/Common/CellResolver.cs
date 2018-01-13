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
    public class CellResolver : ICellResolver
    {
        private static readonly Lazy<CellResolver> lazy = new Lazy<CellResolver>();

        public static ICellResolver Instance => lazy.Value;

        public virtual ICell<T> Resolve<T>(T value)
        {
            return new Cell<T>(value);
        }

        public virtual ICell ResolveObject(object value)
        {
            if (value == null) return new Cell(value);
            return (ICell)Activator.CreateInstance(typeof(Cell<>).MakeGenericType(value.GetType()), value);
        }

        public virtual ICell[,] ResolveToArray(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new ICell[imax, jmax];
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    result[i, j] = ResolveObject(value[i, j]);
                }
            }
            return result;
        }

        public virtual ICell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value)
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

        public virtual IList<IList<ICell>> ResolveToList(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new List<IList<ICell>>(imax);
            for (int i = 0; i < imax; i++)
            {
                result.Add(new List<ICell>(jmax));
                for (int j = 0; j < jmax; j++)
                {
                    result[i].Add(ResolveObject(value[i, j]));
                }
            }
            return result;
        }

        public virtual IList<IList<ICell>> ResolveToList(IEnumerable<IEnumerable<object>> value)
        {
            var result = value.Select(x => (IList<ICell>)x.Select(item => ResolveObject(item)).ToList()).ToList();
            var max = result.Max(x => x.Count);
            foreach (var item in result)
            {
                while (item.Count < max)
                {
                    item.Add(ResolveObject(null));
                }
            }
            return result;
        }

        ICell IResolver<object, ICell>.Resolve(object value) => ResolveObject(value);

        ICell IGenericResolver<ICell>.ResolveGeneric<U>(U value) => Resolve(value);

        ICell[,] IResolver<object[,], ICell[,]>.Resolve(object[,] value) => ResolveToArray(value);

        ICell[,] IResolver<IEnumerable<IEnumerable<object>>, ICell[,]>.Resolve(IEnumerable<IEnumerable<object>> value) => ResolveToArray(value);

        IList<IList<ICell>> IResolver<object[,], IList<IList<ICell>>>.Resolve(object[,] value) => ResolveToList(value);

        IList<IList<ICell>> IResolver<IEnumerable<IEnumerable<object>>, IList<IList<ICell>>>.Resolve(IEnumerable<IEnumerable<object>> value) => ResolveToList(value);
    }
}