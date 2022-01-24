using ConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Modelos {
  public abstract class Base {
    public int Id { get; init; }
    public string Nome { get; set; }
    public string Telefone { get; set; }

    public new abstract string ToString();
    public abstract void SalvarNovo();
    public abstract void Atualizar();
  }
}
