// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions
{
    public interface ICollectionXd<T> : IEnumerable<IIntersectionXd<T>>, IEnumerable<T>, IEnumerable
    {
        int Dimensions { get; }

        int Count { get; }
        IIndexXd Counts { get; }

        bool IsReadOnly { get; }

        bool IsSynchronized { get; }

        void Add(IEnumerable<T> items, int dimension);

        void Clear();

        bool Contains(T item);

        bool Remove(IEnumerable<T> items, int dimension);

        void CopyTo(Array array, int arrayIndex);
    }
}