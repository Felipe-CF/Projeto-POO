namespace Multigrafo;
using System.Collections.Generic;

public class Grafo : Multigrafo{
    protected List<Vertice> vertices;
    protected List<Aresta> arestas;


    protected Aresta buscaAresta(){
        return null;
    }

    protected Vertice buscaVertice(){
        return null;
    }

    public List<Vertice> finalVertices(Aresta a){
        return null;
    }

    public Vertice oposto(Vertice v, Aresta a){
        return null;

    }

    public bool ehAdjacente(Vertice v, Vertice w){
        return false;

    }

    public void substituir(Vertice v, Object x){

    }

    public void substituir(Aresta a, Object x){

    }

    public Vertice inserirVertice(Object o){
        return null;

    }

    public Aresta inserirAresta(Vertice v, Vertice w, Object o){
        return null;
    }

    public Object removeVértice(Vertice v){
        return null;

    }
    
    public Object removeAresta(Aresta a){
        return null;

    }

}