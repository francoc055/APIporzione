using System.Numerics;

internal class Program
{
    private static void Main(string[] args)
    {
        //Console.WriteLine(Solucionar(10).ToString());
        //Console.WriteLine(Solucionar(100).ToString());
        Console.WriteLine(Solucionar(BigInteger.Pow(2023, 100)));


    }

    public static BigInteger Solucionar(BigInteger n)
    {
        BigInteger a = 1;
        BigInteger b = 1;
        BigInteger c = 1;
        BigInteger d = 1;
        BigInteger modulo = BigInteger.Pow(10, 10);

        for (BigInteger i = 0; i < n; i++)
        {
            BigInteger resultado = (3 * d + c + 4 * b + a) % modulo;
            a = b;
            b = c;
            c = d;
            d = resultado;
        }

        return d;
    }
}