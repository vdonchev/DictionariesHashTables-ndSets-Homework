namespace _04.OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private BinaryTree<T> root;

        public int Count { get; private set; }

        public void Add(T value)
        {
            if (this.root == null)
            {
                this.root = new BinaryTree<T>(value);
                this.Count++;
            }
            else
            {
                if (this.root.AddChild(value))
                {
                    this.Count++;
                }
            }
        }

        public bool Remove(T value)
        {
            var currentNode = this.root.ContainsValue(value);
            if (currentNode == null)
            {
                return false;
            }
            
            if (currentNode.RightNode != null &&
                currentNode.LeftNode != null)
            {
                var nodeToBeMoved = currentNode.RightNode;

                while (nodeToBeMoved.LeftNode != null)
                {
                    nodeToBeMoved = nodeToBeMoved.LeftNode;
                }

                nodeToBeMoved.Parent.LeftNode = null;

                nodeToBeMoved.LeftNode = currentNode.LeftNode;
                nodeToBeMoved.LeftNode.Parent = nodeToBeMoved;
                nodeToBeMoved.RightNode = currentNode.RightNode;
                nodeToBeMoved.RightNode.Parent = nodeToBeMoved;

                if (currentNode.Parent == null)
                {
                    nodeToBeMoved.Parent = null;
                    this.root = nodeToBeMoved;
                }
                else
                {
                    if (currentNode.IsLeftChild())
                    {
                        currentNode.Parent.LeftNode = nodeToBeMoved;
                    }
                    else
                    {
                        currentNode.Parent.RightNode = nodeToBeMoved;
                    }

                    nodeToBeMoved.Parent = currentNode.Parent;
                }
            }
            else if (currentNode.RightNode != null)
            {
                if (currentNode.IsLeftChild())
                {
                    currentNode.Parent.LeftNode = currentNode.RightNode;
                }
                else
                {
                    currentNode.Parent.RightNode = currentNode.RightNode;
                }

                currentNode.RightNode.Parent = currentNode.Parent;
            }
            else if (currentNode.LeftNode != null)
            {
                if (currentNode.IsLeftChild())
                {
                    currentNode.Parent.LeftNode = currentNode.LeftNode;
                }
                else
                {
                    currentNode.Parent.RightNode = currentNode.LeftNode;
                }

                currentNode.LeftNode.Parent = currentNode.Parent;
            }
            else
            {
                if (currentNode.IsLeftChild())
                {
                    currentNode.Parent.LeftNode = null;
                }
                else
                {
                    currentNode.Parent.RightNode = null;
                }
            }

            this.Count--;
            return true;
        }

        public bool Contains(T value)
        {
            return this.root.ContainsValue(value) != null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.root.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void PrintBfsStyle()
        {
            this.root.PrintEach();
        }

        public T[] ToArray()
        {
            var array = new T[this.Count];
            var index = 0;
            foreach (var tree in this.root)
            {
                array[index++] = tree;
            }

            return array;
        }
    }
}