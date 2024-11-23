using System;
using System.Collections;
using System.Collections.Generic;

public class No {
    protected int chave, cor;
    protected No fe, fd, pai;
    protected bool duplo_negro;

    public No(int c){
        setChave(c);
        fd = null;
        fe = null;
        pai = null;
        cor = -1;
        duplo_negro = false;
    }
    public No(){
        fd = null;
        fe = null;
        pai = null;
        cor = -1;
        duplo_negro = false;
    }
    public No(int c, No o_pai, No o_fe, No o_fd){
        setChave(c);
        fd = o_fd;
        fe = o_fe;
        pai = o_pai;
        cor = -1;
        duplo_negro = false;

    }
    public void setChave(int i){chave = i;}
    public void setPai(No p){pai = p;}
    public void setFilhoDireito(No f){fd = f;}
    public void setFilhoEsquerdo(No f){fe = f;}
    public void setCor(int f){cor = f;}
    public void setDuploNegro(bool b){duplo_negro = b;}
    public bool getDuploNegro(){return duplo_negro;}
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
