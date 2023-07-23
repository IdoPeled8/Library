//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Logic;
//using Windows.UI.Xaml.Automation.Provider;
//using Windows.UI.Xaml.Controls;

//namespace UnitTest
//{

//    [TestClass]
//    public class AddItemTests
//    {
//        LibraryManager CollectionManager;

//        [TestInitialize]
//        public void Initialize()
//        {
//            CollectionManager = new LibraryManager();
//            CollectionManager.CreateDefaultData(); // making 18 items
//        }

//        [TestMethod]
//        public void AddItemTest()
//        {
//            CollectionManager.AddItem("itemm", "me", "comedy", "10", "2", "2000", "10", "15", "author");

//            List<Item> TempList = CollectionManager.GetAllItems();
//            Assert.IsTrue(TempList.Count == 19);
//            Assert.IsTrue(TempList[18].Name == "itemm");
//        }
//        [TestMethod]
//        public void AddItemFailTest()
//        {
//            //name is null
//            Assert.ThrowsException<Exception>(() => CollectionManager.AddItem("", "me", "comedy", "10", "2", "2000", "10", "15", "author"));
//            //date is to high
//            Assert.ThrowsException<ArgumentException>(() => CollectionManager.AddItem("itemmm", "me", "comedy", "10", "2", "3000", "10", "15", "author"));

//            List<Item> temp = CollectionManager.GetAllItems();

//            Assert.IsTrue(temp.Count == 18);
//        }

//        [TestMethod]
//        public void MakeBookItem_Test()
//        {
//            CollectionManager.AddItem("itemm", "me", "comedy", "10", "2", "2000", "10", "15", "author");

//            List<Item> TempList = CollectionManager.GetAllItems();

//            Assert.IsInstanceOfType(TempList[17], typeof(Book));
//            Assert.IsNotInstanceOfType(TempList[17], typeof(Magazine));
//        }

//    }
//    [TestClass]
//    public class EditItemTests
//    {
//        LibraryManager CollectionManager;

//        [TestInitialize]
//        public void Initialize()
//        {
//            CollectionManager = new LibraryManager();
//            CollectionManager.CreateDefaultData(); 

//        }

//        [TestMethod]
//        public void EditItemNameTest()
//        {

//            List<Item> TempList = CollectionManager.GetAllItems();
//            CollectionManager.EditItem(TempList[0], "ChangeName");

//            Assert.IsTrue(TempList[0].Name == "ChangeName");
//        }

//        [TestMethod]
//        public void EditThrowTest()
//        {
//            List<Item> tempList = CollectionManager.GetAllItems();

//            Assert.ThrowsException<ArgumentException>(() => CollectionManager.EditItem(tempList[0], null, null, null, "putPrice"));
//        }

//    }

//    [TestClass]
//    public class OverallItemTests
//    {
//        LibraryManager CollectionManager;

//        [TestInitialize]
//        public void Initialize()
//        {
//            CollectionManager = new LibraryManager();
//            CollectionManager.CreateDefaultData(); // making 7 items
//        }

//        [TestMethod]
//        public void RemoveItemTest()
//        {
//            List<Item> TempList = CollectionManager.GetAllItems();

//            CollectionManager.RemoveItem(TempList[5]);

//            Assert.IsTrue(TempList.Count == 17);
//        }
//    }

//}

