using System;
using System.Collections.Generic;

public class Arvore{
    private No raiz;
    private int size;

    public Arvore(){
        raiz = null;
        size = 0;
     }

    public bool ehVazia(){
        return size == 0;
    }

    public int tamanho(){
        return size;
    }

    protected void fincandoRaiz(No n){
        if(!ehVazia())
            throw new Exception("A arvore ja foi plantada");
        
        raiz = n;

        n.setPai(null);
    }

    public bool ehRaiz(No n){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");

        return raiz == n;
    }

    public No aRaiz(){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");

        return raiz;
    }

    public bool temFilhoEsquerdo(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
            
        return n.getFilhoEsquerdo() != null;
    }

    public bool temFilhoDireito(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        return n.getFilhoDireito() != null;
    }

    public bool ehExterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
        
        // false or false = true
        return !(temFilhoDireito(n) || temFilhoDireito(n));
    }

    public bool ehInterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
        
        return temFilhoEsquerdo(n) || temFilhoDireito(n);
    }

    public No buscaNo(No n){
        No r = raiz;

        while(r != null){

            // se n for menor, desce pela esquerda
            if (n.getChave() < r.getChave()){

                if(r.getFilhoEsquerdo() != null)
                    r = r.getFilhoEsquerdo();

                else 
                    return null;
            } 

            // se n for igual, para
            else if (n.getChave() == r.getChave())
                break;
            
            // se n for maior, desce pela direita
            else if (n.getChave() > r.getChave()){

                if(r.getFilhoDireito() != null)
                    r = r.getFilhoDireito(); 

                else 
                    return null;

            }  
            
        }
        
        return r;
        
    }

    public void inserirNo(No novo_no){

        if(novo_no == null)
            Console.WriteLine("Nó passado é vazio");
        
        else{

            if(ehVazia())
                fincandoRaiz(novo_no);

            else{

                No n = buscaNo(novo_no);

                novo_no.setPai(n);

                if(novo_no.getChave() < novo_no.getPai().getChave())
                    novo_no.getPai().setFilhoEsquerdo(novo_no);
                else
                    novo_no.getPai().setFilhoDireito(novo_no);

            }

            size++;
        }
    }

    public No remover(No n){

        if(ehVazia())
            throw new Exception("Arvore esta vazia");

        No a_remover = buscaNo(n); // tento achar o nó

        // se não achei ele...
        if(a_remover == null)
            throw new Exception("Nó não foi inserido na árvore");

        // se é interno...
        if (ehInterno(a_remover)){

            No o_maior = null;

            // se o nó a ser removido tem filhos a direita...
            if(a_remover.getFilhoDireito() != null){

                o_maior = menorDosMaiores(a_remover.getFilhoDireito());

                if(ehRaiz(a_remover)){
                    
                    raiz = o_maior;

                    a_remover.getFilhoEsquerdo().setPai(o_maior);

                    a_remover.getFilhoDireito().setPai(o_maior);

                }

                else{
                    
                    No pai = a_remover.getPai();

                    if(pai.getFilhoDireito() == a_remover)
                        pai.setFilhoDireito(o_maior);

                    else if (pai.getFilhoEsquerdo() == a_remover)
                        pai.setFilhoEsquerdo(o_maior);
                    
                }

            }

            // se o nó a ser removido não tinha filhos a direita
            if(o_maior == null){
                // se era a raiz...
                if(ehRaiz(a_remover)){
                    
                    raiz = a_remover.getFilhoEsquerdo();

                    a_remover.getFilhoEsquerdo().setPai(null);

                }
                // se não era...
                else{
                
                    No pai = a_remover.getPai();

                    a_remover.getFilhoEsquerdo().setPai(pai);

                    if(pai.getFilhoEsquerdo() == a_remover)
                        pai.setFilhoEsquerdo(a_remover.getFilhoEsquerdo());

                    else if(pai.getFilhoDireito() == a_remover)
                        pai.setFilhoDireito(a_remover.getFilhoEsquerdo());

                }

            }

            // se tinha e ele pegou o menor deles
            else{
                a_remover.getFilhoEsquerdo().setPai(o_maior);

                if (a_remover.getFilhoDireito() != null)
                    a_remover.getFilhoDireito().setPai(o_maior);

            }

        } 
        
        // se achei...
        else{
            // se for externo
            if (ehExterno(a_remover)){

                if(ehRaiz(a_remover)){
                    raiz = null;
                }

                else{

                    No pai_n = a_remover.getPai();

                    // checo de que lado ele esta
                    if(pai_n.getFilhoEsquerdo() == a_remover)
                        pai_n.setFilhoEsquerdo(null);
                    
                    else if(pai_n.getFilhoDireito() == a_remover)
                        pai_n.setFilhoDireito(null);

                }

            }

            else{

                No pai = a_remover.getPai(); // pego o pai do nó a ser removido

                // checa se o nó a ser removido tem um filho dir                     eito
                if(a_remover.getFilhoDireito() != null){

                    No no_maior = menorDosMaiores(a_remover.getFilhoDireito()); // pego o proximo maior nó
                    
                    // referencio o pai do nó n, a ser removido, e 
                    // seus filhos para o menor dos maiores nós que n
                    if(pai.getFilhoEsquerdo() == a_remover)
                        pai.setFilhoEsquerdo(no_maior);     
                                                        
                    else if(pai.getFilhoDireito() == a_remover)
                        pai.setFilhoDireito(no_maior);
                    
                    a_remover.getFilhoEsquerdo().setPai(no_maior);

                    a_remover.getFilhoDireito().setPai(no_maior);
                    
                }

                // se não, o no a se remover tem um filho direito
                else{

                    if(pai.getFilhoEsquerdo() == a_remover)
                        pai.setFilhoEsquerdo(a_remover.getFilhoEsquerdo());

                    else if(pai.getFilhoDireito() == a_remover)
                        pai.setFilhoDireito(a_remover.getFilhoEsquerdo());

                    a_remover.getFilhoEsquerdo().setPai(a_remover.getPai());

                }

            }
            
            a_remover.setPai(null);

            a_remover.setFilhoDireito(null);
                
            a_remover.setFilhoEsquerdo(null);

            size--;

            return a_remover;
        }

    }

    private No menorDosMaiores(No n){
        
        while(n.getFilhoEsquerdo() != null)
            n = n.getFilhoEsquerdo();
        
        if (ehExterno(n)){
            n.getPai().setFilhoEsquerdo(null);

            n.setPai(null);
        }

        else{
            n.getPai().setFilhoEsquerdo(n.getFilhoDireito());

            n.getFilhoDireito().setPai(n.getPai());

            n.setPai(null);

            n.setFilhoDireito(null);
        }
        
        return n;
    }

    public int profundidade(No n){
        if(ehRaiz(n))
            return 0;
        else
            return 1 + profundidade(n.getPai());
    }

    public int altura(No n){
        if(ehExterno(n))
            return 0;
        
        int h = 0;

        foreach(No w in n.filhos())
            h = Math.Max(h, altura(w));

        return h + 1;       

    }


    public string emOrdem(No n){
        string retorno = "";

        if(ehInterno(n) && temFilhoEsquerdo(n) )
            retorno += emOrdem(n.getFilhoEsquerdo());
        
        retorno += n.getChave().ToString() + " ";

        if(ehInterno(n) && temFilhoDireito(n))
            retorno += emOrdem(n.getFilhoDireito());

        return retorno;
    }

    public override string ToString()
    {
        return emOrdem(raiz);
    }

    // public string preOrdem(No n){

    //     string retorno = n.getElemento().ToString() + " ";

    //     if(ehInterno(n) && temFilhoEsquerdo(n))
    //         retorno += emOrdem(n.getFilhoEsquerdo());

    //     else if(ehInterno(n) && temFilhoDireito(n))
    //         retorno += emOrdem(n.getFilhoDireito());

    //     return retorno;
    // }


    // public string posOrdem(No n){
    //     string retorno = "";

    //     if(ehInterno(n) && temFilhoEsquerdo(n))
    //         retorno += emOrdem(n.getFilhoEsquerdo());

    //     else if(ehInterno(n) && temFilhoDireito(n))
    //         retorno += emOrdem(n.getFilhoDireito());

    //     retorno += n.getElemento().ToString() + " ";

    //     return retorno;

    // }
}

