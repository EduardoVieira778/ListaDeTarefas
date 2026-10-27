using ListaDeTarefas;
using System.Text.Json;

namespace ListaDeTarefas {
    internal class Program {
        static void Main(string[] args) {
            var tarefas = CarregarTarefas();
            int proximoId = tarefas.Count > 0 ? tarefas.Max(t => t.Id) + 1 : 1;

            void SalvarTarefas(List<Tarefa> lista) {
                string caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "tarefas.json");
                string json = JsonSerializer.Serialize(lista, new JsonSerializerOptions {
                    WriteIndented = true
                });
                File.WriteAllText(caminhoArquivo, json);
            }

            List<Tarefa> CarregarTarefas() {
                string caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "tarefas.json");

                if (!File.Exists(caminhoArquivo))
                    return new List<Tarefa>();

                try {
                    string json = File.ReadAllText(caminhoArquivo);
                    return JsonSerializer.Deserialize<List<Tarefa>>(json) ?? new List<Tarefa>();
                }
                catch {
                    Console.WriteLine("Não foi possível carregar as tarefas salvas.");
                    return new List<Tarefa>();
                }
            }

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
                    SalvarTarefas(tarefas);
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
                        SalvarTarefas(tarefas);
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
                        SalvarTarefas(tarefas);
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
