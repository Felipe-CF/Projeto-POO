using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Projeto{
    public class Especialidade : IIdentificador{
    public int id{get; set;}
    public string nomeEsp{get;set;}
    public override string ToString(){
        return $"{id} - {nomeEsp}";
    }

}



class NEspecialidade : NRegistros<Especialidade>{
    public NEspecialidade() : base("Especialidades.xml"){}
}

class Especialidades{
    List<Especialidade> objetos = new List<Especialidade>();

// abre o arquivo xml e armazena seus dados em uma lista
    public void AbrirEspecialidadeXml(){
        try{
        XmlSerializer arquivo = new XmlSerializer(typeof(List<Especialidade>));
        StreamReader f = new StreamReader("Especialidade.xml");
        objetos = (List<Especialidade>) arquivo.Deserialize(f);
        f.Close();
        } catch(FileNotFoundException){}
    }

// escreve em um arquivo xml os dados da lista
    public void EscreverEspecialidadeXml(){
        XmlSerializer arquivo = new XmlSerializer(typeof(List<Especialidade>));
        StreamWriter f = new StreamWriter("Especialidade.xml");
        arquivo.Serialize(f, objetos);
        f.Close();  
    }
    public void Inserir(Especialidade novoObjeto){
          AbrirEspecialidadeXml(); // abro registro
          int id = objetos.Count+1;
          if(id == objetos.Last().id)
               id++;
          novoObjeto.id = id;
          objetos.Add(novoObjeto);
          EscreverEspecialidadeXml(); // registro novo objeto
     }

     public List<Especialidade> Listar(){
          AbrirEspecialidadeXml();
          return objetos;
     }
     public Especialidade Listar(int id){
          AbrirEspecialidadeXml();
          foreach(Especialidade obj in objetos)
               if(obj.id == id) return obj;
          return default(Especialidade); // retorna null do tipo T, o valor padrão
          // se fosse um struct seria 0
     }
     public int Atualizar(Especialidade novoObjeto){
          AbrirEspecialidadeXml(); 
          foreach(Especialidade obj in objetos){
               if(obj.id == novoObjeto.id){
                obj.nomeEsp = novoObjeto.nomeEsp;
               EscreverEspecialidadeXml(); 
               return 1;
               }       
          }
          return 0;    
     }
    
     public void Excluir(Especialidade objeto){
         AbrirEspecialidadeXml();
          Especialidade obj = Listar(objeto.id);
          if(obj != null) objetos.Remove(obj); Console.WriteLine("Objeto Excluído!");
          EscreverEspecialidadeXml();
     }

}

}

    
