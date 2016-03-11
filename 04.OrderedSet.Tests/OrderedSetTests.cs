namespace _04.OrderedSet.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OrderedSetTests
    {
        [TestMethod]
        public void Add_FewItems_ShouldAdd()
        {
            var sortedSet = new OrderedSet<int>();

            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);

            Assert.AreEqual(3, sortedSet.Count);
            for (int i = 1; i < sortedSet.Count; i++)
            {
                var contains = sortedSet.Contains(i);
                Assert.IsTrue(contains);
            }
        }

        [TestMethod]
        public void Add_SameItems_ShouldAddOnlyOnce()
        {
            var sortedSet = new OrderedSet<int>();

            sortedSet.Add(1);
            sortedSet.Add(1);
            sortedSet.Add(1);

            Assert.AreEqual(1, sortedSet.Count);
            var contains = sortedSet.Contains(1);
            Assert.IsTrue(contains);
            var doNotContains = sortedSet.Contains(1000);
            Assert.IsFalse(doNotContains);
        }

        [TestMethod]
        public void Contains_ShouldReturnCorrectly()
        {
            var sortedSet = new OrderedSet<int>();

            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);

            var contains = sortedSet.Contains(1);
            Assert.IsTrue(contains);
            var doNotContains = sortedSet.Contains(4);
            Assert.IsFalse(doNotContains);
        }

        [TestMethod]
        public void Remove_ExistingElements_ShouldRemove()
        {
            var sortedSet = new OrderedSet<int>();

            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);

            var removed = sortedSet.Remove(2);
            Assert.IsTrue(removed);
            removed = sortedSet.Remove(3);
            Assert.IsTrue(removed);

            Assert.AreEqual(1, sortedSet.Count);
        }

        [TestMethod]
        public void Remove_NonExistingElements_ShouldReturnFalse()
        {
            var sortedSet = new OrderedSet<int>();

            sortedSet.Add(1);
            sortedSet.Add(2);
            sortedSet.Add(3);

            var removed = sortedSet.Remove(2000);
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void Count_EmptySet_ShouldReturnCorrectly()
        {
            var sortedSet = new OrderedSet<int>();
            Assert.AreEqual(0, sortedSet.Count);
        }

        [TestMethod]
        public void Count_SetWithElements_ShouldReturnCorrectly()
        {
            var sortedSet = new OrderedSet<int>();
            for (int i = 0; i < 1000; i++)
            {
                sortedSet.Add(i);
            }

            Assert.AreEqual(1000, sortedSet.Count);
        }

        [TestMethod]
        public void IEnumerable_ShouldEnumerateCorrectly()
        {
            var sortedSet = new OrderedSet<int>();
            sortedSet.Add(500);
            sortedSet.Add(-1);
            sortedSet.Add(0);
            sortedSet.Add(69);

            var expected = new[] { -1, 0, 69, 500 };
            var index = 0;
            foreach (var item in sortedSet)
            {
                Assert.AreEqual(expected[index++], item);
            }
        }

        [TestMethod]
        public void Order_ItemsSouldBeSorted()
        {
            var sortedSet = new OrderedSet<int>();
            sortedSet.Add(500);
            sortedSet.Add(-1);
            sortedSet.Add(0);
            sortedSet.Add(69);

            var expected = new[] { -1, 0, 69, 500 };
            CollectionAssert.AreEqual(expected, sortedSet.ToArray());
        }
    }
}
