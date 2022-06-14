using ConsoleApp.Modelos;
using System;
using System.Text;

namespace ConsoleApp.Utils {
  public static class SystemInterface {

    // Codigo das telas
    private const int TELA_HOME = 0;
    private const int TELA_USUARIO = 1;

    public static void IniciarInterfaces() {
      int tela_atual = TELA_HOME;

      // Controle de telas
      while(true) {

        if(tela_atual == TELA_HOME) {
          tela_atual = MostrarTelaInicial();
        }

        if(tela_atual == TELA_USUARIO) {
          tela_atual = MostrarTelaUsuario();
        }

      }

    }

    public static int MostrarTelaUsuario() {

      // Codigos das opções
      const int LISTAR = 1;
      const int CRIAR = 2;
      const int ATUALIZAR = 3;
      const int EXCLUIR = 4;
      const int IR_PARA_HOME = 0;

      int retorno = TELA_USUARIO; // define o retorno como valor da constante de tela atual (TELA_USUARIO = 1)

      // Configuração do console
      Console.Title = "SGG - SISTEMA DE GESTAO GERAL - USUARIOS";
      Console.CursorVisible = true;
      Console.SetWindowSize(80, 25);
      Console.Clear();

      var sb = new StringBuilder();

      sb.AppendLine(" ==============================================");
      sb.AppendLine(" |        DIGITE UMA DAS OPCOES ABAIXO        |");
      sb.AppendLine(" |                                            |");
      sb.AppendLine(" | [0] - VOLTAR PARA A TELA HOME              |");
      sb.AppendLine(" | [1] - LISTAR TODOS OS USUARIOS             |");
      sb.AppendLine(" | [2] - CRIAR NOVO USUARIO                   |");
      sb.AppendLine(" | [3] - ATUALIZAR USUARIO EXISTENTE          |");
      sb.AppendLine(" | [4] - EXCLUIR USUARIO                      |");
      sb.AppendLine(" ==============================================");
      sb.Append(" OPCAO SELECTIONADA: ");

      Console.Write(sb.ToString());

      var resposta = int.Parse(Console.ReadLine());

      if(resposta == IR_PARA_HOME) {
        retorno = TELA_HOME;
      } else if(resposta == LISTAR) {
        ListarUsuarios();
      } else if(resposta == CRIAR) {
        CriarNovoUsuario();
      } else if(resposta == EXCLUIR) {
        ExcluirUsuario();
      } else {
        Auxiliar.Esperar(" ALTERNATIVA INVALIDA");
      }

      return retorno;
    }
    
    public static void ExcluirUsuario() {
      Console.WriteLine(" EXCLUINDO USUARIOS");
      Console.Write(" ID: ");

      Usuario.Excluir(int.Parse(Console.ReadLine()));
      Auxiliar.Esperar(" USUARIO EXCLUIDO!", 3);
    }
    
    public static void AtualizarUsuario() {
      Console.WriteLine(" ATUALIZANDO USUARIO");
      Console.Write(" ID DO USUARIO QUE SERA ATUALIZADO: ");
      var usuario = Usuario.BuscarPorId(int.Parse(Console.ReadLine()));

      if(usuario != null) {
        Console.Write(" NOME: ");
        usuario.Nome = Console.ReadLine();
        Console.Write(" TELEFONE: ");
        usuario.Telefone = Console.ReadLine();
        usuario.Atualizar();
      } else {
        Auxiliar.Esperar(" USUARIO NAO LOCALIZADO!", 3);
      }

    }

    public static int MostrarTelaInicial() {

      // Codigo das opções do menu
      const int SAIR = 0;
      const int LEITURA = 1;
      const int TABUADA = 2;
      const int MEDIA = 3;
      const int MENU_USUARIO = 4;

      int retorno = TELA_HOME; // define o retorno como valor da constante de tela atual (TELA_HOME = 0)

      Console.Title = "SGG - SISTEMA DE GESTAO GERAL";
      Console.CursorVisible = true;
      Console.SetWindowSize(80, 25);
      Console.Clear();

      var sb = new StringBuilder();

      sb.AppendLine(" ==============================================");
      sb.AppendLine(" |                OLA, USUARIO!               |");
      sb.AppendLine(" |        DIGITE UMA DAS OPCOES ABAIXO        |");
      sb.AppendLine(" |                                            |");
      sb.AppendLine(" | [0] - SAIR DO PROGRAMA                     |");
      sb.AppendLine(" | [1] - LEITURA DE ARQUIVOS                  |");
      sb.AppendLine(" | [2] - EXECTAR TABUADA                      |");
      sb.AppendLine(" | [3] - CALCULAR MEDIA                       |");
      sb.AppendLine(" | [4] - IR PARA MENU DE USUARIO              |");
      sb.AppendLine(" ==============================================");
      sb.Append(" OPCAO SELECTIONADA: ");

      Console.Write(sb.ToString());

      var resposta = int.Parse(Console.ReadLine());

      if(resposta == SAIR) {
        Environment.Exit(0);

      } else if(resposta == LEITURA) {
        LerArquivo();

      } else if(resposta == TABUADA) {
        MostraTabuada();

      } else if(resposta == MEDIA) {
        CalcularMedia();

      } else if(resposta == MENU_USUARIO) {
        retorno = TELA_USUARIO;

      } else {
        Auxiliar.Esperar(" ALTERNATIVA INVALIDA");
      }
      return retorno;
    }

    private static void CalcularMedia() {
      Calculos.Media();
    }

    private static void MostraTabuada() {
      Console.Write(" TABUADA DO N°: ");
      var numero = int.Parse(Console.ReadLine());
      Calculos.Tabuada(numero);
    }

    private static void LerArquivo() {
      Console.Write(" INFORME O NOME DO ARQUIVO COM A EXTENSAO: ");
      var nomeArquivo = Console.ReadLine();
      Arquivo.Ler(nomeArquivo, mostrarNaTela: true);
    }

    private static void ListarUsuarios() {
      Console.WriteLine("");
      foreach(var usuario in Usuario.ListarTodos()) {
        Console.WriteLine(usuario.ToString());
        Auxiliar.Esperar(" ====================", 2);
      }
    }

    private static void CriarNovoUsuario() {
      Console.WriteLine(" CADASTRANDO USUARIO...");

      Console.Write(" NOME: ");
      var nome = Console.ReadLine();

      Console.Write(" CPF: ");
      var cpf = Console.ReadLine();

      Console.Write(" TELEFONE: ");
      var telefone = Console.ReadLine();

      new Usuario(nome: nome, telefone: telefone, cpf: cpf).SalvarNovo();

      Auxiliar.Esperar(" USUARIO CRIADO COM SUCESSO!", 3);
    }

  }
}
