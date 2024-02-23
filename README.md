<div align="center">
  <h2>Simulação de Entrevista para Engineer Manager</h2>  
  <p>Soluções para os desafios propostos na simulação de entrevista para Engineer Manager no canal do <a href="https://youtu.be/6HP_9pP7BHE?si=6LLtZPYv3PGTyRV1">Augusto Galego</a>.</p>
</div>
<hr>
<div>
  <h3>1 - Fibonacci</h3>
  <p style="text-align: justify;">O primeiro tópico da entrevista foi um algoritmo que imprimisse no console e retornasse 
  um vetor com os N primeiros termos da sequência de Fibonacci.</p> 
  <p style="text-align: justify;">Assumindo que o algoritmo deve retornar um erro caso a quantidade
  de termos passados no argumento da função seja negativa e assumindo também que o 0 será incluído
  como primeiro termo na sequência de fibonacci, temos:</p>

  ```csharp
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
```
  <h3>2 - Enésimo maior termo de uma árvore binária de pesquisa</h3>
  <p style="text-align: justify;">A segunda parte da entrevista consistiu em implantar um algoritmo recursivo que retornasse o 
  enésimo nó de maior valor em uma árvore binária de pesquisa.</p> 
  <p style="text-align: justify;">Caso o valor de 
  "num" seja menor que 1, uma exceção será gerada.</p>
  
```csharp
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
    return resultNode != null ? resultNode : throw new Exception("Nésimo termo não encontrado") ;
}

public static TreeNode NthBiggestNode(TreeNode node, int num, int[] counter)
{
    if (node == null) return null;

    /*Iniciando da direita, o algoritmo irá caminhar pelos nós da árvore em ordem descrescente.
     * Logo, o enésimo nó que visitarmos será o enésimo maior termo. */
    TreeNode right = NthBiggestNode(node.Right, num, counter);

    if (right != null)
        return right;

    counter[0]++;
    if (counter[0] == num) return node;

    return NthBiggestNode(node.Left, num, counter);
}
```

  <h3>3 - Enésimo maior termo de uma árvore binária de pesquisa (não recursivamente)</h3>
  <p style="text-align: justify;">Já a terceira e última parte da entrevista era implantar um algoritmo que fizesse
  a mesma coisa que o segundo porém não-recursivamente, assumindo que poderíamos nos 
  deparar com uma árvore muito grande e ocorrer o overflow na stack.</p> 
  <p style="text-align: justify;">Para essa solução, implementei o inverso do algoritmo de travessia de Morris, utilizado para percorrer
  uma árvore de maneira crescente e não recursiva.</p>

```csharp
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
```  
</div>

