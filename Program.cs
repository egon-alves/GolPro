// Program.cs
using System;

namespace GolPro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tela tela = new Tela(ConsoleColor.Yellow, ConsoleColor.DarkGreen);
            tela.PrepararTela("GolPRO - Sistema de registro", 0, 0, 79, 24);

            // mostrar um menu
            List<string> opcoes = new List<string>();
            opcoes.Add("1 - Opção A  ");
            opcoes.Add("0 - Sair     ");
            string op = Tela.MostrarMenu(2, 2, opcoes);
        }
    }
}