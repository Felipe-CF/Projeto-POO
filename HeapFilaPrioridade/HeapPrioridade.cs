using System;


public class HeapPrioridade{
    private No raiz;

    private No ultimo;

    private int size;

    public HeapPrioridade(){
        raiz = null;
        size = 0;
    }

    public bool ehVazio(){
        return size == 0;
    }

    public int tamanho(){
        return size;
    }
    
    public No minimo(){
        return raiz.getFilhoEsquerdo;
    }

    private bool ehFilhoDireito(No n){
        No pai =  n.getPai();

        if (pai.getFilhoDireito() == n)
            return true;

        else
            return false; 
    }
    private bool ehFilhoEsquerdo(No n){
        No pai =  n.getPai();

        if (pai.getFilhoEsquerdo() == n)
            return true;

        else
            return false; 
    }
    
    public void inserir(object e, int c){
        No novo_no = new No(e, c);

        if (ehVazio()){
            raiz.setFilhoEsquerdo(novo_no);
            ultimo = novo_no;
        }
        
        else{
            while ehFilhoDireito(ultimo)
                ultimo = ultimo.getPai();
            
            if ehFilhoEsquerdo(ultimo){
                ultimo = ultimo.getPai();
                ultimo = ultimo.getFilhoDireito
            }

            while (ultimo != null)
                ultimo = ultimo.getFilhoEsquerdo()
            
            ultimo = novo_no;
        }

        size++;

        upHeap();
    }

    private void upHeap(){
        No no = ultimo;

        No pai = no.getPai();

        while (no.getChave() < pai.getChave()){
            int chave_pai  = pai.getChave();

            object elemento_pai = pai.getElemento();

            pai.setChave(no.getChave());

            pai.setElemento(no.getElemento());

            no.setChave(chave_pai);

            pai.setElemento(elemento_pai);

            no = no.getPai();

        }
    }

    public No removeMin(){
        raiz.setChave(ultimo.getChave());
        
        raiz.setElemento(ultimo.getElemento());

        ultimo = null;

        ultimo = ultimo.getPai();



    }

    private void downHeap(){
        No no = ultimo;
        

        while (no.getFilhoEsquerdo() != null){
            int chave_pai  = pai.getChave();

            object elemento_pai = pai.getElemento();

            pai.setChave(no.getChave());

            pai.setElemento(no.getElemento());

            no.setChave(chave_pai);

            pai.setElemento(elemento_pai);

            no = no.getPai();

        }
    }


}