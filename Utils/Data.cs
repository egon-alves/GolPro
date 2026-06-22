using System.IO;
using System.Collections.Generic;
using GolPro.Models;

namespace GolPro.Utils
{
    public class Data
    {
        private string _arquivo;

        public Data(string arquivo)
        {
            this._arquivo = arquivo;
            Directory.CreateDirectory(Path.GetDirectoryName(arquivo));
        }

        public void SalvarTimes(List<TimeModel> times)
        {
            using (StreamWriter sw = new StreamWriter(this._arquivo))
            {
                foreach (TimeModel time in times)
                {
                    sw.WriteLine(time.Serializar()); // reutiliza o método do Model
                }
            }
        }

        public List<TimeModel> CarregarTimes()
        {
            List<TimeModel> times = new List<TimeModel>();

            if (!File.Exists(this._arquivo))
                return times; // arquivo ainda não existe, retorna lista vazia

            using (StreamReader sr = new StreamReader(this._arquivo))
            {
                string linha;
                while ((linha = sr.ReadLine()) != null)
                {
                    string[] partes = linha.Split(';');
                    if (partes.Length == 9)
                    {
                        // TryParse: se o valor for inválido, usa 0 em vez de quebrar
                        int.TryParse(partes[3], out int pontos);
                        int.TryParse(partes[4], out int vitorias);
                        int.TryParse(partes[5], out int empates);
                        int.TryParse(partes[6], out int derrotas);
                        int.TryParse(partes[7], out int golsPro);
                        int.TryParse(partes[8], out int golsContra);

                        TimeModel time = new TimeModel(partes[0], partes[1], partes[2])
                        {
                            Pontos    = pontos,
                            Vitorias  = vitorias,
                            Empates   = empates,
                            Derrotas  = derrotas,
                            GolsPro   = golsPro,
                            GolsContra = golsContra
                        };

                        times.Add(time);
                    }
                }
            }

            return times;
        }
    

    // Area de Jogadores
    
        public void SalvarJogador(List<JogadorModel> jogadores)
        {
            using(StreamWriter sw = new StreamWriter(this._arquivo))
            {
                foreach(JogadorModel jogador in jogadores)
                {
                    sw.WriteLine(jogador.Serializar());
                }
            }
        }
    


        public List<JogadorModel> CarregarJogadores()
    {
        List<JogadorModel> jogadores = new List<JogadorModel>();

        if (!File.Exists(this._arquivo))
            return jogadores;

        using (StreamReader sr = new StreamReader(this._arquivo))
        {
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                string[] partes = linha.Split(';');
                if (partes.Length == 6)  // 6 campos, não 9
                {
                    int.TryParse(partes[4], out int numeroCamisa);
                    int.TryParse(partes[5], out int golsMarcados);

                    JogadorModel jogador = new JogadorModel(partes[0], partes[1], partes[2], partes[3], numeroCamisa)
                    {
                        GolsMarcados = golsMarcados
                    };

                    jogadores.Add(jogador);
                }
            }
        }

        return jogadores;
    }

    public void SalvarPartidas(List<PartidaModel> partidas)
    {
        using(StreamWriter sw = new StreamWriter(this._arquivo))
        {
            foreach(PartidaModel partida in partidas)
            {
                sw.WriteLine(partida.Serializar());
            }
        }
    }

    public List<PartidaModel> CarregarPartidas()
    {

        List<PartidaModel> partidas = new List<PartidaModel>();

        if (!File.Exists(this._arquivo))
            return partidas;
        
        using(StreamReader sr = new StreamReader(this._arquivo))
        {
            string linha;
            while ((linha = sr.ReadLine()) != null)
            {
                string[] partes = linha.Split(';');
                if (partes.Length == 6)
                {
                    int.TryParse(partes[0], out int id);
                    DateTime.TryParseExact(partes[3], "yyyyMMdd",
                        null, System.Globalization.DateTimeStyles.None, out DateTime data);
                    int.TryParse(partes[4], out int golsMandante);
                    int.TryParse(partes[5], out int golsVisitante);

                    PartidaModel partida = new PartidaModel(
                        id, partes[1], partes[2], data, golsMandante, golsVisitante
                    );
                    partidas.Add(partida);
                }
            }
        }

        return partidas;
    }
    
    }
}
