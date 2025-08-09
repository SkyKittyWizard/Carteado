using System;
using System.Text;
using Carteado.Enums;

namespace Carteado.Models;

public class Carta
{
    private Elemento Naipe { get; }
    private int Valor { get; }

    public Carta(Elemento naipe, int valor)
    {
        Naipe = naipe;
        Valor = valor;
    }

    public override string ToString() =>
        $"[{GetChar((char)Naipe)} {Valor}]";

    static char GetChar(char mana)
    {
        return mana switch
        {
            (char)Elemento.Fogo => '\u25ee',
            (char)Elemento.Agua => '\u2248',
            (char)Elemento.Raio => '\u2605',
            (char)Elemento.Terra => '\u26f0',
            (char)Elemento.Grama => '\u219f',
            _ => '\u25cf'
        };
    }

    static ConsoleColor GetColor(char mana)
    {
        return mana switch
        {
            (char)Elemento.Fogo => ConsoleColor.Red,
            (char)Elemento.Agua => ConsoleColor.Blue,
            (char)Elemento.Raio => ConsoleColor.Yellow,
            (char)Elemento.Terra => ConsoleColor.Magenta,
            (char)Elemento.Grama => ConsoleColor.Green,
            _ => ConsoleColor.DarkGray
        };
    }

    public void Display()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = GetColor((char)Naipe);
        Console.Write($"[{GetChar((char)Naipe)} {Valor}] ");
        Console.ResetColor();
    }
}