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

    public Object getRotulo() {
        return rotulo;
    }
    public void setRotulo(Object r) {
        rotulo = r;
    }

    public List<Aresta> arestasIn(){
        return arestas_in;
    }
    public List<Aresta> arestasOut(){
        return arestas_out;
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
        vertice_in = vertice_out = null;
    }
    public Aresta(Vertice v1, Vertice v2, Object r){
        rotulo = r;
        vertice_in = v1;
        vertice_out = v2;
    }
    public Vertice verticeIn(){
        return vertice_in;
    }
    public Vertice verticeOut(){
        return vertice_out;
    }

    public Object getRotulo() {
        return rotulo;
    }
    public void setRotulo(Object r) {
        rotulo = r;
    }

    public void setVerticeIn(Vertice v) {
        vertice_in = v;
    }
    public void setVerticeOut(Vertice v) {
        vertice_out = v;
    }

    
    public List<Vertice> vertices(){
        if (vertice_in == null)
            return null;

        List<Vertice> vertices = new List<Vertice>();

        vertices.Add(vertice_in);

        vertices.Add(vertice_out);

        return vertices;
    }
    public override string ToString() {
        return $"{vertice_in} - {rotulo.ToString()} - {vertice_out}";
    }
}