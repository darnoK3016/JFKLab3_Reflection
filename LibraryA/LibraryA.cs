using Prism;

namespace LibraryA
{
    public class LibraryA : IContract
    {
        [Description("Suma dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda1(double a, double b)
        {
            return a + b;
        }
        [Description("Różnica dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda2(double a, double b)
        {
            return a - b;
        }
        [Description("Iloczyn dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda3(double a, double b)
        {
            return a * b;
        }
        [Description("Iloraz dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda4(double a, double b)
        {
            return a / b;
        }
    }
}
