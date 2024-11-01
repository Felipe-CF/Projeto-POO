using System;
using System.Collections.Generic;

public class Arvore{
    private No raiz;
    private int size;

    public Arvore(){
        raiz = null;
        size = 0;
     }

    public bool ehVazia(){
        return size == 0;
    }

    public int tamanho(){
        return size;
    }

    protected void fincandoRaiz(No n){
        if(!ehVazia())
            throw new Exception("A arvore ja foi plantada");
        
        raiz = n;

        size++;
    }

    public bool ehRaiz(No n){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");

        return raiz == n;
    }

    public No aRaiz(){   //
        if(ehVazia())
            throw new Exception("A arvore não existe");

        return raiz;
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
        
        // false or false = true
        return !(temFilhoDireito(n) || temFilhoDireito(n));
    }

    public bool ehInterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
        
        return temFilhoDireito(n) || temFilhoDireito(n);
    }

    public No buscaNo(No n){
        No r = raiz;

        while(r != null){
            if (n.getChave() < r.getChave()) // se for menor, desce pela esquerda
                r = r.getFilhoEsquerdo();

            if (n.getChave() == r.getChave())
                break;
            
            else
                r = r.getFilhoDireito(); 
            
        }
        
        return r;
        
    }

    public void inserirNo(No novo_no){

        if(novo_no == null)
            Console.WriteLine("Nó passado é vazio");
        
        else{

            if(ehVazia())
                fincandoRaiz(novo_no);

            else{

                No n = buscaNo(novo_no);

                novo_no.setPai(n.getPai());

                if(novo_no.getChave() < n.getPai().getChave())
                    n.getPai().setFilhoEsquerdo(novo_no);
                else
                    n.getPai().setFilhoDireito(novo_no);
                
                n.setPai(null);

            }

            size++;
        }
    }

    public No remover(No a_remover){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        No pai = a_remover.getPai();

        // checa se o no a se remover tem um filho direito
        if(a_remover.getFilhoDireito() != null){

            No no_maior = menorDosMaiores(a_remover.getFilhoDireito());
            
            if(pai.getFilhoEsquerdo() == a_remover)
                pai.setFilhoEsquerdo(no_maior);

            else if(pai.getFilhoDireito() == a_remover)
                pai.setFilhoDireito(no_maior);
            
            a_remover.getFilhoEsquerdo().setPai(no_maior);

            a_remover.getFilhoDireito().setPai(no_maior);
            
        }

        // se não, o no a se remover tem um filho direito
        else{

            if(pai.getFilhoEsquerdo() == a_remover)
                pai.setFilhoEsquerdo(a_remover.getFilhoEsquerdo());

            else if(pai.getFilhoDireito() == a_remover)
                pai.setFilhoDireito(a_remover.getFilhoEsquerdo());

            a_remover.getFilhoEsquerdo().setPai(a_remover.getPai());

        }
        
        a_remover.setPai(null);

        a_remover.setFilhoDireito(null);
            
        a_remover.setFilhoEsquerdo(null);

        size--;

        return null;
    }

    private No menorDosMaiores(No n){
        
        while(n.getFilhoEsquerdo() != null)
            n = n.getFilhoEsquerdo();
        
        if (ehExterno(n)){
            n.getPai().setFilhoEsquerdo(null);

            n.setPai(null);
        }

        else{
            n.getPai().setFilhoEsquerdo(n.getFilhoDireito());

            n.getFilhoDireito().setPai(n.getPai());

            n.setPai(null);

            n.setFilhoDireito(null);
        }
        
        return n;
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


    // public string preOrdem(No n){
    
    //     string retorno = n.getElemento().ToString() + " ";

    //     if(ehInterno(n) && temFilhoEsquerdo(n))
    //         retorno += emOrdem(n.getFilhoEsquerdo());

    //     else if(ehInterno(n) && temFilhoDireito(n))
    //         retorno += emOrdem(n.getFilhoDireito());

    //     return retorno;
    // }
    
    // public string emOrdem(No n){
    //     string retorno = "";

    //     if(ehInterno(n) && temFilhoEsquerdo(n) )
    //         retorno += emOrdem(n.getFilhoEsquerdo());
        
    //     retorno += n.getElemento().ToString() + " ";

    //     if(ehInterno(n) && temFilhoDireito(n))
    //         retorno += emOrdem(n.getFilhoDireito());

    //     return retorno;
    // }

    // public string posOrdem(No n){
    //     string retorno = "";

    //     if(ehInterno(n) && temFilhoEsquerdo(n))
    //         retorno += emOrdem(n.getFilhoEsquerdo());

    //     else if(ehInterno(n) && temFilhoDireito(n))
    //         retorno += emOrdem(n.getFilhoDireito());

    //     retorno += n.getElemento().ToString() + " ";

    //     return retorno;
    
    // }
}

