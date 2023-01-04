using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace BancoInter
{
    class Program
    {
        static List<string> titulares = new List<string>();
        static List<string> cpfs = new List<string>();
        static List<string> telefones = new List<string>();
        static List<double> saldo = new List<double>();
        static List<string> ids = new List<string>();

        static int option;
        static int optionToCheckingAccount;
        static string name;
        static string role;
        static int clientKey;
        static int getClientKey;
        static int adminKey;
        static int getAdminKey;
        static int count = 1;
        static int idConfiguration;
        static string[] argsMain;

        //Função introdutoria
      
          
        //static void CheckWelcome()
        //{
           
        //}

        //Funções do Admin
        static void ShowOptionsAdmin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-----------------------------------------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("1 - Cadastrar um novo usuário");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("2 - Deletar um usuário");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("4 - Detalhes do usuário");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("5 - Total disponível no banco");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("6 - Conta corrente do usuário -- disponivel apenas ao cliente");
            Console.ResetColor();

            Console.WriteLine("0 - Sair do programa");
            Console.WriteLine();
            Console.Write("Digite a alternativa desejada: ");
        }

        static void ErrorMessageInput()
        {
            Console.WriteLine(" XXXXX Parece que você informou um valor não válido XXXXX");
        }
        
        static void GeneateID()
        {
            do
            {
                if (titulares.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(" ---- Antes de prosseguir, escolha uma configuração: ");
                    Console.WriteLine("     (1) Gerar ID randomico");
                    Console.WriteLine("     (2) Gerar ID sequencial");
                    Console.Write("Digite o número da opção: ");
                    Console.ResetColor();

                    idConfiguration = int.Parse(Console.ReadLine());
                }
                if (idConfiguration == 1)
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var stringChars = new char[8];
                    var random = new Random();

                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    var finalString = new String(stringChars);
                    ids.Add(finalString);
                }
                else if (idConfiguration == 2)
                {
                    string sequential = Convert.ToString(count++);

                    ids.Add(sequential);
                }
                else 
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("   -- Informe uma opção válida --");
                    Console.ResetColor();
                    Console.WriteLine();

                }
            } while (idConfiguration != 1 && idConfiguration != 2);
           
        }
        static void CreateNewUser()
        {
            Console.WriteLine();
            GeneateID();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine(" ----- Para criar um novo usuário, digite a seguir as informações pedidas ----- ");
                Console.Write("Digite o nome do titular:      ");
                Console.ResetColor();
                string titular = Console.ReadLine();
                titulares.Add(titular);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Digite apenas numeros do CPF:  ");
                Console.ResetColor();
                string cpf = Console.ReadLine();
                cpfs.Add(cpf);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Digite o telefone:   +55  11  9");
                Console.ResetColor();
                string telefone = Console.ReadLine();
                telefones.Add(telefone);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Digite o saldo inicial:      R$");
                Console.ResetColor();
                double saldoInicial = double.Parse(Console.ReadLine());
                saldo.Add(saldoInicial);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Deseja confirmar o cadastro? (s/n) ");
                Console.ResetColor();
                string confirmacao = Console.ReadLine().ToLower();

                Console.ForegroundColor = ConsoleColor.Magenta;
                if (confirmacao == "n")
                {
                    titulares.RemoveAt(titulares.Count - 1);
                    cpfs.RemoveAt(cpfs.Count - 1);
                    telefones.RemoveAt(telefones.Count - 1);
                    saldo.RemoveAt(saldo.Count - 1);
                    ids.RemoveAt(ids.Count - 1);
                    Console.WriteLine("Cadastro cancelado.");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Usuário cadastrado com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.ResetColor();
        }

        static void DeleteUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Write("Insira o CPF: ");
            string CPFtoDelete = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(CPFtoDelete);

            if (indexToFind == -1)
            {
                Console.WriteLine();
                ErrorMessageInput();
            }
            else
            {
                titulares.RemoveAt(indexToFind);
                cpfs.RemoveAt(indexToFind);
                telefones.RemoveAt(indexToFind);
                saldo.RemoveAt(indexToFind);

                Console.WriteLine();
                Console.WriteLine("Usuário deletado com sucesso!");
            }
            Console.ResetColor();

        }

        static void ShowAllUser()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            int indexOfList = titulares.Count();
            Console.WriteLine($"Total de contas registradas no sistema: {indexOfList} ");
            Console.WriteLine("+--+--+--+--+--+--+--+--+--+--+--+--+--+--+");

            for (int i = 0; i < indexOfList; i++)
            {
                Console.WriteLine($"               {titulares[i]}");
                Console.WriteLine($"               {cpfs[i]}");
                Console.WriteLine($"               {telefones[i]}");
                Console.WriteLine($"               {saldo[i]}");
                Console.WriteLine($"               {ids[i]}");
                Console.WriteLine("+--+--+--+--+--+--+--+--+--+--+--+--+--+--+");

            }
            Console.ResetColor();
        }

        static void ShowInfosUser()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Digite o CPF do usuário para exibir os detalhes: ");
            string cpf = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(cpf);
            if (indexToFind == -1)
            {
                Console.WriteLine("CPF inválido");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("     Titular: " + titulares[indexToFind]);
                Console.WriteLine("     CPF: " + cpfs[indexToFind]);
                Console.WriteLine("     Telefone: " + telefones[indexToFind]);
                Console.WriteLine("     Saldo: " + saldo[indexToFind].ToString("C"));
                Console.WriteLine("     ID: " + ids[indexToFind]);


                Console.ResetColor();
            }
        }

        static void AllValueStored()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine($"Total armazenado no banco: R${saldo.Sum():F2}");

            Console.WriteLine();
            Console.ResetColor();

        }

        static string messageOptionCheckingAccount()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("               ------------------ATENÇÃO------------------");
            Console.ResetColor();
            Console.WriteLine("  --          Essa ação esta disponivel apenas para clientes           -- ");
            Console.WriteLine(" --   caso queira utilizar essa função, logue novamente como cliente     -- ");
            Console.Write(" --                       logar novamente? (s/n):   ");

            string checkLogin = Console.ReadLine();

            if(checkLogin == "s")
            {
                option = 0;
                return checkLogin;

            } 
            return "n";
        }
        static void showIntroBankToAdmin()
        {

            string checkLogin = "n";
                Console.Write(" Informe a senha: ");
                adminKey = 123;
                getAdminKey = int.Parse(Console.ReadLine());
                if (getAdminKey == adminKey)
                {
                    Console.WriteLine($" {name}, você tem autorização às seguintes ações ");
                    Console.WriteLine();

                    do
                    {
                        ShowOptionsAdmin();
                        option = int.Parse(Console.ReadLine());

                        switch (option)
                        {
                            case 1:
                                CreateNewUser();
                                break;
                            case 2:
                                DeleteUser();
                                break;
                            case 3:
                                ShowAllUser();
                                break;
                            case 4:
                                ShowInfosUser();
                                break;
                            case 5:
                                AllValueStored();
                                break;
                            case 6:
                                checkLogin = messageOptionCheckingAccount();
                                break;
                            default: Console.WriteLine("Opção inválida, tente novamente");
                                break;
                        }
                    } while (option != 0);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=- Programa de admin encerrado =-=-=-=-=-=-=-=-=-=-=-=-=-");
                    Console.ResetColor();

                    if(checkLogin == "s")
                    {
                         Main(argsMain);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Algo deu errado, certifique que a senha esteja correta");
                    Console.ResetColor();

                    showIntroBankToAdmin();
                }
        }


        //Funções do User
       
        static void ShowOptionsUser()
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("-----------------------------------------------------------------");
                Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  1 - Sacar  ");
                Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("  2 - Depositar  ");
                Console.ResetColor();

                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("  3 - Transferir  ");
                Console.ResetColor();

                Console.WriteLine("  0 - Encerrar programa   ");
                optionToCheckingAccount = int.Parse(Console.ReadLine());

                switch (optionToCheckingAccount)
                {
                    case 1:
                        Withdraw();
                        break;
                    case 2:
                        Deposit();
                        break;
                    case 3:
                        Transfer();
                        break;
                }
            } while (optionToCheckingAccount != 0);
            Console.WriteLine("Ação da conta corrente foi encerrada.");
        }

        static void Withdraw()
        {
            Console.Write("Insira seu CPF para prosseguir a ação: ");
            string CPFWithdraw = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(CPFWithdraw);
            if (indexToFind == -1)
            {
                Console.WriteLine("CPF inválido.");
            }
            else
            {
                Console.Write("Informe o valor: R$");
                double valueToWithdraw = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToWithdraw > saldo[indexToFind])
                    {
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.WriteLine("Gostaria de tentar outro valor? (s/n) ");
                        tryAgainOrNot = Console.ReadLine();
                    }
                    else
                    {
                        saldo[indexToFind] -= valueToWithdraw;
                        Console.WriteLine($"Sacado com sucesso, agora o saldo atual é: {saldo[indexToFind]:F2}");
                        tryAgainOrNot = "n";
                    }
                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: ");
                        valueToWithdraw = double.Parse(Console.ReadLine().ToLower());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void Deposit()
        {
            Console.Write("Insira seu CPF para processeguir a ação: ");
            string CPFDeposit = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(CPFDeposit);
            if (indexToFind == -1)
            {
                Console.WriteLine("CPF inválido.");
            }
            else
            {
                Console.Write("Informe o valor: R$");
                double valueToDeposit = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToDeposit > saldo[indexToFind])
                    {
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.WriteLine("Gostaria de tentar outro valor? (s/n) ");
                        tryAgainOrNot = Console.ReadLine();
                    }
                    else
                    {
                        saldo[indexToFind] += valueToDeposit;
                        Console.WriteLine($"Depositado com sucesso, agora o saldo atual é: {saldo[indexToFind]:F2}");
                        tryAgainOrNot = "n";
                    }
                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: R$");
                        valueToDeposit = double.Parse(Console.ReadLine().ToLower());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void Transfer()
        {
            Console.Write("Insira seu CPF para prosseguir a ação: ");
            string CPFTransferUser1 = Console.ReadLine();
            int indexUser1 = cpfs.IndexOf(CPFTransferUser1);
            if (indexUser1 == -1)
            {
                Console.WriteLine("CPF inválido.");
            }
            else
            {
                Console.Write("Insira o CPF do destinatário: ");
                string CPFTransferUser2 = Console.ReadLine();
                int indexUser2 = cpfs.IndexOf(CPFTransferUser2);
                if (indexUser2 == -1)
                {
                    Console.WriteLine("Usuario não localizado");
                }

                Console.Write("Informe o valor: R$");
                double valueToTransfer = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToTransfer > saldo[indexUser1])
                    {
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.WriteLine("Gostaria de tentar outro valor? (s/n) ");
                        tryAgainOrNot = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        saldo[indexUser1] -= valueToTransfer;
                        saldo[indexUser2] += valueToTransfer;
                        Console.WriteLine($"Transferido com sucesso, agora o seu saldo atual é: R${saldo[indexUser1]:F2}");
                        Console.WriteLine($"E o saldo atual de {titulares[indexUser2]} é: R${saldo[indexUser2]:F2}");
                        tryAgainOrNot = "n";
                    }
                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: R$");
                        valueToTransfer = double.Parse(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        static void showIntroBankToUser()
        {
            do
            {
                
                Console.Write(" Informe a senha: ");

                clientKey = 123;
                getClientKey = int.Parse(Console.ReadLine());

                if (getClientKey == clientKey)
                {
                    Console.WriteLine($" {name}, você tem autorização às seguintes ações");
                    Console.WriteLine();

                        ShowOptionsUser();

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=- Programa encerrado =-=-=-=-=-=-=-=-=-=-=-=-=-");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Algo deu errado, certifique que a senha esteja correta: ");
                    Console.ResetColor();
                }

            } while (getClientKey != clientKey);
        }


        //Função principal
        static void Main(string[] args)
        {
            argsMain = args;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- BANCO INTER =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write(" Olá, informe como gostaria de ser chamado(a): ");
            Console.ForegroundColor = ConsoleColor.Green;
            name = Console.ReadLine();
            Console.ResetColor();

            

            
            Console.Write($" Bem vindo ao BANCO INTER {name}, você é: Admin ou cliente? ");
            Console.ForegroundColor = ConsoleColor.Green;
            role = Console.ReadLine().ToLower();
            Console.ResetColor();
            Console.WriteLine();

            if (role == "admin")
            {
                showIntroBankToAdmin();
            }
            else if (role == "cliente")
            {
                showIntroBankToUser();
            }
            else if (role != "cliente" || role != "admin")
            {
                Console.WriteLine("Algo deu errado, certifique que esta escrevendo corretamente");
                Console.Write("admin ou cliente : ");
                
                Main(args);
            }





        }
    }
}