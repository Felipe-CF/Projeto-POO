using System;
using System.Collections.Generic;
public class ListaLigada{
    private No inicio;
    private No fim;
    private int size;

    public ListaLigada(){
        inicio = new No();
        fim = new No();
        inicio.setProximo(fim);
        fim.setAnterior(inicio);
        size=0;
    }

    public int tamanho(){
        return size;
    }

    public Boolean ehVazia(){
        return size == 0;
    }

    public Boolean ehPrimeiro(No n){
        if(ehVazia())
            throw new Exception("Só tem vento");

        return (n.getElemento() == inicio.getProximo().getElemento());
    }

    public Boolean ehUltimo(No n){
        if(ehVazia())
            throw new Exception("Só tem vento");

        return (n.getElemento() == fim.getAnterior().getElemento());
    }

    public No primeiro(){
        if(ehVazia())
            throw new Exception("Só tem vento");

        return inicio.getProximo();
    }

    public No ultimo(){
        if(ehVazia())
            throw new Exception("Só tem vento");

        return fim.getAnterior();
    }

    public No anterior(No p){
        if(ehVazia())
            throw new Exception("Só tem vento");

        return p.getAnterior();
    }

    public No proximo(No p){
        if(ehVazia())
            throw new Exception("Só tem vento");

        return fim.getProximo();
    }

    private No cataNo(No n){
        if(ehVazia())
            throw new Exception("Só tem vento");

        No retorno = new No();
        
        No cursor = inicio.getProximo();

        while(cursor != fim){
            if (n.getElemento() == cursor.getElemento()){
                retorno = cursor;
                break;
            }
            cursor = cursor.getProximo();
        }

        return retorno;
    }

    public void inserePrimeiro(Object o){
        No novo_no = new No(o);
        if(ehVazia()){
            inicio.setProximo(novo_no);
            fim.setAnterior(novo_no);
            novo_no.setAnterior(inicio);
            novo_no.setProximo(fim);
        }
        else{
            novo_no.setAnterior(inicio);
            novo_no.setProximo(inicio.getProximo());
            inicio.getProximo().setAnterior(novo_no);
            inicio.setProximo(novo_no);
        }
        size++;
    }

    public void insereUltimo(Object o){
        No novo_no = new No(o);
        if(ehVazia()){
            inicio.setProximo(novo_no);
            fim.setAnterior(novo_no);
            novo_no.setAnterior(inicio);
            novo_no.setProximo(fim);
        }
        else{
            novo_no.setProximo(fim);
            novo_no.setAnterior(fim.getAnterior());
            fim.getAnterior().setProximo(novo_no);
            inicio.setAnterior(novo_no);
        }
        size++;
    }

    public void insereAntes(No n, Object o){
        if(ehVazia())
            throw new Exception("Só tem vento");

        if(n.getElemento() ==  null)
            throw new Exception("Nó mal dado");

        No novo_no = new No(o);

        novo_no.setProximo(n);

        novo_no.setAnterior(n.getAnterior());

        n.getAnterior().setProximo(novo_no);

        n.setAnterior(novo_no);

        size++;

    }

    public void insereDepois(No n, Object o){
        if(ehVazia())
            throw new Exception("Só tem vento");

        if(n.getElemento() ==  null)
            throw new Exception("Nó mal dado");

        No novo_no = new No(o);

        novo_no.setAnterior(n);

        novo_no.setProximo(n.getProximo());

        n.getProximo().setAnterior(novo_no);

        n.setProximo(novo_no);

        size++;

    }

    public Object trocarElemento(No n, Object o){
        if(ehVazia())
            throw new Exception("Só tem vento");

        if(n.getElemento() ==  null)
            throw new Exception("Nó mal dado");

        Object retorno = n.getElemento();

        n.setElemento(o);

        return retorno;
    }
    public Object trocaTroca(No n, No q){
        
        return null;
    }

}