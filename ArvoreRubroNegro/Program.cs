
using System;
using ArvoreRubroNegro;

namespace AvoreRubroNegro;

class Program
{
    static void Main(string[] args)
    {
        
        // No no30 = new No(30);
        // No no35 = new No(35);
        // No no40 = new No(40);
        // No no45 = new No(45);
        // No no50 = new No(50);
        // No no60 = new No(60);
        // No no65 = new No(65);
        // No no70 = new No(70);
        // No no75 = new No(75);
        // No no80 = new No(80);
        No no7 = new No(7);

        No no5 = new No(5);

        No no14 = new No(14);

        No no45 = new No(45);

        No no50 = new No(50);

        No no60 = new No(60);

        No no65 = new No(65);

        No no70 = new No(70);

        No no75 = new No(75);
        
        No no80 = new No(80);



        ArvoreRN arvore = new ArvoreRN();

        arvore.inserirNo(no35);
        arvore.inserirNo(no30);
        arvore.inserirNo(no40);
        arvore.inserirNo(no50);
        arvore.inserirNo(no75);
        arvore.inserirNo(no45);


        Console.WriteLine(arvore);

        // No removido = arvore.remover(no50); caso 1 funcionando
        // No removido = arvore.remover(no75); caso 2 funcionando
        No removido = arvore.remover(no35); 

        Console.WriteLine(removido);

        Console.WriteLine(arvore);

        // No removido = arvore.remover(no100);

        int x = 2;

        

    }


}
