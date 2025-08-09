using System;
using System.Collections.Generic;
using Carteado.Enums;

namespace Carteado.Models;

public class Baralho
{
    private static readonly Random random = new();

    private readonly List<Carta> Cartas;

    public Baralho()
    {
        Cartas = CriarBaralho();
    }

    public List<Carta> CriarBaralho()
    {
        var baralho = new List<Carta>();

        var valores = new int[] { 1, 2, 3, 4, 5 };
        var naipes = Enum.GetValues<Elemento>();

        foreach (var v in valores)
        {
            foreach (var n in naipes)
            {
                baralho.Add(new Carta(n, v));
            }
        }

        return baralho;
    }

    public void Embaralhar()
    {
        for (var n = Cartas.Count - 1; n > 0; --n)
        {
            int k = random.Next(n + 1);
            (Cartas[k], Cartas[n]) = (Cartas[n], Cartas[k]);
        }
    }

    public void Display()
    {
        for (var i = 0; i < Cartas.Count; ++i)
        {
            if (i % 5 == 0) { Console.WriteLine(); }

            Cartas[i].Display();
        }
    }
}