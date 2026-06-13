using System;
using System.Collections.Generic;
using GolPro.utils;
using System;

namespace GolPro
{
    internal class Program
    {
        static void Main(string[] args)
        {

        
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Tela tela = new Tela(ConsoleColor.Yellow, ConsoleColor.Black);

            // Menu configurado em uma lista
            List<string> listMenu = new List<string>
            {
             "1 - Cadastros de Times",
             "2 - Cadastros de Jogadores",
             "3 - Registro de Pardidas",
             "4 - Tabela de Classificação",
             "5 - Estatísticas de Jogadores",
             "0 - Sair     "

             };
            string menuEscolhido = "";

            while (true)
            {
                // Desenha a interface e lê a opção em uma única chamada controlada pelo loop
                tela.PrepararTela("GolPRO - Sistema de registro", 2, 2, 79, 24);
                menuEscolhido = tela.MostrarMenu(listMenu);

                if (menuEscolhido == "0")
                {
                    Console.ResetColor();
                    Console.Clear();
                    Console.WriteLine("Até mais!");
                    break; // Sai do loop e encerra o programa
                }

                if (menuEscolhido == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Cadastro de Jogador");

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey(); // Aguarda o usuário interagir antes de reiniciar o loop
                }
                else if (menuEscolhido == "2")
                {
                    Console.Clear();
                    Console.WriteLine("Cadastro de Time");

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey(); // Aguarda o usuário interagir antes de reiniciar o loop
                }
                else if (menuEscolhido == "3")
                {
                    Console.Clear();
                    Console.WriteLine("Registro de Gol");

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey(); // Aguarda o usuário interagir antes de reiniciar o loop
                }
            }
        }
    }
}