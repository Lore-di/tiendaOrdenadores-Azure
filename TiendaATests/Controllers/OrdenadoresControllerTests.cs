using TiendaA01.CrossCuting.Logging;
using TiendaA01.Services;
using Microsoft.AspNetCore.Mvc;
using TiendaA01.Models;

namespace TiendaA01.Controllers.Tests
{
    [TestClass()]
    public class OrdenadoresControllerTests
    {
        readonly OrdenadoresController _controlador = new(new FakeRepositorioPedido(), new FakeRepositorioOrdenador(), new FakeRepositorioComponente(), new LoggerManager());


        [TestMethod()]
        public void IndexEncontradoTest()
        {
            var result = _controlador.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);

            var ordenadores = result.ViewData.Model as List<Ordenador>;
            Assert.IsNotNull(ordenadores);
            Assert.AreEqual(2, ordenadores.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = _controlador.Details(2) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);

            var ordenador = result.ViewData.Model as Ordenador;
            Assert.IsNotNull(ordenador);
            Assert.AreEqual("Ordenador de Andres", ordenador.Descripcion);
        }

        [TestMethod]
        public void OrdenadoresDetalleVistaNoEncontradaTest()
        {
            var result = _controlador.Details(90) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ViewName);
            Assert.IsNull(result.ViewData.Model);
        }

        [TestMethod()]
        public void GetCreateTest()
        {
            var result = _controlador.Create() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void PostCreateTest()
        {
            var listaComponentes = new List<Componente>();
            listaComponentes.Add(new Componente(){OrdenadorId = 3});
            Assert.IsNotNull(listaComponentes);

            Ordenador miOrdenador = new Ordenador() { Descripcion = "nuevo Ordenador", Componentes = listaComponentes, Id = 3, PedidoId = 1 };
           
            var result = _controlador.Create() as ViewResult;
            
            Assert.IsNotNull(miOrdenador);
            Assert.IsNotNull(result);

            //var lista = 

            Assert.AreEqual("nuevo Ordenador", miOrdenador.Descripcion);
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod()]
        public void GetEditTest()
        {
            var result = _controlador.Edit(1) as ViewResult;
            Assert.IsNotNull(result);

            var ordenador = result.ViewData.Model as Ordenador;

            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual("Ordenador de Maria", ordenador.Descripcion);
        }

        [TestMethod]
        public void OrdenadoresEditVistaNoEncontradaTest()
        {
            var result = _controlador.Edit(90) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Ordenador));
            var ordenador = result.ViewData.Model as Ordenador;

            Assert.AreEqual("", ordenador.Descripcion);
        }

        /*     [TestMethod()]
             public void PostEditTest()
             {
                 var result = _controlador.Edit(3) as ViewResult;

                 Assert.Fail();
             }*/

        [TestMethod()]
        public void GetDeleteTest()
        {
            var result = _controlador.Delete(2) as ViewResult;
            var ordenador = _controlador.ViewData.Model as Ordenador;

            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.AreEqual("Ordenador de Andres", ordenador.Descripcion);
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            var result = _controlador.DeleteConfirmed(2) as RedirectToActionResult;
            var result1 = _controlador.Index() as ViewResult;
            var lista = result1.ViewData.Model as List<Ordenador>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(lista);

            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(1, lista.Count);
        }

        [TestMethod]
        public void OrdenadoresDeleteVistaNoEncontradaTest()
        {
            var result = _controlador.Delete(90) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var ordenador = result.ViewData.Model as Ordenador;
            Assert.IsNotNull(ordenador);
            Assert.AreEqual("", ordenador.Descripcion);
        }
    }
}
