// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    [Serializable]
    public class Matrix2d<T> : IMatrix2d<T>, IReadOnlyMatrix2d<T>, IReadOnlyMatrixXd<T>
    {
        private const int _defaultCapacity = 4;

        private T[,] _items;
        private int _sizeTotal;
        private int _sizeX;
        private int _sizeY;
        private int _version;

        [NonSerialized]
        private Object _syncRoot;

        private static readonly T[,] _emptyArray = new T[0, 0];

        public Matrix2d()
        {
            _items = _emptyArray;
        }

        public Matrix2d(int capacityX, int capacityY)
        {
            _items = new T[capacityX, capacityY];
        }

        public Matrix2d(Index2d capacities) : this(capacities.X, capacities.Y)
        {
        }

        public Matrix2d(IEnumerable<T> collection, Index2d counts)
        {
            throw new NotImplementedException();
        }

        public T this[Index2d index]
        {
            get => this[index.X, index.Y];
            set => this[index.X, index.Y] = value;
        }

        public T this[int x, int y]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        T IMatrixXd<T>.this[IIndexXd index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        T IReadOnlyMatrixXd<T>.this[IIndexXd index] => ((IMatrixXd<T>)this)[index];

        public int Count => _sizeTotal;
        public int CountX => _sizeX;
        public int CountY => _sizeY;
        public Index2d Counts => new Index2d(_sizeX, _sizeY);
        IIndexXd IReadOnlyCollectionXd<T>.Counts => Counts;
        IIndexXd ICollectionXd<T>.Counts => Counts;
        public bool IsFixedSize => false;
        public int Dimensions => 2;
        public bool IsReadOnly { get; } //todo:
        public bool IsSynchronized { get; } //todo:

        public void Add(IEnumerable<T> items, int dimension)
        {
            throw new NotImplementedException();
        }

        public void AddX(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public void AddY(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int x, int y)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, IIndexXd index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<IIntersectionXd<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Index2d IndexOf(T intem)
        {
            throw new NotImplementedException();
        }

        public int IndexXOf(T item)
        {
            throw new NotImplementedException();
        }

        public int IndexYOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(IIndexXd index, T item)
        {
            throw new NotImplementedException();
        }

        public void InsertX(int x, T item)
        {
            throw new NotImplementedException();
        }

        public void InsertY(int y, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IEnumerable<T> items, int dimension)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(IIndexXd index, int dimension)
        {
            throw new NotImplementedException();
        }

        public void RemoveAtX(int x)
        {
            throw new NotImplementedException();
        }

        public void RemoveAtY(int y)
        {
            throw new NotImplementedException();
        }

        public bool RemoveX(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public bool RemoveY(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        IEnumerator<Intersection2d<T>> IEnumerable<Intersection2d<T>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IIndexXd IMatrixXd<T>.IndexOf(T item)
        {
            throw new NotImplementedException();
        }
    }
}