using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Projeto{
       public class Medico : IIdentificador{
    public int id{get; set;}
    public string nome{get; set;}
    public string crm{get; set;}
    public string senha{get;set;}
    public string contato{get;set;}
    public int idEspecialidade{get; set;}
    public override string ToString() {return $"{id} - {nome}   CRM:{crm}\nsenha = {senha} contato = {contato}";}
}
// Id Especialidade:{idEspecialidade}
class NMedico : NRegistros<Medico>{
        public NMedico() : base("Medicos.xml"){}
        public List<Medico> ListarPorEspecialidade(int idE){
        List<Medico> medicosPorEspecialidade = new List<Medico>(); // lista que terá todos os médicos
        // de uma determinada especialidade; ela só existirá nessa chamada
            foreach(Medico obj in View.MedicosListar())
                if(obj.idEspecialidade == idE) medicosPorEspecialidade.Add(obj);
            return medicosPorEspecialidade;
        }
    }

    class Medicos{
        List<Medico> objetos = new List<Medico>();

// abre o arquivo xml e armazena seus dados em uma lista
    public void AbrirMedicoXml(){
        try{
            XmlSerializer arquivo = new XmlSerializer(typeof(List<Medico>));
            StreamReader f = new StreamReader("Medicos.xml");
            objetos = (List<Medico>) arquivo.Deserialize(f);
            f.Close();
        } catch(FileNotFoundException){
            Console.WriteLine("Arquivo Medicos.xml não encontrado! Verifique se o nome está correto.");
        }
    }

// escreve em um arquivo xml os dados da lista
    public void EscreverMedicoXml(){
        XmlSerializer arquivo = new XmlSerializer(typeof(List<Medico>));
        StreamWriter f = new StreamWriter("Medicos.xml");
        arquivo.Serialize(f, objetos);
        f.Close();  
        }
    
    public void Inserir(Medico novoObjeto){
          AbrirMedicoXml(); // abro registro
          int id = 0;
          foreach(Medico objeto in objetos) // defino o id
               id++;
          novoObjeto.id = ++id;
          objetos.Add(novoObjeto);
          EscreverMedicoXml(); // registro novo objeto
     }

     public List<Medico> Listar(){
          AbrirMedicoXml();
          return objetos;
     }
     public Medico Listar(int id){
          AbrirMedicoXml();
          foreach(Medico obj in objetos)
               if(obj.id == id) return obj;
          return default(Medico); // retorna null do tipo T, o valor padrão
          // se fosse um struct seria 0
     }
     public int Atualizar(Medico novoObjeto){
          AbrirMedicoXml(); 
          foreach(Medico obj in objetos){
               if(obj.id == novoObjeto.id){
                obj.nome = novoObjeto.nome; 
                obj.crm = novoObjeto.crm; 
                obj.idEspecialidade = novoObjeto.idEspecialidade; 
                obj.senha = novoObjeto.senha;
                obj.contato = novoObjeto.contato;
                EscreverMedicoXml(); 
                return 1;
               }      
          }
          EscreverMedicoXml(); 
          return 0;    
     }
     public void Excluir(Medico objeto){
         AbrirMedicoXml();
          Medico obj = Listar(objeto.id);
          if(obj != null) 
            objetos.Remove(obj); Console.WriteLine("Médico(a) Excluído(a)!");
            EscreverMedicoXml();
        }

    }


}

 