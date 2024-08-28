using System;

public class No{
    private object elemento;
    private int chave;

    private No pai;

    private No fe;
    
    private No fd;

    public No(object e, int c){
        pai = null;

        fe = null;

        fd = null;

        elemento = e;

        chave = c;

    }

    public void setElemento(object e){elemento = e;}
    public void setChave(int c){chave = c;}
    public void setPai(No p){pai = p;}
    public void setFilhoDireito(No f){fd = f;}
    public void setFilhoEsquerdo(No f){fe = f;}
    public object getElemento(){return elemento;}
    public object getChave(){return chave;}
    public No getPai(){return pai;}
    public No getFilhoDireito(){return fd;}
    public No getFilhoEsquerdo(){return fe;}

    public ArrayList filhos(){
        ArrayList os_filhos = new ArrayList();

        os_filhos.Add(fe);

        os_filhos.Add(fd);
        
        return os_filhos;
    }
    public No menorDosFilhos(){
        if(fe == null)
            return null;
        
        if(fe.getChave() < fd.getChave() || fd == null)
            return fe;
        
        else if (fe.getChave() > fd.getChave())
            return fd;
    }
}
