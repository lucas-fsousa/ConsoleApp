using System;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Threading;
using System.Collections;

namespace ConsoleApp.Utils {
  public static class Arquivo {

    private static string retornaPath() {
      var caminho = string.Empty;

      // verifica se o sistema possui alguma variavel de ambiente denominada "Ambiente" com valor "DEV"
      var ambienteDev = Environment.GetEnvironmentVariable("Ambiente") == "DEV";

      // Verifica se o ambiente é desenvolvimento ou produção
      if(ambienteDev) {
        caminho = ConfigurationManager.AppSettings["Desenvolvimento"];

      } else {
        caminho = ConfigurationManager.AppSettings["Producao"];
      }
      return caminho;
    }

    private static void Criar(string pathNomeArquivo) {
      Console.Write($" ARQUIVO NAO LOCALIZADO! DESEJA CRIAR O ARQUIVO? [s/N]: ");
      var criar = Console.ReadLine().ToLower();

      if(criar.Equals("s")) {
        try {
          // faz a criação do arquivo
          using(File.OpenWrite(pathNomeArquivo)) { }

          // animação da criação do arquivo
          Console.Write(" CRIANDO ARQUIVO: .");
          for(int i = 1;i <= 100;i++) {
            Thread.Sleep(50);
            if(i % 3 == 0)
              Console.Write(".");
            if(i == 100)
              Console.WriteLine("100%");
          }

          Auxiliar.Esperar(" ARQUIVO CRIADO COM SUCESSO!");
        } catch(Exception) {
          Auxiliar.Esperar(" DIRETORIO OU ARQUIVO INVALIDO.");
        }
      } else {
        Auxiliar.Esperar(" ACAO CANCELADA PELO USUARIO...");
      }
    }


    public static string[] Ler(string nomeArquivo, string path = "", bool mostrarNaTela = false) {
      string[] retorno = Array.Empty<string>();

      // Verifica se o path foi informado
      if(string.IsNullOrEmpty(path)) {
        path = retornaPath();
      }

      // verifica se o arquivo existe, se não existir é criado um arquivo.
      var pathNomeArquivo = Path.Combine(path, nomeArquivo);
      if(!File.Exists(pathNomeArquivo)) {
        Criar(pathNomeArquivo);
      }
      Console.WriteLine($" INICIANDO LEITURA DO ARQUIVO {nomeArquivo}");
      var linhas = File.ReadAllLines(pathNomeArquivo);
      if(linhas.Count() > 0) { // verifica se é um retorno válido
        retorno = linhas;
      }

      if(mostrarNaTela && linhas.Count() > 0) {
        foreach(var linha in linhas) {
          Console.WriteLine(" " + linha);
        }
        Auxiliar.Esperar(" LEITURA ENCERRADA. AGUARDE!", 5);
      }

      return retorno;
    }

    public static void Adicionar(string nomeArquivo, string[] linhas, string path = "") {
      // Verifica se o path foi informado
      if(string.IsNullOrEmpty(path)) {
        path = retornaPath();
      }
      // verifica se o arquivo existe, se não existir é criado um arquivo.
      var pathNomeArquivo = Path.Combine(path, nomeArquivo);
      if(!File.Exists(pathNomeArquivo)) {
        Criar(pathNomeArquivo);

      }
      File.AppendAllLines(pathNomeArquivo, linhas);
      Auxiliar.Esperar(" ARQUIVO ATUALIZADO COM SUCESSO!");
    }

    public static void Substituir(string nomeArquivo, string linhaAntiga, string novaLinha, string path = "") {
      // Verifica se o path foi informado
      if(string.IsNullOrEmpty(path)) {
        path = retornaPath();
      }
      // verifica se o arquivo existe, se não existir é criado um arquivo.
      var pathNomeArquivo = Path.Combine(path, nomeArquivo);
      if(!File.Exists(pathNomeArquivo)) {
        Auxiliar.Esperar(" ARQUIVO INEXISTENTE. LEITURA CANCELADA", 2);
        return;
      }

      var linhas = Ler(pathNomeArquivo); // obtem todos os itens do arquivo

      if(linhas.Count() > 0) {
        for(int i = 0; i < linhas.Length; i++) {
          var linha = linhas[i];

          if(linha.Equals(linhaAntiga)) { // faz a substituição da linha
            linhas[i] = novaLinha;
            File.WriteAllLines(pathNomeArquivo, linhas);
            Auxiliar.Esperar(" ITEM SUBSTITUIDO COM SUCESSO!");
          }
        }

      } else {
        Auxiliar.Esperar(" O ITEM NAO FOI ALTERADO!");
      }
      
      
      
    }
  }
}
