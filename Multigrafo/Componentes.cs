using System;
using System.Collections;

public class Vertice{
    protected List<Aresta> arestas_in;
    protected List<Aresta> arestas_out;
    protected Object rotulo;

    public Vertice(Object r){
        rotulo = r;
        arestas_in = arestas_out = null;
    }
    public override string ToString() {
        return rotulo.ToString();
    }

}

public class Aresta{
    protected Object rotulo;
    protected Vertice vertice_in;
    protected Vertice vertice_out;

    public Aresta(Object r){
        rotulo = r;
        vertice_in = null;
        vertice_out = null;
    }
    public Aresta(Object r, Vertice i, Vertice o){
        rotulo = r;
        vertice_in = i;
        vertice_out = o;
    }
    public Vertice verticeIn(){
        return vertice_in;
    }
    public Vertice verticeOut(){
        return vertice_out;
    }
    public override string ToString() {
        return $"{vertice_in} - {rotulo.ToString()} - {vertice_out}";
    }
}