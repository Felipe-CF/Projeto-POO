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

        Arvore arvore = new Arvore();

        arvore.inserirNo(no32);
        arvore.inserirNo(no31);
        arvore.inserirNo(no35);
        arvore.inserirNo(no33);
        arvore.inserirNo(no36);
        arvore.inserirNo(no38);
        arvore.inserirNo(no25);

        Console.WriteLine(arvore);
    }
}
