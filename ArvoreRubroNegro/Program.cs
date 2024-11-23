
using System;
using ArvoreRubroNegro;

namespace AvoreRubroNegro;

class Program
{
    static void Main(string[] args)
    {
        No no50 = new No(50);
        No no40 = new No(40);
        No no60 = new No(60);
        No no30 = new No(30);
        No no35 = new No(35);
        No no70 = new No(70);
        No no65 = new No(65);



        ArvoreRN arvore = new ArvoreRN();

        arvore.inserirNo(no50);
        // arvore.inserirNo(no40);
        arvore.inserirNo(no60);
        arvore.inserirNo(no70);

        Console.WriteLine(arvore);

        // // RDE
        // // arvore.inserirNo(no50);
        // // arvore.inserirNo(no20);
        // // arvore.inserirNo(no80);
        // // arvore.inserirNo(no10);
        // // arvore.inserirNo(no70);
        // // arvore.inserirNo(no90);
        // // arvore.inserirNo(no60);

        // // No removido = arvore.remover(no10);

    
        // No no50 = new No(50);
        // No no20 = new No(20);
        // No no80 = new No(80);
        // No no10 = new No(10);
        // No no40 = new No(40);
        // No no100 = new No(100);
        // No no30 = new No(30);

        // arvore.inserirNo(no50);
        // arvore.inserirNo(no20);
        // arvore.inserirNo(no80);
        // arvore.inserirNo(no10);
        // arvore.inserirNo(no40);
        // arvore.inserirNo(no100);
        // arvore.inserirNo(no30);

        // Console.WriteLine(arvore);

        // No removido = arvore.remover(no100);

        

    }


}
