namespace Multigrafo;
using System.Collections.Generic;

public class Grafo : Multigrafo{
    protected List<Vertice> vertices;
    protected List<Aresta> arestas;
    protected int grau;

    public Grafo(){
        grau = 0;
        vertices = new List<Vertice>();
        arestas = new List<Aresta>();
    }

    protected Aresta buscaAresta(Aresta a){
        if (grau == 0)
            return null;
        
        foreach(Aresta aresta in arestas){
            if(aresta == a)
                return aresta;
        }

        return null;
    }

    protected Vertice buscaVertice(Vertice v){
        if (vertices.Count == 0)
            return null;
        
        foreach(Vertice vertice in vertices){
            if(vertice == v)
                return vertice;
        }

        return null;
    }

    public IEnumerator<Vertice> finalVertices(Aresta a){
        Aresta aresta = buscaAresta(a);
        if (aresta == null){
            Console.WriteLine("aresta não encontrada");

            return null;
        }   

        else{
            List<Vertice> vertices = new List<Vertice>();

            vertices.Add(aresta.verticeIn());

            vertices.Add(aresta.verticeOut());

            return vertices.GetEnumerator(); // iterador dos vertices

        }
    }

    public Vertice oposto(Vertice v, Aresta a){
        Aresta aresta = buscaAresta(a);
        if (aresta == null){
            Console.WriteLine("aresta não encontrada");

            return null;
        } 

        else if(v == aresta.verticeIn())
            return aresta.verticeOut();

        else if(v == aresta.verticeOut())
            return aresta.verticeIn();
        
        Console.WriteLine($"vértice {v} não incidente na aresta");

        return null;

    }

    public bool ehAdjacente(Vertice v, Vertice w){
        Vertice v1 = buscaVertice(v);
        
        if(v1 == null){
            Console.WriteLine($"vertice {v1} não inserido no grafo");
            return false;

        } 

        Vertice v2 = buscaVertice(w);

        if(v2 == null){
            Console.WriteLine($"vertice {v1} não inserido no grafo");
            return false;

        } 

        foreach(Aresta aresta in arestas){

            List<Vertice> adjacentes = aresta.vertices();

            if(adjacentes.Contains(v1) && adjacentes.Contains(v2))
                return true;
        }

        return false;

    }

    public void substituir(Vertice v, Object x){
        Vertice v1 = buscaVertice(v);
        
        if(v1 == null){
            Console.WriteLine($"vertice {v1} não inserido no grafo");
            return;

        } 

        v1.setRotulo(x);
        
    }

    public void substituir(Aresta a, Object x){
        Aresta a1 = buscaAresta(a);
        
        if(a1 == null){
            Console.WriteLine($"vertice {a1} não inserido no grafo");
            return;

        } 

        a1.setRotulo(x);
    }

    public Vertice inserirVertice(Object o){
        Vertice v = new Vertice(o);

        vertices.Add(v);

        return v;

    }

    public Aresta inserirAresta(Vertice v, Vertice w, Object o){
        Aresta a = new Aresta(v, w, o);

        arestas.Add(a);

        return a;
    }

    public Object removeVértice(Vertice v){
        Vertice vertice_remover = buscaVertice(v);

        if(vertice_remover == null){
            Console.WriteLine($"vertice {vertice_remover} não inserido no grafo");
            return null;

        } 

        foreach(Aresta aresta in arestas){
            

            if (aresta.verticeIn() == vertice_remover){
                aresta.setVerticeIn(null);
                Object o = removeAresta(aresta);
            }

            else if (aresta.verticeOut() == vertice_remover){
                aresta.setVerticeOut(null);
                Object o = removeAresta(aresta);
            }

        }

        Object retorno = vertice_remover.getRotulo();

        vertice_remover = null;

        return retorno;

    }

    public Object removeAresta(Aresta a){
        Aresta aresta_remover = buscaAresta(a);

        if(aresta_remover == null){
            Console.WriteLine($"aresta {aresta_remover} não inserido no grafo");
            return null;

        } 

        foreach(Vertice vertice in vertices){
            
            if (vertice.arestasIn().Contains(aresta_remover))
                vertice.arestasIn().Remove(aresta_remover);

            else if(vertice.arestasOut().Contains(aresta_remover))
                vertice.arestasOut().Remove(aresta_remover);

        }

        Object retorno = aresta_remover.getRotulo();

        aresta_remover = null;

        return retorno;

    }

}