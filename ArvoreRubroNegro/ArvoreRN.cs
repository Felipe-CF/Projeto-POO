using System;
using System.Collections.Generic;
using System.IO.Pipes;
namespace ArvoreRubroNegro;

public class ArvoreRN{
    private No raiz;

    private No folha;
    private int size;

    public ArvoreRN(){
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

        n.setCor(1);
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

        // se n for menor, desce pela esquerda
        if (n.getChave() < r.getChave() && r.getFilhoEsquerdo() != folha)
            return buscaNo(n, r.getFilhoEsquerdo());
        
        // se n for maior, desce pela direita
        else if (n.getChave() > r.getChave() && r.getFilhoDireito() != folha)
            return buscaNo(n, r.getFilhoDireito());
        
        return r;

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

    
    public No remover(No n){

        if(ehVazia())
            throw new Exception("Arvore esta vazia");

        No a_remover = buscaNo(n, raiz); // tento achar o nó

        No retorno = new No();

        // // se não achei ele...
        if(a_remover == folha)
            throw new Exception("Nó não foi inserido na árvore");
        
        else{

            retorno.setChave(a_remover.getChave());

            retorno.setCor(a_remover.getCor());

            int cor_removido = a_remover.getCor();

            if(a_remover.getFilhoDireito() != folha){

                var sucessor = menorDosMaiores(a_remover.getFilhoDireito()); // retorna chave e cor

                int cor_sucessor = sucessor.Item2;

                a_remover.setChave(sucessor.Item1); // atualizo os valores para remover o nó

                a_remover.setCor(cor_sucessor);

                size--;

                // situacao 1 será ignorada -  R e R

                if(cor_removido == 1 && cor_sucessor == -1){ // situacao 2 - N e R

                    a_remover.setCor(sucessor.Item2 * (-1));

                    a_remover.setChave(sucessor.Item1);

                    a_remover.setDuploNegro(true);

                }

                else if (cor_removido == 1 && cor_sucessor == 1) // situacao 3 - N e N
                    remocaoSituacaoTres(a_remover);
                
                else if(cor_removido == -1 && cor_sucessor == 1){ // situacao 4 - R e N
                    
                    No pai = a_remover.getPai();

                    if(pai.getFilhoDireito() == a_remover)
                        pai.getFilhoEsquerdo().setCor(-1);
                    
                    else
                        pai.getFilhoDireito().setCor(-1);

                    remocaoSituacaoTres(a_remover);

                }

            }

        }

        return retorno;

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

        if(no.getPai().getCor() == -1){

            No tio = oTio(no);

            No pai = no.getPai();

            No avo = pai.getPai();

            // caso 2...
            if(tio != folha){ // se o tio for rubro...

                tio.setCor(tio.getCor() * (-1));

                pai.setCor(pai.getCor() * (-1));

                if(avo != raiz){

                    avo.setCor(avo.getCor() * (-1));

                    checaBalancoInsercao(avo);

                }
                
            }

            else{
                insercaoCasoTres(no, pai, avo);
            }
        }

    }
    

    public void insercaoCasoTres(No no, No pai, No avo){
        
        // gira para a direita
        if(avo.getFilhoEsquerdo() == pai){ 

            if(pai.getFilhoDireito() == no){

                no.setPai(avo);

                avo.setFilhoEsquerdo(no);

                if(no.getFilhoEsquerdo() != folha)
                    no.getFilhoEsquerdo().setPai(pai);
                
                pai.setFilhoDireito(no.getFilhoEsquerdo());

                pai.setPai(no);

                no.setFilhoEsquerdo(pai);

                rotacaoDireita(pai, no, avo);

            }

            else
                rotacaoDireita(no, pai, avo);

            
        }

        // gira para a esquerda
        else{

            if(pai.getFilhoEsquerdo() == no){
                
                no.setPai(avo);

                avo.setFilhoDireito(no);

                if(no.getFilhoDireito() != folha)
                    no.getFilhoDireito().setPai(pai);
                
                pai.setFilhoEsquerdo(no.getFilhoDireito());

                pai.setPai(no);

                no.setFilhoDireito(pai);

                rotacaoEsquerda(pai, no, avo);
            }

            else
                rotacaoEsquerda(no, pai, avo);

            
            
        }

        no.setCor(no.getCor() * (-1));

        avo.setCor(avo.getCor() * (-1));
    }
    

    public void rotacaoDireita(No no, No pai, No avo){
        
        No bisavo = avo.getPai();

        if(bisavo == null)
            raiz = pai;

        else {

            if(bisavo.getFilhoDireito() == avo)
                bisavo.setFilhoDireito(pai);

            else 
                bisavo.setFilhoEsquerdo(pai);

        }

        pai.setPai(bisavo);

        if(pai.getFilhoDireito() != folha)
            pai.getFilhoDireito().setPai(avo);

        pai.getFilhoEsquerdo().setPai(avo);

        avo.setFilhoEsquerdo(pai.getFilhoDireito());

        avo.setPai(pai);

        pai.setFilhoDireito(avo);

    }
    

    public void rotacaoEsquerda(No no, No pai, No avo){

        No bisavo = avo.getPai();

        if(bisavo == null)
            raiz = pai;

        else{

            if(bisavo.getFilhoDireito() == avo)
                bisavo.setFilhoDireito(pai);
            else 
                bisavo.setFilhoEsquerdo(pai);

        }

        pai.setPai(bisavo);

        if(pai.getFilhoEsquerdo() != folha)
            pai.getFilhoEsquerdo().setPai(avo);

        avo.setFilhoDireito(pai.getFilhoEsquerdo());

        avo.setPai(pai);

        pai.setFilhoEsquerdo(avo);

    }


    private No oIrmao(No no){

        No pai = no.getPai();

        if(pai.getFilhoDireito() == no)
            return pai.getFilhoEsquerdo();

        else
            return pai.getFilhoDireito();
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