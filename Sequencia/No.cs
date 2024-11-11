using System;
using System.Collections.Generic;

public class No<T>{
    private int e;
    private No<T> proximo;
    private No<T> anterior;

    public No(T elem){
        e = elem;
    }
    public void setElemento(T elem){
        e = elem;
    }
    public T getElemento(){
        return e;
    }
    public void setProximo(No<T> n){
        proximo = n;
    }
    public No<T> getProximo(){
        return proximo;
    }
    public void setAnterior(No<T> n){
        anterior = n;
    }
    public No<T> getAnterior(){
        return anterior;
    }
}