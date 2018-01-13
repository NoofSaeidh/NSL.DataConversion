// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Common
{
    public class TableResolver : ITableResolver
    {
        private static readonly Lazy<TableResolver> lazy = new Lazy<TableResolver>();

        public static TableResolver Instance => lazy.Value;

        private readonly ICellResolver _cellResolver;

        public TableResolver(ICellResolver cellResolver)
        {
            _cellResolver = cellResolver ?? throw new ArgumentNullException(nameof(cellResolver));
        }

        // Use signleton CellResolver
        public TableResolver() : this(CellResolver.Instance)
        {
        }

        public IModifiableTable ResolveModifiableTable(IEnumerable<IEnumerable<object>> value)
        {
            return new ModifiableTable(_cellResolver.ResolveToArray(value));
        }

        public IModifiableTable ResolveModifiableTable(object[,] value)
        {
            return new ModifiableTable(_cellResolver.ResolveToArray(value));
        }

        public ITable ResolveTable(IEnumerable<IEnumerable<object>> value)
        {
            return new SimpleTable(_cellResolver.ResolveToArray(value));
        }

        public ITable ResolveTable(object[,] value)
        {
            return new SimpleTable(_cellResolver.ResolveToArray(value));
        }

        ITable IResolver<object[,], ITable>.Resolve(object[,] value) => ResolveTable(value);

        ITable IResolver<IEnumerable<IEnumerable<object>>, ITable>.Resolve(IEnumerable<IEnumerable<object>> value) => ResolveTable(value);

        IModifiableTable IResolver<object[,], IModifiableTable>.Resolve(object[,] value) => ResolveModifiableTable(value);

        IModifiableTable IResolver<IEnumerable<IEnumerable<object>>, IModifiableTable>.Resolve(IEnumerable<IEnumerable<object>> value) => ResolveModifiableTable(value);
    }
}