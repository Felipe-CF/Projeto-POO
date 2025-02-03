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
        if (a == null)
            return null;
        
        foreach(Aresta aresta in arestas){
            if(Object.ReferenceEquals(a, aresta))
            return aresta;
        }

        return null;
    }

    protected Vertice buscaVertice(Vertice v){
        if (v == null)
            return null;
        
        foreach(Vertice vertice in vertices){
            if(Object.ReferenceEquals(v, vertice))
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

        else if(Object.ReferenceEquals(v ,aresta.verticeIn()))
            return aresta.verticeOut();

        else if(Object.ReferenceEquals(v ,aresta.verticeOut()))
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
        if(v == null || w == null){
            Console.WriteLine("um ou mais vertices passados são vazios");
            return null;
        }
        else{

            Vertice v1 = buscaVertice(v);

            Vertice v2 = buscaVertice(w);

            Aresta a = new Aresta(v1, v2, o);

            arestas.Add(a);

            v1.setAresta(a);

            v2.setAresta(a);

            return a;
        }
    }

    public Object removeVertice(Vertice v){
        Vertice vertice_remover = buscaVertice(v);

        if(vertice_remover == null){
            Console.WriteLine($"vertice {vertice_remover} não inserido no grafo");
            return null;

        } 

        List<Aresta> todas_arestas = new List<Aresta>();

        todas_arestas.AddRange(vertice_remover.getArestas());

        todas_arestas.AddRange(vertice_remover.arestasIn());

        todas_arestas.AddRange(vertice_remover.arestasOut());

        foreach(Aresta aresta in todas_arestas){
            Vertice oposto_de_v = oposto(vertice_remover, aresta);

            oposto_de_v.removerAresta(aresta);

            arestas.Remove(aresta);
        }

        vertices.Remove(vertice_remover);
        
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

        foreach(Vertice vertice in vertices)
            vertice.removerAresta(aresta_remover);

        Object retorno = aresta_remover.getRotulo();

        aresta_remover = null;

        return retorno;

    }

    public IEnumerator<Aresta> arestasIncidentes(Vertice v){
        Vertice v1 = buscaVertice(v);

        if(v1 == null){
            Console.WriteLine($"vértice {v1} não inserido no grafo");
            return null;
        }

        List<Aresta> retorno = new List<Aresta>();

        foreach(Aresta aresta in arestas){
            if(aresta.seIncide(v))
                retorno.Add(aresta);
        }

        return retorno.GetEnumerator();
    }

    public List<Vertice> getVertices(){
        return vertices;
    }

    public List<Aresta> GetArestas(){
        return arestas;
    }

    public bool ehDirecionada(Aresta a){

        if(a == null){
            Console.WriteLine("Aresta passada é nula");
            return false;
        }

        Aresta aresta = buscaAresta(a);

        if (aresta == null){
            Console.WriteLine("Aresta passada não foi inserida no grafo");
            return false;
        }
        
        Vertice v1 = aresta.verticeIn();

        Vertice v2 = aresta.verticeOut();

        if(v1.seEhArestaSemDirecao(aresta) && v2.seEhArestaSemDirecao(aresta))
            return false;

        return true;
    }

    public Aresta inserirArestaDirecionada(Vertice v_out, Vertice v_in, Object o){

        if(o == null)
            return null;

        Vertice v1 = buscaVertice(v_out);

        if(v1 == null){
            Console.WriteLine($"vértice {v1} não inserido");
            return null;
        }

        Vertice v2 = buscaVertice(v_in);

        if(v2 == null){
            Console.WriteLine($"vértice {v2} não inserido");
            return null;
        }

        Aresta aresta = new Aresta(v1, v2, o);

        foreach(Vertice vertice in vertices){
            if(Object.ReferenceEquals(vertice, v_out))
                vertice.setArestaOut(aresta);

            else if(Object.ReferenceEquals(vertice, v_in))
                vertice.setArestaIn(aresta);

        }

        arestas.Add(aresta);

        return aresta;
    }

}