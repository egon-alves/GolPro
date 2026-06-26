## GolPro — Sistema de Gerenciamento de Campeonatos de Futebol


# 1 - Visão Geral
O GolPro é um sistema de gerenciamento de campeonatos de futebol, permitindo cadastrar times, jogadores e partidas, além de gerar automaticamente a
tabela de classificação e o ranking de artilheiros.

# 2 - Objetivo
Prover um sistema console em C# para registro e acompanhamento de campeonatos
de futebol, automatizando o cálculo de pontuação, saldo de gols e artilharia.

# 3 - Escopo
O sistema abrange:
- Cadastro, alteração e exclusão de times, jogadores e partidas.
- Geração automática da tabela de classificação e relatório de artilheiros.

O sistema não abrange:
- Autenticação ou controle de acesso por usuário.
- Interface gráfica, opera exclusivamente via console.
- Integração com sistemas externos ou APIs.
- Gerenciamento de múltiplos campeonatos simultâneos.

# 4 - Regras de Negócio

RN01 - **Unicidade de Time:** Não é possível cadastrar dois times com o mesmo código. 
Garantido em: `TimeController.CRUD()` ao validar com `FindByCode()`.

RN02 - **Atribuição de Gols Exclusiva:** O gol de um jogador só pode ser registrado se o jogador pertencer ao time informado na partida. 
Garantido em: `PartidaController.RegistrarGolsJogadores()`.

RN03 - **Estorno de Estatísticas na Exclusão/Alteração:** Se uma partida for alterada ou excluída, os gols, pontos e saldo dos times, bem como os gols dos jogadores, devem ser estornados antes de aplicar o novo resultado. 
Garantido em: `PartidaController.CRUD()` e `PartidaController.EstornarGolsJogadores()`.

RN04 - **Pontuação da Partida:** A vitória vale 3 pontos, o empate 1 ponto e a derrota 0 pontos. 
Garantido em: `TimeModel.RegistrarPartida()` e `TimeModel.EstornarResultado()`.

RN05 - **Artilharia Mínima:** Apenas jogadores que marcaram pelo menos 1 gol (GolsMarcados > 0) podem aparecer no relatório de artilharia. 
Garantido em: `PartidaController.ReportArtilheiros()`.

# 5 - Requisitos Funcionais

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


# 7 - Diagrama de Classes 

![alt text](/Documentação/diagramadeclasse.png)

# 8 - Mocks da Tela (Plain Text)

**Menu Principal**
                                                      
  ╔════════════════════════════════════════════════════════════════════════════╗                                                                    
  ║                       GOLPRO - SISTEMA DE REGISTRO                         ║                                                                    
  ╠════════════════════════════════════════════════════════════════════════════╣                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                    ┌────────────────────↑↓ 0-9 Enter─┐                     ║                                                                    
  ║                    │ ► 1 - Cadastros de Times        │                     ║                                                                    
  ║                    │   2 - Cadastros de Jogadores    │                     ║                                                                    
  ║                    │   3 - Registro de Partidas      │                     ║                                                                    
  ║                    │   4 - Tabela de Classificação   │                     ║                                                                    
  ║                    │   5 - Estatísticas de Jogadores │                     ║                                                                    
  ║                    │   0 - Sair                      │                     ║                                                                    
  ║                    │                                 │                     ║                                                                    
  ║                    └─────────────────────────────────┘                     ║                                                                    
  ║                                                                            ║                                                                    
  ║ Navegação:                                                                 ║                                                                    
  ║ ↑ ↓    Move a seleção                                                      ║                                                                    
  ║ ENTER  Confirma a opção                                                    ║                                                                    
  ║ ESC    Volta ao menu anterior                                              ║                                                                    
  ║                                                                            ║                                                                    
  ╚════════════════════════════════════════════════════════════════ GolPro v1.0     

**1 - Cadastros de Times**

  ╔════════════════════════════════════════════════════════════════════════════╗                                                                    
  ║                             CADASTRO DE TIMES                              ║                                                                    
  ╠════════════════════════════════════════════════════════════════════════════╣                                                                    
  ║ Código  :                                                                  ║                                                                    
  ║                                                                            ║                                                                    
  ║ Nome    :                                                                  ║                                                                    
  ║                                                                            ║                                                                    
  ║ Cidade  :                                                                  ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                          ┌─────↑↓ 0-9 Enter─┐              ║                                                                    
  ║                                          │ ► S - Sim (Sair) │              ║                                                                    
  ║                                          │   N - Não        │              ║                                                                    
  ║                                          │                  │              ║                                                                    
  ║                                          └──────────────────┘              ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║ Navegação:                                                                 ║                                                                    
  ║ ESC    Volta ao menu anterior                                              ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ╚════════════════════════════════════════════════════════════════ GolPro v1.0                                                                     

**2 - Cadastros de Jogadores** 

  ╔════════════════════════════════════════════════════════════════════════════╗                                                                    
  ║                           CADASTRO DE JOGADORES                            ║                                                                    
  ╠════════════════════════════════════════════════════════════════════════════╣                                                                    
  ║ Matrícula      :                                                           ║                                                                    
  ║                                                                            ║                                                                    
  ║ Nome           :                                                           ║                                                                    
  ║                                                                            ║                                                                    
  ║   Posicoes: Goleiro / Zagueiro / Lateral / Meia / Atacante                 ║                                                                    
  ║ Posicao        :                                                           ║                                                                    
  ║                                                                            ║                                                                    
  ║ Codigo do Time :                                                           ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║ Navegação:                                                                 ║                                                                    
  ║ ESC    Volta ao menu anterior                                              ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ╚════════════════════════════════════════════════════════════════ GolPro v1.0                                                                     

**3 - Registro de Partidas** 
                                                          
  ╔════════════════════════════════════════════════════════════════════════════╗                                                                    
  ║                           REGISTRO DE PARTIDAS                             ║                                                                    
  ╠════════════════════════════════════════════════════════════════════════════╣                                                                    
  ║ ID              :                                                          ║                                                                    
  ║                                                                            ║                                                                    
  ║ Mandante        :                                                          ║                                                                    
  ║                                                                            ║                                                                    
  ║ Visitante       :                                                          ║                                                                    
  ║                                                                            ║                                                                    
  ║ Data (dd/MM/yyyy):                                                         ║                                                                    
  ║                                                                            ║                                                                    
  ║ Gols Mandante   :                                                          ║                                                                    
  ║                                                                            ║                                                                    
  ║ Gols Visitante  :                                                          ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║ Navegação:                                                                 ║                                                                    
  ║ ESC    Volta ao menu anterior                                              ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ╚════════════════════════════════════════════════════════════════ GolPro v1.0                                                                     
                                                                                                                                                    
                                                                                 

**4 - Tabela de Classificação**                                                                                                                                       
  ╔════════════════════════════════════════════════════════════════════════════╗                                                                    
  ║                          TABELA DE CLASSIFICAÇÃO                           ║                                                                    
  ╠════════════════════════════════════════════════════════════════════════════╣                                                                    
  ║ Cód  Nome                 Pts  V   E   D   GP  GC  SG                      ║                                                                    
  ║                                                                            ║                                                                    
  ║ PAL  Palmeiras             12   4   0   0  10   1   9                      ║                                                                    
  ║ FLA  Flamengo               7   2   1   1   5   4   1                      ║                                                                    
  ║ SAO  São Paulo              4   1   1   2   3   5  -2                      ║                                                                    
  ║ CAM  Atlético Mineiro       2   0   2   2   2   5  -3                      ║                                                                    
  ║ BOT  Botafogo               1   0   1   1   1   3  -2                      ║                                                                    
  ║ COR  Corinthians            1   0   1   1   1   3  -2                      ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║ Pressione qualquer tecla para voltar...                                    ║                                                                    
  ║                                                                            ║                                                                    
  ╚════════════════════════════════════════════════════════════════ GolPro v1.0   

**5 - Estatísticas de Jogadores** 
                                                                                                                                              
  ╔════════════════════════════════════════════════════════════════════════════╗                                                                    
  ║                         ARTILHEIROS DO CAMPEONATO                          ║                                                                    
  ╠════════════════════════════════════════════════════════════════════════════╣                                                                    
  ║ Matrícula  Nome                 Time  Gols                                 ║                                                                    
  ║                                                                            ║                                                                    
  ║ 2001       Endrick              PAL      5                                 ║                                                                    
  ║ 2002       Veiga                PAL      3                                 ║                                                                    
  ║ 1001       Pedro                FLA      3                                 ║                                                                    
  ║ 2003       Estêvão              PAL      2                                 ║                                                                    
  ║ 3001       Calleri              SAO      2                                 ║                                                                    
  ║ 1002       Arrascaeta           FLA      1                                 ║                                                                    
  ║ 1003       Luiz Araújo          FLA      1                                 ║                                                                    
  ║ 3002       Lucas Moura          SAO      1                                 ║                                                                    
  ║ 4001       Hulk                 CAM      1                                 ║                                                                    
  ║ 4002       Paulino              CAM      1                                 ║                                                                    
  ║ 5001       Junior Santos        BOT      1                                 ║                                                                    
  ║ 6001       Garro                COR      1                                 ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║                                                                            ║                                                                    
  ║ Pressione qualquer tecla para voltar...                                    ║                                                                    
  ║                                                                            ║                                                                    
  ╚════════════════════════════════════════════════════════════════ GolPro v1.0    