using ConsoleApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Modelos {
  public class Usuario : Base {
    private const string NOME_TABELA = "Usuario.txt";

    public string Cpf { get; set; }

    public Usuario(string nome, string telefone, string cpf, int id = 0) {
      this.Id = id;
      this.Nome = nome;
      this.Cpf = cpf;
      this.Telefone = telefone;
    }

    public override void SalvarNovo() {
      var data = Arquivo.Ler(NOME_TABELA);
      var existeCabecalho = data.FirstOrDefault() == "id;nome;telefone;cpf;" ? true : false;
      var usuario = Array.Empty<string>();

      if(existeCabecalho) {
        // pega o id do ultimo item e acrescenta + 1 para gerar um ID FAKE incremental
        var id = int.Parse(data.Last().Split(";")[0]) + 1;
        usuario = new string[] { $"{id};{this.Nome};{this.Telefone};{this.Cpf};" };

      } else {

        var id = 1;
        usuario = new string[] {
          "id;nome;telefone;cpf;",
          $"{id};{this.Nome};{this.Telefone};{this.Cpf};"
        };
      }

      Arquivo.Adicionar(NOME_TABELA, usuario);
    }

    public static Usuario BuscarPorId(int id) {
      return ListarTodos().First(x => x.Id == id);
    }

    public static List<Usuario> ListarTodos() {
      var lista = new List<Usuario>();
      foreach(var baseItem in Arquivo.Ler(NOME_TABELA)) {

        if(baseItem.Contains("id;nome;telefone;cpf;")) // verifica se é o cabecalho da tabela
          continue;

        if(baseItem.Equals("")) // Verifica se a linha é vazia
          continue;

        var itens = baseItem.Split(";");
        lista.Add(new Usuario(
          id: int.Parse(itens[0]),
          nome: itens[1],
          telefone: itens[2],
          cpf: itens[3]
        ));

      }
      return lista;
    }

    public override string ToString() {
      var sb = new StringBuilder();
      sb.AppendLine($" DETALHES DO OBJETO");
      sb.AppendLine($" ID: {this.Id}");
      sb.AppendLine($" CPF: {this.Cpf}");
      sb.AppendLine($" NOME: {this.Nome}");
      sb.AppendLine($" TELEFONE: {this.Telefone}");

      return sb.ToString();
    }

    public override void Atualizar() {

      var usuarioAntigo = Arquivo.Ler(NOME_TABELA).Where(x => x.Split(";").Contains($"{this.Id}")).FirstOrDefault();
      if(string.IsNullOrEmpty(usuarioAntigo)) {
        Auxiliar.Esperar(" USUARIO NAO LOCALIZADO!", 3);
        return;
      }

      var usuarioAtualizado = string.Format($"{this.Id};{this.Nome};{this.Telefone};{this.Cpf};");
      Arquivo.Substituir(NOME_TABELA, usuarioAntigo, usuarioAtualizado);
    }

    public static void Excluir(int id) {

      var usuario = Arquivo.Ler(NOME_TABELA).Where(x => x.Split(";").Contains($"{id}")).FirstOrDefault();
      if(string.IsNullOrEmpty(usuario)) {
        Auxiliar.Esperar(" USUARIO NAO EXISTENTE!", 3);
        return;
      }

      var line = string.Format($"");
      Arquivo.Substituir(NOME_TABELA, usuario, line);
    }
  }
}
