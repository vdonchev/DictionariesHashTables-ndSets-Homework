namespace _04.OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftNode = null,
            BinaryTree<T> rightNode = null,
            BinaryTree<T> parent = null)
        {
            this.Value = value;
            this.LeftNode = leftNode;
            this.RightNode = rightNode;
            this.Parent = parent;
        }

        public T Value { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public BinaryTree<T> LeftNode { get; set; }

        public BinaryTree<T> RightNode { get; set; }

        public bool AddChild(T value)
        {
            if (this.Value.CompareTo(value) < 0)
            {
                if (this.RightNode == null)
                {
                    this.RightNode = new BinaryTree<T>(value);
                    this.RightNode.Parent = this;
                    return true;
                }

                return this.RightNode.AddChild(value);
            }

            if (this.Value.CompareTo(value) > 0)
            {
                if (this.LeftNode == null)
                {
                    this.LeftNode = new BinaryTree<T>(value);
                    this.LeftNode.Parent = this;
                    return true;
                }

                return this.LeftNode.AddChild(value);
            }

            return false;
        }

        public BinaryTree<T> ContainsValue(T value)
        {
            if (this.Value.CompareTo(value) == 0)
            {
                return this;
            }

            if (this.Value.CompareTo(value) < 0)
            {
                return this.RightNode?.ContainsValue(value);
            }

            if (this.Value.CompareTo(value) > 0)
            {
                return this.LeftNode?.ContainsValue(value);
            }

            return null;
        }

        public bool IsLeftChild()
        {
            return this.Parent != null && this.Parent.LeftNode == this;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.LeftNode != null)
            {
                foreach (var node in this.LeftNode)
                {
                    yield return node;
                }
            }

            yield return this.Value;

            if (this.RightNode != null)
            {
                foreach (var node in this.RightNode)
                {
                    yield return node;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void PrintEach(int indent = 0)
        {
            Console.WriteLine(new string(' ', indent * 2) + this.Value);

            if (this.LeftNode != null)
            {
                this.LeftNode.PrintEach(indent + 1);
            }

            if (this.RightNode != null)
            {
                this.RightNode.PrintEach(indent + 1);
            }
        }
    }
}