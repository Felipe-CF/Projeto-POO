using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Projeto{
     public class Consulta : IIdentificador{
    public int id{get;set;}
    public int idPaciente{get;set;}
    public int idMedico{get;set;}
    public string status{get;set;}
    public DateTime data{get;set;}
    public string anamnese{get;set;}
    public string requisicao{get;set;}
    public override string ToString(){
        return $"Consulta {id}\nCódigo do Paciente: {idPaciente}      Código do Médico: {idMedico}\nMarcada para o dia {data.ToString("dd/MM/yyyy")}\nSituação: {status}\n";
    }

}

class NConsulta : NRegistros<Consulta>{
    public NConsulta() : base("Consultas.xml"){}

    public List<Consulta> ConsultasMarcadasDoPaciente(int pacienteId){
     List<Consulta> consultasDp = new List<Consulta>();
          foreach(Consulta obj in View.ListarConsultas())
               if(obj.idPaciente == pacienteId && obj.status == "marcada")  consultasDp.Add(obj); 
     return consultasDp;
    }
}

class Consultas{
    List<Consulta> objetos = new List<Consulta>();

    public void AbrirConsultaXml(){
        try{
        XmlSerializer arquivo = new XmlSerializer(typeof(List<Consulta>));
        StreamReader f = new StreamReader("Consultas.xml");
        objetos = (List<Consulta>) arquivo.Deserialize(f);
        f.Close();
        } catch(FileNotFoundException){}
    }

    public void EscreverConsultaXml(){
        XmlSerializer arquivo = new XmlSerializer(typeof(List<Consulta>));
        StreamWriter f = new StreamWriter("Consultas.xml");
        arquivo.Serialize(f, objetos);
        f.Close();  
    }
    public void Inserir(Consulta novoObjeto){
          AbrirConsultaXml(); 
           int id = objetos.Count+1;
          if(id == objetos.Last().id)
               id++;
          novoObjeto.id = id;
          objetos.Add(novoObjeto);
          EscreverConsultaXml(); 
     }

     public List<Consulta> Listar(){
          AbrirConsultaXml();
          return objetos;
     }
     public Consulta Listar(int id){
          AbrirConsultaXml();
          foreach(Consulta obj in objetos)
               if(obj.id == id) return obj;
          return default(Consulta); 
     }
     public int Atualizar(Consulta novoObjeto){
          AbrirConsultaXml(); 
          foreach(Consulta obj in objetos){
               if(obj.id == novoObjeto.id){
                obj.idPaciente = novoObjeto.idPaciente;
                obj.idMedico = novoObjeto.idMedico;
                obj.status = novoObjeto.status;
                obj.data = novoObjeto.data;
                obj.anamnese = novoObjeto.anamnese;
                obj.requisicao = novoObjeto.requisicao;
               EscreverConsultaXml(); 
               return 1;
               }       
          }
          return 0;    
     }
    
     public void Excluir(Consulta objeto){
         AbrirConsultaXml();
          Consulta obj = Listar(objeto.id);
          if(obj != null) objetos.Remove(obj); Console.WriteLine("Objeto Excluído!");
          EscreverConsultaXml();
     }

}


}
     
