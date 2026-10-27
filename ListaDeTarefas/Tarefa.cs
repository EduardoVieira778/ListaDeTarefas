using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeTarefas {
    public class Tarefa {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public bool Concluida { get; set; }

        public override string ToString() {
            string marcador = Concluida ? "[x]" : "[ ]";
            return $"{Id}. {marcador} {Descricao}";
        }
    }
}
