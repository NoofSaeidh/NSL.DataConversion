// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.XLSX
{
    public class XLSXCellResolver : CellResolver, IXLSXCellResolver
    {
        public virtual IXLSXCell Resolve(object value)
        {
            return new XLSXCell(value);
        }

        public new virtual IXLSXCell ResolveObject(object value) => Resolve(value);

        public new virtual IXLSXCell[,] ResolveToArray(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new IXLSXCell[imax, jmax];
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    result[i, j] = Resolve(value[i, j]);
                }
            }
            return result;
        }

        public new virtual IXLSXCell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value)
        {
            var array = value.Select(x => x.ToArray()).ToArray();
            var jmax = array.Max(x => x.Length);
            var result = new IXLSXCell[array.Length, jmax];
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

        public new virtual IList<IList<IXLSXCell>> ResolveToList(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new List<IList<IXLSXCell>>(imax);
            for (int i = 0; i < imax; i++)
            {
                result.Add(new List<IXLSXCell>(jmax));
                for (int j = 0; j < jmax; j++)
                {
                    result[i].Add(ResolveObject(value[i, j]));
                }
            }
            return result;
        }

        public new virtual IList<IList<IXLSXCell>> ResolveToList(IEnumerable<IEnumerable<object>> value)
        {
            var result = value.Select(x => (IList<IXLSXCell>)x.Select(item => ResolveObject(item)).ToList()).ToList();
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

        IXLSXCell[,] IResolver<object[,], IXLSXCell[,]>.Resolve(object[,] value) => ResolveToArray(value);

        IEnumerable<IEnumerable<IXLSXCell>> IResolver<IEnumerable<IEnumerable<object>>, IEnumerable<IEnumerable<IXLSXCell>>>.Resolve(IEnumerable<IEnumerable<object>> value) => ResolveToList(value);
    }
}