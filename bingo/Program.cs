//*(-1) -> marca e desmarca um determinado número na tabela

//VARIÁVEIS
int qtdJogadores = 1, qtdCartelas = 1, contadorCartelaAndou = 0, contadorLinha = 0, contadorBingo = 0, numeroSorteado = 0;
bool linhaCompleta = false, colunaCompleta = false, bingo = false;

//VETORES
//cada posição é um jogador. ao acessar jogadoresPontos[0] o retorno é a pontuação do jogador 1 (i + 1). 
int[] jogadoresPontos, numerosDoJogo = new int[99], contadorColuna = new int[5], vetorContadorBingo;

//MATRIZES
//o INDEX do vetor, está relacionado ao INDEX do vetor jogadoresPontos
//vetorDeMatrizes[0] -> acessando o vetor de cartelas do jogador 1
int[][][,] vetorDeMatrizes;


//-------------------------------------------------------------------------
//FUNCOES

//menu -> obtém do usuário tudo que o jogo precisa para funcionar
void menu()
{
    Console.WriteLine("---------------------- BINGO ----------------------\n");
    Console.Write("Informe a quantidade de jogadores: ");
    qtdJogadores = int.Parse(Console.ReadLine());

    Console.Write("\nInforme a quantidade de tabelas por jogador: ");
    qtdCartelas = int.Parse(Console.ReadLine());

    Console.WriteLine("Pressione qualquer tecla para iniciar o jogo!");
    Console.ReadKey();
    Console.WriteLine("---------------------- JOGO ----------------------");

}

//inicializa os vetores de acordo com a quantidade de jogadores selecionados
void iniciarVetores()
{
    jogadoresPontos = new int[qtdJogadores];
    vetorDeMatrizes = new int[qtdJogadores][][,];
    vetorContadorBingo = new int[qtdJogadores * qtdCartelas];
}


//inicializa matrizes e usa a função popularMatriz para atribuir os valores
void iniciaCartelas()
{
    for (int jogador = 0; jogador < qtdJogadores; jogador++)
    {
        vetorDeMatrizes[jogador] = new int[qtdCartelas][,];
        for (int cartela = 0; cartela < qtdCartelas; cartela++)
        {
            vetorDeMatrizes[jogador][cartela] = popularCartelas();
        }
    }
}

//populajogador a matriz dos jogadores (utilizar esta função no momento de atribuir valores em uma matriz)
int[,] popularCartelas()
{
    int[,] matriz = new int[5, 5];
    int[] indexSelecionados = new int[99];
    int sorteioIndex = 0;

    for (int l = 0; l < 5; l++)
    {
        for (int c = 0; c < 5; c++)
        {
            sorteioIndex = new Random().Next(1, 100);
            if (indexSelecionados[sorteioIndex - 1] == 0)
            {
                matriz[l, c] = sorteioIndex; //atribui o valor sorteado, que também é o index do vetor indexSelecionados
                indexSelecionados[sorteioIndex - 1] = 1; //1 significa que este index já foi selecionado
            }
            else
            {
                c--;
            }
        }
    }

    return matriz;
}

void sortearNumero()
{
    do
    {
        numeroSorteado = new Random().Next(1, 100);
        if (numerosDoJogo[numeroSorteado - 1] == 0) numerosDoJogo[numeroSorteado - 1] = 1;
    } while (numerosDoJogo[numeroSorteado - 1] == 0);
}

void proximaRodada()
{
    for (int jogador = 0; jogador < qtdJogadores; jogador++)
    {
        {
            for (int cartela = 0; cartela < qtdCartelas; cartela++)
            {
                //iniciou nova cartela, zera contador do bingo, da cartela e das colunas
                //contadorBingo = 0;
                contadorCartelaAndou = 0;
                for (int i = 0; i < 5; i++) contadorColuna[i] = 0;

                for (int linha = 0; linha < 5; linha++)
                {
                    contadorLinha = 0;

                    for (int coluna = 0; coluna < 5; coluna++)
                    {
                        contadorCartelaAndou++;
                        //a posição da cartela bateu com o numero sorteado, muda o valor para negativo e soma +1 nos contadores
                        if (vetorDeMatrizes[jogador][cartela][linha, coluna] == numeroSorteado)
                        {
                            //muda o valor pra negativo
                            if (numerosDoJogo[numeroSorteado - 1] == 0)
                            {
                                vetorDeMatrizes[jogador][cartela][linha, coluna] *= (-1);
                            }

                            //verifica contadores
                            verificaContadores(jogador, cartela, coluna);
                        }
                    }
                }
            }
        }
    }
}

void verificaContadores(int jogador, int cartela, int coluna)
{
    if (!linhaCompleta) contadorLinha++;
    //bateu linha
    if (contadorLinha == 5)
    {
        linhaCompleta = true;
        jogadoresPontos[jogador]++;
        Console.WriteLine($"Jogador {jogador} pontuou com uma LINHA!");
    }

    if (!colunaCompleta) contadorColuna[coluna]++;
    for (int i = 0; i < 5; i++)
    {
        //bateu alguma coluna
        if (contadorColuna[i] == 5)
        {
            colunaCompleta = true;
            jogadoresPontos[jogador]++;
            Console.WriteLine($"Jogador {jogador} pontuou com uma COLUNA!");
        }
    }


    for (int cart = 0; cart < qtdJogadores * qtdCartelas; cart++)
    {
        if (cart == cartela)
        {
            //vetorContadorBingo[cart * jogador + 1]++;

            if (vetorContadorBingo[cart] == 25)
            {
                jogadoresPontos[jogador] += 5;
                bingo = true;
                imprimeMatriz(vetorDeMatrizes[jogador][cartela]);
                Console.WriteLine($"Jogador {jogador} fez um BINGO!!!");
            }
        }
    }

    //if (!bingo) contadorBingo++;
    //Console.WriteLine($"Contador bingo: {contadorBingo}");
    //if (contadorBingo == 25)
    //{
    //    jogadoresPontos[jogador] += 5;
    //    bingo = true;
    //    imprimeMatriz(vetorDeMatrizes[jogador][cartela]);
    //    Console.WriteLine($"Jogador {jogador} fez um BINGO!!!");
    //}
}

void imprimeMatriz(int[,] matriz)
{
    Console.WriteLine("CARTELA VENCEDORA:\n");
    for (int l = 0; l < 5; l++)
    {
        if (l > 0) Console.WriteLine();
        for (int c = 0; c < 5; c++)
        {
            Console.Write("| " + (matriz[l, c]).ToString().PadLeft(2, '0') + "|  ");
        }
    }
}

//-------------------------------------------------------------------------
//PROGRAMA

menu();

iniciarVetores();

iniciaCartelas();

do
{
    //sorteia novo numero
    sortearNumero();
    //faz toda lógica da rodada
    proximaRodada();
    //avisa que o numero ja foi sorteado para que não troque o sinal de um número já sorteado na cartela novamente
    numerosDoJogo[numeroSorteado - 1] = 1;
} while (bingo == false);

Console.WriteLine("fim");