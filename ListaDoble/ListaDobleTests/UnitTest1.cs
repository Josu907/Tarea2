using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ListaDobleTests
{
    [TestClass]
    public class ListaDobleTests
    {
        [TestMethod]
        public void InsertInOrder_ShouldInsertCorrectly()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();

            // Act
            lista.InsertInOrder(5);
            lista.InsertInOrder(2);
            lista.InsertInOrder(10);
            
            // Assert
            Assert.AreEqual(2, lista.DeleteFirst()); // El primer valor debería ser 2
            Assert.AreEqual(5, lista.DeleteFirst()); // Luego el 5
            Assert.AreEqual(10, lista.DeleteFirst()); // Finalmente el 10
        }

        [TestMethod]
        public void DeleteFirst_ShouldDeleteCorrectly()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(5);
            lista.InsertInOrder(2);
            lista.InsertInOrder(10);

            // Act
            int deletedValue = lista.DeleteFirst();

            // Assert
            Assert.AreEqual(2, deletedValue);
        }

        [TestMethod]
        public void DeleteLast_ShouldDeleteCorrectly()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(5);
            lista.InsertInOrder(2);
            lista.InsertInOrder(10);

            // Act
            int deletedValue = lista.DeleteLast();

            // Assert
            Assert.AreEqual(10, deletedValue);
        }

        [TestMethod]
        public void GetMiddle_ShouldReturnMiddleElement()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();
            lista.InsertInOrder(5);
            lista.InsertInOrder(2);
            lista.InsertInOrder(10);

            // Act
            int middleValue = lista.GetMiddle();

            // Assert
            Assert.AreEqual(5, middleValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetMiddle_EmptyList_ShouldThrowException()
        {
            // Arrange
            ListaDoble lista = new ListaDoble();

            // Act & Assert
            lista.GetMiddle(); // Debe lanzar una excepción
        }

        [TestMethod]
        public void MergeSorted_Ascending_ShouldMergeCorrectly()
        {
            // Arrange
            ListaDoble listaA = new ListaDoble();
            listaA.InsertInOrder(0);
            listaA.InsertInOrder(2);
            listaA.InsertInOrder(6);

            ListaDoble listaB = new ListaDoble();
            listaB.InsertInOrder(1);
            listaB.InsertInOrder(5);
            listaB.InsertInOrder(7);

            // Act
            listaA.MergeSorted(listaA, listaB, SortDirection.Asc);

            // Assert
            Assert.AreEqual(0, listaA.DeleteFirst());
            Assert.AreEqual(1, listaA.DeleteFirst());
            Assert.AreEqual(2, listaA.DeleteFirst());
            Assert.AreEqual(5, listaA.DeleteFirst());
            Assert.AreEqual(6, listaA.DeleteFirst());
            Assert.AreEqual(7, listaA.DeleteFirst());
        }

        [TestMethod]
        public void MergeSorted_Descending_ShouldMergeCorrectly()
        {
            // Arrange
            ListaDoble listaA = new ListaDoble();
            listaA.InsertInOrder(6);
            listaA.InsertInOrder(4);

            ListaDoble listaB = new ListaDoble();
            listaB.InsertInOrder(5);
            listaB.InsertInOrder(3);

            // Act
            listaA.MergeSorted(listaA, listaB, SortDirection.Desc);

            // Assert
            Assert.AreEqual(3, listaA.DeleteFirst());
            Assert.AreEqual(5, listaA.DeleteFirst());
            Assert.AreEqual(6, listaA.DeleteFirst());
            Assert.AreEqual(4, listaA.DeleteFirst());
        }
    }
}
