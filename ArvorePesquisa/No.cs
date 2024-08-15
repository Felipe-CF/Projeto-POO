using System;
using System.Collections;
using System.Collections.Generic;

public class No {
    protected int elemento;
    protected No fe, fd, pai;

    public No(int e){
        setElemento(e);
        fd = null;
        fe = null;
        pai = null;
    }
    public void setElemento(int i){elemento = i;}
    public void setPai(No p){pai = p;}
    public void setFilhoDireito(No f){fd = f;}
    public void setFilhoEsquerdo(No f){fe = f;}
    public int getElemento(){return elemento;}
    public No getPai(){return pai;}
    public No getFilhoDireito(){return fd;}
    public No getFilhoEsquerdo(){return fe;}

    public ArrayList filhos(){
        ArrayList os_filhos = new ArrayList();

        os_filhos.Add(fe);

        os_filhos.Add(fd);
        
        return os_filhos;
    }
}


public class Raiz{
    protected No raiz;

    public Raiz(){
        raiz = null;
    }
    public void setRaiz(No n){
        raiz = n;
    }
    public No getRaiz(){
        return raiz;
    }


}