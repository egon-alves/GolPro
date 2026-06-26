# 9 - Persistência em Arquivo Texto

## Classe responsável

A classe `Data` (em `Utils/Data.cs`) centraliza toda a leitura e gravação de dados em disco. Cada controller instancia um objeto `Data` apontando para o arquivo correspondente:

- `TimeController` → `Utils/Data/times.txt`
- `JogadorController` → `Utils/Data/jogadores.txt`
- `PartidaController` → `Utils/Data/partidas.txt`

A leitura acontece no construtor de cada controller (ao iniciar o programa), e a gravação acontece após cada operação de inclusão, alteração ou exclusão.

---

## Formato dos arquivos

Cada linha do arquivo representa um objeto. Os campos são separados por ponto e vírgula (`;`).

### times.txt

```
codigo;nome;cidade;pontos;vitorias;empates;derrotas;golsPro;golsContra
```

Exemplo:
```
PAL;Palmeiras;São Paulo;12;4;0;0;10;1
FLA;Flamengo;Rio de Janeiro;7;2;1;1;5;4
```

### jogadores.txt

```
matricula;nome;posicao;codigoTime;golsMarcados
```

Exemplo:
```
2001;Endrick;Atacante;PAL;5
1001;Pedro;Atacante;FLA;3
```

### partidas.txt

```
id;codigoMandante;codigoVisitante;data;golsMandante;golsVisitante;matriculasMandante;matriculasVisitante
```

- A data é gravada no formato `yyyyMMdd` (ex: `20260610`).
- As matrículas dos autores dos gols são gravadas como uma lista separada por vírgula dentro do campo (ex: `2001,2002`). Se não houver gols, o campo fica vazio.

Exemplo:
```
1;PAL;FLA;20260610;3;1;2001,2002,2003;1001
```

---

## Como o parsing funciona

A leitura usa `StreamReader` linha a linha. Cada linha é dividida com `Split(';')`, gerando um array de partes.

**Times:** espera exatamente 9 partes. Os campos numéricos usam `int.TryParse` — se o valor estiver corrompido no arquivo, assume `0` em vez de quebrar o programa.

**Jogadores:** aceita 5 ou mais partes. O campo `golsMarcados` também usa `TryParse`.

**Partidas:** aceita 6 ou mais partes (compatibilidade com registros antigos sem lista de gols). Se houver 8 partes, as posições `[6]` e `[7]` são divididas por vírgula com `Split(',')` para reconstruir as listas de matrículas dos autores dos gols.

A gravação usa `StreamWriter` e chama o método `Serializar()` de cada model, que retorna a linha formatada com os campos separados por `;`.
