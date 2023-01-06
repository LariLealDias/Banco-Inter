using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
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


        //Funções do Admin
        static void ShowOptionsAdmin()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-----------------------------------------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1 - Cadastrar um novo usuário");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
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
            Console.WriteLine(  );
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" XXXXX Parece que você informou um valor não válido XXXXX");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void GeneateID()
        {
            do
            {
                if (titulares.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
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
                    ErrorMessageInput();
                    Console.WriteLine();
                }
            } while (idConfiguration != 1 && idConfiguration != 2);
        }

        static void CreateNewUser()
        {
            Console.WriteLine();
            GeneateID();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ----- Para criar um novo usuário, digite a seguir as informações pedidas ----- ");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Digite o nome do titular:      ");
                Console.ResetColor();
                string titular = Console.ReadLine();
                bool stringValidation = Regex.IsMatch(titular, @"^[ a-zA-Z á]*$");
                if (stringValidation)
                {
                    titulares.Add(titular);
                    break;
                }
                else
                {
                    ErrorMessageInput();
                }
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Digite o CPF:                  ");
                Console.ResetColor();
                string cpf = Console.ReadLine();
                bool numberValidation = Regex.IsMatch(cpf, "^[0-9]+$");
                if (numberValidation && cpf.Length == 11)
                {
                    cpfs.Add(cpf);
                    break;
                }
                else
                {
                    ErrorMessageInput();
                }
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Digite o telefone:   +55  11  9");
                Console.ResetColor();
                string telefone = Console.ReadLine();
                bool numberValidation = Regex.IsMatch(telefone, "^[0-9]+$");
                if(numberValidation && telefone.Length == 8)
                {
                    telefones.Add(telefone);
                    break;
                }
                else
                {
                    ErrorMessageInput();
                }
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Digite o saldo inicial:      R$");
                Console.ResetColor();
                string saldoInicial = Console.ReadLine();
                bool numberValidation = Regex.IsMatch(saldoInicial, "^[A-Za-z0-9]*\\d+[A-Za-z0-9]*$");
                if (numberValidation)
                {
                    double saldoConvert = Convert.ToDouble(saldoInicial);
                    saldo.Add(saldoConvert);
                    break;
                }
                else
                {
                    ErrorMessageInput();
                }
            }


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Deseja confirmar o cadastro? (s/n) ");
            Console.ResetColor();
            string confirmacao = Console.ReadLine().ToLower();

            Console.ForegroundColor = ConsoleColor.Cyan;
            if (confirmacao == "s")
            {
                Console.WriteLine();
                Console.WriteLine("Usuário cadastrado com sucesso!");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                titulares.RemoveAt(titulares.Count - 1);
                cpfs.RemoveAt(cpfs.Count - 1);
                telefones.RemoveAt(telefones.Count - 1);
                saldo.RemoveAt(saldo.Count - 1);
                ids.RemoveAt(ids.Count - 1);
                Console.WriteLine();
                Console.WriteLine("Cadastro cancelado.");
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static void DeleteUser()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

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
                Console.WriteLine();
                Console.WriteLine();

            }
            Console.ResetColor();
        }

        static void ShowAllUsers()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            int indexOfList = titulares.Count();
            Console.WriteLine($"Total de contas registradas no sistema: {indexOfList} ");
            Console.WriteLine("+--+--+--+--+--+--+--+--+--+--+--+--+--+--+");

            for (int i = 0; i < indexOfList; i++)
            {
                Console.WriteLine($"             > Nome: {titulares[i]}");
                Console.WriteLine($"               CPF:  {cpfs[i]}");
                Console.WriteLine($"               ID:   {ids[i]}");
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
            while (true)
            {
                if (indexToFind == -1)
                {
                    Console.WriteLine();
                    Console.WriteLine("CPF inválido");
                    Console.WriteLine();
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
                    break;
                }
            }
        }

        static void AllValueStored()
        {
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Total armazenado no banco: R${saldo.Sum():F2}");

            Console.WriteLine();
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
                        ShowAllUsers();
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

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("  1 - Sacar  ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("  2 - Depositar  ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("  3 - Transferir  ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("  4 - Saber meu saldo");
                Console.ResetColor();

                Console.WriteLine("  0 - Encerrar programa   ");
                Console.WriteLine();
                Console.Write("Digite a alternativa desejada: ");
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
                    case 4:
                        ShowBankBalance();
                        break;
                }
            } while (optionToCheckingAccount != 0);
            Console.WriteLine("Ação da conta corrente foi encerrada.");
        }

        static void Withdraw()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Insira seu CPF para prosseguir a ação: ");
            Console.ResetColor();
            string CPFWithdraw = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            int indexToFind = cpfs.IndexOf(CPFWithdraw);
            if (indexToFind == -1)
            {
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.Write("Informe o valor: R$");
                Console.ResetColor();
                double valueToWithdraw = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToWithdraw > saldo[indexToFind])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.Write("Gostaria de tentar outro valor? (s/n) ");
                        Console.ResetColor();
                        tryAgainOrNot = Console.ReadLine();
                    }
                    else
                    {
                        saldo[indexToFind] -= valueToWithdraw;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Sacado com sucesso, agora o saldo atual é: {saldo[indexToFind]:F2}");
                        tryAgainOrNot = "n";
                    }

                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: R$");
                        Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Insira seu CPF para processeguir a ação: ");
            Console.ResetColor();
            string CPFDeposit = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(CPFDeposit);
            if (indexToFind == -1)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("Informe o valor: R$");

                double valueToDeposit = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                saldo[indexToFind] += valueToDeposit;
                Console.WriteLine($"Depositado com sucesso, agora o saldo atual é: {saldo[indexToFind]:F2}");
                Console.ResetColor();
            }
        }

        static void Transfer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Insira seu CPF para prosseguir a ação: ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string CPFTransferUser1 = Console.ReadLine();
            int indexUser1 = cpfs.IndexOf(CPFTransferUser1);
            if (indexUser1 == -1)
            {
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                string CPFTransferUser2;
                int indexUser2;
                while (true)
                {
                    Console.Write("Insira o CPF do destinatário: ");
                    Console.ResetColor();

                    CPFTransferUser2 = Console.ReadLine();
                     indexUser2 = cpfs.IndexOf(CPFTransferUser2);
                    if (indexUser2 == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Usuario não localizado");
                    }
                    else
                    {
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Informe o valor: R$");
                Console.ResetColor();

                double valueToTransfer = double.Parse(Console.ReadLine());
                string tryAgainOrNot;

                while (true)
                {
                    if (valueToTransfer > saldo[indexUser1])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("Parece que você não tem saldo o sulficiente");
                        Console.Write("Gostaria de tentar outro valor? (s/n) ");
                        Console.ResetColor();

                        tryAgainOrNot = Console.ReadLine().ToLower();
                    }
                    else
                    {
                        saldo[indexUser1] -= valueToTransfer;
                        saldo[indexUser2] += valueToTransfer;
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Transferido com sucesso, agora o seu saldo atual é: R${saldo[indexUser1]:F2}");
                        tryAgainOrNot = "n";
                    }
                    if (tryAgainOrNot == "s")
                    {
                        Console.Write("Informe o valor: R$");
                        Console.ResetColor();

                        valueToTransfer = double.Parse(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        static void ShowBankBalance()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Insira seu CPF para processeguir a ação: ");
            string CPFToBankBalance = Console.ReadLine();

            int indexToFind = cpfs.IndexOf(CPFToBankBalance);
            if (indexToFind == -1)
            {
                Console.WriteLine();
                Console.WriteLine("CPF inválido.");
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"saldo atual de {titulares[indexToFind]} é: {saldo[indexToFind]:F2}");
            }
            Console.ResetColor();
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
                    Console.WriteLine("Algo deu errado, certifique que a senha esteja correta ");
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
                Console.WriteLine("Algo deu errado, certifique que esta escrevendo corretamente ADMIN ou CLIENTE");

                Main(args);
            }
        }
    }
}