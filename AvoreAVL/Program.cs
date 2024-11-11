using ArvoreAVL;

namespace AvoreAVL;

class Program
{
    static void Main(string[] args)
    {
        No no32 = new No(32);
        No no31 = new No(31);
        No no35 = new No(35);
        No no33 = new No(33);
        No no36 = new No(36);
        No no38 = new No(38);
        No no25 = new No(25);

        No no50 = new No(50);
        No no70 = new No(70);
        No no20 = new No(20);
        No no30 = new No(30);
        No no10 = new No(10);
        No no5 = new No(5);



        Arvore arvore = new Arvore();

        // arvore.inserirNo(no32);
        // arvore.inserirNo(no31);
        // arvore.inserirNo(no35);
        // arvore.inserirNo(no33);
        // arvore.inserirNo(no36);
        // arvore.inserirNo(no38);
        // arvore.inserirNo(no25);

        arvore.inserirNo(no50);
        arvore.inserirNo(no70);
        arvore.inserirNo(no20);
        arvore.inserirNo(no30);
        arvore.inserirNo(no10);
        arvore.inserirNo(no5);

        Console.WriteLine(arvore);
    }
}
