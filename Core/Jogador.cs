using System.Collections.Generic;

namespace Carteado.Core;

public class Jogador
{
    public string Nome { get; set; }
    public List<Carta> Mao { get; set; }

    public Jogador(string nome)
    {
        Nome = nome;
        Mao = [];
    }

    public void ReceberCarta(Carta carta)
    {
        Mao.Add(carta);
    }

    public Carta? JogarCarta(int index)
    {
        if (index >= 0 && index < Mao.Count)
        {
            Carta carta = Mao[index];
            Mao.RemoveAt(index);
            return carta;
        }
        return null;
    }

    public void ComprarCartas(List<Carta> cartas)
    {
        Mao.AddRange(cartas);
    }

    public bool TemCartas()
    {
        return Mao.Count > 0;
    }

    public int CartasNaMao()
    {
        return Mao.Count;
    }
}