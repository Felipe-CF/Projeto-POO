using System;
using System.Collections.Generic;
using System.IO.Pipes;
namespace ArvoreRubroNegro;

public class Arvore{
    private No raiz;

    private No folha;
    private int size;

    public Arvore(){
        raiz = null;

        size = 0;

        folha = null;

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

        n.setFilhoEsquerdo(folha);

        n.setFilhoDireito(folha);

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
            
        return n.getFilhoEsquerdo() != folha;
    }


    private bool temFilhoDireito(No n){
        if(ehVazia())
            throw new Exception("Não existe arvore");

        return n.getFilhoDireito() != folha;
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


    private No buscaNo(No n, No r){

        if(r == folha)
            return r;

        // se n for menor, desce pela esquerda
        if (n.getChave() < r.getChave())
            return buscaNo(n, r.getFilhoEsquerdo());
        
        // se n for maior, desce pela direita
        else if (n.getChave() == r.getChave())
            return r;
        
        else
            return buscaNo(n, r.getFilhoDireito());

    }
        

    public void inserirNo(No novo_no){

        if(novo_no == null)
            Console.WriteLine("Nó passado é vazio");
        
        else{

            if(ehVazia())

                fincandoRaiz(novo_no);

            else{

                No n = buscaNo(novo_no, raiz);

                if (n != novo_no){

                    novo_no.setPai(n);

                    novo_no.setFilhoEsquerdo(folha);

                    novo_no.setFilhoDireito(folha);

                    if(novo_no.getChave() < novo_no.getPai().getChave())
                        novo_no.getPai().setFilhoEsquerdo(novo_no);
                    else
                        novo_no.getPai().setFilhoDireito(novo_no);
                }


            }

            size++;

            if(size > 1)
                checaBalancoInsercao(novo_no);
        }
    }


    private (int, int) menorDosMaiores(No n){
        
        No menor_maior = n;

        while(menor_maior.getFilhoEsquerdo() != folha)
            menor_maior = menor_maior.getFilhoEsquerdo();
        
        if(ehInterno(menor_maior)){

            No pai = menor_maior.getPai();

            pai.setFilhoEsquerdo(menor_maior.getFilhoDireito());

            menor_maior.getFilhoDireito().setPai(pai);

        }

        menor_maior.setPai(null);

        menor_maior.setFilhoDireito(null);
        
        menor_maior.setFilhoEsquerdo(null);

        return (menor_maior.getChave(), menor_maior.getCor());
    }

    
    public void remover(No n){

        if(ehVazia())
            throw new Exception("Arvore esta vazia");

        No a_remover = buscaNo(n, raiz); // tento achar o nó

        // // se não achei ele...
        if(a_remover == folha)
            throw new Exception("Nó não foi inserido na árvore");
        
        else if(a_remover.getFilhoDireito() != folha){

            var sucessor = menorDosMaiores(a_remover.getFilhoDireito()); // retorna chave e cor

            int cor_sucessor = sucessor.Item2;

            int cor_removido = sucessor.Item2;

            a_remover.setChave(sucessor.Item1); // atualizo os valores para remover o nó

            a_remover.setCor(cor_sucessor);

            size--;

            // situacao 1 será ignorada

            if(cor_removido == 1 && cor_sucessor == -1){ // situacao 2

                a_remover.setCor(sucessor.Item2 * (-1));

                a_remover.setChave(sucessor.Item1);

                a_remover.setDuploNegro(true);

            }
            else if (a_remover.getCor() == 1 && cor_removido == 1) // situacao 3
                remocaoSituacaoTres(a_remover);
            
            else{

            }

        }
        

        
        }


    private No oTio(No no){

        No avo = no.getPai().getPai();

        if(avo.getFilhoDireito() == no.getPai())
            return avo.getFilhoEsquerdo();
        else 
            return avo.getFilhoDireito();

    }


    public void checaBalancoInsercao(No no){

        // caso 1 é ignorado

        if(no.getPai().getCor() != -1 && no != raiz){

            No tio = oTio(no);

            No pai = no.getPai();

            No avo = pai.getPai();

            // caso 2...
            if(tio.getCor() == -1){ // se o tio for rubro...

                tio.setCor(tio.getCor() * (-1));

                pai.setCor(pai.getCor() * (-1));

                if(avo != raiz)
                    avo.setCor(avo.getCor() * (-1));
                
                checaBalancoInsercao(avo);
            }

            else{
                insercaoCasoTres(no, pai, avo);
            }
        }

    }
    

    public void insercaoCasoTres(No no, No pai, No avo){
        
        if(avo.getFilhoEsquerdo() == pai){

            if(pai.getFilhoDireito() == no)
                rotacaoEsquerda(no, pai, avo);
            
            rotacaoDireita(no, pai, avo);
        }
        else{
            if(pai.getFilhoEsquerdo() == no)
                rotacaoDireita(no, pai, avo);
            
            rotacaoEsquerda(no, pai, avo);
            
        }

        pai.setCor(pai.getCor() * (-1));
    }
    

    private No oIrmao(No no){

        No pai = no.getPai();

        if(pai.getFilhoDireito() == no)
            return pai.getFilhoEsquerdo();

        else
            return pai.getFilhoDireito();
    }


    private void checaBalancoRemocao(No no){
        
        No irmao = oIrmao(no);

        No pai = no.getPai();

        No avo = pai.getPai();

        if(irmao.getCor() == -1){
            rotacaoEsquerda(no, pai, avo);
            
        }

    }


    private void remocaoSituacaoTres(No no){

        No irmao = oIrmao(no);

        No pai = no.getPai();

        No avo = pai.getPai();

        // se irmao for rubro [caso 1]
        // se x é negro, tem irmão w rubro e pai negro.
        if(no.getCor() == 1 &&  irmao.getCor() == -1 && pai.getCor() == 1){

            remocaoCaso1(no, pai, irmao, avo);

            remocaoCaso2b(no, pai, irmao, avo);

        }
        
        bool se_filho_direito_preto = (irmao.getFilhoDireito().getCor() == 1) || (irmao.getFilhoDireito() == folha);

        bool se_filho_esquerdo_preto = (irmao.getFilhoEsquerdo().getCor() == 1) || (irmao.getFilhoEsquerdo() == folha);

        // [caso 2a]
        // se x é negro, tem irmão negro, com filhos negros e pai negro.
        if(no.getCor() == 1 && irmao.getCor() == 1 && irmao.getFilhoDireito().getCor() == 1 && 
        se_filho_esquerdo_preto && se_filho_direito_preto && pai.getCor() == 1)
            remocaoCaso2a(no, pai, irmao, avo);

        // [caso 2b]
        // se no é negro, tem irmão negro, com filhos negros e pai rubro.
        if(no.getCor() == 1 && irmao.getCor() == 1 && irmao.getFilhoDireito().getCor() == 1 && 
        se_filho_esquerdo_preto && se_filho_direito_preto && pai.getCor() == -1)
            remocaoCaso2b(no, pai, irmao, avo);

        // [caso 3]
        // se no é negro, tem irmão negro, com filho direito negro e o esquerdo, rubro; pai de qualquer cor.
        if(no.getCor() == 1 && irmao.getCor() == 1 && irmao.getFilhoDireito().getCor() == 1 && 
        irmao.getFilhoEsquerdo().getCor() == -1)
            remocaoCaso3(no, pai, irmao, avo);
        
        // [caso 4]
        // se no é negro, tem irmão negro, com filho direito negro e o esquerdo, qualquer cor; pai de qualquer cor.
        if(no.getCor() == 1 && irmao.getCor() == 1 && irmao.getFilhoDireito().getCor() == -1)
            remocaoCaso4(no, pai, irmao, avo);
        
    }


    private void remocaoCaso1(No no, No pai, No irmao, No avo){
        // Faça uma rotação simples esquerda
        
        no.setDuploNegro(true); // marca como duplo negro

        // fazer rotação simples esquerda
        if(pai.getFilhoDireito() == irmao){

            irmao.getFilhoEsquerdo().setPai(pai);

            pai.setFilhoDireito(irmao.getFilhoEsquerdo());

            if(pai != raiz){ // se não for a raiz...

                if(avo.getFilhoDireito() == pai) // procuro saber de que lado está o pai
                    avo.setFilhoDireito(irmao);

                else
                    avo.setFilhoEsquerdo(irmao);

            }

            else{ // se for...

                raiz = irmao;

                irmao.setPai(null);

            }

            irmao.setFilhoEsquerdo(pai);

            pai.setPai(irmao);

        }

        // fazer rotação simples direita
        else{

            irmao.getFilhoDireito().setPai(pai);

            pai.setFilhoEsquerdo(irmao.getFilhoDireito());

            if(pai != raiz){ // se não for a raiz...

                if(avo.getFilhoDireito() == pai) // procuro saber de que lado está o pai
                    avo.setFilhoDireito(irmao);

                else
                    avo.setFilhoEsquerdo(irmao);

            }

            else{ // se for...

                raiz = irmao;

                irmao.setPai(null);

            }

            irmao.setFilhoDireito(pai);

            pai.setPai(irmao);

        }
        
        pai.setCor(pai.getCor() * (-1)); // Pinte pai de rubro
        
        irmao.setCor(irmao.getCor() * (-1)); // Pinte irmao de negro 

    }


    private void remocaoCaso2a(No no, No pai, No irmao, No avo){

        // pinta o irmao de rubro
        irmao.setCor(irmao.getCor() * (-1));

        // muda a referencia do duplo negro para o pai 
        no.setDuploNegro(false);

        pai.setDuploNegro(true);
    }


    private void remocaoCaso2b(No no, No pai, No irmao, No avo){
        // pintar o irmao de rubro...
        irmao.setCor(irmao.getCor() * (-1));

        // e o pai de negro
        pai.setCor(pai.getCor() * (-1));

        // retirar o duplo negro
        no.setDuploNegro(false);

    }


    private void remocaoCaso3(No no, No pai, No irmao, No avo){

        // rotação será feita no irmao e depois o caso 4 é chamado
        // fazer rotação simples direita, independente do lado do irmão

        irmao.getFilhoEsquerdo().setPai(pai);

        if(pai.getFilhoDireito() == irmao){

            pai.setFilhoDireito(irmao.getFilhoEsquerdo());

            if(irmao.getFilhoEsquerdo().getFilhoDireito() != folha)
                irmao.getFilhoEsquerdo().getFilhoDireito().setPai(irmao);
            
            irmao.setFilhoEsquerdo(irmao.getFilhoEsquerdo().getFilhoDireito());

            pai.getFilhoDireito().setFilhoDireito(irmao);

            irmao.setPai(pai.getFilhoDireito());
        }
        
        else{

            pai.setFilhoEsquerdo(irmao.getFilhoEsquerdo());

            if(irmao.getFilhoEsquerdo().getFilhoDireito() != folha)
                irmao.getFilhoEsquerdo().getFilhoDireito().setPai(irmao);
            
            irmao.setFilhoEsquerdo(irmao.getFilhoEsquerdo().getFilhoDireito());

            pai.getFilhoEsquerdo().setFilhoDireito(irmao);

            irmao.setPai(pai.getFilhoEsquerdo());
        }
        
        pai.setCor(pai.getCor() * (-1)); // Pinte pai de rubro
        
        irmao.setCor(irmao.getCor() * (-1)); // Pinte irmao de negro 

    }

    private void remocaoCaso4(No no, No pai, No irmao, No avo){

        int cor_pai = pai.getCor();

        irmao.setPai(pai.getPai());

        if(pai == raiz)
            raiz = irmao;
        
        else{

            if(avo.getFilhoDireito() == pai)
                avo.setFilhoDireito(irmao);

            else
                avo.setFilhoEsquerdo(irmao);
        }

        if(pai.getFilhoDireito() == irmao){

            pai.setFilhoDireito(irmao.getFilhoEsquerdo());

            irmao.getFilhoEsquerdo().setPai(pai);

            irmao.setFilhoEsquerdo(pai);
            
        }

        else{

            pai.setFilhoEsquerdo(irmao.getFilhoDireito());

            irmao.getFilhoDireito().setPai(pai);

            irmao.setFilhoDireito(pai);

        }

        pai.setPai(irmao);

        pai.setCor(1); // pinto o pai de preto

        irmao.setCor(cor_pai); // cor antiga do pai

        irmao.getFilhoDireito().setCor(1);

    }

    public void rotacaoDireita(No no, No pai, No avo){
        
        No bisavo = avo.getPai();

        if(bisavo != null){

            if(bisavo.getFilhoDireito() == avo)
                bisavo.setFilhoDireito(pai);
            else 
                bisavo.setFilhoEsquerdo(pai);

        }

        pai.setPai(bisavo);

        pai.getFilhoDireito().setPai(avo);

        avo.setFilhoEsquerdo(pai.getFilhoDireito());

        avo.setPai(pai);

        pai.setFilhoDireito(avo);

    }
    

    public void rotacaoEsquerda(No no, No pai, No avo){

        No bisavo = avo.getPai();

        if(bisavo != null){

            if(bisavo.getFilhoDireito() == avo)
                bisavo.setFilhoDireito(pai);
            else 
                bisavo.setFilhoEsquerdo(pai);

        }

        pai.setPai(bisavo);

        pai.getFilhoEsquerdo().setPai(avo);

        avo.setFilhoDireito(pai.getFilhoEsquerdo());

        avo.setPai(pai);

        pai.setFilhoEsquerdo(avo);

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

    public override string ToString()
    {
        return "[ " + emOrdem(raiz) + "]";
    }


}