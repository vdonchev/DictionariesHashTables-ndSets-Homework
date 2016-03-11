namespace _01.HashMap
{
    using System.Collections.Generic;

    public class KeyValue<TKey, TValue>
    {
        public KeyValue(
            TKey key,
            TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as KeyValue<TKey, TValue>;
            if (other != null)
            {
                return object.Equals(this.Key, other.Key) && object.Equals(this.Value, other.Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.CombineHashCodes(this.Key.GetHashCode(), this.Value.GetHashCode());
        }

        private int CombineHashCodes(int h1, int h2)
        {
            return ((h1 << 5) + h1) ^ h2;
        }
    }
}