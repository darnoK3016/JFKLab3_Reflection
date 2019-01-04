using Prism;

namespace ProgramA
{
    public class ProgramA : IContract
    {
        [Description("Liczba najmniejsza", Copyright = "Konrad Bandurski")]
        public double Metoda1(double a, double b)
        {
            return System.Math.Min(a, b);
        }
        [Description("Liczba największa", Copyright = "Konrad Bandurski")]
        public double Metoda2(double a, double b)
        {
            return System.Math.Max(a, b);
        }
        [Description("Logarytm liczby 'a' o podstawie 'b'", Copyright = "Konrad Bandurski")]
        public double Metoda3(double a, double b)
        {
            return System.Math.Log(a, b);
        }
        [Description("Zwraca kąt, którego styczna jest ilorazem dwóch liczb", Copyright = "Konrad Bandurski")]
        public double Metoda4(double a, double b)
        {
            return System.Math.Atan2(a,b);
        }
    }
 }