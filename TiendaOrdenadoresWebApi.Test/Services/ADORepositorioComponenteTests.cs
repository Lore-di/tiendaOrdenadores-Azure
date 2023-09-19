using Microsoft.AspNetCore.Mvc;
using TiendaOrdenadoresWebApi.Controllers;
using TiendaOrdenadoresWebApi.Models;

namespace TiendaOrdenadoresWebApi.Services.Tests
{
    [TestClass()]
    public class ADORepositorioComponenteTests
    {
        private readonly ComponentesController controlador;

        public ADORepositorioComponenteTests()
        {
            var fakeRepo = new FakeADOComponenteRepositorio();
            controlador = new(new FakeADOComponenteRepositorio());
        }

        [TestMethod()]
        public void AddComponenteTest()
        {
            var resultado = controlador.Get() as OkObjectResult;
            Assert.IsNotNull(resultado);

            var lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(6, lista.Count);

            Componente componente = new()
            {
                Id = 9,
                Descripcion = "ABC"
            };

            controlador.Post(componente);
            resultado = controlador.Get() as OkObjectResult;
            Assert.IsNotNull(resultado);

            lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            
            Assert.AreEqual(7, lista.Count);

            controlador.Delete(9);
            resultado = controlador.Get() as OkObjectResult;

            Assert.IsNotNull(resultado);
            lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(6, lista.Count);
        }

        [TestMethod()]
        public void BorraComponenteTest()
        {
            var resultado = controlador.Get() as OkObjectResult;
            Assert.IsNotNull(resultado);

            var lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(6, lista.Count);

            controlador.Delete(6);
            resultado = controlador.Get() as OkObjectResult;
            Assert.IsNotNull(resultado);
            lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(5, lista.Count);
        }

        [TestMethod()]
        public void BorraComponenteMalTest()
        {
            var resultado = controlador.Get() as OkObjectResult;
            Assert.IsNotNull(resultado);

            var lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(6, lista.Count);

            controlador.Delete(65);
            resultado = controlador.Get() as OkObjectResult;
            Assert.IsNotNull(resultado);

            lista = resultado.Value as List<Componente>;
            Assert.IsNotNull(lista);
            Assert.AreEqual(6, lista.Count);
        }

        [TestMethod()]
        public void ListaComponentesTest()
        {
            var resultado = controlador.Get() as OkObjectResult;

            Assert.IsNotNull(resultado);

            var lista = resultado.Value as List<Componente>;

            Assert.IsNotNull(lista);

            Assert.AreEqual(6, lista.Count);
        }

        [TestMethod()]
        public void TomaComponenteTest()
        {
            var resultado = controlador.Get(1) as OkObjectResult;
            Assert.IsNotNull(resultado);

            var componente = resultado.Value as Componente;
            Assert.IsNotNull(componente);

            Assert.AreEqual(1, componente.TipoComponente);
            Assert.AreEqual(100, componente.Coste);
            Assert.AreEqual("Banco de memoria SDRAM", componente.Descripcion);
        }

        [TestMethod()]
        public void TomaComponenteTestMal()
        {
            var resultado = controlador.Get(90) as OkObjectResult;
            Assert.IsNotNull(resultado);

            var componente = resultado.Value as Componente;
            Assert.IsNotNull(componente);
            Assert.AreEqual(0, componente.Coste);
            Assert.AreEqual(0, componente.Calor);
        }

        [TestMethod()]
        public void UpdateComponenteTest()
        {
            var fakeRepo = new FakeADOComponenteRepositorio();
            var componenteActualizado = new Componente
            {
                Id = 1,
                Descripcion = "prueba",
                Coste = 999
            };

            var resultado = controlador.Put(1, componenteActualizado) as NoContentResult;
            Assert.IsNotNull(resultado);

            var componenteActual = fakeRepo.TomaComponente(1);
            Assert.IsNotNull(componenteActual);
            Assert.AreEqual(componenteActualizado.Coste, componenteActual.Coste);
            Assert.AreEqual(componenteActualizado.Descripcion, componenteActual.Descripcion);
        }
    }
}