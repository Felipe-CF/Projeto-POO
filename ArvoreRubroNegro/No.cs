using System;
using System.Collections;
using System.Collections.Generic;

public class No {
    protected int chave, cor;
    protected No fe, fd, pai;

    public No(int c){
        setchave(c);
        fd = null;
        fe = null;
        pai = null;
        cor = -1;
    }
    public No(){
        fd = null;
        fe = null;
        pai = null;
        cor = -1;
    }
    public No(int c, No o_pai, No o_fe, No o_fd){
        setchave(c);
        fd = o_fd;
        fe = o_fe;
        pai = o_pai;
        cor = -1;
    }
    public void setchave(int i){chave = i;}
    public void setPai(No p){pai = p;}
    public void setFilhoDireito(No f){fd = f;}
    public void setFilhoEsquerdo(No f){fe = f;}
    public void setCor(int f){cor = f;}
    public int getCor(){return cor;}
    public int getChave(){return chave;}
    public No getPai(){return pai;}
    public No getFilhoDireito(){return fd;}
    public No getFilhoEsquerdo(){return fe;}
    public override string ToString()
    {
        return $"{chave}";
    }
}
