using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Projeto{
       interface IIdentificador{
     int id {get;set;}
}

class NRegistros<T> where T : IIdentificador
 {
    private List<T> objetos = new List<T>();
    private string nomeArquivo;
    public NRegistros(string nomeArquivo){
     this.nomeArquivo = nomeArquivo;
    }
    public void AbrirXml(){ // Abro o xml e retorna uma lista com objetos do tipo especifico
     XmlSerializer arquivo = new XmlSerializer(typeof(List<T>));
     // tratamento de erros/exceções
     try{
          StreamReader f = new StreamReader(nomeArquivo);
          objetos = (List<T>)arquivo.Deserialize(f); // guardos os objetos na lista da classe Registros
          f.Close();
     } 
     catch(FileNotFoundException){
          Console.WriteLine($"Arquivo {nomeArquivo} não encontrado! Verifique se o nome está correto.");
     }
    }
    public void EscreverXml(){ // Abro o xml e atualizo as infos dos objetos do tipo especifico
     XmlSerializer arquivo = new XmlSerializer(typeof(List<T>));
     StreamWriter f = new StreamWriter(nomeArquivo);
     arquivo.Serialize(f, objetos);
     f.Close();
    }

     public void Inserir(T novoObjeto){
          AbrirXml(); // abro registro
          int id = 0;
          foreach(T objeto in objetos) // defino o id
               id++;
          novoObjeto.id = ++id;
          objetos.Add(novoObjeto);
          EscreverXml(); // registro novo objeto
     }

     public List<T> Listar(){
          AbrirXml();
          return objetos;
     }
     public T Listar(int id){
          AbrirXml();
          foreach(T obj in objetos)
               if(obj.id == id) return obj;
          return default(T); // retorna null do tipo T, o valor padrão
          // se fosse um struct seria 0
     }
     public int Atualizar(T novoObjeto){
          int index = 0;
          AbrirXml();
          foreach(T obj in objetos){
               if(obj.id == novoObjeto.id){
               objetos.Insert(index, novoObjeto); 
               objetos.RemoveAt(++index); 
               EscreverXml(); 
               return 1;
               }
               index++;        
          }
          return 0;    
     }
     public void Excluir(T objeto){
          AbrirXml();
          T obj = Listar(objeto.id);
          if(obj != null) objetos.Remove(obj);
          EscreverXml();
     }

 }



}

   
