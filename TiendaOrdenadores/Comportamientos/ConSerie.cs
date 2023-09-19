namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConSerie : ISerie
    {
        public ConSerie(string serie)
        {
            NumeroSerie = serie;
        }

        public string NumeroSerie { get; set; }
    }
}