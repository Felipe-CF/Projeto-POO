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

        Aresta a1 = new Aresta("a1");
        Aresta a2 = new Aresta("a2");
        Aresta a3 = new Aresta("a3");
        Aresta a4 = new Aresta("a4");
        Aresta a5 = new Aresta("a5");

        


    }
}
