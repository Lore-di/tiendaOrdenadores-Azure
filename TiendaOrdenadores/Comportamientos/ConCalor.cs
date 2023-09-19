namespace TiendaOrdenadoresA.Comportamientos
{
    public class ConCalor : ICalor
    {

        public ConCalor(int calor)
        {
            Calor = calor;
        }

        public int Calor { get; }
    }
}