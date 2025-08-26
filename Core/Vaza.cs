using System;
using System.Collections.Generic;
using System.Linq;
using Carteado.Enums;

namespace Carteado.Core;

public class JogoVaza
{
    private readonly List<Carta> baralho;
    private readonly List<Jogador> jogadores;
    private readonly Random random;

    public JogoVaza()
    {
        baralho = [];
        jogadores = [];
        random = new Random();
    }

    public void CriarBaralho()
    {
        baralho.Clear();
        foreach (Naipe naipe in Enum.GetValues<Naipe>())
        {
            for (int valor = 1; valor <= 5; ++valor)
            {
                baralho.Add(new Carta(valor, naipe));
            }
        }
    }

    public void Embaralhar()
    {
        for (int i = baralho.Count - 1; i > 0; --i)
        {
            int j = random.Next(i + 1);
            (baralho[j], baralho[i]) = (baralho[i], baralho[j]);
        }
    }

    public void AdicionarJogador()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Jogador: ");
        Console.ResetColor();

        var jogador = Console.ReadLine()!;
        jogadores.Add(new Jogador(jogador));
    }

    public void DistribuirCartas()
    {
        foreach (var jogador in jogadores)
        {
            jogador.Mao.Clear();
            for (int i = 0; i < 5; ++i)
            {
                if (baralho.Count > 0)
                {
                    jogador.ReceberCarta(baralho[0]);
                    baralho.RemoveAt(0);
                }
            }
        }
    }

    public static bool NaipeGanha(Naipe atacante, Naipe defensor)
    {
        if (atacante == Naipe.Spock && defensor == Naipe.Pedra
            || defensor == Naipe.Spock && atacante == Naipe.Pedra)
        {
            Console.WriteLine("\nSpock vaporiza Pedra.");
        }

        if (atacante == Naipe.Spock && defensor == Naipe.Tesoura
            || defensor == Naipe.Spock && atacante == Naipe.Tesoura)
        {
            Console.WriteLine("\nSpock esmaga Tesoura.");
        }

        if (atacante == Naipe.Pedra && defensor == Naipe.Lagarto
            || defensor == Naipe.Pedra && atacante == Naipe.Lagarto)
        {
            Console.WriteLine("\nPedra esmaga Lagarto.");
        }

        if (atacante == Naipe.Pedra && defensor == Naipe.Tesoura
            || defensor == Naipe.Pedra && atacante == Naipe.Tesoura)
        {
            Console.WriteLine("\nPedra quebra Tesoura.");
        }

        if (atacante == Naipe.Lagarto && defensor == Naipe.Spock
            || defensor == Naipe.Lagarto && atacante == Naipe.Spock)
        {
            Console.WriteLine("\nLagarto envenena Spock.");
        }

        if (atacante == Naipe.Lagarto && defensor == Naipe.Papel
            || defensor == Naipe.Lagarto && atacante == Naipe.Papel)
        {
            Console.WriteLine("\nLagarto come Papel.");
        }

        if (atacante == Naipe.Tesoura && defensor == Naipe.Lagarto
            || defensor == Naipe.Tesoura && atacante == Naipe.Lagarto)
        {
            Console.WriteLine("\nTesoura decapita Lagarto.");
        }

        if (atacante == Naipe.Tesoura && defensor == Naipe.Papel
            || defensor == Naipe.Tesoura && atacante == Naipe.Papel)
        {
            Console.WriteLine("\nTesoura corta Papel.");
        }

        if (atacante == Naipe.Papel && defensor == Naipe.Spock
            || defensor == Naipe.Papel && atacante == Naipe.Spock)
        {
            Console.WriteLine("\nPapel refuta Spock.");
        }

        if (atacante == Naipe.Papel && defensor == Naipe.Pedra
            || defensor == Naipe.Papel && atacante == Naipe.Pedra)
        {
            Console.WriteLine("\nPapel cobre Pedra.");
        }

        return (atacante == Naipe.Spock && defensor == Naipe.Pedra) ||
               (atacante == Naipe.Spock && defensor == Naipe.Tesoura) ||
               (atacante == Naipe.Pedra && defensor == Naipe.Lagarto) ||
               (atacante == Naipe.Pedra && defensor == Naipe.Tesoura) ||
               (atacante == Naipe.Lagarto && defensor == Naipe.Spock) ||
               (atacante == Naipe.Lagarto && defensor == Naipe.Papel) ||
               (atacante == Naipe.Tesoura && defensor == Naipe.Lagarto) ||
               (atacante == Naipe.Tesoura && defensor == Naipe.Papel) ||
               (atacante == Naipe.Papel && defensor == Naipe.Spock) ||
               (atacante == Naipe.Papel && defensor == Naipe.Pedra);
    }

    public void Jogar()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("CARTEADO");
        Console.ResetColor();

        CriarBaralho();
        Embaralhar();

        AdicionarJogador();
        AdicionarJogador();

        DistribuirCartas();

        bool jogoAtivo = true;
        int rodada = 1;

        while (jogoAtivo)
        {
            Console.Write("\nRODADA");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" {rodada} ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[{baralho.Count}] cartas restantes");
            Console.ResetColor();

            List<Carta> cartasJogadas = [];
            List<Jogador> jogadoresQueJogaram = [];

            foreach (var jogador in jogadores)
            {
                if (jogador.TemCartas())
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n{jogador.Nome}, suas cartas:");
                    Console.ResetColor();
                    for (int i = 0; i < jogador.Mao.Count; i++)
                    {
                        Console.Write($"{i + 1}. ");
                        jogador.Mao[i].Display();
                        Console.WriteLine();
                    }

                    int escolha = 0;
                    bool valido = false;

                    while (!valido)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"\nEscolha uma carta (1-{jogador.Mao.Count}): ");
                        Console.ResetColor();
                        string input = Console.ReadLine()!;

                        try
                        {
                            escolha = Convert.ToInt32(input);
                            if (escolha >= 1 && escolha <= jogador.Mao.Count)
                            {
                                valido = true;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine($"Digite um número entre 1 e {jogador.Mao.Count}");
                                Console.ResetColor();
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Digite um número válido!");
                            Console.ResetColor();
                        }
                    }

                    var cartaJogada = jogador.JogarCarta(escolha - 1);
                    cartasJogadas.Add(cartaJogada!);
                    jogadoresQueJogaram.Add(jogador);
                    Console.Write($"{jogador.Nome} jogou: ");
                    cartaJogada!.Display();
                    Console.WriteLine();
                }
            }

            var cartaVencedora = cartasJogadas[0];
            var jogadorVencedor = jogadoresQueJogaram[0];
            bool empate = false;

            for (int i = 1; i < cartasJogadas.Count; i++)
            {
                if (cartasJogadas[i].Valor < cartaVencedora.Valor)
                {
                    if (!NaipeGanha(cartaVencedora.Naipe, cartasJogadas[i].Naipe))
                    {
                        cartaVencedora = cartasJogadas[i];
                        jogadorVencedor = jogadoresQueJogaram[i];
                        empate = false;
                    }
                }
                else if (cartasJogadas[i].Valor == cartaVencedora.Valor)
                {
                    if (NaipeGanha(cartasJogadas[i].Naipe, cartaVencedora.Naipe))
                    {
                        cartaVencedora = cartasJogadas[i];
                        jogadorVencedor = jogadoresQueJogaram[i];
                        empate = false;
                    }
                    else if (!NaipeGanha(cartaVencedora.Naipe, cartasJogadas[i].Naipe))
                    {
                        empate = true;
                    }
                }
                else
                {
                    if (NaipeGanha(cartasJogadas[i].Naipe, cartaVencedora.Naipe))
                    {
                        cartaVencedora = cartasJogadas[i];
                        jogadorVencedor = jogadoresQueJogaram[i];
                        empate = false;
                    }
                }
            }

            if (empate)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nEmpate na rodada! Nenhum jogador compra cartas.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n{jogadorVencedor.Nome} venceu a rodada com {cartaVencedora}!");
                Console.ResetColor();

                var perdedor = jogadoresQueJogaram.First(j => j != jogadorVencedor);
                int cartasParaComprar = cartaVencedora.Valor;

                if (baralho.Count >= cartasParaComprar)
                {
                    List<Carta> cartasCompradas = [.. baralho.Take(cartasParaComprar)];
                    perdedor.ComprarCartas(cartasCompradas);
                    baralho.RemoveRange(0, cartasParaComprar);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{perdedor.Nome} comprou {cartasParaComprar} carta(s).");
                    Console.ResetColor();
                }
                else if (baralho.Count > 0)
                {
                    perdedor.ComprarCartas(baralho);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{perdedor.Nome} comprou {baralho.Count} carta(s) restantes.");
                    Console.ResetColor();
                    baralho.Clear();
                }
            }

            foreach (var jogador in jogadores)
            {
                if (!jogador.TemCartas())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{jogador.Nome} venceu o jogo!");
                    Console.ResetColor();
                    jogoAtivo = false;
                    break;
                }
            }

            if (baralho.Count == 0 && jogoAtivo)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nBaralho vazio!\n");
                Console.ResetColor();

                Console.WriteLine($"Placar");
                foreach (var jogador in jogadores)
                {
                    Console.WriteLine($"{jogador.Nome} - {jogador.Mao.Count} carta(s)");
                }

                var jogadorComMenosCartas = jogadores.OrderBy(j => j.Mao.Count).First();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"\n{jogadorComMenosCartas.Nome}");
                Console.ResetColor();
                Console.WriteLine(" venceu o jogo com menos cartas!");
                jogoAtivo = false;
                break;
            }

            rodada++;
        }
    }
}