using TiendaA01.CrossCuting.Logging;
using TiendaA01.Services;
using Microsoft.AspNetCore.Mvc;
using TiendaA01.Models;

namespace TiendaA01.Controllers.Tests
{
    [TestClass()]
    public class ComponentesControllerTests
    {
        readonly ComponentesController _controlador = new(new FakeRepositorioComponente(), new LoggerManager(),new FakeRepositorioOrdenador());


        [TestMethod()]
        public void IndexEncontradoTest()
        {
            var result = _controlador.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);

            var componentes = result.ViewData.Model as List<Componente>;
            Assert.IsNotNull(componentes);
            Assert.AreEqual(6, componentes.Count);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var result = _controlador.ComponenteDetails(2) as ViewResult;
            Assert.IsNotNull( result);
            Assert.AreEqual("ComponenteDetails", result.ViewName);

            var componente = result.ViewData.Model as Componente;
            Assert.IsNotNull(componente);
            Assert.AreEqual(10, componente.Calor);
        }

            [TestMethod]
            public void ComponentesDetalleVistaNoEncontradaTest()
            {
                var result = _controlador.ComponenteDetails(90) as ViewResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("ComponenteDetails", result.ViewName);
                Assert.IsNotNull(result.ViewData.Model);
                var componente = result.ViewData.Model as Componente;
                Assert.IsNotNull(componente);
                Assert.AreEqual(0, componente.Coste);
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
            Componente miComponente = new Componente() { Calor = 30, Cores = 10, Coste = 300,Descripcion = "bibibi", Id = 9, Megas = 99, Serie = "jijiji", TipoComponente = 3};
            var result = _controlador.Create(miComponente) as RedirectToActionResult;
            var result1 = _controlador.Index() as ViewResult;

            Assert.IsNotNull(result1);
            Assert.IsNotNull(miComponente);
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            var lista = result1.ViewData.Model as List<Componente>;
            Assert.IsNotNull(lista);

            var componenteCreado = lista.FirstOrDefault(p=>p.Id == 9);

            Assert.AreEqual(7, lista.Count);
            Assert.AreEqual("bibibi",  componenteCreado.Descripcion);
        }

        [TestMethod()]
        public void GetEditTest()
        {
            var result = _controlador.Edit(3) as ViewResult;
            Assert.IsNotNull(result);
            var componente = result.ViewData.Model as Componente;

            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual(9, componente.Coste);
        }

        [TestMethod]
        public void ComponentesEditVistaNoEncontradaTest()
        {
            var result = _controlador.Edit(90) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var componente = result.ViewData.Model as Componente;
            Assert.IsNotNull(componente);
            Assert.AreEqual(0, componente.Coste);
        }

             [TestMethod()]
             public void PostEditTest()
             {
                 var componente = new Componente()
                 {
                     Id = 3,
                     Calor = 899
                 };

                 var result = _controlador.Edit(componente) as RedirectToActionResult;
                 var result1 = _controlador.Index() as ViewResult;

                 Assert.IsNotNull(result);
                 Assert.IsNotNull(componente);
                 Assert.IsNotNull(result1);

                 var lista = result1.ViewData.Model as List<Componente>;

                 Assert.IsNotNull(lista);

                 var componenteLista = lista.FirstOrDefault(p => p.Id == 3);

                 Assert.IsNotNull(componenteLista);
                 Assert.AreEqual("Index", result.ActionName);
                 Assert.AreEqual(899, componenteLista.Calor);
                 Assert.AreEqual(componente.Calor, componenteLista.Calor);
             }

        [TestMethod()]
        public void GetDeleteTest()
        {
            var result = _controlador.Delete(3) as ViewResult;
            var componente = _controlador.ViewData.Model as Componente;

            Assert.IsNotNull(result);
            Assert.AreEqual("Delete",  result.ViewName);
            Assert.AreEqual(9, componente.Coste);
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            var result = _controlador.DeleteConfirmed(3) as RedirectToActionResult;
            var result1 = _controlador.Index() as ViewResult;
            var lista = result1.ViewData.Model as List<Componente>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(lista);

            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(5, lista.Count);
        }

        [TestMethod]
        public void ComponentesDeleteVistaNoEncontradaTest()
        {
            var result = _controlador.Delete(90) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
            Assert.IsNotNull(result.ViewData.Model);
            var componente = result.ViewData.Model as Componente;
            Assert.IsNotNull(componente);
            Assert.AreEqual(0, componente.Coste);
        }
    }
}