using System;
using System.Collections.Generic;
using GolPro.Utils;
using GolPro.Models;
using GolPro.Controller;

namespace GolPro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Tela tela = new Tela(ConsoleColor.Black, ConsoleColor.Green);

            // col=2, row=2, width=77, height=22  →  moldura de (2,2) até (79,24)
            TimeController timeController = new TimeController(2, 2, 77, 22, tela);
            JogadorController jogadorController = new JogadorController(2, 2, 77, 22, tela, timeController.Times);
            PartidaController partidaController = new PartidaController(2, 2, 77, 22, tela, timeController, jogadorController);

            List<string> listMenu = new List<string>
            {
                "1 - Cadastros de Times",
                "2 - Cadastros de Jogadores",
                "3 - Registro de Partidas",
                "4 - Tabela de Classificação",
                "5 - Estatísticas de Jogadores",
                "0 - Sair     "
            };

            string menuEscolhido = "";

            while (true)
            {
                try
                {
                    tela.PrepararTela("GolPRO - Sistema de registro", 2, 2, 79, 24);

                    int navCol = 6;
                    int navRow = 19;
                    Console.SetCursorPosition(navCol, navRow++); Console.Write("Navegação:");
                    Console.SetCursorPosition(navCol, navRow++); Console.Write("↑ ↓    Move a seleção");
                    Console.SetCursorPosition(navCol, navRow++); Console.Write("ENTER  Confirma a opção");
                    

                    menuEscolhido = tela.MostrarMenu(listMenu);

                    if (menuEscolhido == "0")
                    {
                        Console.ResetColor();
                        Console.Clear();
                        Console.WriteLine("Até mais!");
                        break;
                    }
                    else if (menuEscolhido == "1")
                    {
                        timeController.CRUD();
                    }
                    else if (menuEscolhido == "2")
                    {
                        jogadorController.CRUD();
                    }
                    else if (menuEscolhido == "3")
                    {
                        partidaController.CRUD();
                    }
                    else if(menuEscolhido == "4") {
                        partidaController.Relatorio();
                    }
                    else if(menuEscolhido == "5") {
                        partidaController.ReportArtilheiros();
                    }
                }
                catch (VoltarMenuException)
                {
                    Console.SetCursorPosition(2, 23);
                    Console.Write("Operação cancelada. Voltando ao menu...");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
    }
}
