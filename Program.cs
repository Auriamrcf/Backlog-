using System;
using System.Collections.Generic; 
using System.Text;

namespace Backlog
{
    class Program 
    {
        static void Main(string[] args)
        {
            // cria lista para incluir os items do backlog. 
            List<BacklogItem> listaBackLog = new List<BacklogItem>();
            // menu inicial
            Console.WriteLine("Olá! Bem vindo (a) ao Backlog. Veja abaixo o que você pode fazer:");
            Console.WriteLine("");
            // exibe opções de comandos disponíveis. 
            mostrarAjuda();
            Console.WriteLine("");
            Console.Write(">");
            // pede para o usuário escolher o comando desejado. 
            string comando = Console.ReadLine();
            // cria laço para que o usuário sempre retorne ao menu principal. 
            bool fecharPrograma = false;
        	while (!fecharPrograma) {
                switch (comando) {
                    case "1":
            // metodo que permite que o usuário inclua items ao backlog. 
                        adicionar(listaBackLog);
                        break;
            // metodo que permite que o usuário veja items incluídos no backlog. 
                    case "2":
                        ver(listaBackLog);
                        break;
                    case "3":
            // permite que o usuário entre em um novo metódo, de edição de items do backlog. 
                        editarCard(listaBackLog);
                        break;
                    default:    
                        break;
                }
            // reinicia loop
            mostrarAjuda();
            Console.WriteLine("");
            Console.Write(">");
            comando = Console.ReadLine();
            }
        }

        static void ver (List<BacklogItem> listaBackLog){
            Console.Clear();
            Console.WriteLine("Lista de Items:");
            Console.WriteLine("");
                for (int i = 0; i< listaBackLog.Count;i++){
                Console.WriteLine(listaBackLog[i].ToString());
                Console.WriteLine("");
                }
            Console.Write(">");
            Console.ReadLine();
        }
        static void adicionar (List<BacklogItem> listaBackLog){
            // cria laço para que o usuário possa criar quantos cards quiser antes de retornar ao menu principal. 
            bool parar = false;
            while (!parar){
            // solicita o tipo de item (feature, mudança defeito ou debito tecnico)
                Console.Clear();
                Console.WriteLine("Qual o tipo desse item? (Feature, Mudança, Defeito ou Débito Técnico)");
                Console.WriteLine("");
                Console.WriteLine("-> 1 - Feature");
                Console.WriteLine("-> 2 - Mudança");
                Console.WriteLine("-> 3 - Defeito");
                Console.WriteLine("-> 4 - Débito Técnico");
                Console.WriteLine("");
                Console.Write(">");
                string type = Console.ReadLine();
            // solitica o título do item. 
                perguntar("Qual o título desse novo item?");
                string titulo = Console.ReadLine();
            // solicita a descrição do item. 
                perguntar("Qual a descrição desse novo item?");
                string descricao = Console.ReadLine();
            // solicita a estimativa do item. Como deve ser um número inteiro, permite que, caso o usuário erre, ele tente novamente por meio de um laço. 
                perguntar("Qual a estimativa desse novo item?");
                int estimativa = 0;
                bool parar2 = false;
                    while(parar2 == false){
                    string str = Console.ReadLine();
                    Console.Write(">");
                        try {
                            estimativa = Convert.ToInt32(str);
                            parar2 = true;
                        } catch(Exception e){
                            Console.WriteLine("Coloque um número inteiro!");
                        }
                    }
            // solicita o status do item. Só que já existe uma lista de status existentes no arquivo status list. O usuário não pode inserir outra. Temos o metodo validar status para isso.     
                Console.Clear();
                Console.WriteLine("Qual o status desse novo item?");
                Console.WriteLine("");
                Status status = 0;
                status = validarStatus(status);
            //pede para o usuário inserir atributo especial do backlog item. Por exemplo, um débito técnico precisa de uma justificativa.
            // cria o objeto e coloca na lista após definir atributo especial.  
                if (type == "1"){   
                    Console.Clear();
                    perguntar("Qual o critério de aceite dessa nova feature?");
                    string criterioAceiteStr = Console.ReadLine();
                    Feature novoItem = new Feature (titulo,descricao,estimativa,status,criterioAceiteStr);
                    listaBackLog.Add(novoItem);                
                } else if (type == "2"){
                    perguntar("Qual a feature que terá essa mudança?");
                    string featureStr = Console.ReadLine();
                    Mudanca novoItem = new Mudanca (titulo,descricao,estimativa,status,featureStr);   
                    listaBackLog.Add(novoItem);              
                } else if (type == "3"){
                    perguntar("Qual a reprodução do defeito?");
                    string reproducaoDefeitoStr = Console.ReadLine();
                    Defeito novoItem = new Defeito (titulo,descricao,estimativa,status,reproducaoDefeitoStr);
                    listaBackLog.Add(novoItem);                 
                } else if (type == "4"){
                    perguntar("Qual a justificativa desse debito tecnico?");
                    string justificativaStr = Console.ReadLine();
                    DebitoTecnico novoItem = new DebitoTecnico (titulo,descricao,estimativa,status,justificativaStr);
                    listaBackLog.Add(novoItem); 
                } 
                // pergunta se o usuário deseja inserir um novo objeto. Em caso positivo, volta para o início. 
                Console.Clear();
                Console.WriteLine("Selecione a opção desejada:");
                Console.WriteLine("");
                Console.WriteLine("-> 1 - para voltar ao menu inicial.");
                Console.WriteLine("-> 2 - para adicionar um novo item");
                Console.WriteLine("");
                Console.WriteLine("Digite o comando desejado:");
                Console.Write(">");
                string comando = Console.ReadLine();
                if (comando != "2"){
                    parar = true;
                }
            }
        }
        static Status validarStatus (Status status){
            Console.WriteLine("-> 1 - Em progresso");
            Console.WriteLine("-> 2 - A começar");
            Console.WriteLine("-> 3 - Finalizar");
            Console.WriteLine("");
            Console.Write(">");
            string str = Console.ReadLine();
            Console.Write(">");
            bool parar = false;
            while(!parar){
                switch (str){
                    case "1":
                    status = Status.EmProgresso;
                    parar = true;
                    break;
                    case "2":
                    status = Status.Acomecar;
                    parar = true;
                    break;
                    default:
                        Console.WriteLine("1 - Em progresso");
                        Console.WriteLine("2 - A começar");
                        Console.WriteLine("3 - Finalizar");
                        str = Console.ReadLine();
                    break;
                    }
                }
            return status;
            }
        static void listarOpcoesDeEdicao (){
            Console.Clear();
            Console.WriteLine("Selecione o que você deseja fazer:");
            Console.WriteLine("");
            Console.WriteLine("1 - Editar card existente.");
            Console.WriteLine("2 - Deletar card existente.");
            Console.WriteLine("");
            Console.Write(">");
        }
        static void editarCard (List<BacklogItem> listaBackLog){
            listarOpcoesDeEdicao();
            string comando = Console.ReadLine();
            // cria laço para que o usuário sempre retorne ao menu principal. 
                switch (comando) {
                    case "1":
            // metodo que permite que o usuário edite items do backlog. 
                        break;
            // metodo que permite que o usuário delete items do backlog
                    case "2":
                    deletarCard(listaBackLog);
                        break;
                    case "3":
                    Console.ReadLine();
                        break;
                    default:    
                        break;
                }
            // reinicia loop
            listarOpcoesDeEdicao();
            comando = Console.ReadLine();
        }
        static void deletarCard(List<BacklogItem> listaBackLog){
        Console.Clear();
        Console.WriteLine("Insira o título do card que você deseja deletar:");
        Console.Write(">");
        string tituloStr = Console.ReadLine();
        // verifica se o título está na lista.
        BacklogItem itemRemover = null;
            foreach(BacklogItem item in listaBackLog){
                if(tituloStr == item.getTitulo()){
                    itemRemover = item;
                }
            }
            if (itemRemover == null){
                Console.Clear();
                Console.WriteLine("Item não encontrado");
                Console.WriteLine("");
                Console.Write(">");
                Console.ReadLine();
            } else {
                Console.Clear();
                listaBackLog.Remove(itemRemover);
                Console.WriteLine("O item foi removido com sucesso");
                Console.WriteLine("");
                Console.Write(">");
                Console.ReadLine();      
            }
            
        }
        static void mostrarAjuda(){
            Console.Clear();
            Console.WriteLine("Veja os comandos disponíveis:");
            Console.WriteLine("");
            Console.WriteLine("-> 1 - permite que você adicione novos items ao backlog");
            Console.WriteLine("-> 2 - permite que você veja todos os items existentes.");
            Console.WriteLine("-> 3 - edita/ deleta cards existentes.");
        }
        static void perguntar (string pergunta){
            Console.Clear();
            Console.WriteLine(pergunta);
            Console.Write(">");  
        }

    // fecha program
    }
    // fecha namespace
}
