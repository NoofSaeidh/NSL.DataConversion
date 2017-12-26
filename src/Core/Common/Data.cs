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
    public class Data : ICollection<ITable>
    {
        #region ICollection<ITable> implementation

        public int Count { get; }
        public bool IsReadOnly { get; }

        public void Add(ITable item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ITable item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ITable[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ITable> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(ITable item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}