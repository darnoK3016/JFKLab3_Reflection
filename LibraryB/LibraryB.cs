using Prism;

namespace LibraryB
{
    public class LibraryB : IContract
    {
        [Description("Obliczanie pole trójkąta", Copyright = "Konrad Bandurski")]
        public double Metoda1(double a, double b)
        {
            return a * b / 2;
        }
        [Description("Obliczanie pola prostokąta", Copyright = "Konrad Bandurski")]
        public double Metoda2(double a, double b)
        {
            return a * b;
        }
        [Description("Potęgowanie", Copyright = "Konrad Bandurski")]
        public double Metoda3(double a, double b)
        {
            return System.Math.Pow(a, b);
        }
        [Description("Pierwiastkowanie n stopnia", Copyright = "Konrad Bandurski")]
        public double Metoda4(double a, double b)
        {
            return System.Math.Pow(a,-b);
        }
    }
}
