using System;
using Carteado.Core;

namespace Carteado;

internal class Program
{
    static void Main(string[] args)
    {
        JogoVaza vaza = new();
        vaza.Jogar();

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}