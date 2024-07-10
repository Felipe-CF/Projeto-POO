using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;
using System.Linq;


namespace Projeto{
     class Usuario
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Seja bem-vindo a Clínica Médica Bem estar!");
            int usuario = 0;
            do{
                usuario = MenuInicial();
                switch(usuario){
                    case 1: MenuAdmin(); break;
                    case 2: MenuMedico(); break;
                    case 3: TriagemPaciente(); break;                  
                }
            }while(usuario != 4);
            Console.WriteLine("Até Mais!");
            
        }
        public static int MenuInicial(){
                try{
                    Console.WriteLine("Olá, usuário. Você está no nosso menu principal. Poderia informar  o tipo de acesso desejado?\n1 - Administrador\n2 - Médico\n3 - Paciente\n4 - Encerrar atendimento");
                } catch(FormatException){}
                return int.Parse(Console.ReadLine());
        }
        public static void MenuAdmin(){
            int opadmin = 0;
            Console.WriteLine("\nOlá, Admin! Qual operação deseja realizar?");
            do{
                opadmin = OperacoesAdmin();
                switch(opadmin){
                    case 1: InserirEspecialidade(); break;
                    case 2: ListarEspecialidades(); break;
                    case 3: AtualizarEspecialidade(); break;
                    case 4: ExcluirEspecialidade(); break;
                    case 5: InserirMedico(); break;
                    case 6: ListarMedicos(); break;
                    case 7: AtualizarMedico(); break;
                    case 8: ExcluirMedico(); break;
                    case 9: ListarTodosPacientes(); break;
                    case 10: ExcluirPaciente(); break;
                    case 11: ListarConsultas(); break;
                    case 12: ExcluirConsulta(); break;
                }
            }while(opadmin != 13);
        }

        public static int OperacoesAdmin(){
                Console.WriteLine("---- Gerenciando Especialidades ----");
                Console.WriteLine("1 - Inserir especialidade     2 - Listar especialidade\n3 - Atualizar especialidade   4 - Excluir especialidade");
                Console.WriteLine("\n---- Gerenciando Medicos ----");
                Console.WriteLine("5 - Inserir médico        6 - Listar médicos\n7 - Atualizar médico      8 - Excluir médico");
                Console.WriteLine("\n---- Gerenciando Pacientes ----");
                Console.WriteLine("9 - Listar pacientes        10 - Excluir paciente \n");
                Console.WriteLine("---- Gerenciando Consultas ----");
                Console.WriteLine("11 - Listar consultas        12 - Excluir consulta\n13 - Sair");
                Console.Write("Escolha uma operação: ");
                return int.Parse(Console.ReadLine());
        }
        // ===== Especialidade
        public static void InserirEspecialidade(){
            Console.Write("Informe o nome da especialidade: ");
            string nomeEsp = Console.ReadLine();
            View.EspecialidadeInserir(nomeEsp);
            Console.WriteLine("Cadastro realizado!\n");        
        }
        public static void ListarEspecialidades(){
            Console.WriteLine("\n ---- Especialidades cadastradas ---- ");
            foreach(Especialidade especialidade in View.EspecialidadeListar())
                Console.WriteLine(especialidade);
            Console.WriteLine("--------------------------------------\n");       
        }
        public static void AtualizarEspecialidade(){
            ListarEspecialidades();
            Console.Write("Informe o id  da especialidade você deseja alterar/atualizar: ");
            int idAtt = int.Parse(Console.ReadLine());
            Console.Write("\nInforme o nome da especialidade: ");
            string nomeAtt = Console.ReadLine();
            View.EspecialidadeAtualizar(idAtt, nomeAtt);
        }
        public static void ExcluirEspecialidade(){
            ListarEspecialidades();
            Console.Write("Informe o id  da especialidade você deseja excluir: ");
            int idEx = int.Parse(Console.ReadLine());
            View.EspecialidadeExcluir(idEx);
        }

        // ===== Médico
        public static void InserirMedico(){
            try{
                Console.Write("Informe o nome do profissional: ");  string nomeMed = Console.ReadLine();
                Console.Write("Informe o CRM: ");   string crm = Console.ReadLine();
                ListarEspecialidades();
                Console.Write("Informe o id da especialidade: "); int idEspecialidade = int.Parse(Console.ReadLine());
                Console.Write("Informe o email: "); string contato = Console.ReadLine();
                string senha;
                do{
                    Console.Write("Digite a senha de acesso(sem espaços): ");
                    senha = Console.ReadLine();
                    Console.Write("Confirme a senha: ");
                }while(!senha.Equals(Console.ReadLine()));
                Console.Write("Informe a senha: "); 
                View.MedicoInserir(nomeMed, crm, idEspecialidade, senha, contato);
                Console.WriteLine("Cadastro realizado!\n");        
            } catch(FormatException){ Console.WriteLine("Dado(s) informado(s) não compatíveis do médico.");}
        }

        public static void ListarMedicos(){
                Console.WriteLine("\n ---- Médicos(as) cadastrados(as) ---- ");
                foreach(Medico med in View.MedicosListar())
                    Console.WriteLine(med);
                Console.WriteLine("-----------------------------------\n");        
        }
        public static void AtualizarMedico(){
            ListarMedicos();
            Console.Write("Informe o id do(a) médico(a) você deseja alterar/atualizar: ");
            int idAtt = int.Parse(Console.ReadLine());
            Console.Write("\nInforme o nome do(a) médico(a): ");
            string nomeAtt = Console.ReadLine();
            Console.Write("\nInforme o CRM: ");
            string crmAtt = Console.ReadLine();
            ListarEspecialidades();
            Console.Write("\nInforme o id da especialidade do(a) profissional: ");
            int idEspecAtt = int.Parse(Console.ReadLine());
            Console.Write("Informe o email: "); string contato = Console.ReadLine();
            string senha;
                do{
                    Console.Write("Digite a senha de acesso(sem espaços): ");
                    senha = Console.ReadLine();
                    Console.Write("Confirme a senha: ");
                }while(!senha.Equals(Console.ReadLine()));
                Console.Write("Informe a senha: "); 
            View.MedicoAtualizar(idAtt, nomeAtt, crmAtt, idEspecAtt, senha, contato);
        }

        public static void ExcluirMedico(){
            try{
                ListarMedicos();
                Console.Write("Informe o id  do(a) você deseja excluir: ");
                int idEx = int.Parse(Console.ReadLine());
                View.MedicoExcluir(idEx); 
            } catch(FormatException){Console.WriteLine("Dado(s) informado(s) não compatíveis do médico.");}
        }

        // ===== Paciente

        public static void ListarTodosPacientes(){
            View.ListarPacientes();
        }
        public static void ExcluirPaciente(){
            try{
                View.ListarPacientes();
                Console.Write("Informe o id  do(a) paciente que você deseja excluir: ");
                int idEx;
                while(true){
                    idEx = int.Parse(Console.ReadLine());
                    if(idEx > 0) break;
                    Console.Write("Id Invalido. Digita o valor correspondente ao paciente existente nos registros: ");
                }
                View.PacienteExcluir(idEx);
            } catch(FormatException){
                Console.WriteLine("Id do paciente informado não é compatível");
            }


        }
        

        // ===== Consulta

        public static void ListarConsultas(){
            Console.WriteLine("\n------ Todas as consultas gravadas no sistema ------ ");
            List<Consulta> consultas = View.ListarConsultas();
            NPaciente nP = new NPaciente();
            NMedico nM = new NMedico();
            foreach(Consulta obj in consultas){
                Paciente p = nP.Listar(obj.idPaciente);
                Medico m = nM.Listar(obj.idMedico);
                Console.WriteLine($"\nId Consulta:{obj.id}");
                Console.WriteLine(p);
                Console.WriteLine($"Medico(a):{m.id} - {m.nome}   CRM:{m.crm}\ncontato = {m.contato}");
                Console.WriteLine($"Situação:{obj.status}");
                }
            Console.WriteLine(" ----------------------------------------------------- \n");
        }

        public static void ExcluirConsulta(){
            ListarConsultas();
            Console.Write("Informe o id da consulta: ");
            int idC = int.Parse(Console.ReadLine());
            View.ConsultaExcluir(idC);
        }

        



        //==================================================
        //                      PACIENTE 
        //==================================================

        public static void TriagemPaciente(){
            int opPac = 0;
            Console.WriteLine("\nOlá! Você já é cadastrado no nosso sistema?\n1 - Sim   2 - Não");
            opPac = int.Parse(Console.ReadLine());
            if(opPac == 2) Cadastro();
            MenuPaciente();
        }
        public static void MenuPaciente(){
            Paciente user = null;
            do{
                user = View.LoginPaciente();
            } while(user == null);
            int opPaciente;
            do{
                opPaciente = OperacoesPaciente();
                switch(opPaciente){
                    case 1: MarcarAgendamento(user);  break;
                    case 2: GerarRequisicao(user); break;
                    case 3: AtualizarDados(user);  break;
                    case 4: MostrarDados(user);    break;
                    case 5: AlterarConsultaMarcada(user);    break;
                    case 6: MinhasConsultas(user);    break;
                }
            } while(opPaciente != 7);
        }
        public static int OperacoesPaciente(){
            Console.WriteLine("---- Menu do Paciente ----");
            Console.WriteLine("1 - Marcar uma consulta          2 - Requisição");
            Console.WriteLine("3 - Atualizar dados              4 - Mostrar dados");
            Console.WriteLine("5 - Alterar consulta marcada     6 - Minhas consultas\n7 - Sair");
            Console.WriteLine("--------------------------");
            Console.Write("Escolha uma operação: ");
            return int.Parse(Console.ReadLine());
        }

        public static void Cadastro(){
            try{
                string senhap, nomep, enderecop, contatop;

                Console.Write("\nDigite o nome do paciente: ");
                nomep = Console.ReadLine();
                
                string data;
                DateTime data1;
                do{
                Console.Write("Informe a data de nascimento(ex: dia/mes/ano): ");
                data = Console.ReadLine();
                if(DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data1))
                    break;
                else{throw new FormatException("Data inválida");}
                } while(true);

                Console.Write("Digite o endereco do paciente: ");
                enderecop = Console.ReadLine();
                Console.Write("Digite o email do paciente(ex: 123@Xmail.com) que será usado no login: ");
                contatop = Console.ReadLine();
                NPaciente nP = new NPaciente();
                do{
                    Console.Write("Digite a senha de acesso(sem espaços): ");
                    senhap = Console.ReadLine();
                    Console.Write("Confirme a senha: ");
                }while(!senhap.Equals(Console.ReadLine()));
                
                // Prossegue com o cadastro do novo paciente
                Paciente novoPaciente = new Paciente{nome = nomep,nasc = data1, endereco = enderecop, 
                contato = contatop, senha = senhap};
                nP.Inserir(novoPaciente);
                Console.WriteLine("Paciente cadastrado com sucesso!");
            }
            catch(FormatException){}
            catch(Exception ex){Console.WriteLine("Ocorreu uma exceção: {ex.Message}");}
        }
        public static void MarcarAgendamento(Paciente user){
            Console.WriteLine(" ------ Menu de Agendamento ------ ");
            ListarEspecialidades();
            Console.Write("Escolha a especialidade desejada: ");
            int idESp = int.Parse(Console.ReadLine());
            View.MedicosPorEspecialidade(idESp);
            Console.Write("Informe o id do(a) profissional desejado(a): ");
            int idMed = int.Parse(Console.ReadLine());

            string data;
            DateTime data1;
            do{
                Console.Write("Informe a data que você deseja(ex: dia/mes/ano): ");
                data = Console.ReadLine();
                if(DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data1)){
                    if(data1 > DateTime.Today){View.MarcarConsulta(user.id, idMed, data1); break;}
                    else{Console.WriteLine("Digite uma data de um data válida");}
                }
                else{throw new FormatException("Data inválida");}
            } while(true);

            try{
                Console.WriteLine("Deseja gerar comprovante do agendamento?\n1 - Sim    2 - Não");
                int comp = int.Parse(Console.ReadLine());
                if(comp == 1) View.GerarComprovante(user.id, data1);
            } catch(FormatException){}
        }

        public static void GerarRequisicao(Paciente user){
            string requisicao = View.PegaUltimaRequisicao(user.id);
            using(StreamWriter f = new StreamWriter("Requisição.txt"))
            f.WriteLine(requisicao);
        }
        
        public static void AtualizarDados(Paciente user){
            Console.Write("\nDigite o nome do paciente: ");
            string nome = Console.ReadLine();
            
            string data;
            DateTime data1;
            do{
                Console.Write("Informe a data de nascimento(ex: dia/mes/ano): ");
                data = Console.ReadLine();
                if(DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data1)) 
                    break;
                else{throw new FormatException("Data inválida");}
            } while(true);

            Console.Write("Digite o endereco do paciente: ");
            string endereco = Console.ReadLine();
            Console.Write("Digite o email do paciente(ex: 123@Xmail.com) que será usado no login: ");
            string contato = Console.ReadLine();
            View.PacienteAtualizar(user.id, nome, data1, endereco, contato); 
        }
        public static void MostrarDados(Paciente user){
            Console.WriteLine(" ------ Seus Dados ------ ");
            Console.WriteLine(View.MostrarDadosPaciente(user.id));
            Console.WriteLine("--------------------------\n");
        }

        public static void AlterarConsultaMarcada(Paciente user){
            View.ListarMinhasConsultasMarcadas(user.id);
            Console.Write("Digite o id da consulta: ");
            int idC = int.Parse(Console.ReadLine());
            View.AlterandoConsultaMarcada(idC);  
        }

        public static void MinhasConsultas(Paciente user){
            View.ListarTodasAsMinhasConsultas(user.id);
        }

        //==================================================
        //                      MEDICO
        //==================================================
        public static void MenuMedico(){
            Medico user = View.LoginMedico();
            if(user != null){
                Console.WriteLine($"Bem vindo ao consultória Dr(a) {user.nome}");
                int opMed;
                do{
                   opMed = OperacoesMed();
                   switch(opMed){
                     case 1: IniciarConsulta(user);             break;
                     case 2: VerAgenda(user);                   break;
                     case 3: ChecarHistoricoPaciente(user);     break;
                     case 4: AtualizarConsulta(user);           break;
                     case 5: MostrarDados(user);                break;
                   }
                } while(opMed != 6);
            }

        }

        public static int OperacoesMed(){
            Console.WriteLine("---- Menu do Médico ----");
            Console.WriteLine("1 - Iniciar consulta             2 - Ver Agenda");
            Console.WriteLine("3 - Historico de um paciente     4 - Atualizar consulta");
            Console.WriteLine("5 - Mostrar dados                6 - Sair do consultório");
            Console.WriteLine("--------------------------");
            Console.Write("Escolha uma operação: "); return int.Parse(Console.ReadLine());
        }

        public static void IniciarConsulta(Medico user){
            View.ListarPacientes();
            Console.Write("Informe o id do paciente: "); 
            int idP = int.Parse(Console.ReadLine());
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
            Consulta consultaDaVez = View.ConsultaIniciar(user.id, idP, data1);
            if(consultaDaVez != null){
                View.AlterarConsulta(View.Consulta(consultaDaVez));
                Console.WriteLine("Consulta finalizada");
            }
            else{Console.WriteLine("Não foi possível realizar a consulta. Tente novamente.");}
        }

        public static void VerAgenda(Medico user){
            string data;
            DateTime data1;
            do{
                Console.Write("Informe a data de hoje(ex: dia/mes/ano): ");
                data = Console.ReadLine();
                if(DateTime.TryParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data1)){
                    if(data1 >= DateTime.Today) break;
                    else Console.WriteLine("Digite uma data de um data válida");
                }
                else{throw new FormatException("Data inválida");}
            } while(true);

            List<Consulta> agendaHoje = View.BuscarAgenda(user.id, data1);
            //Console.WriteLine(agendaHoje[0]);
            if(agendaHoje != null){
                Console.WriteLine($"\n ------ Sua agenda do dia {data1.ToString("dd/MM/yyyy")} ------ ");
                foreach(Consulta obj in agendaHoje)
                    if(obj.idMedico == user.id && obj.status == "marcada") Console.WriteLine(obj);
                Console.WriteLine($"-----------------------------------------\n");
            }
        }

        public static void HistoricoDoPaciente(Medico user){
            ListarTodosPacientes();
            Console.Write("Informe o id do paciente: ");
            int idP = int.Parse(Console.ReadLine());
            List<Consulta> consultas = View.TodosOsHistoricos();
            Console.WriteLine(" ------ Histórico do paciente ------ ");
            foreach(Consulta obj in consultas)
                if(obj.idPaciente == idP && obj.idMedico == user.id && obj.status == "realizada") Console.WriteLine(obj);
            Console.WriteLine("-------------------------------------\n");
        }

        public static void AtualizarConsulta(Medico user){
            HistoricoDoPaciente(user);
            Console.WriteLine("Informe o id da Consulta: ");
            int idC = int.Parse(Console.ReadLine());
            Console.WriteLine(" ------ Consulta ------ ");
            View.ConsultaAtualizar(idC);

        }

        public static void ChecarHistoricoPaciente(Medico user){
            View.HistoricoDoPaciente(user.id);
            // List<Consulta> consultas = View.TodosOsHistoricos();
            // Console.WriteLine(" ------ Histórico do paciente ------ ");
            // foreach(Consulta obj in consultas)
            //     if(obj.idPaciente == idP && obj.idMedico == user.id && obj.status == "realizada")
            //         Console.WriteLine($"Data da Consulta:{obj.data.ToString("dd/MM/yyyy")}\nAnamnese: {obj.anamnese}\nRequisição: {obj.requisicao}\n");
            // Console.WriteLine("-------------------------------------\n");
        }

        public static void MostrarDados(Medico user){
            Console.WriteLine("\n ------ Seus dados ------ ");
            Console.WriteLine(user);
            Console.WriteLine("------------------------\n");
        }
           
}

}

   


