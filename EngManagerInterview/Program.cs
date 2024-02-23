using System;
using System.Collections.Generic;

namespace EngManagerInterview
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Fibonacci(6);


            TreeNode root = new TreeNode(5);
            root.Inserir(1);
            root.Inserir(2);
            root.Inserir(10);
            root.Inserir(20);

            /*
            var node = NthBiggestNode(root, 6);
            Console.WriteLine(node.Value); */

            var node = NthBiggestNodeNonRecursive(root, 4);
            Console.WriteLine(node.Value);

            Console.ReadKey();
        }

        public static int[] Fibonacci(int num)
        {
            if (num < 0) throw new Exception("Argument must be a positive number");

            int[] sequence = new int[num];
            int count = 0;
            int previous = 0;
            int current = 1;


            while (count < num)
            {
                sequence[count] = previous;

                var aux = current;
                current = current + previous;
                previous = aux;

                count++;
            }

            foreach (int val in sequence)
            {
                Console.Write($"{val} |");
            }

            return sequence;
        }

        /* A segunda parte da entrevista foi implantar um algoritmo recursivo que retornasse o 
         Nésimo nó de maior valor em uma árvore binária de pesquisa. Caso o valor de 
         num seja menor que 1, uma exceção será gerada */
        public static TreeNode NthBiggestNode(TreeNode root, int num)
        {
            if (root == null) return null;
            if (num < 0) throw new Exception(
                "o número relativo à posição de maior valor deve ser maior que 1");

            /*usei um array para o contador ao invés de um inteiro simples porque dessa
             * maneira quando passamos o contador no argumento da função passamos apenas uma
             * referência e não uma cópia dessa varável. Caso passássmos um inteiro ele seria zerado
             * a cada iteração do método.*/
            int[] count = { 0 };
            TreeNode resultNode = NthBiggestNode(root, num, count);
            return resultNode != null ? resultNode : throw new Exception("Nésimo termo não encontrado");
        }

        public static TreeNode NthBiggestNode(TreeNode node, int num, int[] counter)
        {
            if (node == null) return null;

            /*Iniciando da direita, o algoritmo irá caminhar os nós da árvore em ordem descrescente.
             * Logo, o enésimo nó que visitarmos será o enésimo maior termo. */
            TreeNode right = NthBiggestNode(node.Right, num, counter);

            if (right != null)
                return right;

            counter[0]++;
            if (counter[0] == num) return node;

            return NthBiggestNode(node.Left, num, counter);
        }

        /* Já a terceira e última parte da entrevista era implantar um algoritmo que fizesse
         * a mesma coisa que o segundo porém não-recursivamente, assumindo que poderíamos nos 
         * deparar com uma árvore muito grande e ocorrer o overflow na stack. */
        public static TreeNode NthBiggestNodeNonRecursive(TreeNode root, int num)
        {
            if (root == null) return null;
            if (num < 0) throw new Exception(
                "o número relativo à posição de maior valor deve ser maior que 1");

            int counter = 0;
            TreeNode current = root;

            while (current != null)
            {
                //Enquanto o nó atual não é nulo, checa se o nó possui um filho à direita
                if (current.Right == null)
                {
                    //Atualiza o nó atual para o nó à esquerda
                    counter++;
                    if (counter == num) return current;
                    current = current.Left;
                }
                else
                {
                    //Caso contrário, atualiza o nó para o filho esquerdo do nó mais à esquerda da parte direita da árvore
                    TreeNode temp = current.Right;
                    while (temp.Left != null
                           && temp.Left != current)
                        temp = temp.Left;

                    if (temp.Left == null)
                    {
                        temp.Left = current;
                        current = current.Right;
                    }
                    else
                    {
                        temp.Left = null;
                        counter++;
                        if (counter == num) return current;
                        current = current.Left;
                    }
                }
            }

            return null;
        }
    }

    public class TreeNode
    {
        public int Value;
        public TreeNode Right;
        public TreeNode Left;

        public TreeNode(int value)
        {
            Value = value;
            Right = Left = null;
        }

        public void Inserir(int c)
        {
            if (c < Value)
            {
                if (Left == null)
                    Left = new TreeNode(c);
                else
                    Left.Inserir(c);
            }
            else if (c > Value)
            {
                if (Right == null)
                    Right = new TreeNode(c);
                else
                    Right.Inserir(c);
            }
            else
                Console.Write("Chave duplicada. Impossível inserir!");
        }

    }
}

