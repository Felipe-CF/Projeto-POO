using System;

namespace ArvorePesquisa;

class Program
{
    static void Main(string[] args)
    {
        string s = "1";

        string b = s + "";
        
        Console.WriteLine(b);

        No no10 = new No(10);
        No no5 = new No(5);
        No no3 = new No(3);
        No no7 = new No(7);
        No no20 = new No(20);
        No no14 = new No(14);
        No no13 = new No(13);
        No no25 = new No(25);
        No no23 = new No(23);
        No no22 = new No(22);

        Arvore arvore = new Arvore();

        arvore.inserirNo(no10);

        arvore.inserirNo(no5);
        
        arvore.inserirNo(no3);

        arvore.inserirNo(no7);
        
        arvore.inserirNo(no20);

        arvore.inserirNo(no14);

        Console.WriteLine(arvore);

        // No teste = arvore.remover(no20);
        // No teste = arvore.remover(no10);
        No teste = arvore.remover(no3);

        Console.WriteLine(teste);
        
        Console.WriteLine(arvore);

    }
}
