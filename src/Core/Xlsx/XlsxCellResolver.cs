// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using NSL.DataConversion.Core.Common;
using NSL.DataConversion.Core.Xlsx.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Xlsx
{
    public class XlsxCellResolver : CellResolver, IXlsxCellResolver
    {
        private static readonly Lazy<XlsxCellResolver> lazy = new Lazy<XlsxCellResolver>();

        public new static XlsxCellResolver Instance => lazy.Value;

        public virtual IXlsxCell Resolve(object value)
        {
            return new XlsxCell(value);
        }

        public new virtual IXlsxCell ResolveObject(object value) => Resolve(value);

        public new virtual IXlsxCell[,] ResolveToArray(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new IXlsxCell[imax, jmax];
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    result[i, j] = Resolve(value[i, j]);
                }
            }
            return result;
        }

        public new virtual IXlsxCell[,] ResolveToArray(IEnumerable<IEnumerable<object>> value)
        {
            var array = value.Select(x => x.ToArray()).ToArray();
            var jmax = array.Max(x => x.Length);
            var result = new IXlsxCell[array.Length, jmax];
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

        public new virtual IList<IList<IXlsxCell>> ResolveToList(object[,] value)
        {
            var imax = value.GetLength(0);
            var jmax = value.GetLength(1);
            var result = new List<IList<IXlsxCell>>(imax);
            for (int i = 0; i < imax; i++)
            {
                result.Add(new List<IXlsxCell>(jmax));
                for (int j = 0; j < jmax; j++)
                {
                    result[i].Add(ResolveObject(value[i, j]));
                }
            }
            return result;
        }

        public new virtual IList<IList<IXlsxCell>> ResolveToList(IEnumerable<IEnumerable<object>> value)
        {
            var result = value.Select(x => (IList<IXlsxCell>)x.Select(item => ResolveObject(item)).ToList()).ToList();
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

        public virtual IXlsxCell Resolve(ICellWrapper value)
        {
            throw new NotImplementedException();
        }

        public virtual IXlsxCell[,] Resolve(ICellWrapper[,] value)
        {
            throw new NotImplementedException();
        }

        IXlsxCell[,] IResolver<object[,], IXlsxCell[,]>.Resolve(object[,] value) => ResolveToArray(value);

        IEnumerable<IEnumerable<IXlsxCell>> IResolver<IEnumerable<IEnumerable<object>>, IEnumerable<IEnumerable<IXlsxCell>>>.Resolve(IEnumerable<IEnumerable<object>> value) => ResolveToList(value);
    }
}