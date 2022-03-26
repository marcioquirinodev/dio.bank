using System;
using Dio.Bank.Enum;

namespace Dio.Bank.Class
{
    public class Conta
    {
        private TipoConta TipoConta { get; set; }
        private decimal Saldo { get; set; }
        private decimal Credito { get; set; }
        private string Nome { get; set; }
        public string NummeroConta { get; set; }
        public Conta(TipoConta tipo, decimal saldo, string nome)
        {
            TipoConta = tipo;
            Saldo = saldo;
            Nome = nome;
            // Crédito será de 30% do valor do PRIMEIRO saldo.
            Credito = (saldo / 100) * 30;
            // Número da conta será gerada automaticamente.
            NummeroConta = Convert.ToString(new Random().Next(100000, 999999));
        }

        #region Métodos
        public bool Depositar(decimal valor)
        {
            if (valor > 0)
            {
                Saldo += valor;
                return true;
            }
            else
            {
                throw new ArgumentException("Escolha um valor superior a zero!");
            }
        }
        public bool Sacar(decimal valor)
        {
            if (valor > 0)
            {
                if (Saldo >= valor)
                {
                    Saldo -= valor;
                    return true;
                }
                else
                {
                    if ((Saldo + Credito) >= valor)
                    {
                        valor -= Saldo;
                        Saldo = 0;
                        Credito -= valor;
                        return true;
                    }
                    else
                    {
                        throw new ArgumentException("Você não possui salso suficiente! :(");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Escolha um valor superior a zero!");
            }
        }

        public bool Transferir(decimal valor, Conta Destino)
        {
            if (Sacar(valor) && Destino.Depositar(valor)) return true;
            return false;
        }

        private string TipoContaPorExtenso()
        {
            if (TipoConta == TipoConta.PessoaFisica) return "Pessoa Física";
            return "Pessoa Jurídica";
        }

        public string MostrarInfoResumoConta()
        {
            string infoConta =
              "\n\n"
            + $"Número:\t\t{NummeroConta}\n"
            + $"Titular:\t{Nome}\n"
            + "..................................................................";
            return infoConta;
        }
        public override string ToString()
        {
            char pad = ' ';
            string infoConta =
              "\n\n"
            + $"==================================================================\n"
            + "********************** INFORMAÇÕES DA CONTA **********************\n"
            + "==================================================================\n\n"
            + $"Titular:\t{Nome}\n"
            + $"Número:\t\t{NummeroConta}\n"
            + $"Tipo de Conta:\t{TipoContaPorExtenso()} \n\n"
            + $"Saldo:......................... {Saldo.ToString("C").PadLeft(19, pad)}\n"
            + $"Crédito:....................... {Credito.ToString("C").PadLeft(19, pad)}\n"
            + $"Saldo Total:................... {(Saldo + Credito).ToString("C").PadLeft(19, pad)}\n"
            + "..................................................................";
            return infoConta;
        }
        #endregion
    }
}