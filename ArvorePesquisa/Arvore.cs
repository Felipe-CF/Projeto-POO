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

        size++;
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
        
        return temFilhoDireito(n) || temFilhoDireito(n);
    }

    public No buscaNo(No n){
        No r = raiz;

        while(r != null){
            if (n.getChave() < r.getChave()) // se for menor, desce pela esquerda
                r = r.getFilhoEsquerdo();

            if (n.getChave() == r.getChave())
                break;
            
            else
                r = r.getFilhoDireito(); 
            
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

                novo_no.setPai(n.getPai());

                if(novo_no.getChave() < n.getPai().getChave())
                    n.getPai().setFilhoEsquerdo(novo_no);
                else
                    n.getPai().setFilhoDireito(novo_no);
                
                n.setPai(null);

            }

            size++;
        }
    }

    public No remover(No r){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        No removido = r;

        if(r.getFilhoDireito() != null){
            No n = menorDosMaiores(r.getFilhoDireito());
        }
        


        return null;
    }

    private No menorDosMaiores(No n){
        
        while(n.getFilhoEsquerdo() != null)
            n = n.getFilhoEsquerdo();
        
        
        
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


    public string preOrdem(No n){
    
        string retorno = n.getElemento().ToString() + " ";

        if(ehInterno(n) && temFilhoEsquerdo(n))
            retorno += emOrdem(n.getFilhoEsquerdo());

        else if(ehInterno(n) && temFilhoDireito(n))
            retorno += emOrdem(n.getFilhoDireito());

        return retorno;
    }
    
    public string emOrdem(No n){
        string retorno = "";

        if(ehInterno(n) && temFilhoEsquerdo(n) )
            retorno += emOrdem(n.getFilhoEsquerdo());
        
        retorno += n.getElemento().ToString() + " ";

        if(ehInterno(n) && temFilhoDireito(n))
            retorno += emOrdem(n.getFilhoDireito());

        return retorno;
    }

    public string posOrdem(No n){
        string retorno = "";

        if(ehInterno(n) && temFilhoEsquerdo(n))
            retorno += emOrdem(n.getFilhoEsquerdo());

        else if(ehInterno(n) && temFilhoDireito(n))
            retorno += emOrdem(n.getFilhoDireito());

        retorno += n.getElemento().ToString() + " ";

        return retorno;
    
    }
}

