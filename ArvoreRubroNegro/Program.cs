
using System;
using ArvoreRubroNegro;

namespace AvoreRubroNegro;

class Program
{
    static void Main(string[] args)
    {
        ArvoreRN arvore = new ArvoreRN();

        
        No no7 = new No(7);

        No no9 = new No(9);

        No no5 = new No(5);

        No no8 = new No(8);

        No no10 = new No(10);

        arvore.inserirNo(no7);

        arvore.inserirNo(no5);

        arvore.inserirNo(no9);
        
        arvore.inserirNo(no8);

        arvore.inserirNo(no10);

        no9.setCor(-1);

        no8.setCor(1);
        
        no10.setCor(1);

        Console.WriteLine(arvore);

        No removido = arvore.remover(no9); 

        Console.WriteLine(removido);

        Console.WriteLine(arvore);

        int x = 2;

        

    }


}
