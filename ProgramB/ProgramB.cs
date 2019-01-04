using Prism;

namespace ProgramB
{
    public class ProgramB : IContract
    {
        [Description("Zaokrąglona suma dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda1(double a, double b)
        {
            return System.Math.Round(a + b);
        }
        [Description("Zaokrąglona różnica dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda2(double a, double b)
        {
            return System.Math.Round(a - b);
        }
        [Description("Zaokrąglony iloczyn dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda3(double a, double b)
        {
            return System.Math.Round(a * b);
        }
        [Description("Zaokrąglony iloraz dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda4(double a, double b)
        {
            return System.Math.Round(a / b);
        }
    }
}
