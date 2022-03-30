using System;
using System.Collections.Generic;
namespace SistemaDeHotel.Models
{
    public class Cliente : IComparable<Cliente>
    {
        public Cliente(int id)
        {
            Id = id;
        }
    
        public Cliente(string nome, string cPF, string fone, DateTime dataNascimento, ESexo sexo)
        {
            Nome = nome;
            CPF = cPF;
            Fone = fone;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public Cliente(int id, string nome, string cPF, string fone, DateTime dataNascimento, ESexo sexo) : this(id)
        {
            Nome = nome;
            CPF = cPF;
            Fone = fone;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Fone { get; set; }
        public DateTime DataNascimento { get; set; }
        public ESexo Sexo { get; set; }

        public override string ToString()
        {
            return string.Format("Cliente ID: {0}, Nome: {1}, CPF: {2}, Fone: {3}, DataNascimento: {4}, Sexo: {5}",
                Id,
                Nome,
                CPF,
                Fone,
                DataNascimento.ToString("dd/MM/yyyy"),
                Sexo);
        }
       public Cliente(){
    
      }
      public int CompareTo(Cliente cliente){
        return Nome.CompareTo(cliente.Nome);
      }

    }
}
