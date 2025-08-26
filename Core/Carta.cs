using System;
using System.Text;
using Carteado.Enums;

namespace Carteado.Core;

public class Carta
{
    public int Valor { get; set; }
    public Naipe Naipe { get; set; }

    public Carta(int valor, Naipe naipe)
    {
        Valor = valor;
        Naipe = naipe;
    }

    public override string ToString() =>
        $"{Valor} de {Naipe}";

    private static string GetChar(Naipe naipe)
    {
        return naipe switch
        {
            Naipe.Pedra => "Pedra",
            Naipe.Papel => "Papel",
            Naipe.Tesoura => "Tesoura",
            Naipe.Lagarto => "Lagarto",
            Naipe.Spock => "Spock",
            _ => ""
        };
    }

    private static ConsoleColor GetColor(Naipe naipe)
    {
        return naipe switch
        {
            Naipe.Pedra => ConsoleColor.Red,
            Naipe.Papel => ConsoleColor.Yellow,
            Naipe.Tesoura => ConsoleColor.Magenta,
            Naipe.Lagarto => ConsoleColor.Green,
            Naipe.Spock => ConsoleColor.Blue,
            _ => ConsoleColor.DarkGray
        };
    }

    public void Display()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = GetColor(Naipe);
        Console.Write($"[{GetChar(Naipe)} {Valor}] ");
        Console.ResetColor();
    }
}