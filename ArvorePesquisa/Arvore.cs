using System;
using System.Collections.Generic;

public class Arvore{
    private Raiz raiz;
    private int size;

    public Arvore(){
        raiz = new Raiz();
        size = 0;
     }

    public bool ehVazia(){
        return size == 0;
    }

    public int tamanho(){
        return size;
    }

    public void fincandoRaiz(int e){
        if(!ehVazia())
            throw new Exception("A arvore ja foi plantada");
        
        No n = new No(e);

        raiz.setRaiz(n);

        size++;
    }

    public bool ehRaiz(No n){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");

        return raiz.getRaiz() == n;
    }

    public No aRaiz(){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");
        return raiz.getRaiz();
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
        
        return !(temFilhoDireito(n) || temFilhoDireito(n));
    }

    public bool ehInterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        return temFilhoDireito(n) || temFilhoDireito(n);
    }

    public No buscaNo(No r, No n){
        if(ehExterno(r))
            return r;

        if (r.getElemento() >  n.getElemento()) // se for menor, desce pela esquerda
            return buscaNo(r.getFilhoEsquerdo(), n);
        
        else if(r.getElemento() == n.getElemento()) // se for igual, retorna
            return r;

        else 
            return buscaNo(r.getFilhoDireito(), n); // se for maior, desce pela direita
        
    }

    public void inserirNo(No r, int o){
        if(ehVazia())
            fincandoRaiz(o);

        else{
            No novo_no = new No(o);

            No no_referencia = buscaNo(raiz.getRaiz(), novo_no);

            if(novo_no.getElemento() > no_referencia.getElemento()){

                no_referencia.setFilhoDireito(novo_no);

                novo_no.setPai(no_referencia);

                size++;
            }
            else if(novo_no.getElemento() < no_referencia.getElemento()){

                no_referencia.setFilhoEsquerdo(novo_no);

                novo_no.setPai(no_referencia);

                size++;
            }

        }

    }

    public No remover(No v){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        No no_a_remover = buscaNo(raiz.getRaiz(), v); // acho o no que sera removido

        No removido = null;

        if(no_a_remover != null){ // se o nó existe na arvore
            removido.setElemento(no_a_remover.getElemento()); // pego o elemento dele

            int valor_deslocado;

            if(ehExterno(no_a_remover))
                no_a_remover = null;

            else if(temFilhoEsquerdo(no_a_remover)){

                valor_deslocado = moverMaiorDaEsquerda(no_a_remover.getFilhoEsquerdo());

                no_a_remover.setElemento(valor_deslocado);
            }
            else {

                valor_deslocado = moverMenorDaDireita(no_a_remover.getFilhoEsquerdo());

                no_a_remover.setElemento(valor_deslocado);
            }        

            size--;

        }

        return removido;
    }

    private int moverMaiorDaEsquerda(No n){
        if(!temFilhoDireito(n)){
            int valor = n.getElemento();
            n = null;
            return valor;
        }
        else 
            return moverMaiorDaEsquerda(n.getFilhoDireito());
    }

    private int moverMenorDaDireita(No n){
        if(!temFilhoEsquerdo(n)){
            int valor = n.getElemento();
            n = null;
            return valor;
        }
        else 
            return moverMenorDaDireita(n.getFilhoDireito());
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

