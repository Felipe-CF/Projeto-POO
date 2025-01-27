namespace Multigrafo;
using System.Collections;

public interface Multigrafo{
    IEnumerator<Vertice> finalVertices(Aresta a);
    Vertice oposto(Vertice v, Aresta a);
    bool ehAdjacente(Vertice v, Vertice w);
    void substituir(Vertice v, Object x);
    void substituir(Aresta a, Object x);
    Vertice inserirVertice(Object o);
    Aresta inserirAresta(Vertice v, Vertice w, Object o);
    Object removeVertice(Vertice v);
    Object removeAresta(Aresta a);
    }