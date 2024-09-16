using System;
using System.Collections;
using System.Collections.Generic;

public class Heap{
    private Raiz raiz;

    private No ultimo;
    private int tam;

    public Heap(){
        raiz = null;
        tam = 0;
        ultimo = raiz;
    }

    public int tamanho(){
        return tam;
    }

    public bool ehVazia(){
        return tam == 0;
    }
    private bool ehFilhoDireito(No n){
        No pai = n.getPai();

        return pai.getFilhoDireito() == n;
    }
    private bool ehFilhoEsquerdo(No n){
        No pai = n.getPai();

        return pai.getFilhoEsquerdo() == n;
    }

    private bool ehRaiz(No n){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");

        return raiz.getRaiz() == n;
    }

    public No aRaiz(){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");
        return raiz.getRaiz();
    }

    private bool temFilhoEsquerdo(No n){
        if(ehVazia())
            throw new Exception("Não existe heap");
            
        return n.getFilhoEsquerdo() != null;
    }

    private bool temFilhoDireito(No n){
        if(ehVazia())
            throw new Exception("Não existe heap");

        return n.getFilhoDireito() != null;
    }

    private bool ehExterno(No n){
        if(ehVazia())
            throw new Exception("Não existe heap");
        
        return !(temFilhoDireito(n) || temFilhoDireito(n));
    }

    private bool ehInterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        return temFilhoDireito(n) || temFilhoDireito(n);
    }

    private void enraizar(No n){
        raiz.setRaiz(n);
        ultimo = n;
        tam++;
    }

    public void inserir(int c, object o){
        No novo_no = new No(o, c);

        if(ehVazia())
            enraizar(novo_no);

        else{

            while(ehFilhoDireito(ultimo))
                ultimo = ultimo.getPai();

            if(ehFilhoEsquerdo(ultimo)){
                ultimo = ultimo.getPai();

                ultimo = ultimo.getFilhoDireito();
            }

            while(ultimo != null)
                ultimo = ultimo.getFilhoEsquerdo();
            
            ultimo = novo_no;

            tam++;

            upHeap();

        }    


    }

    private void upHeap(){
        No n = ultimo;

        No aux = new No();

        No pai = n.getPai();

        while(n.getChave() > pai.getChave() || n != raiz){

            int chave_pai = pai.getChave();

            object pai_o = pai.getElemento();

            pai.setChave(n.getChave());

            pai.setElemento(n.getElemento());

            n.setChave(chave_pai);

            n.setElemento(pai_o);

            n = n.getPai();

        }

    }
    

    public No removeMin(){
        No retorno = new No();

        No a_raiz = raiz.getRaiz();

        retorno.setChave(raiz.getRaiz().getChave());

        retorno.setElemento(raiz.getRaiz().getElemento());

        a_raiz.setChave(ultimo.getChave());

        a_raiz.setElemento(a_raiz.getElemento());

        ultimo = null;

        ultimo = ultimo.getPai();

        tam--;

        downHeap(a_raiz);

        realocandoUltimo();

    }

    private void downHeap(No n){
        
        while(ehInterno(n)){

            int chave_esquerda = n.getFilhoEsquerdo().getChave();

            int chave_direita = n.getFilhoDireito().getChave();

            if( chave_esquerda < chave_direita ){

                No esq = n.getFilhoEsquerdo();

                esq.setElemento(n.getElemento());

                esq.setChave(n.getChave());

                object obj = n.getFilhoEsquerdo().getElemento();
                
                n.setChave(chave_esquerda);

                n.setElemento(obj);

                n = n.getFilhoEsquerdo();

            }

            else if( chave_esquerda > chave_direita ){

                No dir = n.getFilhoDireito();

                dir.setElemento(n.getElemento());

                dir.setChave(n.getChave());

                object obj = n.getFilhoDireito().getElemento();
                
                n.setChave(chave_direita);

                n.setElemento(obj);

                n = n.getFilhoDireito();

            }

            else
                break;
        }
    }

    private No menorFilho(ArrayList f){
        No esq = f[0];

        No dir = f[1];

        if(esq != null){

            if(dir != null){

                if (esq.getChave() > dir.getChave())
                    return dir;

            }
            
            return esq;
            
        }

        if (dir != null)

            return dir;

    }

    private void realocandoUltimo(){

    }
}