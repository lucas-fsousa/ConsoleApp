using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp.Utils {
  public static class Calculos {
    public static void Media() {
      int continuar = 0;
      do {
        Console.Write(" QUANTIDADE DE NOTAS A SEREM INFORMADAS: ");
        var quantidade = int.Parse(Console.ReadLine());
        var listaNotas = new List<float>();

        for(int i = 1;i <= quantidade;i++) {
          Console.Write($" DIGITE A NOTA {i}: ");
          listaNotas.Add(float.Parse(Console.ReadLine()));
        }

        Console.WriteLine($" A MEDIA CALCUlADA FOI {(listaNotas.Sum() / listaNotas.Count()).ToString("N2")}");
        Console.Write(" PARA CONTINuAR PRESSIONE 1 OU PRESSIONE 0 PARA CANCELAR: ");
        var resposta = Console.ReadLine();
        continuar = string.IsNullOrEmpty(resposta) ? 0 : int.Parse(resposta);

      } while(continuar != 0);

    }

    public static void Tabuada(int numero) {
      Console.WriteLine(" INICIANDO...");
      for(int i = 1;i <= 10;i++) {
        Console.WriteLine($"\t{numero} X {i} = {numero * i}");
        Thread.Sleep(1000);
      }
      Auxiliar.Esperar(" ENCERRADO", 3);
    }
  }
}
