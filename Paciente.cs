using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Projeto{
      public class Paciente : IIdentificador{
    public int id{get;set;}
    public string nome{get;set;}
    public DateTime nasc{get;set;}
    public string endereco{get;set;}
    public string contato{get;set;}
    public string senha{get;set;}
    public override string ToString(){
        return $"Paciente:{id} - {nome}  Nascimento:{nasc.ToString("dd/MM/yyyy")}\nEndereço: {endereco}  Contato:{contato}";
    }
}

    class NPaciente : NRegistros<Paciente>{
        public NPaciente() : base("Pacientes.xml"){}

        internal int Atualizar(int id, string nnome, DateTime data, string nendereco, string ncontato, string senha1)
        {
            throw new NotImplementedException();
        }
    }

    class Pacientes{
         List<Paciente> objetos = new List<Paciente>();

// abre o arquivo xml e armazena seus dados em uma lista
    public void AbrirPacienteXml(){
        try{
            XmlSerializer arquivo = new XmlSerializer(typeof(List<Paciente>));
            StreamReader f = new StreamReader("Pacientes.xml");
            objetos = (List<Paciente>) arquivo.Deserialize(f);
            f.Close();
        } catch(FileNotFoundException){}
    }

// escreve em um arquivo xml os dados da lista
    public void EscreverPacienteXml(){
        XmlSerializer arquivo = new XmlSerializer(typeof(List<Paciente>));
        StreamWriter f = new StreamWriter("Pacientes.xml");
        arquivo.Serialize(f, objetos);
        f.Close();  
        }
    
    public void Inserir(Paciente novoObjeto){
          AbrirPacienteXml(); // abro registro
          int id = 0;
          foreach(Paciente objeto in objetos) // defino o id
               id++;
          novoObjeto.id = ++id;
          objetos.Add(novoObjeto);
          EscreverPacienteXml(); // registro novo objeto
     }

     public List<Paciente> Listar(){
          AbrirPacienteXml();
          return objetos;
     }
     public Paciente Listar(int id){
          AbrirPacienteXml();
          foreach(Paciente obj in objetos)
               if(obj.id == id) return obj;
          return default(Paciente); // retorna null do tipo T, o valor padrão
          // se fosse um struct seria 0
     }
     public int Atualizar(Paciente Nobj){
          AbrirPacienteXml(); 
          foreach(Paciente obj in objetos){
               if(obj.id == Nobj.id){
                obj.nome = Nobj.nome;  
                obj.nasc = Nobj.nasc;
                obj.endereco = Nobj.endereco;
                obj.contato = Nobj.contato;
                obj.senha = Nobj.senha;              
                EscreverPacienteXml(); 
                return 1;
               }      
          }
          return 0;    
     }
     public void Excluir(Paciente objeto){
         AbrirPacienteXml();
          Paciente obj = Listar(objeto.id);
          if(obj != null) 
            objetos.Remove(obj); Console.WriteLine("Médico(a) Excluído(a)!");
            EscreverPacienteXml();
     }
    }


}

  