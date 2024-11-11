using System;
using System.Collections.Generic;
namespace ArvoreAVL;

public class ArvoreAVL{
    private No raiz;
    private int size;

    public ArvoreAVL(){
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

        n.setPai(null);
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


    private bool temFilhoEsquerdo(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
            
        return n.getFilhoEsquerdo() != null;
    }


    private bool temFilhoDireito(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        return n.getFilhoDireito() != null;
    }


    private bool ehExterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
        
        // false or false = true
        return !(temFilhoDireito(n) || temFilhoDireito(n));
    }


    private bool ehInterno(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");
        
        return temFilhoEsquerdo(n) || temFilhoDireito(n);
    }


    private No buscaNo(No n){
        No r = raiz;

        while(r != null){

            // se n for menor, desce pela esquerda
            if (n.getChave() < r.getChave()){

                if(r.getFilhoEsquerdo() != null)
                    r = r.getFilhoEsquerdo();

                else 
                    return r;
            } 

            // se n for igual, para
            else if (n.getChave() == r.getChave())
                break;
            
            // se n for maior, desce pela direita
            else if (n.getChave() > r.getChave()){

                if(r.getFilhoDireito() != null)
                    r = r.getFilhoDireito(); 

                else 
                    return r;

            }  
            
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

                novo_no.setPai(n);

                if(novo_no.getChave() < novo_no.getPai().getChave())
                    novo_no.getPai().setFilhoEsquerdo(novo_no);
                else
                    novo_no.getPai().setFilhoDireito(novo_no);

            }

            size++;

            checaBalanco(novo_no, "insercao");
        }
    }


    private int menorDosMaiores(No n){
        
        while(n.getFilhoEsquerdo() != null)
            n = n.getFilhoEsquerdo();
        
        if (ehInterno(n)){
            n.getFilhoDireito().setPai(n.getPai());

            n.getPai().setFilhoEsquerdo(n.getFilhoDireito());
        }
        else{

            if(n.getPai().getFilhoEsquerdo() == n)
                n.getPai().setFilhoEsquerdo(null);
            else
                n.getPai().setFilhoDireito(null);
        }

        n.setPai(null);

        n.setFilhoDireito(null);

        n.setFilhoEsquerdo(null);
        
        return n.getChave();
    }

    
    public No remover(No n){

        if(ehVazia())
            throw new Exception("Arvore esta vazia");

        No a_remover = buscaNo(n); // tento achar o nó

        // se não achei ele...
        if(a_remover == null)
            throw new Exception("Nó não foi inserido na árvore");

        No o_maior = new No();

        int chave_maior;

        if (ehInterno(n)){

            if (temFilhoDireito(n)){

                o_maior.setchave(a_remover.getChave());

                chave_maior = menorDosMaiores(n.getFilhoDireito());

                a_remover.setchave(chave_maior);

                if(a_remover.getPai() == null)
                    raiz = a_remover;

            }
            
            else{

                o_maior.setchave(a_remover.getChave());

                a_remover.getFilhoEsquerdo().setPai(a_remover.getPai());
                
                if(ehRaiz(a_remover))
                    raiz = a_remover.getFilhoEsquerdo();

                else{

                    if(a_remover.getPai().getFilhoEsquerdo() == a_remover)
                        a_remover.getPai().setFilhoEsquerdo(a_remover.getFilhoEsquerdo());

                    else
                        a_remover.getPai().setFilhoDireito(a_remover.getFilhoEsquerdo());
                }
            }

            checaBalanco(a_remover, "remocao");

        }

        else{ // se o nó for externo...

            if(size == 1) // checo se é a raiz
                raiz = null;

            else{

                if(a_remover.getPai().getFilhoEsquerdo() == a_remover)
                    a_remover.getPai().setFilhoEsquerdo(null);

                else if(a_remover.getPai().getFilhoDireito() == a_remover){
                    a_remover.getPai().setFilhoDireito(null);
                }

                checaBalanco(a_remover, "remocao");
            }
            
            a_remover.setPai(null);

            o_maior = a_remover;

        }

        size--;

        return o_maior;
        
        }


    public void checaBalanco(No n, string operacao){
        int fb = 0;

        No pai = n.getPai();

        if(pai.getFilhoEsquerdo() == n){

            if(operacao == "insercao")
                fb = 1;

            else if(operacao == "remocao")
                fb = -1;
        }

        else if(pai.getFilhoDireito() == n){
            
            if(operacao == "insercao")
                fb = -1;

            else if(operacao == "remocao")
                fb = 1;
        }
        
        n = pai;

        n.setFator(n.getFator() + fb); // atualiza o fb do pai

        if(n.getFator() > 1)
            rotacaoDireita(n);

        else if (n.getFator() < -1)
            rotacaoEsquerda(n);
        
        if(n.getPai() != null)
            checaBalanco(n, operacao);

    }

    public void rotacaoDireita(No n){
        
        n.getFilhoEsquerdo().setPai(n.getPai());

        if(n.getPai() == null) // se for a raiz...
            raiz = n.getFilhoDireito();
        
        else{ // se não for...

            No pai = n.getPai();

            // if()

            n.getPai().setFilhoEsquerdo(n.getFilhoDireito());
        }

        n.setPai(n.getFilhoDireito());
        
        if(n.getFilhoDireito().getFilhoEsquerdo() != null)
            n.getFilhoDireito().getFilhoEsquerdo().setPai(n);
        
        n.setFilhoDireito(n.getFilhoDireito().getFilhoEsquerdo());

        n.getPai().setFilhoEsquerdo(n);


    }
    
    public void rotacaoEsquerda(No n){

        n.getFilhoDireito().setPai(n.getPai());

        if(n.getPai() == null)
            raiz = n.getFilhoDireito();
        
        else
            n.getPai().setFilhoDireito(n.getFilhoDireito());

        n.setPai(n.getFilhoDireito());
        
        if(n.getFilhoDireito().getFilhoEsquerdo() != null)
            n.getFilhoDireito().getFilhoEsquerdo().setPai(n);
        
        n.setFilhoDireito(n.getFilhoDireito().getFilhoEsquerdo());

        n.getPai().setFilhoEsquerdo(n);

    }


    private string emOrdem(No n){
        string retorno = "";

        if(ehInterno(n) && temFilhoEsquerdo(n) )
            retorno += emOrdem(n.getFilhoEsquerdo());
        
        retorno += n.getChave().ToString() + " ";

        if(ehInterno(n) && temFilhoDireito(n))
            retorno += emOrdem(n.getFilhoDireito());

        return retorno;
    }


    // public int profundidade(No n){
    //     if(ehRaiz(n))
    //         return 0;
    //     else
    //         return 1 + profundidade(n.getPai());
    // }


    // public int altura(No n){
    //     if(ehExterno(n))
    //         return 0;
        
    //     int h = 0;

    //     foreach(No w in n.filhos())
    //         h = Math.Max(h, altura(w));

    //     return h + 1;       

    // }


    public override string ToString()
    {
        string arvore = "[ ";

        arvore += emOrdem(raiz) + "]";
        return arvore;
    }


}



    // public string preOrdem(No n){

    //     string retorno = n.getElemento().ToString() + " ";

    //     if(ehInterno(n) && temFilhoEsquerdo(n))
    //         retorno += emOrdem(n.getFilhoEsquerdo());

    //     else if(ehInterno(n) && temFilhoDireito(n))
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

