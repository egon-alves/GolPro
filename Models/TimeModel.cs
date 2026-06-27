using System;

namespace GolPro.Models
{
    public class TimeModel
    {

        private string _codigo;
        private string _nome;
        private string _cidade;
        private int _pontos;
        private int _vitorias;
        private int _empates;
        private int _derrotas;
        private int _golsPro;
        private int _golsContra;


        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Cidade
        {
            get { return _cidade; }
            set { _cidade = value; }
        }

        public int Pontos
        {
            get { return _pontos; }
            set { _pontos = value; }
        }

        public int Vitorias
        {
            get { return _vitorias; }
            set { _vitorias = value; }
        }

        public int Empates
        {
            get { return _empates; }
            set { _empates = value; }
        }

        public int Derrotas
        {
            get { return _derrotas; }
            set { _derrotas = value; }
        }

        public int GolsPro
        {
            get { return _golsPro; }
            set { _golsPro = value; }
        }

        public int GolsContra
        {
            get { return _golsContra; }
            set { _golsContra = value; }
        }

        public int SaldoGols
        {
            get { return _golsPro - _golsContra; }
        }



        public TimeModel()
        {

            _codigo = "";
            _nome = "";
            _cidade = "";
            _pontos = 0;
            _vitorias = 0;
            _empates = 0;
            _derrotas = 0;
            _golsPro = 0;
            _golsContra = 0;

        }

        public TimeModel(String codigo, string nome, string cidade)
        {

            _codigo = codigo;
            _nome = nome;
            _cidade = cidade;
            _pontos = 0;
            _vitorias = 0;
            _empates = 0;
            _derrotas = 0;
            _golsPro = 0;
            _golsContra = 0;

        }



        /// <summary>
        /// Registra o resultado de uma partida, atualizando gols pró/contra, vitórias, empates, derrotas e pontos do time.
        /// </summary>
             public void RegistrarPartida(int golsMarcados, int golsSofridos)
        {
            _golsPro += golsMarcados;
            _golsContra += golsSofridos;

            if(golsMarcados > golsSofridos)
            {
                _vitorias++;
                _pontos += 3;
            }
            else if (golsMarcados == golsSofridos)
            {
                _empates++;
                _pontos += 1;
            }
            else
            {
                _derrotas++;
            }

        }

   
        /// <summary>
        /// Estorna o resultado de uma partida, revertendo gols pró/contra, vitórias, empates, derrotas e pontos do time.
        /// </summary>
        public void EstornarResultado(int golsMarcados, int golsSofridos)
        {
            _golsPro -= golsMarcados;
            _golsContra -= golsSofridos;

            if(golsMarcados > golsSofridos)
            {
                _vitorias--;
                _pontos -= 3;
            }
            else if (golsMarcados == golsSofridos)
            {
                _empates--;
                _pontos -= 1;
            }
            else
            {
                _derrotas--;
            }

        }



        /// <summary>
        /// Serializa os dados do time em uma string formatada separada por ponto e vírgula.
        /// </summary>
        public string Serializar()
        {
            return $"{_codigo};{_nome};{_cidade};{_pontos};{_vitorias};{_empates};{_derrotas};{_golsPro};{_golsContra}";
        }

    }

}