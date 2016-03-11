namespace HashMap.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _01.HashMap;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HashMapTests
    {
        [TestMethod]
        public void Add_EmptyHashMap_NoDuplicates_ShouldAddElement()
        {
            // Arrange
            var hashMap = new HashMap<string, int>();

            // Act
            var elements = new KeyValue<string, int>[]
            {
            new KeyValue<string, int>("Peter", 5),
            new KeyValue<string, int>("Maria", 6),
            new KeyValue<string, int>("George", 4),
            new KeyValue<string, int>("Kiril", 5)
            };
            foreach (var element in elements)
            {
                hashMap.Add(element.Key, element.Value);
            }

            // Assert
            var actualElements = hashMap.ToList();
            CollectionAssert.AreEquivalent(elements, actualElements);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_EmptyHashMap_Duplicates_ShouldThrowException()
        {
            // Arrange
            var hashMap = new HashMap<string, string>();

            // Act
            hashMap.Add("Peter", "first");
            hashMap.Add("Peter", "second");
        }

        [TestMethod]
        public void Add_1000_Elements_Grow_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>(1);

            // Act
            var expectedElements = new List<KeyValue<string, int>>();
            for (int i = 0; i < 1000; i++)
            {
                hashMap.Add("key" + i, i);
                expectedElements.Add(new KeyValue<string, int>("key" + i, i));
            }

            // Assert
            var actualElements = hashMap.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void AddOrReplace_WithDuplicates_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>();

            // Act
            hashMap.AddOrReplace("Peter", 555);
            hashMap.AddOrReplace("Maria", 999);
            hashMap.AddOrReplace("Maria", 123);
            hashMap.AddOrReplace("Maria", 6);
            hashMap.AddOrReplace("Peter", 5);

            // Assert
            var expectedElements = new KeyValue<string, int>[]
            {
            new KeyValue<string, int>("Peter", 5),
            new KeyValue<string, int>("Maria", 6)
            };
            var actualElements = hashMap.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void Count_Empty_Add_Remove_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>();

            // Assert
            Assert.AreEqual(0, hashMap.Count);

            // Act & Assert
            hashMap.Add("Peter", 555);
            hashMap.AddOrReplace("Peter", 555);
            hashMap.AddOrReplace("Ivan", 555);
            Assert.AreEqual(2, hashMap.Count);

            // Act & Assert
            hashMap.Remove("Peter");
            Assert.AreEqual(1, hashMap.Count);

            // Act & Assert
            hashMap.Remove("Peter");
            Assert.AreEqual(1, hashMap.Count);

            // Act & Assert
            hashMap.Remove("Ivan");
            Assert.AreEqual(0, hashMap.Count);
        }

        [TestMethod]
        public void Get_ExistingElement_ShouldReturnTheValue()
        {
            // Arrange
            var hashMap = new HashMap<int, string>();

            // Act
            hashMap.Add(555, "Peter");
            var actualValue = hashMap.Get(555);

            // Assert
            Assert.AreEqual("Peter", actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Get_NonExistingElement_ShouldThrowException()
        {
            // Arrange
            var hashMap = new HashMap<int, string>();

            // Act
            hashMap.Get(12345);
        }

        [TestMethod]
        public void Indexer_ExistingElement_ShouldReturnTheValue()
        {
            // Arrange
            var hashMap = new HashMap<int, string>();

            // Act
            hashMap.Add(555, "Peter");
            var actualValue = hashMap[555];

            // Assert
            Assert.AreEqual("Peter", actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Indexer_NonExistingElement_ShouldThrowException()
        {
            // Arrange
            var hashMap = new HashMap<int, string>();

            // Act
            var value = hashMap[12345];
        }

        [TestMethod]
        public void Indexer_AddReplace_WithDuplicates_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>();

            // Act
            hashMap["Peter"] = 555;
            hashMap["Maria"] = 999;
            hashMap["Maria"] = 123;
            hashMap["Maria"] = 6;
            hashMap["Peter"] = 5;

            // Assert
            var expectedElements = new KeyValue<string, int>[]
            {
            new KeyValue<string, int>("Peter", 5),
            new KeyValue<string, int>("Maria", 6)
            };
            var actualElements = hashMap.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void Capacity_Grow_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>(2);

            // Assert
            Assert.AreEqual(2, hashMap.Capacity);

            // Act
            hashMap.Add("Peter", 123);
            hashMap.Add("Maria", 456);

            // Assert
            Assert.AreEqual(4, hashMap.Capacity);

            // Act
            hashMap.Add("Tanya", 555);
            hashMap.Add("George", 777);

            // Assert
            Assert.AreEqual(8, hashMap.Capacity);
        }

        [TestMethod]
        public void TryGetValue_ExistingElement_ShouldReturnTheValue()
        {
            // Arrange
            var hashMap = new HashMap<int, string>();

            // Act
            hashMap.Add(555, "Peter");
            string value;
            var result = hashMap.TryGetValue(555, out value);

            // Assert
            Assert.AreEqual("Peter", value);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryGetValue_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var hashMap = new HashMap<int, string>();

            // Act
            string value;
            var result = hashMap.TryGetValue(555, out value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Find_ExistingElement_ShouldReturnTheElement()
        {
            // Arrange
            var hashMap = new HashMap<string, DateTime>();
            var name = "Maria";
            var date = new DateTime(1995, 7, 18);
            hashMap.Add(name, date);

            // Act
            var element = hashMap.Find(name);

            // Assert
            var expectedElement = new KeyValue<string, DateTime>(name, date);
            Assert.AreEqual(expectedElement, element);
        }

        [TestMethod]
        public void Find_NonExistingElement_ShouldReturnNull()
        {
            // Arrange
            var hashMap = new HashMap<string, DateTime>();

            // Act
            var element = hashMap.Find("Maria");

            // Assert
            Assert.IsNull(element);
        }

        [TestMethod]
        public void ContainsKey_ExistingElement_ShouldReturnTrue()
        {
            // Arrange
            var hashMap = new HashMap<DateTime, string>();
            var date = new DateTime(1995, 7, 18);
            hashMap.Add(date, "Some value");

            // Act
            var containsKey = hashMap.ContainsKey(date);

            // Assert
            Assert.IsTrue(containsKey);
        }

        [TestMethod]
        public void ContainsKey_NonExistingElement_ShouldReturnFalse()
        {
            // Arrange
            var hashMap = new HashMap<DateTime, string>();
            var date = new DateTime(1995, 7, 18);

            // Act
            var containsKey = hashMap.ContainsKey(date);

            // Assert
            Assert.IsFalse(containsKey);
        }

        [TestMethod]
        public void Remove_ExistingElement_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, double>();
            hashMap.Add("Peter", 12.5);
            hashMap.Add("Maria", 99.9);

            // Assert
            Assert.AreEqual(2, hashMap.Count);

            // Act
            var removed = hashMap.Remove("Peter");

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(1, hashMap.Count);
        }

        [TestMethod]
        public void Remove_NonExistingElement_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, double>();
            hashMap.Add("Peter", 12.5);
            hashMap.Add("Maria", 99.9);

            // Assert
            Assert.AreEqual(2, hashMap.Count);

            // Act
            var removed = hashMap.Remove("George");

            // Assert
            Assert.IsFalse(removed);
            Assert.AreEqual(2, hashMap.Count);
        }

        [TestMethod]
        public void Remove_5000_Elements_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>();
            var keys = new List<string>();
            var count = 5000;
            for (int i = 0; i < count; i++)
            {
                var key = Guid.NewGuid().ToString();
                keys.Add(key);
                hashMap.Add(key, i);
            }

            // Assert
            Assert.AreEqual(count, hashMap.Count);

            // Act & Assert
            keys.Reverse();
            foreach (var key in keys)
            {
                hashMap.Remove(key);
                count--;
                Assert.AreEqual(count, hashMap.Count);
            }

            // Assert
            var expectedElements = new List<KeyValue<string, int>>();
            var actualElements = hashMap.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);
        }

        [TestMethod]
        public void Clear_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, int>();

            // Assert
            Assert.AreEqual(0, hashMap.Count);

            // Act
            hashMap.Clear();

            // Assert
            Assert.AreEqual(0, hashMap.Count);

            // Arrange
            hashMap.Add("Peter", 5);
            hashMap.Add("George", 7);
            hashMap.Add("Maria", 3);

            // Assert
            Assert.AreEqual(3, hashMap.Count);

            // Act
            hashMap.Clear();

            // Assert
            Assert.AreEqual(0, hashMap.Count);
            var expectedElements = new List<KeyValue<string, int>>();
            var actualElements = hashMap.ToList();
            CollectionAssert.AreEquivalent(expectedElements, actualElements);

            hashMap.Add("Peter", 5);
            hashMap.Add("George", 7);
            hashMap.Add("Maria", 3);

            // Assert
            Assert.AreEqual(3, hashMap.Count);
        }

        [TestMethod]
        public void Keys_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, double>();

            // Assert
            CollectionAssert.AreEquivalent(new string[0], hashMap.Keys.ToArray());

            // Arrange
            hashMap.Add("Peter", 12.5);
            hashMap.Add("Maria", 99.9);
            hashMap["Peter"] = 123.45;

            // Act
            var keys = hashMap.Keys;

            // Assert
            var expectedKeys = new string[] { "Peter", "Maria" };
            CollectionAssert.AreEquivalent(expectedKeys, keys.ToArray());
        }

        [TestMethod]
        public void Values_ShouldWorkCorrectly()
        {
            // Arrange
            var hashMap = new HashMap<string, double>();

            // Assert
            CollectionAssert.AreEquivalent(new string[0], hashMap.Values.ToArray());

            // Arrange
            hashMap.Add("Peter", 12.5);
            hashMap.Add("Maria", 99.9);
            hashMap["Peter"] = 123.45;

            // Act
            var values = hashMap.Values;

            // Assert
            var expectedValues = new double[] { 99.9, 123.45 };
            CollectionAssert.AreEquivalent(expectedValues, values.ToArray());
        }
    }
}
