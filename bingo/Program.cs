//VARIAVEIS
//TODA TABELA (uma cabela a cada 5 linhas) TERÁ AS SEGUINTES INFORMAÇÕES NA COLUNA 6 (índice 5)
//[0, 5] -> numero do jogador (indice ou indice + 1)
//[1, 5] -> pontuação total da tabela
//{2, 5] -> se fez alguma LINHA
//[3, 5] -> se fez alguma COLUNA
//[4, 5] -> se fez BINGO
//*(-1) -> marca e desmarca um determinado número na tabela
int[,] matriz;
int jogadores = 1, contadorLinha = 0, contadorColuna = 0, bingo = 0;


//-------------------------------------------------------------------------
//FUNCOES
//criaMatriz
void criaMatriz(int nJogadores) {
    matriz = new int[nJogadores * 5, 6];
}


//popular a matriz dos jogadores
void popularMatriz()
{
    int[] indexSelecionados = new int[100];
    int sorteioIndex = new Random().Next(1, 100);


    for (int l = 0; l < jogadores * 5; l++)
    {
        if (l > 0) Console.WriteLine();
        for (int c = 0; c < 5; c++)
        {
            if (indexSelecionados[sorteioIndex] == 0)
            {
                matriz[l, c] = sorteioIndex; //atribui o valor sorteado, que também é o index do vetor indexSelecionados
                indexSelecionados[sorteioIndex] = 1; //1 significa que este index já foi selecionado
            }
        }
    }
}

//verificarRodada 


//-------------------------------------------------------------------------
//PROGRAMA