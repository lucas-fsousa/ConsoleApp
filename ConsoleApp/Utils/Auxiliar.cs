using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Utils {
  public static class Auxiliar {
    public static void Esperar(string mensagem, int segundos=1) {
      Console.WriteLine(mensagem.ToUpper());
      Thread.Sleep(segundos * 1000);
    }
  }
}
