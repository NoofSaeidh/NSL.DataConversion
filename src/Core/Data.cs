// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSL.DataConversion.Core
{
    public class Data : ICollection<Table>
    {
        #region ICollection<Table> implementation

        public int Count { get; }
        public bool IsReadOnly { get; }

        public void Add(Table item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Table item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Table[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Table> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Table item)
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