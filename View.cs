using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;

namespace  Projeto{
    static class View{
// Crio a lista de especialidade através da herança NRegistros mudando o tipo T para Especialidade(id, nomeEsp)
// Ao criar uma nova especialidade, preciso criar uma Especialidade (objeto da entidade - nome) e depois eu instancio a classe NEspecialidade
// (passando o nome do arquivo para NRegistros - Especialidade.xml) que me permitirá acessar o CRUD do arquivo Xml 

    //==================================================
    //                      ADMIN
    //==================================================
    public static void EspecialidadeInserir(string nome){
        Especialidade e = new Especialidade{nomeEsp = nome};
        NEspecialidade nE = new NEspecialidade();
        nE.Inserir(e);
    }
    public static List<Especialidade> EspecialidadeListar(){
        NEspecialidade nE = new NEspecialidade();
        return nE.Listar();
    }
    public static void EspecialidadeAtualizar(int idAtt, string nomeAtt){
        int op = 0;
        if(idAtt < 1) throw new ArgumentOutOfRangeException("Id inválido");
        NEspecialidade nE = new NEspecialidade();
        Especialidade novaEspec = new Especialidade{nomeEsp = nomeAtt};
        foreach(Especialidade obj in EspecialidadeListar()){
            if(obj.id == idAtt) novaEspec.id = obj.id; op = nE.Atualizar(novaEspec);
        }
        if(op == 1) Console.WriteLine("Alteração feita com sucesso!");
        else{Console.WriteLine("Id não encontrado! Alteração não realizada.");}
    }
    public static void EspecialidadeExcluir(int idEx){
        Especialidade esp = new Especialidade{id = idEx};
        NEspecialidade nE = new NEspecialidade();
        nE.Excluir(esp);
    }

    public static void MedicoInserir(string nomeMed, string Crm, int idEspec, string sEnha, string cOntato){
        if(idEspec < 1) throw new ArgumentOutOfRangeException("idEspecialidade inválido");
        Medico med = new Medico{nome = nomeMed, crm = Crm, idEspecialidade = idEspec, senha = sEnha, contato = cOntato};
         NMedico m = new NMedico();
         m.Inserir(med);
    }
    
    public static List<Medico> MedicosListar(){
        NMedico nM = new NMedico();
        return nM.Listar();
    }
    
    public static void MedicoAtualizar(int idAtt, string nomeAtt, string crmAtt, int idEspecAtt, string senhA, string contatO){
        int op = 0;
        if(idAtt < 1 || idEspecAtt < 1) throw new ArgumentOutOfRangeException("Id inválido");
        NMedico nM = new NMedico();
        Medico novaMed = new Medico{ id = idAtt, nome = nomeAtt, crm = crmAtt, idEspecialidade = idEspecAtt, senha = senhA, contato = contatO};
        foreach(Medico obj in MedicosListar()){
            if(obj.id == idAtt) op = nM.Atualizar(novaMed);
        }
        if(op == 1) Console.WriteLine("Alteração feita com sucesso!");
        else{Console.WriteLine("Id não encontrado! Alteração não realizada.");}
    }

    public static void MedicoExcluir(int idEx){
        Medico med = new Medico{id = idEx};
        NMedico nM = new NMedico();
        nM.Excluir(med);   
    }

    public static void ListarPacientes(){
        NPaciente nP = new NPaciente();
        List<Paciente> pacientes = nP.Listar();
        Console.WriteLine(" ---- Pacientes cadastrados ---- ");
        foreach(Paciente obj in pacientes)
            Console.WriteLine(obj);
        Console.WriteLine(" ------------------------------- ");
    }

    public static void PacienteExcluir(int idP){
        try{
            NPaciente nP = new NPaciente();
        List<Paciente> pacientes = nP.Listar();
        foreach(Paciente obj in pacientes)
            if(obj.id == idP) nP.Excluir(obj);
        } catch(FileNotFoundException){}
    }

    public static List<Consulta> ListarConsultas(){
        NConsulta nC = new NConsulta();
        return nC.Listar();
    }

    public static List<Consulta> AtualizandoConsulta(int idC){
        NConsulta nC = new NConsulta();
        return nC.Listar(); 
       }
       public static void ConsultaExcluir(int idC){
        try{
            NConsulta nC = new NConsulta();
            Consulta consultA = new Consulta{id = idC};
            nC.Excluir(consultA); 
        } catch(FileNotFoundException){}
       }

        

    //==================================================
    //                      PACIENTE 
    //==================================================
                
    public static Paciente LoginPaciente(){
        try{
            Console.WriteLine(" ---------    Tela de Login ---------- ");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();
            NPaciente nP = new NPaciente();
            List<Paciente> pacientes = nP.Listar();
            foreach(Paciente user in pacientes) 
            if(user.contato == email && user.senha == senha) return user;
        }
        catch(FormatException){}
        catch(FileNotFoundException){}
            return default(Paciente);
    }

    public static void MarcarConsulta(int idPac, int idMed, DateTime data1){
        NConsulta nC = new NConsulta();
        Consulta c = new Consulta{idPaciente = idPac, idMedico = idMed, status = "marcada", data = data1};
        nC.Inserir(c);
    }

    public static void MedicosPorEspecialidade(int idE){
        try{
            NMedico nM = new NMedico();
            List<Medico> listaM = nM.ListarPorEspecialidade(idE);
            //Console.WriteLine(listaM[0]);
            Console.WriteLine($" ------ Médicos ------- ");
            foreach(Medico obj in listaM) Console.WriteLine($"{obj.id} - {obj.nome}");
        } catch(FormatException){} catch(FileNotFoundException){} catch(Exception ex){}
    }

    public static void GerarComprovante(int UserId, DateTime data){
        NConsulta nC = new NConsulta();
        List<Consulta> consultas = nC.Listar();
        consultas.Reverse();
        NPaciente nP = new NPaciente();
        NMedico nM = new NMedico();
        foreach(Consulta obj in consultas)
            if(obj.idPaciente == UserId && obj.data == data && obj.status == "marcada"){
                using(StreamWriter f = new StreamWriter("ComprovanteDeConsulta.txt"))
                    f.WriteLine($"Consulta:{obj.id}\nPaciente:{nP.Listar(UserId).nome}  Data de Nascimento:{nP.Listar(obj.idPaciente).nasc.ToString("dd/MM/yyyy")}\nData do agendamento:{DateTime.Now}  Data da consulta:{obj.data.ToString("dd/MM/yyyy")}\nMédico(a):{nM.Listar(obj.idMedico).nome}  CRM:{nM.Listar(obj.idMedico).crm}\nSituação:{obj.status}");
                break;  
            }
    }
    
    public static void PacienteInserir(string nomeP, DateTime data, string enderecoP, string contatoP, string senhaP){
        Paciente paciente = new Paciente{nome = nomeP, nasc = data, endereco = enderecoP, contato = contatoP, senha = senhaP};
         NPaciente nP = new NPaciente();
         nP.Inserir(paciente);
         Console.WriteLine("Parabéns, seu cadastro foi realizado com sucesso.");
    }

    public static Paciente MostrarDadosPaciente(int id){
        NPaciente nP = new NPaciente();
        return nP.Listar(id);
    }
    
    public static string PegaUltimaRequisicao(int idU){
        try{
            NConsulta nC = new NConsulta();
            List<Consulta> consultas = nC.Listar();
            NPaciente nP = new NPaciente();
            NMedico nM = new NMedico();
            consultas.Reverse();
            foreach(Consulta obj in consultas){
                if(idU == obj.idPaciente && obj.status == "realizada") 
                    return $"Código paciente:{obj.idPaciente}   Nome:{nP.Listar(obj.idPaciente).nome}   Data de Nascimento:{nP.Listar(obj.idPaciente).nasc.ToString("dd/MM/yyyy")}\nCódigo Médico:{obj.idMedico}   Médico(a):{nM.Listar(obj.idMedico).nome}   CRM:{nM.Listar(obj.idMedico).crm}\nData da Consulta:{obj.data.ToString("dd/MM/yyyy")}   Requisição: {obj.requisicao}";
            } 
        } catch(FileNotFoundException){}
        return $"Sem consultas realizadas";
    }
    public static void PacienteAtualizar(int Id, string Nnome, DateTime data, string Nendereco, string Ncontato){
        NPaciente nP = new NPaciente(); 
        string senha1;
        do{
            Console.Write("Digite a senha de acesso(sem espaços): ");
            senha1 = Console.ReadLine();
            Console.Write("Confirme a senha: ");
        }while(!senha1.Equals(Console.ReadLine()));
        Paciente novoPaciente = new Paciente{id = Id, nome = Nnome, nasc = data, 
        endereco = Nendereco, contato = Ncontato, senha = senha1};
        int r = nP.Atualizar(novoPaciente);
        if(r == 1) Console.WriteLine("Paciente atualizado com sucesso!");
        else{Console.WriteLine("Não foi possível atualizar seus dados!");}
    }

    public static void AlterandoConsultaMarcada(int idC){
        NConsulta nC = new NConsulta();
        Consulta consulta = nC.Listar(idC);

        Console.WriteLine(" ------ Especialidades ------ ");
        foreach(Especialidade obj in EspecialidadeListar())
                Console.WriteLine(obj);
        Console.WriteLine("------------------------------");
        Console.Write("Informe o id da especialidade: ");
        int idE = int.Parse(Console.ReadLine());

        MedicosPorEspecialidade(idE);
        Console.Write("Informe o id do médico: ");
        consulta.idMedico = int.Parse(Console.ReadLine());

        string data;
        DateTime data1;
        do{
            Console.Write("Informe a data da consulta(ex: dia/mes/ano): ");
            data = Console.ReadLine();
            if(DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data1)){
                if(data1 >= DateTime.Today) break;
                else Console.WriteLine("Digite uma data de um data válida");
            }
            else{throw new FormatException("Data inválida");}
        } while(true);

        consulta.data = data1;
        nC.Atualizar(consulta);
        Console.WriteLine("Consulta atualizada com sucesso");
        }

    public static void ListarMinhasConsultasMarcadas(int idP){
        NConsulta nC = new NConsulta();
        List<Consulta> minhasC = nC.ConsultasMarcadasDoPaciente(idP);
        if(minhasC.Count == 0){ Console.WriteLine("Você não tem consultas marcadas"); return;}
        Console.WriteLine(" ---------- Suas consultas ---------- ");
        foreach(Consulta obj in minhasC) Console.WriteLine(obj);
    }

    public static void ListarTodasAsMinhasConsultas(int idP){
        NConsulta nC = new NConsulta();
        List<Consulta> consultas = nC.Listar();
        NEspecialidade nE = new NEspecialidade();
        Especialidade e;
        NMedico nM = new NMedico();
        Medico m;
        int q = 0;
        Console.WriteLine("\n----------- Seu histórico de consultas ---------- ");
        foreach(Consulta obj in consultas){
            if(idP == obj.idPaciente) {   
                ++q;
                m = nM.Listar(obj.idMedico);
                e = nE.Listar(m.idEspecialidade);
                Console.WriteLine($"Consulta {q}    ID:{obj.id}");
                Console.WriteLine($"Médico(a):{m.nome}  Especialidade:{e.nomeEsp}   CRM:{m.crm}");
                Console.WriteLine($"Data da consulta:{obj.data.ToString("dd/MM/yyyy")}  Situação:{obj.status}\n");
            }
        }
        Console.WriteLine("--------------------------------------------------\n");
    }



    //==================================================
    //                      MEDICOS 
    //==================================================
    public static Medico LoginMedico(){
        try{
            Console.WriteLine(" ---------  Tela de Login ---------- ");
            Console.Write("Email: ");
            string email = Console.ReadLine();
            Console.Write("Senha: ");
            string senha = Console.ReadLine();
            Console.WriteLine("------------------------------------ ");
            NMedico nP = new NMedico();
            List<Medico> medicos = nP.Listar();
            foreach(Medico user in medicos) 
            if(user.contato == email && user.senha == senha) return user;
        }
        catch(FormatException){}
        catch(FileNotFoundException){}
            return default(Medico);
    }
    public static Consulta ConsultaIniciar(int idMed, int idP, DateTime dataHoje){
        try{
            NConsulta nC = new NConsulta();
            List<Consulta> consultas = nC.Listar();
            foreach(Consulta obj in consultas)
                if(obj.idMedico == idMed && obj.idPaciente == idP && obj.status == "marcada") 
                    return obj;
        } catch(FileNotFoundException){}
        return default(Consulta);
        
    }

    public static Consulta Consulta(Consulta consultA){
            Console.WriteLine(" ------ Consulta iniciada ------ ");
            //
            Console.WriteLine("Digite aqui a Anamnese: ");
            string a = Console.ReadLine();
            Console.WriteLine("Digite aqui a Requisição: ");
            string r = Console.ReadLine();
            consultA.anamnese = a;
            consultA.requisicao = r;
            return consultA;
    }

    public static void HistoricoDoPaciente(int idM){
            ListarPacientes();
            Console.Write("Informe o id do paciente: ");
            int idP = int.Parse(Console.ReadLine());
            List<Consulta> consultas = View.TodosOsHistoricos();
            NPaciente nP = new NPaciente();
            int q = 0;
            Console.WriteLine($"\n ------ Histórico do(a) paciente {nP.Listar(idP).nome}------ ");
            foreach(Consulta obj in consultas)
                if(obj.idPaciente == idP && obj.idMedico == idM && obj.status == "realizada") {
                    ++q;
                Console.WriteLine($"Consulta {q}    ID:{obj.id}");
                Console.WriteLine($"Data da consulta:{obj.data.ToString("dd/MM/yyyy")}");
                Console.WriteLine($"Anamnese:{obj.anamnese}");
                Console.WriteLine($"Requisição:{obj.requisicao}\n");
                }
            Console.WriteLine("-------------------------------------------------------------\n");
        }

    public static void AlterarConsulta(Consulta consultA){
        try{
            consultA.status = "realizada";
            NConsulta nC = new NConsulta();
            nC.Atualizar(consultA);
        } catch(FileNotFoundException){}
    }

    public static List<Consulta> BuscarAgenda(int idUser, DateTime dataHoje){
        List<Consulta> agenda = new List<Consulta>();
        try{
            NConsulta nC = new NConsulta();
            agenda = nC.Listar();
            // foreach(Consulta obj in agenda)
            //     Console.WriteLine(obj);
            return agenda;
        } catch(FileNotFoundException){}
        return default(List<Consulta>);
    }
    
    public static List<Consulta> TodosOsHistoricos(){
        List<Consulta> historicos = new List<Consulta>();
        try{
            NConsulta nC = new NConsulta();
            historicos = nC.Listar();
            return historicos;
        } catch(FileNotFoundException){}
        return default(List<Consulta>);
    }
    public static List<Consulta> ConsultaAtualizar(int idC){
        try{
        NConsulta nC = new NConsulta();
        Consulta consulta = nC.Listar(idC);
        Console.WriteLine(consulta);
        Console.WriteLine($"Anamnese: {consulta.anamnese}\nRequisição: {consulta.requisicao}");
        Console.Write("Anamnese: ");
        consulta.anamnese += " " + Console.ReadLine();
        Console.Write("Requisição: ");
        consulta.requisicao +=  " " + Console.ReadLine();
        nC.Atualizar(consulta);
        Console.WriteLine("Consulta atualizada\n");
        } catch(FileNotFoundException){}
        return default(List<Consulta>);
    }
  }   


}

    


