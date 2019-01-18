using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mercado_TADS
{
    /*
     *  developed by: Gustavo Henrique Almeida da Mata.
     *  last update: 10/11/2018
     * 
     *  
     *  NOTA: EM CASO DE TESTE, TODOS OS DIRETÓRIOS DEVEM SER ALTERADOS LOCALMENTE PARA SUA MAQUINA
     *  PARA QUE OS ARQUIVOS BINÁRIOS SEJAM CRIADOS.
     */

    class Program
    {
        public struct Produto   //Estrutura de Registro
        {
            public string nome;
            public int cod;
            public double preco;
            public int qtd;
        }
        static void Main(string[] args)
        {
            do
            {   
                Console.Clear();
                Console.WriteLine("-----------------Mercado----------------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("   1- Gerenciar Produtos.\n   2- Modo Usuário.\n   3- Sair.");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Digite a opção desejada:");
                int opc = int.Parse(Console.ReadLine()); //Faz input da opção escolhida.
                valida(ref opc, 3);//Sub-rotina de Validação para a opção escolhida.
                switch (opc)
                {
                    case 1:
                        string caminho = @"D:login.dat"; //Este caminho é local do computador, sendo necessário sua alteração para o funcionamento do código.

                        Console.Clear();
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine("Você já é cadastrado?[S/N]");
                        Console.WriteLine("----------------------------");
                        Console.Write("R: ");
                        char R = char.Parse(Console.ReadLine()); 
                        validaChar(ref R, "Login");
                        if (R == 's' || R == 'S')
                        {
                            Logar(caminho);
                        }
                        else
                        {
                            PrimeiroAcesso(caminho);
                        }
                        break;

                    case 2:
                        Menu_Usuario();
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;
                }
            } while (true);


        }

        static void PrimeiroAcesso(string caminho)
        {
            if (!File.Exists(caminho)) //Caso o arquivo binário não tenha sido criado no diretório, essa validação fica encarregada de crialo.
            {
                FileStream LoginUsuario = new FileStream(caminho, FileMode.CreateNew, FileAccess.ReadWrite);//Cria o arquivo binário.
                LoginUsuario.Close();//Fecha o Arquivo binário.
            }

            FileStream Cadastro = new FileStream(caminho, FileMode.Open, FileAccess.Write);//Abre o arquivo binário no modo de escrita.
            BinaryWriter cad = new BinaryWriter(Cadastro);//Variavel utilizada para escrever no arquivo binário.

            string confirmasenha = "";
            string pass = "";
            Console.Clear();
            Console.WriteLine("-----------Primeiro Acesso-----------\n\t  Digite seus dados!");
            Console.WriteLine("-------------------------------------");
            Console.Write("Digite o Seu Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Digite seu Nome de usuário: ");
            string usuario = Console.ReadLine();
            Console.Write("E-mail: ");
            string email = Console.ReadLine();
            Console.Write("Senha: ");
            do
            {
                //Faz a senha ficar sensurada (********).
                ConsoleKeyInfo key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            do
            {
                Console.Write("\nConfirme Sua senha: ");

                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        confirmasenha += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && confirmasenha.Length > 0)
                        {
                            confirmasenha = confirmasenha.Substring(0, (confirmasenha.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);


                if (pass != confirmasenha)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("A senha digitada não é não corresponde a digitada anteriormente.\n \t\t  Tente novamente!.");
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.ReadKey();
                }
            } while (pass != confirmasenha);


            cad.Write(usuario); cad.Write(pass); cad.Write(nome); cad.Write(email); //Escreve dentro do arquivo binário. 
            cad.Close();//Fecha o Escritor.
            Cadastro.Close();//Fecha o Arquivo.
            Console.Clear();//Limpa a tela de console.
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("Usuário cadastrado Com Sucesso!");
            Console.WriteLine("---------------------------------");
            Console.ReadKey();
            Menu_Funcionario(); //Chama a subrotina de para o menu de funcionario.
        }//Registra um usuário no sistema.
        static void Logar(string c)
        {
            FileStream login = new FileStream(c, FileMode.Open, FileAccess.Read);//Abre o Arquivo binário para leitura.
            BinaryReader L = new BinaryReader(login);

            string senha = "";

            string usuario, user = "", S = "", Name;
            user = L.ReadString(); 
            S = L.ReadString();
            Name = L.ReadString();

            int flag = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("--------------LOGIN-------------");
                Console.WriteLine("       Digite seus dados");
                Console.WriteLine("--------------------------------");
                Console.Write("Usuário:"); usuario = Console.ReadLine();
                Console.WriteLine("--------------------------------");

                Console.WriteLine("--------------------------------");
                Console.Write("Senha:");


                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        senha += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                        {
                            senha = senha.Substring(0, (senha.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                Console.WriteLine("--------------------------------");

                if (senha == S && usuario == user)
                {
                    Console.Clear();

                    Console.WriteLine("--------------LOGIN EFETUADO COM SUCESSO!--------------\n");
                    Console.WriteLine("                Bem vindo {0}!\n", Name);
                    Console.WriteLine("-------------------------------------------------------");
                    Console.ReadKey();
                    L.Close();
                    login.Close();
                    Menu_Funcionario();
                }
                else
                {
                    flag += 1;
                    Console.Clear();
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("Você digitou o usuário ou senha invalidos, Tente novamente!");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.ReadKey();
                }

                if (flag == 3)
                {
                    Console.Clear();
                    Console.WriteLine("------------------------");
                    Console.WriteLine("Você Errou muitas vezes! Tente novamente mais tarde.");
                    Console.WriteLine("-------------------------");
                    Console.ReadKey();
                    Environment.Exit(0);

                }
            } while (senha != S || usuario != user);



        }//Loga um usuário no sistema.
        static void alterar_senha(string C)
        {
            string back = @"D:MercadoMERCADOTADS.bak"; //Caminho deve ser Alterado para o bom funcionamento do programa.

            FileStream Conta = new FileStream(C, FileMode.Open, FileAccess.Read);//Abre o arquivo binário para leitura.
            BinaryReader Usr = new BinaryReader(Conta);

            FileStream backp = new FileStream(back, FileMode.Create, FileAccess.Write);//Abre o arquivo binário para escrita para backup.
            BinaryWriter b = new BinaryWriter(backp);

            string senha = "", NS = "";
            string usuario = Usr.ReadString();
            string S = Usr.ReadString();
            string name = Usr.ReadString();
            string email = Usr.ReadString();

            do
            {
                Console.Clear();
                senha = "";
                Console.WriteLine("-----------Alteração de Senha-----------");
                Console.Write("Digite a sua senha Atual: ");
                do
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    // Backspace Should Not Work
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        senha += key.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                        {
                            senha = senha.Substring(0, (senha.Length - 1));
                            Console.Write("\b \b");
                        }
                        else if (key.Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                } while (true);
                Console.WriteLine("\n----------------------------------------");

                if (senha == S)
                {
                    do
                    {
                        Console.Write("\nDigite Sua Nova Senha: ");

                        do
                        {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            // Backspace Should Not Work
                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                            {
                                NS += key.KeyChar;
                                Console.Write("*");
                            }
                            else
                            {
                                if (key.Key == ConsoleKey.Backspace && NS.Length > 0)
                                {
                                    NS = NS.Substring(0, (NS.Length - 1));
                                    Console.Write("\b \b");
                                }
                                else if (key.Key == ConsoleKey.Enter)
                                {
                                    break;
                                }
                            }
                        } while (true);
                        if (NS == S)
                        {
                            Console.Clear();
                            Console.WriteLine("\n---------------------------------------");
                            Console.WriteLine("A senha digitada é igual a senha atual.");
                            Console.WriteLine("-----------------------------------------");
                            Console.ReadKey();

                        }
                    } while (NS == S);

                }
                else if (senha != S)
                {
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("     A senha digitada está incorreta!\n           Por favor, tente novamente!");
                    Console.WriteLine("--------------------------------------------------");
                    Console.ReadKey();
                }

            } while (senha != S);

            //Escrevem dentro do novo arquivo de backup.
            b.Write(usuario); 
            b.Write(NS);
            b.Write(name);
            b.Write(email);

            Console.Clear();
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("Senha alterada com sucesso!");
            Console.WriteLine("--------------------------------");
            Console.ReadKey();
            b.Close(); //fecha o escritor do backup.
            Usr.Close(); //Fecha o Leitor do arquivo principal.
            backp.Close();//fecha o arquivo de backup.
            Conta.Close();//fecha o arquivo principal.

            File.Delete(C);//Deleta o arquivo referente a o caminho estipulado.
            File.Move(back, C);//Move o arquivo de backup(secundário) para o local de caminho principal, tornando assim o arquivo de backup Principal.



        }//Altera a Senha de um usuário do sistema(só pode ser alterado já estando logado em uma conta).


        static void Menu_Funcionario()
        {
            char resp = 's';//Força a entra no while abaixo.
            while (resp == 's')//Serve para repetir o menu caso o usúario erre.
            {

                Console.Clear();
                Console.WriteLine("---------------------------Menu--------------------------");
                Console.WriteLine("       Este programa foi desenvolvido para Auxíliar\n              nas tarefas de um Mercado.");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine(" 1- Gerenciar Produtos de Higiene.\n 2- Gerenciar Produtos Alimentícios.\n 3- Gerenciar Produtos de Limpeza.\n 4- Alterar Senha.\n 5- Deslogar.");
                Console.WriteLine("---------------------------------------------------------");


                Console.Write("Digite a opção desejada:");
                int op = int.Parse(Console.ReadLine());//Entra com uma das opções: Alimentos, Higiene e etc...
                valida(ref op, 5);//Valida a opção digitada.

                string caminhoH;

                switch (op)
                {
                    case 1:
                        caminhoH = (@"D:Higiene.dat");
                        ProdutoM(caminhoH, 1);
                        break;

                    case 2:

                        caminhoH = (@"D:Alimento.dat");
                        ProdutoM(caminhoH, 2);
                        break;

                    case 3:
                        caminhoH = (@"D:Limpeza.dat");
                        ProdutoM(caminhoH, 3);
                        break;

                    case 4:
                        caminhoH = (@"D:login.dat");
                        alterar_senha(caminhoH);
                        break;

                    case 5:
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Deseja Mesmo sair?[S/N]");
                        Console.WriteLine("----------------------");
                        Console.Write("R:");
                        char opc = char.Parse(Console.ReadLine());
                        validaChar(ref opc, "Deslogar");
                        if (opc == 's' || opc == 'S')
                        {
                            resp = 'n';
                        }
                        break;
                }
            }
        }//Menu principal que ficava no metodo MAIN.
        static void Menu_Usuario()
        {
            int op = 0;
            while (op != 5)
            {

                Console.Clear();
                Console.WriteLine("-----------------------Menu Usuário----------------------");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("1-Listar Produtos de Higiene.\n2-Listar Produtos Alimentícios.\n3-Listar Produtos de Limpeza.\n4-Finalizar compras.\n5-Sair");
                Console.WriteLine("---------------------------------------------------------");

                Console.Write("Digite a opção desejada:");
                op = int.Parse(Console.ReadLine());//Entra com uma das opções: Alimentos, Higiene e etc...
                valida(ref op, 5);//Valida a opção digitada.

                string caminhoH;

                switch (op)
                {
                    case 1:

                        caminhoH = (@"D:Higiene.dat");
                        ListarProd(caminhoH, 2);
                        break;

                    case 2:

                        caminhoH = (@"D:Alimento.dat");
                        ListarProd(caminhoH, 2);
                        break;

                    case 3:

                        caminhoH = (@"D:Limpeza.dat");
                        ListarProd(caminhoH, 2);
                        break;

                    case 4:
                        caminhoH = (@"D:Produtos_Comprados.dat");
                        Finalizar_Compra(caminhoH);
                        break;
                    case 5:
                        op = 5;
                        break;

                }
            }


        }

        static void ProdutoM(string caminhoH, int prod)
        {
            if (!File.Exists(caminhoH))
            {
                FileStream Produto = new FileStream(caminhoH, FileMode.CreateNew, FileAccess.ReadWrite);
                Produto.Close();
            }

            char resp = 's';//Força a entra no while abaixo.
            while (resp == 's')//Serve para repetir o menu caso o usúario erre.
            {
                Console.Clear();
                Console.WriteLine("--------------------Menu de produtos---------------------");
                Console.WriteLine(" 1- Cadastrar novo Produto.\n 2- Listar produtos cadastrados.\n 3- Alterar produto. \n 4- Excluir Produto.\n 5- Voltar ao Menu Principal. ");
                Console.WriteLine("---------------------------------------------------------");
                Console.Write("Digite a opção desejada:");
                int opHigi = int.Parse(Console.ReadLine());//Opção digitada.
                valida(ref opHigi, 5);

                switch (opHigi)
                {
                    case 1:
                        CadastraProd(caminhoH, prod);
                        break;
                    case 2:
                        ListarProd(caminhoH, 1);
                        break;
                    case 3:
                        alterar_Produto(caminhoH);
                        break;
                    case 4:
                        Excluir(caminhoH);
                        break;
                    case 5:
                        resp = 'n';
                        break;

                }
            }

        }//Menu para gerencia dos produtos.
        static void cod_prod(string caminho)

        {
            Console.Write("Deseja comprar algo ? (S/N)");

            char resp = char.Parse(Console.ReadLine());

            while (resp != 'n' && resp != 'N' && resp != 'S' && resp != 's')
            {
                Console.Write("\n   Opção invalida digite novamente...\n          Deseja comprar algo ? (S/N): ");
                resp = char.Parse(Console.ReadLine());
                Console.WriteLine("");
            }

            while ((resp != 'n' && resp != 'N'))
            {

                Produto prod = new Produto();
                FileStream produto = new FileStream(caminho, FileMode.Open, FileAccess.Read);
                BinaryReader pesq = new BinaryReader(produto);


                Console.WriteLine("-----------------Mercado-----------------");
                Console.Write("Digite o código do Produto: ");
                int cod = int.Parse(Console.ReadLine());//Recebe o código do protudo. 
                                                        //Não criei Validação pois a flag criada não permite nenhum caracter digitado além dos cadastrados.


                string nome = "";
                double preco = 0;
                int codi = 0, qtd = 0, flag = 0;//flag para validação.
                int quant = 0;
                while (pesq.PeekChar() != -1)
                {

                    codi = pesq.ReadInt32();
                    nome = pesq.ReadString();
                    preco = pesq.ReadDouble();
                    qtd = pesq.ReadInt32();

                    if (cod == codi) // cod = Código digitado pelo usuário.
                    {
                        flag = 1;
                        Console.Clear();
                        Console.WriteLine("-----------------------------Produto----------------------------------");
                        Console.WriteLine("Código do produto | Nome do Produto | Preço do Produto |    Estoque ");
                        Console.WriteLine("       {0}           {1}              R${2}                {3}   ", codi, nome, preco, qtd);
                        Console.WriteLine("----------------------------------------------------------------------\n\n");

                    }
                }
                if (flag == 0)
                {
                    Console.Clear();
                    Console.WriteLine("---------------------------------------------------");
                    Console.WriteLine("         Código digitado não consta no estoque!\n                    Tente novamente!");
                    Console.WriteLine("---------------------------------------------------");
                    Console.ReadKey();
                    cod_prod(caminho);
                }
                Console.WriteLine("Adcionar Produto ao carrinho?[S/N]");
                char op = char.Parse(Console.ReadLine());
                validaChar(ref op, "Codigo");

                Console.WriteLine("Quantos produtos deseja Adicionar? ");
                quant = int.Parse(Console.ReadLine());
                if (qtd <= 0)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("O Estoque deste produto está vazio.");
                    Console.WriteLine("----------------------------------");
                    Console.ReadKey();
                    Menu_Usuario();
                }
                else
                {
                    while (quant > qtd)
                    {

                        Console.WriteLine("quantidade desejada: " + quant);
                        Console.WriteLine("estoque: " + qtd);
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("Não há tantos produtos suficinte no estoque estoque.");
                        Console.WriteLine("-----------------------------------");
                        Console.ReadKey();
                        Console.WriteLine("Quantos produtos deseja Adicionar? ");
                        quant = int.Parse(Console.ReadLine());
                    }
                }

                if (op == 'S' || op == 's' && quant <= qtd)
                {
                    prod.preco = preco;
                    prod.nome = nome;
                    prod.cod = cod;
                    prod.qtd = quant;
                    Console.Clear();
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("Produto adicionado ao carrinho com Sucesso!");
                    Console.WriteLine("-----------------------------------------------");

                    pesq.Close();
                    produto.Close();
                    Carrinho(preco, "", prod, caminho);
                }


                Console.WriteLine("     \nDeseja Finalizar a compra aqui?[S/N]");
                char subOp = char.Parse(Console.ReadLine());
                validaChar(ref subOp, "Fim-Compra");
                if (subOp == 'S' || subOp == 's')
                {
                    Carrinho(0, "Finalizar Compra", prod, caminho);
                }
                else
                {
                    ListarProd(caminho, 2);
                }

            }

            Menu_Usuario();



        }//Subrrotina utilizada para fazer a compra de um produto através do código.


        static void CadastraProd(string caminho, int prod)
        {
            char resp;
            do
            {
                FileStream cadastro = new FileStream(caminho, FileMode.Append, FileAccess.Write);
                BinaryWriter esc = new BinaryWriter(cadastro);
                Console.Clear();

                string tipo = "";

                if (prod == 1)
                {
                    tipo = "HIGIENE";

                }
                else if (prod == 2)
                {
                    tipo = "ALIMENTOS";
                }
                else if (prod == 3)
                {
                    tipo = "LIMPEZA";
                }

                Console.WriteLine("-------------Cadastro de Produtos de {0}-------------", tipo);
                Console.Write("Digite o Código do Produto: ");
                int codi = int.Parse(Console.ReadLine());
                Console.Write("Digite o nome do produto:");
                string nome = Console.ReadLine();
                Console.Write("Digite o Preço: ");
                double preco = double.Parse(Console.ReadLine());
                Console.Write("Digite a Quantidade: ");
                int qtd = int.Parse(Console.ReadLine());

                esc.Write(codi); esc.Write(nome); esc.Write(preco); esc.Write(qtd);
                esc.Close();
                cadastro.Close();

                Console.Clear();
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Produto cadastrado com sucesso!");
                Console.WriteLine("-------------------------------");
                Console.ReadKey();

                Console.Clear();
                Console.WriteLine("Deseja Cadastrar outro produto?[S/N]");
                resp = char.Parse(Console.ReadLine());
                validaChar(ref resp, "Cadastro");

            } while (resp == 'S' || resp == 's');
        }//Cadastra produtos de higiene.
        static void ListarProd(string caminho, int op)
        {
            int flag = 0;

            if (!File.Exists(caminho))
            {
                FileStream Produto = new FileStream(caminho, FileMode.CreateNew, FileAccess.ReadWrite);
                Produto.Close();
            }

            FileStream cadastro = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            BinaryReader esc = new BinaryReader(cadastro);
            Console.Clear();

            Console.WriteLine("-------------------Listagem de Produtos Cadastrados-------------------");
            while (esc.PeekChar() != -1)
            {
                int codi = esc.ReadInt32();
                string nome = esc.ReadString();
                double preco = esc.ReadDouble();
                int qtd = esc.ReadInt32();



                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Código do produto | Nome do Produto | Preço do Produto |    Estoque ");
                Console.WriteLine("  \t {0} \t\t {1} \t\tR${2}  \t\t  {3}   ", codi, nome, preco, qtd);
                Console.WriteLine("----------------------------------------------------------------------\n\n");

                flag = 1;
            }

            if (flag == 0)
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("         O Estoque está Vazio!\n       Não há produtos cadastrados!");
                Console.WriteLine("-----------------------------------------");
                esc.Close();
                cadastro.Close();
            }
            else
            {
                if (op == 2)
                {
                    esc.Close();
                    cadastro.Close();
                    cod_prod(caminho);
                }

            }

            esc.Close();
            cadastro.Close();

            Console.ReadKey();

        }//Lista todos os produtos Cadastrados
        static void alterar_Produto(string caminho)
        {
            FileStream rel = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            BinaryReader ag = new BinaryReader(rel);
            string back = @"D:alterar.bak";
            FileStream relacao = new FileStream(back, FileMode.Create, FileAccess.Write);
            BinaryWriter ag1 = new BinaryWriter(relacao);

            ListarProd(caminho, 1);

            Console.Write("Digite o codigo que irá alterar: ");

            int codigo = int.Parse(Console.ReadLine());
            int x = 0;

            string nomeAlterar;
            double precoAlterar;
            int qtdAlterar;

            while (ag.PeekChar() != -1)
            {
                int cod = ag.ReadInt32();
                string nome = ag.ReadString();
                double preco = ag.ReadDouble();
                int qtd = ag.ReadInt32();



                if (cod == codigo)
                {

                    Console.Write("Digite o nome do produto:");
                    nomeAlterar = Console.ReadLine();
                    Console.Write("Digite o Preço: ");
                    precoAlterar = double.Parse(Console.ReadLine());
                    Console.Write("Digite a Quantidade: ");
                    qtdAlterar = int.Parse(Console.ReadLine());

                    ag1.Write(codigo);
                    ag1.Write(nomeAlterar);
                    ag1.Write(precoAlterar);
                    ag1.Write(qtdAlterar);

                    Console.WriteLine("alterado com sucesso");
                    Console.ReadKey();
                    x = 1;

                }
                else
                {
                    ag1.Write(cod);
                    ag1.Write(nome);
                    ag1.Write(preco);
                    ag1.Write(qtd);

                }
            }

            ag.Close();
            ag1.Close();
            rel.Close();
            relacao.Close();

            File.Delete(caminho);
            File.Move(back, caminho);

            if (x == 0)
            {
                Console.WriteLine("Produto não cadastrado");
                Console.ReadKey();
            }
        }
        static void Excluir(string caminho)
        {
            FileStream rel = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            BinaryReader ag = new BinaryReader(rel);
            string back = @"D:excluir.bak";

            FileStream relacao = new FileStream(back, FileMode.Create, FileAccess.Write);

            BinaryWriter ag1 = new BinaryWriter(relacao);

            ListarProd(caminho, 1);
            Console.Write("Digite o codigo do produto que irá excluir: ");


            int cod = int.Parse(Console.ReadLine());
            int x = 0;
            while (ag.PeekChar() != -1)
            {

                int codigo = ag.ReadInt32();
                string nome = ag.ReadString();
                double preco = ag.ReadDouble();
                int qtd = ag.ReadInt32();


                if (cod == codigo)
                {
                    x = 1;
                }
                if (cod != codigo)
                {
                    ag1.Write(codigo);
                    ag1.Write(nome);
                    ag1.Write(preco);
                    ag1.Write(qtd);
                }

            }

            if (x == 0)
            {
                Console.WriteLine("Produto não cadastrado");
            }
            else
            {

                Console.WriteLine("excluido");
            }

            ag.Close();
            ag1.Close();
            rel.Close();
            relacao.Close();
            File.Delete(caminho);
            File.Move(back, caminho);


            Console.ReadKey();


        }
        static void altera_estoque(string caminho, int op, int quant)
        {
            FileStream rel = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            BinaryReader ag = new BinaryReader(rel);
            string back = @"D:alterar.bak";
            FileStream relacao = new FileStream(back, FileMode.Create, FileAccess.Write);
            BinaryWriter ag1 = new BinaryWriter(relacao);
            int x = 0;


            while (ag.PeekChar() != -1)
            {
                int cod = ag.ReadInt32();
                string nome = ag.ReadString();
                double preco = ag.ReadDouble();
                int qtd = ag.ReadInt32();

                if (cod == op)
                {

                    qtd = qtd - quant;

                    ag1.Write(cod);
                    ag1.Write(nome);
                    ag1.Write(preco);
                    ag1.Write(qtd);
                    x = 1;

                }
                else
                {
                    ag1.Write(cod);
                    ag1.Write(nome);
                    ag1.Write(preco);
                    ag1.Write(qtd);
                }


            }

            ag.Close();
            ag1.Close();
            rel.Close();
            relacao.Close();

            File.Delete(caminho);
            File.Move(back, caminho);
        }
        static void Finalizar_Compra(string caminho)
        {

            if (!File.Exists(caminho))
            {
                FileStream Produto = new FileStream(caminho, FileMode.CreateNew, FileAccess.ReadWrite);
                Produto.Close();
            }

            FileStream produto = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            BinaryReader leitor = new BinaryReader(produto);

            double total = 0;

            int x = 0;
            Console.Clear();
            Console.WriteLine("------------------------------ITENS COMPRADOS---------------------------------\n");
            while (leitor.PeekChar() != -1)
            {
                string nome = leitor.ReadString();
                double preco = leitor.ReadDouble();
                int cod = leitor.ReadInt32();
                int qtd = leitor.ReadInt32();

                total = (preco * qtd) + total;
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Código do produto | Nome do Produto |   Preço  |   Quantidade");
                Console.WriteLine("     {0}   \t\t{1}     \t\t{2}    \t\t{3}           ", cod, nome, preco, qtd);
                Console.WriteLine("----------------------------------------------------------------------\n\n");
                x = 1;
            }
            Console.WriteLine("----------------------------ITENS COMPRADOS ACIMA-----------------------------\n");

            if (x == 0)
            {
                Console.WriteLine("------------------------------------------------------------------------------");
                Console.WriteLine("                  Você ainda nao realizou nenhuma compra");
                Console.WriteLine("------------------------------------------------------------------------------");


                Console.ReadKey();
                Menu_Usuario();
            }

            char resp = 's';
            while (resp == 's')
            {

                Console.WriteLine("\n\n-----------------------------FINALIZAR PAGAMENTO--------------------------------");
                Console.WriteLine("Preço total a pagar: R${0} ", total + " Reais");
                Console.WriteLine("--------------------------------------------------------------------------------\n");

                Console.WriteLine("Opções de pagamento: \n\n     1.  À Vista.    2.  2x com 5% de juros.     3.  3x com 10% de juros.\n");
                Console.WriteLine("--------------------------------------------------------------------------------\n");

                Console.Write("Digite a forma de pagamento: ");
                int op = int.Parse(Console.ReadLine());
                valida(ref op, 3);
                Console.Clear();
                Console.WriteLine("\n\n----------------------------FINALIZAR PAGAMENTO---------------------------------");
                double valorT = 0;

                if (op == 1)
                {
                    Console.WriteLine("Forma de pagamento: À Vista.");
                    valorT += total;
                }
                else if (op == 2)
                {
                    valorT = total * 0.05 + total;
                    Console.WriteLine("Forma de pagamento: 2x com 5% de juros.");
                    Console.WriteLine("\nValor a pagar: 2x de R${0}", Math.Round(valorT / 2, 2) + " Reais");
                }
                else
                {
                    valorT = total * 0.10 + total;
                    Console.WriteLine("Forma de pagamento: 3x com 10% de juros.");
                    Console.WriteLine("\nValor a pagar: 3x de R${0}", Math.Round(valorT / 3, 2) + " Reais");
                }

                Console.WriteLine("\nTotal a pagar: R${0} ", Math.Round(valorT, 2));
                Console.WriteLine("--------------------------------------------------------------------------------\n");

                Console.WriteLine("Confirmar forma de pagamento?[S/N]");
                Console.Write("R:");
                char op1 = char.Parse(Console.ReadLine());
                validaChar(ref op1, "Compra");
                if (op1 == 's' || op1 == 'S')
                {
                    Console.Clear();
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("     COMPRA FINALIZADA COM SUCESSO!\n       OBRIGADO PELA PREFERENCIA !");
                    Console.WriteLine("-----------------------------------------");
                    leitor.Close();
                    produto.Close();
                    resp = 'n';

                }
                else
                {
                    Console.Clear();
                }

            }
            Console.ReadKey();

        }


        static void Carrinho(double p, string setor, Produto prod, string caminho)
        {
            string caminhoH = @"D:Produtos_Comprados.dat";
            if (!File.Exists(caminhoH))
            {
                FileStream Produto = new FileStream(caminhoH, FileMode.CreateNew, FileAccess.ReadWrite);
                Produto.Close();
            }

            FileStream Soma = new FileStream(caminhoH, FileMode.Append, FileAccess.Write);
            BinaryWriter esc = new BinaryWriter(Soma);

            esc.Write(prod.nome);
            esc.Write(prod.preco);
            esc.Write(prod.cod);
            esc.Write(prod.qtd);

            esc.Close();
            Soma.Close();

            FileStream produto = new FileStream(caminhoH, FileMode.Open, FileAccess.Read);
            BinaryReader leitor = new BinaryReader(produto);
            leitor.Close();
            produto.Close();
            altera_estoque(caminho, prod.cod, prod.qtd);



            if (setor == "Finalizar Compra")
            {
                Finalizar_Compra(caminhoH);
                File.Delete(caminhoH);
                Environment.Exit(0);
            }
        }
        static void valida(ref int op, int max)
        {

            while (op < 1 || op > max)
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Você digitou uma opção inválida, Tente novamente...");
                Console.WriteLine("----------------------------------------------");
                Console.ReadKey();
                Console.WriteLine("----------------------");
                Console.Write("Digite a opção desejada: ");
                op = int.Parse(Console.ReadLine());
            }

        }//Atualizado! 
        static void validaChar(ref char R, string subrotina)
        {
            string msg = "";

            if (subrotina == "Cadastro")
            {
                msg = "Deseja Cadastrar outro produto?[S/N]";
            }
            else if (subrotina == "Código")
            {
                msg = "Adcionar Produto ao carrinho?[S/N]";
            }
            else if (subrotina == "Fim-Compra")
            {
                msg = "Deseja Finalizar a compra aqui ?";
            }
            else if (subrotina == "Login")
            {
                msg = "Você já é cadastrado?[S/N]";
            }
            else if (subrotina == "Deslogar")
            {
                msg = "Deseja mesmo sair?";
            }
            else if (subrotina == "Compra")
            {
                msg = "Confirmar forma de pagamento?[S/N]";
            }


            while (R != 's' && R != 'S' && R != 'n' && R != 'N')
            {
                Console.Clear();
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Você digitou uma opção inválida, Tente novamente...");
                Console.WriteLine("----------------------------------------------");
                Console.ReadKey();

                Console.WriteLine(msg);
                Console.Write("Digite ");
                R = char.Parse(Console.ReadLine());
            }
        }
        //valida as entradas de dados com caracteres no programa.
    }
}