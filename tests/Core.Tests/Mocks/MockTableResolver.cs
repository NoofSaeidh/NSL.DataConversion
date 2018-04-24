using NSL.DataConversion.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core.Tests.Mocks
{
    public class MockTableResolver : ITableResolver
    {
        private readonly MockCellResolver _cellResolver = new MockCellResolver();

      
        public IModifiableTable ResolveModifiableTable(IEnumerable<IEnumerable<object>> value)
        {
            throw new NotImplementedException();
        }

        public IModifiableTable ResolveModifiableTable(object[,] value)
        {
            throw new NotImplementedException();
        }

        public ITable ResolveTable(IEnumerable<IEnumerable<object>> value)
        {
            return new MockTable(_cellResolver.ResolveToArray(value));
        }

        public ITable ResolveTable(object[,] value)
        {
            return new MockTable(_cellResolver.ResolveToArray(value));
        }

        ITable IResolver<object[,],ITable>.Resolve(object[,] value)
        {
            return ResolveTable(value);
        }

        ITable IResolver<IEnumerable<IEnumerable<object>>, ITable>.Resolve(IEnumerable<IEnumerable<object>> value)
        {
            return ResolveTable(value);
        }


        IModifiableTable IResolver<object[,], IModifiableTable>.Resolve(object[,] value)
        {
            throw new NotImplementedException();
        }

        IModifiableTable IResolver<IEnumerable<IEnumerable<object>>, IModifiableTable>.Resolve(IEnumerable<IEnumerable<object>> value)
        {
            throw new NotImplementedException();
        }
    }
}
