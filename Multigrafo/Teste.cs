using System;
using System.Collections;
using System.Collections.Generic;
namespace Multigrafo;

class Teste
{
    static void Main(string[] args)
    {
        Grafo grafo = new Grafo();

        Vertice v1 = new Vertice(1);
        Vertice v2 = new Vertice(2);
        Vertice v3 = new Vertice(3);
        Vertice v4 = new Vertice(4);
        Vertice v5 = new Vertice(5);

        grafo.inserirVertice(v1);
        grafo.inserirVertice(v2);
        grafo.inserirVertice(v3);
        grafo.inserirVertice(v4);
        grafo.inserirVertice(v5);

        grafo.inserirAresta(v1, v2, "a1");
        grafo.inserirAresta(v2, v3, "a2");
        grafo.inserirAresta(v3, v5, "a3");
        grafo.inserirAresta(v5, v1, "a4");
        grafo.inserirAresta(v2, v4, "a5");

        Object o = grafo.removeVertice(v4);



        int x = 2;
        


    }
}
