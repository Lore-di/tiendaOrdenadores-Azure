using TiendaA01.CrossCuting.Logging;
using TiendaA01.Services;
using Microsoft.AspNetCore.Mvc;
using TiendaA01.Models;

namespace TiendaA01.Controllers.Tests
{
    [TestClass()]
    public class PedidosControllerTests
    {
        private readonly PedidosController _controlador = new(new FakeRepositorioPedido(),
            new FakeRepositorioOrdenador(), new LoggerManager());


        [TestMethod()]
        public void IndexTest()
        {
            var result = _controlador.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);

            var pedidos = result.ViewData.Model as List<Pedido>;
            Assert.IsNotNull(pedidos);
            Assert.AreEqual(2, pedidos.Count);

        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = _controlador.Details(1) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);

            var pedidos = result.ViewData.Model as Pedido;
            Assert.IsNotNull(pedidos);
            Assert.AreEqual("Maria", pedidos.Cliente);
        }


        [TestMethod()]
        public void DetailsMalTest()
        {
            var result = _controlador.Details(25) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNull(result.ViewData.Model);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var result = _controlador.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void PostCreateTest()
        {
            var listaComponentes = new List<Componente>();
            listaComponentes.Add(new Componente() { OrdenadorId = 3 });
            var listaOrdenadores = new List<Ordenador>();
            listaOrdenadores.Add(new Ordenador() { PedidoId = 1 });
            Pedido miPedido = new Pedido() { Cliente = "prueba", Ordenadores = listaOrdenadores, Id = 3 };
            

            Assert.IsNotNull(listaComponentes);
            Assert.IsNotNull(listaOrdenadores);

            var result = _controlador.Create(miPedido) as RedirectToActionResult;
            var result1 = _controlador.Index() as ViewResult;
            Assert.AreEqual("Index", result.ActionName);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result);

            var lista = result1.ViewData.Model as List<Pedido>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(3, lista.Count);
        }

        [TestMethod()]
        public void EditTest()
        {
            var result = _controlador.Edit(1) as ViewResult;
            Assert.IsNotNull(result);
            var pedido = result.ViewData.Model as Pedido;

            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual("Maria", pedido.Cliente);
        }

        [TestMethod()]
        public void EditMalTest()
        {
            var result = _controlador.Edit(90) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);

            var pedido = result.ViewData.Model as Pedido;
            Assert.IsNotNull(pedido);
            Assert.AreEqual("", pedido.NombrePedido);
        }

        [TestMethod()]
        public void PostEditTest()
        {
            var pedido = new Pedido()
            {
                Id = 2,
                Cliente = "prueba",
                NombrePedido = "pedido prueba"
            };

            var result = _controlador.Edit(pedido) as RedirectToActionResult;
            var result1 = _controlador.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(pedido);
            Assert.IsNotNull(result1);

            var lista = result1.ViewData.Model as List<Pedido>;

            Assert.IsNotNull(lista);

            var pedidoLista = lista.FirstOrDefault(p => p.Id == 2);

            Assert.IsNotNull(pedidoLista);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("prueba", pedidoLista.Cliente);
            Assert.AreEqual(pedido.NombrePedido, pedidoLista.NombrePedido);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var result = _controlador.Delete(2) as ViewResult;
            var pedido = _controlador.ViewData.Model as Pedido;

            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.AreEqual("Andres", pedido.Cliente);
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            var result = _controlador.DeleteConfirmed(2) as RedirectToActionResult;
            var result1 = _controlador.Index() as ViewResult;
            var lista = result1.ViewData.Model as List<Pedido>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(lista);

            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(1, lista.Count);

        }
    }
}