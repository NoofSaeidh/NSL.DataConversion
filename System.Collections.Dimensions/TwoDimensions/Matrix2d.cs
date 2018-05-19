// This file is licensed under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Dimensions.TwoDimensions
{
    // todo: exception messages
    [Serializable]
    public class Matrix2d<T> : IMatrix2d<T>, IReadOnlyMatrix2d<T>, IReadOnlyMatrixXd<T>
    {
        private enum SizeAction
        {
            Nothing
            Return,
            Throw,
            ExtendItems,
            ExtendInput
        }

        // from mscorlib Array
        internal const int MaxArrayLength = 0X7FEFFFFF;

        internal const int MaxByteArra_capacityY = 0x7FFFFFC7;

        private const int _defaultCapacity = 4;

        private static readonly T[,] _emptyArray = new T[0, 0];
        private T[,] _items;
        private int _sizeTotal;
        private int _sizeX;
        private int _sizeY;
        private int _capacityX;
        private int _capacityY;

        [NonSerialized]
        private object _syncRoot;

        private int _version;

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

        // throw, skip or add default if new collection that added
        // has less values that curretn dimension
        public DimensionChangeBehavior AddingFewerItemsBehavior { get; set; }

        // the same but extend all other values (if FillDefault specified)
        // if new value has more values
        public DimensionChangeBehavior AddingLargerItemsBehavior { get; set; }

        public Index2d Capacities
        {
            get
            {
                return new Index2d(_capacityX, _capacityY);
            }
            set
            {
                // todo: methods
                if (value.X < _sizeX)
                {
                    throw new ArgumentOutOfRangeException();
                }
                if (value.Y < _sizeY)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value.X != _items.GetLength(0)
                    || value.Y != _items.GetLength(1))
                {
                    if (value.X > 0 || value.Y > 0)
                    {
                        T[,] newItems = new T[value.X, value.Y];
                        if (_sizeTotal > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _sizeTotal);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }

                    _capacityX = _items.GetLength(0);
                    _capacityY = _items.GetLength(1);
                }
            }
        }

        public int CapacityX
        {
            get
            {
                return _capacityX;
            }
            set
            {
                // todo: methods
                if (value < _sizeX)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _capacityX)
                {
                    if (value > 0)
                    {
                        T[,] newItems = new T[value, _sizeY];
                        if (_sizeTotal > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _sizeTotal);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }

                    _capacityX = _items.GetLength(0);
                }
            }
        }

        public int CapacityY
        {
            get
            {
                return _capacityY;
            }
            set
            {
                // todo: methods
                if (value < _sizeY)
                {
                    throw new ArgumentOutOfRangeException();
                }

                if (value != _capacityY)
                {
                    if (value > 0)
                    {
                        T[,] newItems = new T[_sizeX, value];
                        if (_sizeTotal > 0)
                        {
                            Array.Copy(_items, 0, newItems, 0, _sizeTotal);
                        }
                        _items = newItems;
                    }
                    else
                    {
                        _items = _emptyArray;
                    }

                    _capacityY = _items.GetLength(1);
                }
            }
        }

        public int CapacityTotal => _items.Length;

        public int Count => _sizeTotal;

        public Index2d Counts => new Index2d(_sizeX, _sizeY);

        IIndexXd IReadOnlyCollectionXd<T>.Counts => Counts;

        IIndexXd ICollectionXd<T>.Counts => Counts;

        public int CountX => _sizeX;

        public int CountY => _sizeY;

        public int Dimensions => 2;

        public bool IsFixedSize => false;

        //todo: sync
        bool ICollectionXd<T>.IsReadOnly => false;

        bool ICollectionXd<T>.IsSynchronized => false;

        object ICollectionXd<T>.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
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
                //todo: method?
                // Following trick can reduce the range check by one
                if (x < 0 || y < 0
                    || (uint)x >= (uint)_sizeX
                    || (uint)y >= (uint)_sizeY)
                {
                    throw new ArgumentOutOfRangeException();
                }
                return _items[x, y];
            }

            set
            {
                if (x < 0 || y < 0
                    || (uint)x >= (uint)_sizeX
                    || (uint)y >= (uint)_sizeY)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _items[x, y] = value;
                _version++;
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

        void ICollectionXd<T>.Add(IEnumerable<T> items, int dimension)
        {
            switch (dimension)
            {
                case 0:
                    AddX(items);
                    return;

                case 1:
                    AddY(items);
                    return;

                default:
                    throw new InvalidOperationException();
            }
        }

        // value for input, size for _sizeX or _sizeY
        private SizeAction CheckSizes(int value, int size)
        {
            if (value < size)
            {
                switch (AddingFewerItemsBehavior)
                {
                    case DimensionChangeBehavior.Throw:
                        return SizeAction.Throw;

                    case DimensionChangeBehavior.Ignore:
                        return SizeAction.Return;

                    case DimensionChangeBehavior.FillDefaults:
                        return SizeAction.ExtendInput;

                    default:
                        throw new NotImplementedException();
                }
            }

            if (value > size)
            {
                switch (AddingFewerItemsBehavior)
                {
                    case DimensionChangeBehavior.Throw:
                        return SizeAction.Throw;

                    case DimensionChangeBehavior.Ignore:
                        return SizeAction.Return;

                    case DimensionChangeBehavior.FillDefaults:
                        return SizeAction.ExtendItems;

                    default:
                        throw new NotImplementedException();
                }
            }

            return SizeAction.Nothing;
        }

        private SizeAction CheckSizeX(int value)
        {
            return CheckSizes(value, _sizeX);
        }

        private SizeAction CheckSizeY(int value)
        {
            return CheckSizes(value, _sizeY);
        }

        private IList<T> EnsureListSizes(IEnumerable<T> items, ref int size)
        {
            var list = items is IList<T> l ? l : items.ToArray();
            var action = CheckSizes(list.Count, size);
            switch (action)
            {
                case SizeAction.Nothing:
                    return list;

                case SizeAction.Throw:
                    throw new ArgumentException();

                case SizeAction.ExtendItems:
                    size = list.Count;
                    return list;

                case SizeAction.ExtendInput:
                    return list.Union(Enumerable.Repeat(default(T), size - list.Count)).ToArray();

                case SizeAction.Return:
                default:
                    return null;
            }
        }

        public void AddX(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var list = EnsureListSizes(items, ref _sizeY);
            if (list == null) return;

            // in EnsureListSizes _sizeY changed
            if (_sizeY >= _capacityY)
                EnsureCapacityY(_sizeY);

            if (_sizeX == _capacityX)
                EnsureCapacityX(_sizeX + 1);

            for (int i = 0; i < list.Count; i++)
            {
                _items[_sizeX, i] = list[i];
            }

            _version++;
        }

        public void AddY(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var list = EnsureListSizes(items, ref _sizeX);
            if (list == null) return;

            if (_sizeX >= _capacityX)
                EnsureCapacityY(_sizeX);

            if (_sizeY == _capacityY)
                EnsureCapacityX(_sizeY + 1);

            for (int i = 0; i < list.Count; i++)
            {
                _items[i, _sizeY] = list[i];
            }

            _version++;
        }

        public void Clear()
        {
            if (_sizeTotal > 0)
            {
                Array.Clear(_items, 0, _sizeTotal);
                _sizeX = _sizeY = _sizeTotal = 0;
            }
            _version++;
        }

        public bool Contains(T item)
        {
            if ((object)item == null)
            {
                for (int i = 0; i < _sizeX; i++)
                    for (int j = 0; j < _sizeY; j++)
                        if ((object)_items[i, j] == null)
                            return true;
                return false;
            }
            else
            {
                EqualityComparer<T> c = EqualityComparer<T>.Default;
                for (int i = 0; i < _sizeX; i++)
                    for (int j = 0; j < _sizeY; j++)
                        if (c.Equals(_items[i, j], item)) return true;

                return false;
            }
        }

        public void CopyTo(Array array, int arrayIndex)
        {
            Array.Copy(_items, 0, array, arrayIndex, _sizeTotal);
        }

        public IEnumerator<Intersection2d<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<IIntersectionXd<T>> IEnumerable<IIntersectionXd<T>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void EnsureCapacityX(int min)
        {
            if (_capacityX < min)
            {
                int newCapacity = _capacityX == 0 ? _defaultCapacity : _capacityX * 2;

                if ((uint)newCapacity * (uint)_capacityX > MaxArrayLength)
                    newCapacity = MaxArrayLength;

                if (newCapacity < min)
                    newCapacity = min;

                CapacityX = newCapacity;
            }
        }

        private void EnsureCapacityY(int min)
        {
            if (_capacityY < min)
            {
                int newCapacity = _capacityY == 0 ? _defaultCapacity : _capacityY * 2;

                if ((uint)newCapacity * (uint)_capacityY > MaxArrayLength)
                    newCapacity = MaxArrayLength;

                if (newCapacity < min)
                    newCapacity = min;

                CapacityY = newCapacity;
            }
        }

        private void EnsureCapacities(Index2d min)
        {
            if (_capacityX < min.X || _capacityY < min.Y)
            {
                int newCapacityX = _capacityX == 0 ? _defaultCapacity : _capacityX * 2;
                int newCapacityY = _capacityY == 0 ? _defaultCapacity : _capacityY * 2;

                if ((uint)newCapacityX * (uint)newCapacityY > MaxArrayLength)
                {
                    // todo: don't know how to ensure
                    throw new NotImplementedException();
                }

                if (newCapacityX < min.X)
                    newCapacityX = min.X;
                if (newCapacityY < min.Y)
                    newCapacityY = min.Y;

                Capacities = new Index2d(newCapacityX, newCapacityY);
            }
        }

        public Index2d IndexOf(T intem)
        {
            throw new NotImplementedException();
        }

        IIndexXd IMatrixXd<T>.IndexOf(T item)
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

        void IMatrixXd<T>.Insert(IIndexXd index, T item)
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

        bool ICollectionXd<T>.Remove(IEnumerable<T> items, int dimension)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(IIndexXd index, int dimension)
        {
            throw new NotImplementedException();
        }

        void IMatrixXd<T>.RemoveAt(IIndexXd index, int dimension)
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
    }
}