using System;
using System.Collections;
using System.Collections.Generic;
namespace Multigrafo;

class Teste
{
    static void Main(string[] args)
    {
        Grafo grafo = new Grafo();

        Vertice v1 = grafo.inserirVertice(1);
        Vertice v2 = grafo.inserirVertice(2);
        Vertice v3 = grafo.inserirVertice(3);
        Vertice v4 = grafo.inserirVertice(4);
        Vertice v5 = grafo.inserirVertice(5);


        Aresta a1 = grafo.inserirArestaDirecionada(v1, v2, "a1");
        Aresta a2 = grafo.inserirArestaDirecionada(v2, v3, "a2");
        Aresta a3 = grafo.inserirArestaDirecionada(v3, v5, "a3");
        Aresta a4 = grafo.inserirArestaDirecionada(v5, v1, "a4");
        Aresta a5 = grafo.inserirArestaDirecionada(v2, v4, "a5");

        Object o = grafo.removeVertice(v4);
        Object m = grafo.removeVertice(v2);

        // List<Aresta> arestas = grafo.GetArestas();

        // arestas.Remove(a5);


        int x = 2;
        


    }
}
