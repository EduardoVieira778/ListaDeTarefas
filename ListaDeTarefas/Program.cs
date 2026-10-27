using ListaDeTarefas;

namespace ListaDeTarefas {
    internal class Program {
        static void Main(string[] args) {
            var tarefas = new List<Tarefa>();
            int proximoId = 1;

            while (true) {
                Console.Clear();
                Console.WriteLine("=== LISTA DE TAREFAS ===");
                Console.WriteLine("1 - Listar tarefas");
                Console.WriteLine("2 - Adicionar tarefa");
                Console.WriteLine("3 - Concluir tarefa");
                Console.WriteLine("4 - Remover tarefa");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string? opcao = Console.ReadLine();

                switch(opcao) {
                    case "1":
                        Listar();
                        break;
                    case "2":
                        Adicionar();
                        break;
                    case "3":
                        Concluir();
                        break;
                    case "4":
                        Remover();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Pausar();
                        break;
                }
            }

            void Listar() {
                Console.Clear();
                Console.WriteLine("--- Suas tarefas ---");

                if(tarefas.Count == 0) {
                    Console.WriteLine("Nenhuma tarefa registrada");
                }
                else {
                    foreach(var tarefa  in tarefas)           
                        Console.WriteLine(tarefa);
                }

                Pausar();

            }

            void Adicionar() {
                Console.Clear();
                Console.Write("Descrição da tarefa: ");
                string? descricao = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(descricao)) {
                    Console.WriteLine("A descição não pode ser vazia.");
                }
                else {
                    tarefas.Add(new Tarefa{ Id = proximoId++, Descricao = descricao.Trim() });
                    Console.WriteLine("Tarefa adicionada!");
                }

                Pausar();
            }

            void Concluir() {
                Console.Write("Número da tarefa a concluir: ");

                if(int.TryParse(Console.ReadLine(), out int id)) {
                    var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
                    if(tarefa != null) {
                        tarefa.Concluida = true;
                        Console.WriteLine("Tarefa concluída!");
                    }
                    else {
                        Console.WriteLine("Tarefa não encontrada.");
                    }
                }
                else {
                    Console.WriteLine("Número inválido.");
                }

                Pausar();
            }

            void Remover() {
                Console.Write("Número da tarefa a remover: ");

                if(int.TryParse(Console.ReadLine(), out int id)) {
                    var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
                    if(tarefa != null) {
                        tarefas.Remove(tarefa);
                        Console.WriteLine("Tarefa removida!");
                    }
                    else {
                        Console.WriteLine("Tarefa não encontrada.");
                    }
                }
                else {
                    Console.WriteLine("Número inválido.");
                }

                Pausar();
            }

            void Pausar() {
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
