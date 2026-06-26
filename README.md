## GolPro — Campeonato Esportivo
# 1 - Visão Geral O GolPro é um sistema de gerenciamento de campeonatos esportivos,
permitindo cadastrar times, jogadores e partidas, além de gerar automaticamente a
tabela de classificação.
# 2 - Funcionalidades Principais
- Cadastro de times com código, nome e cidade.
- Cadastro de jogadores com matrícula, nome, posição e vínculo ao time.
- Registro de partidas com times participantes, data e placar final.
- Atualização do contador de gols de cada jogador ao registrar uma partida.
- Relatório da tabela de classificação ordenada por pontos e saldo de gols.
- Relatório de artilheiros do campeonato.

# Começa o trabalho de desenvolvimento do sistema, seguindo as regras de negócio e requisitos do enunciado

# 3 - Regras de Negócio

RN01 - **Unicidade de Time:** Não é possível cadastrar dois times com o mesmo código. 
Garantido em: `TimeController.CRUD()` ao validar com `FindByCode()`.

RN02 - **Atribuição de Gols Exclusiva:** O gol de um jogador só pode ser registrado se o jogador pertencer ao time informado na partida. 
Garantido em: `PartidaController.RegistrarGolsJogadores()`.

RN03 - **Estorno de Estatísticas na Exclusão/Alteração:** Se uma partida for alterada ou excluída, os gols, pontos e saldo dos times, bem como os gols dos jogadores, devem ser estornados antes de aplicar o novo resultado. 
Garantido em: `PartidaController.CRUD()` e `PartidaController.EstornarGolsJogadores()`.

RN04 - **Pontuação da Partida:** A vitória vale 3 pontos, o empate 1 ponto e a derrota 0 pontos. 
Garantido em: `TimeModel.RegistrarPartida()` e `TimeModel.EstornarResultado()`.

RN05 - **Artilharia Mínima:** Apenas jogadores que marcaram pelo menos 1 gol (GolsMarcados > 0) podem aparecer no relatório de artilharia. 
Garantido em: `PartidaController.ReportArtilheiros()`.# 4 - Requisitos Funcionais

# 4 - Requisitos Funcionais

RF01 - O sistema deve permitir o cadastro de times, incluindo código, nome e cidade. 
Garantido em: `TimeController.CRUD()`

RF02 - O sistema deve permitir o cadastro de jogadores, incluindo matrícula, nome, posição e vínculo ao time.
Garantido em: `JogadorController.CRUD()`

RF03 - O sistema deve permitir o registro de partidas, incluindo os times participantes, data e placar final.
Garantido em: `PartidaController.CRUD()`

RF04 - O sistema deve atualizar o contador de gols de cada jogador ao registrar uma partida.
Garantido em: `JogadorModel.AdicionarGols()` e `PartidaController.RegistrarGolsJogadores()`

RF05 - O sistema deve gerar um relatório da tabela de classificação ordenada por pontos e saldo de gols.
Garantido em: `PartidaController.Relatorio()`

RF06 - O sistema deve gerar um relatório de artilheiros do campeonato.
Garantido em: `PartidaController.ReportArtilheiros()`


# 5 -  Requisitos Não Funcionais

# 6 -  Caso e uso 
UC01 - Cadastrar time
Ator : Usuaria do sistema
Pre-condição : Sistema em execução
Fluxo principal : 1 Usuario acessa " Cadastrar Time" 2 Usuario preenche os campos "Código", "Nome" e "Cidade" 3 Usuario clica em "Salvar"
FLuxo Alternativo : 3a Codigo já existe, o sistema exibe mensagem de erro "Código já cadastrado", oferece opção de 
"Voltar" para corrigir o código ou "Cancelar" para desistir do cadastro
Pós-condição : Time cadastrado com sucesso ou cadastro cancelado
Localização: TimeController.CRUD();

UC02 - Registrar partida
Ator: Usuaria do sistema
Pre-condição: PeloMenos 2 times cadastrados, sistema em execução
Fluxo Principal: 1. Usuário acessa 'Registro de Partidas' 2. Pressiona ENTER (nova partida) 3. Informa mandante, visitante, data e placar 4. Sistema valida (times existem, não são iguais) 5. Sistema pergunta gols por jogador 6. Estatísticas dos times e jogadores são atualizadas 7. Partida é salva
Fluxo Alternativo: 3a. Time não encontrado → mensagem de erro e nova entrada 3b. Times iguais → mensagem de erro e nova entrada
Pós condição: Partida registrada com sucesso, estatísticas atualizadas
Localização: PartidaController.CRUD();

UC03 — Gerar Tabela de Classificação
Ator: Usuária do sistema
Pre-condição: Pelo menos 1 Time cadastrado, sistema em execução
Fluxo Principal: 1. Usuário acessa 'Tabela de Classificação' 2. Sistema ordena times por pontos, saldo de gols e gols pró 3. Sistema exibe tabela formatada com moldura
Pós-codição: Tabela de classificação exibida corretamente
Localização: PartidaController.Relatorio();


UC04 - Gerar Relatório de Artilheiros

Ator: Usuária do sistema
Pre-condição: Pelo menos 1 gol registrado, sistema em execução
Fluxo Principal: Usuário acessa 'Artilheiros do Campeonato' 2. Sistema filtra jogadores com gols > 0 3. Sistema ordena por gols (desc) 4. Sistema exibe ranking formatado com moldura
Fluxo Alternativo: 2a. Nenhum gol registrado → mensagem informativa "Nenhum gol registrado ainda"
Pós-condição: Relatório de artilheiros exibido ou mensagem informativa
Localização: PartidaController.ReportArtilheiros()


# 7 -  Classe 

![alt text](/Documentação/image.png)

# 8 - Mocks da Tela (Plain Text)

**Menu Principal**

![alt text](/Documentação/image-1.png)

**1 - Cadastros de Times**

![alt text](/Documentação/image-2.png)

**2 - Cadastros de Jogadores** 

![alt text](/Documentação/image-5.png)

**3 - Registro de Partidas** 

![alt text](/Documentação/image-4.png)

**4 - Tabela de Classificação** 

**5 - Estatísticas de Jogadores** 