using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicList
{
    class DynamicList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Value used when allocated new List
        /// </summary>
        private static int DefaultSize = 4;

        /// <summary>
        /// List content
        /// </summary>
        private T[] Items;

        /// <summary>
        /// Filled items (List length)
        /// </summary>
        private int ItemsInUse { get; set; } = 0;

        /// <summary>
        /// Total Allocated
        /// </summary>
        public int Capacity { get { return Items.Length; } }

        /// <summary>
        /// Length of List
        /// </summary>
        public int Count { get { return ItemsInUse; } }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index">Numeber of item to retreive</param>
        /// <returns>Desired item</returns>
        public T this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        /// <summary>
        /// Constructor that gets default List size 
        /// </summary>
        /// <param name="Size"></param>
        public DynamicList(int Size)
        {
            DefaultSize = Size;
            Items = new T[DefaultSize];
        }

        public DynamicList() : this(DefaultSize)
        {        
        }

        /// <summary>
        /// Returns DynamicList enumerator 
        /// </summary>
        /// <returns>DynamicEnumerator instance</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicEnumerator<T>(Items, ItemsInUse);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)Items).GetEnumerator();
        }

        /// <summary>
        /// Adds item Item to list
        /// </summary>
        /// <param name="Item">Item to add to list</param>
        public void Add(T Item)
        {            
            if (ItemsInUse + 1 == Capacity)
            {                                
                Array.Resize(ref Items, Capacity * 2);                             
            }            
            Items[ItemsInUse] = Item;
            ItemsInUse++;                                        
        }

        /// <summary>
        /// Removes item by specified value
        /// </summary>
        /// <param name="Item">Value to remove</param>
        /// <returns>Operation success</returns>
        public bool Remove(T Item)
        {
            bool result = false;
            for (int i = 0; i < ItemsInUse; i++)
            {              
                if (Items[i].Equals(Item))
                {                 
                    result = RemoveAt(i);
                }
            }
            return result;
        }

        /// <summary>
        /// Removes item at specified index
        /// </summary>
        /// <param name="Position">Position of removed item</param>
        /// <returns>Operation success</returns>
        public bool RemoveAt(int Position)
        {
            if (Position < ItemsInUse && Position > -1)
            {
                Array.Copy(Items, Position + 1, Items, Position, ItemsInUse - Position - 1);
                ItemsInUse--;
                return true;
            }
            throw new IndexOutOfRangeException();
        }

        /// <summary>
        /// Clears list
        /// </summary>
        public void Clear()
        {
            ItemsInUse = 0;
            Items = new T[DefaultSize];
        }
    }
}
