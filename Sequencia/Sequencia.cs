using System;
using System.Collections.Generic;

public class Sequencia<T>{
    private No<T> primeiro;
    private No<T> ultimo;
    private int size;

    public Sequencia(){
        size = 0;
        primeiro = null;
        ultimo = null;
        primeiro.setProximo(ultimo);
        ultimo.setAnterior(primeiro);
    }

    public int tamanho(){
        return size;
    }

    public boolean ehVazia(){
        return size == 0;
    }

    public No<T> elemAtRank(int r){
        if(ehVazia())
            return null;
        
        else if(r > size || r < 0)
            Console.WriteLine("indice invalido");

        else{
            No<T> cursor = primeiro;

            while(r >= 0){
                cursor = cursor.getProximo();

                --r;
            }

            return cursor;
        }
    } 

    public void insertAtRank(int r, No<T> n){
        if(ehVazia())
            return null;

        else if(r > size || r < 0)
            Console.WriteLine("indice invalido");
        
        else{
            No<T> cursor = atRank(r);

            insereAntesDe(cursor, n.getElemento());

            size++;
        }
    }

    public T replaceAtRank(int r, T o){
        if(ehVazia())
            return null;

        else if(r > size || r < 0)
            Console.WriteLine("indice invalido");
        else{
            No<T> cursor = atRank(r);

            T temp = cursor.getElemento();

            cursor.setElemento(o);

            return temp;
        }
        
    } 

    public No<T> removeAtRank(int r){
        if(ehVazia())
            return null;

        else if(r > size || r < 0)
            Console.WriteLine("indice invalido");

        else{
            No<T> cursor = atRank(r);

            No<T> temp = cursor;

            cursor.getAnterior().setProximo(cursor.getProximo());

            cursor.getProximo().setAnterior(cursor.getAnterior());

            if((--size) == 0){
                primeiro.setProximo(ultimo);
                ultimo.setAnterior(primeiro);
            }

            return temp;
        }
    } 

    public No<T> atRank(int r){
        if(ehVazia())
            Console.WriteLine("sequencia vazia");

        No<T> cursor = primeiro;

        while(r >= 0){
            cursor = cursor.getProximo();

            --r;
        }

        return cursor;
    }

    public int rankOf(No<T> n){
        if(ehVazia())
            Console.WriteLine("sequencia vazia");
        
        No<T> cursor = primeiro.getProximo();

        int temp = 0;

        while(cursor != ultimo){
            if(cursor.getElemento() == n.getElemento())
                return temp;
                
            +=temp;
        }

        return -1;
    }

    public No<T> primeiro(){
        return primeiro.getProximo();
    }

    public No<T> ultimo(){
        return anterior.getAnterior();
    }

    public No<T> antes(No<T> n){
        return n.getAnterior();
    }

    public No<T> depois(No<T> n){
        return n.getAnterior();
    }

    public void insereDepoisDe(No<T> n, T q){
        if(ehVazia())
            Console.WriteLine("a lista está vazia");
        else{
            No<T> o = new No<T>(q);

            o.setAnterior(n);

            o.setProximo(n.getProximo());

            n.getProximo().setAnterior(o);

            n.setProximo(o);

            size++;     
        }

    }

    public void insereAntesDe(No<T> n, T q){
        if(ehVazia())
            Console.WriteLine("a lista está vazia");
        else{

            No<T> o = new No<T>(q);

            o.setProximo(n);

            o.setAnterior(n.getAnterior());

            n.getAnterior().setProximo(o);

            n.setAnterior(o);

            size++;     
        }
    }

    public void inserePrimeiro(No<T> n){
        if(ehVazia()){
            primeiro.setProximo(n);
            ultimo.setAnterior(n);
            n.setAnterior(primeiro);
            n.setProximo(ultimo);
        }
        else{
            n.setAnterior(primeiro);
            n.setProximo(primeiro.getProximo());
            primeiro.getProximo().setAnterior(n);
            primeiro.setProximo(n);
        }
        size++;
    }

    public void insereUltimo(No<T> n){
        if(ehVazia()){
            primeiro.setProximo(n);
            ultimo.setAnterior(n);
            n.setAnterior(primeiro);
            n.setProximo(ultimo);
        }
        else{
            n.setAnterior(ultimo.getAnterior());
            n.setProximo(ultimo);
            ultimo.getAnterior().setProximo(n);
            ultimo.setAnterior(n);
        }
        size++;
    }

    public void remove(No<T> n){
        if(ehVazia())
            Console.WriteLine("a lista está vazia");
        else{
            n.getAnterior().setProximo(n.getProximo());

            n.getProximo().setAnterior(n.getAnterior());

            n.setProximo(null);

            n.setAnterior(null);

            if((--size) == 0){
                primeiro.setProximo(ultimo);

                ultimo.setAnterior(primeiro);
            }
        }
    }

    public void trocarElementos(No<T> n, No<T> o){
        T temp = n.getElemento();

        n.setElemento(o.getElemento());

        o.setElemento(temp);
    }
    
    public T trocarElementoDe(No<T> n, T q){
        T temp = n.getElemento();

        n.setElemento(q);

        return temp;
    }

}