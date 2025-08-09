using Carteado.Models;

namespace Carteado;

internal class Program
{
    static void Main(string[] args)
    {
        var baralho = new Baralho();

        baralho.Display();
        baralho.Embaralhar();
        System.Console.WriteLine();
        baralho.Display();
    }
}