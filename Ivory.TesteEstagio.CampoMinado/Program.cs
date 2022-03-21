using System;

namespace Ivory.TesteEstagio.CampoMinado
{
    class Program
    {
        // alterar função para atualizar um nova tabela.
        // Converte o Tabuleiro de String para uma matriz de Char.
        private static void AtualizaTabuleiro(string entrada, char[,] saida)
        {
            var contador = 0;
            for (var linha = 0; linha < 9; linha++)
            {
                for (var coluna = 0; coluna < 9; coluna++)
                {
                    if (saida[linha, coluna] != 'b')
                    {
                        saida[linha, coluna] = entrada[contador];
                    }
                    contador++;
                }
                // Contador pula /r e /n
                contador += 2;
            }
        }
        // Verifica as laterias de uma casa selecionada
        private static (int quantidadeVagos, int[,] vagos, int quantidadeBombas) VerificarLateris(char[,] Campo, int x, int y)
        {
            // Realiza o Retorno Caso esteja tentando identificar as laterais de uma casa desconhecida.
            if (Campo[x, y] == '-')
            {
                return (-1, null, 0);
            }
            var quantidadeVagos = 0;
            var quantidadeBombas = 0;
            // Utiliza valor de x-1 e y-1 para verificação das Casas Laterias.
            var xNovo = (x - 1);
            var yNovo = (y - 1);
            var vagos = new int[8, 2];
            for (var linha = 0; linha < 3; linha++)
            {
                for (var coluna = 0; coluna < 3; coluna++)
                {
                    if ((linha + xNovo >= 0) && (linha + xNovo < 9) && (coluna + yNovo >= 0) && (coluna + yNovo < 9))
                    {
                        if ((Campo[linha + xNovo, coluna + yNovo] == '-'))
                        {
                            vagos[quantidadeVagos, 0] = linha + xNovo;
                            vagos[quantidadeVagos, 1] = coluna + yNovo;
                            quantidadeVagos++;
                        }
                        else if ((Campo[linha + xNovo, coluna + yNovo] == 'b'))
                        {
                            quantidadeBombas++;
                        }
                    }
                }
            }
            return (quantidadeVagos, vagos, quantidadeBombas);
        }

        static void Main(string[] args)
        {
            var campoMinado = new CampoMinado();
            Console.WriteLine("Início do jogo\n=========");
            Console.WriteLine(campoMinado.Tabuleiro);
            // Realize sua codificação a partir deste ponto, boa sorte!
            var Campo = new char[9, 9];
            string[] Resultado = { "Jogo em aberto", "Vitoria", "GameOver" };
            while (campoMinado.JogoStatus == 0)
            {
                for (var linha = 0; linha < 9; linha++)
                {
                    AtualizaTabuleiro(campoMinado.Tabuleiro, Campo);
                    for (var coluna = 0; coluna < 9; coluna++)
                    {
                        var Saida = VerificarLateris(Campo, linha, coluna);
                        // Converte o Valor da Casa de char para o valor em inteiro
                        var Valor = (int)(Campo[linha, coluna] - 48);
                        if (Saida.quantidadeVagos == (Valor - Saida.quantidadeBombas))
                        {
                            for (var i = 0; i < Saida.quantidadeVagos; i++)
                            {
                                Campo[Saida.vagos[i, 0], Saida.vagos[i, 1]] = 'b';
                            }
                        }
                        Saida = VerificarLateris(Campo, linha, coluna);
                        if ((Valor - Saida.quantidadeBombas) == 0 && Saida.quantidadeVagos > 0)
                        {
                            for (var i = 0; i < Saida.quantidadeVagos; i++)
                            {
                                Console.WriteLine($"Abrir(Linha :{Saida.vagos[i, 0] + 1} Coluna :{Saida.vagos[i, 1] + 1})");
                                campoMinado.Abrir(Saida.vagos[i, 0] + 1, Saida.vagos[i, 1] + 1);
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Jogo Finalizado Com Status :{Resultado[campoMinado.JogoStatus]}\n=========");
            Console.WriteLine(campoMinado.Tabuleiro);
        }
    }
}
