using System;
using System.Collections.Generic;
using System.Linq;
using Dio.Bank.Class;
using Dio.Bank.Enum;

namespace Dio.Bank
{
    class Program
    {
        static List<Conta> contas = new();
        static void Main(string[] args)
        {
            ObterMenu();
        }
        private static void ObterMenu()
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("================================================================");
            Console.WriteLine("******************** Bem-vindo ao DIO.Bank *********************");
            Console.WriteLine("================================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" [1] - Listar Todas as Contas");
            Console.WriteLine(" [2] - Criar Nova Conta");
            Console.WriteLine(" [3] - Transfêrencias");
            Console.WriteLine(" [4] - Saques");
            Console.WriteLine(" [5] - Depósitos");
            Console.WriteLine(" [X] - Sair do Menu");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Selecione uma das opções: ");
            Console.ResetColor();
            string option = Console.ReadLine().ToUpper();

            switch (option)
            {
                case "1":
                    ListarTodasContas();
                    break;
                case "2":
                    CriarConta();
                    break;
                case "3":
                    Transferir();
                    break;
                case "4":
                    Sacar();
                    break;
                case "5":
                    Depositar();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        private static void ListarTodasContas()
        {
            if (contas.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                foreach (var conta in contas)
                {
                    Console.WriteLine(conta.ToString());
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(" ****** Nenhum Cliente Cadastrado! ***** :(");
                Console.WriteLine();
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.Write("Aperte qualquer tecla para continuar ...");
            Console.ReadKey();
            ObterMenu();
        }
        private static void CriarConta()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Digite o Nome Completo: ");
            string nomeCompleto = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Qual o Tipo de Conta? \n\n [1] - Pessoa Física\n [2] - Pessoa Jurídica: ");
            TipoConta tipo = (TipoConta)int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.Write("Valor do Saldo Inicial: ");
            decimal valor = decimal.Parse(Console.ReadLine());
            Conta novaConta = new(tipo, valor, nomeCompleto);
            Console.WriteLine();
            Console.WriteLine();
            contas.Add(novaConta);
            Console.WriteLine(novaConta.ToString());
            Console.WriteLine();

            Console.Write("Aperte qualquer tecla para continuar ...");
            Console.ReadKey();
            ObterMenu();
        }
        private static void Transferir()
        {
            Console.WriteLine();

            if (contas.Count > 1)
            {


                Console.WriteLine("***************** CONTA ORIGEM ************************");
                Console.WriteLine();
                Console.Write("Digite o Número da Conta Origem: ");
                string numConta = Console.ReadLine();
                Conta contaOrigem = contas.Find(x => x.NummeroConta == numConta);
                if (contaOrigem != null)
                {
                    Console.WriteLine(contaOrigem.MostrarInfoResumoConta());
                    Console.WriteLine();
                }
                else
                {
                    throw new NullReferenceException();
                }
                Console.Write("Digite o Valor para Transferência: ");
                decimal valorTransferencia = decimal.Parse(Console.ReadLine());

                Console.WriteLine("***************** CONTA DESTINO ************************");
                Console.WriteLine();
                Console.Write("Digite o Número da Conta Destino: ");
                string numContaDestino = Console.ReadLine();
                Conta contaDestino = contas.Find(x => x.NummeroConta == numContaDestino);
                if (contaDestino != null)
                {
                    Console.WriteLine(contaDestino.MostrarInfoResumoConta());
                    Console.WriteLine();
                }
                else
                {
                    throw new NullReferenceException();
                }
                contaOrigem.Sacar(valorTransferencia);
                contaDestino.Depositar(valorTransferencia);
            }
            else
            {
                Console.WriteLine("Não Existe contas para Transferência!");
            }
            Console.WriteLine();

            Console.Write("Aperte qualquer tecla para continuar ...");
            Console.ReadKey();
            ObterMenu();
        }
        private static void Sacar()
        {
            Console.WriteLine();

            if (contas.Count > 0)
            {
                Console.Write("Digite o Número da Conta: ");
                string numConta = Console.ReadLine();
                Conta contaOrigem = contas.Find(x => x.NummeroConta == numConta);
                if (contaOrigem != null)
                {
                    Console.WriteLine(contaOrigem.ToString());
                    Console.WriteLine();
                }
                else
                {
                    throw new NullReferenceException();
                }
                Console.Write("Digite o Valor do Saque: ");
                decimal valorSaque = decimal.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("=========== EXTRATO APÓS O SAQUE ============");
                contaOrigem.Sacar(valorSaque);
                Console.WriteLine(contaOrigem.ToString());
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Não Existe contas cadastradas");
            }
            Console.WriteLine();

            Console.Write("Aperte qualquer tecla para continuar ...");
            Console.ReadKey();
            ObterMenu();
        }
        private static void Depositar()
        {
            Console.WriteLine();

            if (contas.Count > 0)
            {
                Console.Write("Digite o Número da Conta: ");
                string numConta = Console.ReadLine();
                Conta contaOrigem = contas.Find(x => x.NummeroConta == numConta);
                if (contaOrigem != null)
                {
                    Console.WriteLine(contaOrigem.ToString());
                    Console.WriteLine();
                }
                else
                {
                    throw new NullReferenceException();
                }
                Console.Write("Digite o Valor do Depósito: ");
                decimal valorDeposito = decimal.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine("=========================================== EXTRATO APÓS O DESPÓSTO ==============================================");
                contaOrigem.Depositar(valorDeposito);
                Console.WriteLine(contaOrigem.ToString());
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine("Não Existe contas cadastradas");
            }
            Console.WriteLine();

            Console.Write("Aperte qualquer tecla para continuar ...");
            Console.ReadKey();
            ObterMenu();
        }
    }
}
