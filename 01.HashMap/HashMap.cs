namespace _01.HashMap
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashMap<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DefaultCapacity = 32;
        private const float ResizeFactor = 0.75f;

        private int capacity;
        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public HashMap(int capacity = DefaultCapacity)
        {
            this.Capacity = capacity;
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[this.Capacity];
        }

        public int Count { get; set; }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Capacity should be a positive integer");
                }

                this.capacity = value;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.Select(el => el.Key);
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return this.Select(el => el.Value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.Get(key);
            }

            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (this.GrowNeeded())
            {
                this.GrowSlots();
            }

            var index = this.GetSlotIndex(key);

            if (this.slots[index] == null)
            {
                this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var pair in this.slots[index])
            {
                if (pair.Key.Equals(key))
                {
                    throw new ArgumentException($"Key already exists [{key}]");
                }
            }

            this.slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));

            this.Count++;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var index = this.GetSlotIndex(key);
            if (this.slots[index] != null)
            {
                foreach (var keyValue in this.slots[index])
                {
                    if (keyValue.Key.Equals(key))
                    {
                        return keyValue;
                    }
                }
            }

            return null;
        }

        public TValue Get(TKey key)
        {
            var elelment = this.Find(key);
            if (elelment == null)
            {
                throw new KeyNotFoundException("Element not found");
            }

            return elelment.Value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            var element = this.Find(key);
            if (element == null)
            {
                return false;
            }

            value = element.Value;
            return true;
        }

        public bool ContainsKey(TKey key)
        {
            var element = this.Find(key);

            return element != null;
        }

        public bool Remove(TKey key)
        {
            var index = this.GetSlotIndex(key);
            var elelments = this.slots[index];
            if (elelments != null)
            {
                var currentElement = elelments.First;
                while (currentElement != null)
                {
                    if (currentElement.Value.Key.Equals(key))
                    {
                        elelments.Remove(currentElement);
                        this.Count--;
                        return true;
                    }

                    currentElement = currentElement.Next;
                }
            }

            return false;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            if (this.GrowNeeded())
            {
                this.GrowSlots();
            }

            var index = this.GetSlotIndex(key);

            if (this.slots[index] == null)
            {
                this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var pair in this.slots[index])
            {
                if (pair.Key.Equals(key))
                {
                    pair.Value = value;
                    return false;
                }
            }

            this.slots[index].AddLast(new KeyValue<TKey, TValue>(key, value));
            this.Count++;

            return true;
        }

        public void Clear()
        {
            this.Count = 0;
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var slot in this.slots)
            {
                if (slot != null)
                {
                    var currentelement = slot.First;
                    while (currentelement != null)
                    {
                        yield return currentelement.Value;
                        currentelement = currentelement.Next;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int GetSlotIndex(TKey key)
        {
            var index = Math.Abs(key.GetHashCode()) % this.Capacity;

            return index;
        }

        private bool GrowNeeded()
        {
            var res = (float)(this.Count + 1) / this.Capacity >= ResizeFactor;

            return res;
        }

        private void GrowSlots()
        {
            this.Capacity *= 2;
            var newSlots = new LinkedList<KeyValue<TKey, TValue>>[this.Capacity];
            foreach (var element in this)
            {
                var index = this.GetSlotIndex(element.Key);
                if (newSlots[index] == null)
                {
                    newSlots[index] = new LinkedList<KeyValue<TKey, TValue>>();
                }

                newSlots[index].AddLast(element);
            }

            this.slots = newSlots;
        }
    }
}