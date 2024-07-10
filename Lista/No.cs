using System;
using System.Collections.Generic;
public class No {
    private No anterior;
    private No proximo;
    private Object elemento;

    public No(Object e){
        anterior = null;
        proximo = null;
        elemento = e;
    }
    public No(){
        anterior = null;
        proximo = null;
        elemento = null;
    }

    public void setElemento(Object e){
        elemento = e;
    }
    public void setAnterior(No n){
        anterior = n;
    }
    public void setProximo(No n){
        proximo = n;
    }
    public Object getElemento(){
        return elemento;
    }
    public No getAnterior(){
        return anterior;
    }
    public No getProximo(){
        return proximo;
    }

}