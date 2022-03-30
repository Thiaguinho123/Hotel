using System;
using System.Collections.Generic;
namespace  SistemaDeHotel.Models
{
    public class Hotel : IComparable<Hotel>
    {
        private int _id;
        private string _nome;
        private string _endereco;
        private Quarto[] _quartos;

        public Hotel(int id, string nome, string endereco, int numeroDeQuartos)
        {
            _id = id;
            _nome = nome;
            _endereco = endereco;
            _quartos = new Quarto[numeroDeQuartos];
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public void SetNome(string nome)
        {
            _nome = nome;
        }

        public void SetEndereco(string endereco)
        {
            _endereco = endereco;
        }

        public void SetQuartos(int qtd)
        {
            _quartos = new Quarto[qtd];
        }

        public int GetId()
        {
            return _id;
        }

        public string GetNome()
        {
            return _nome;
        }

        public string GetEndereco()
        {
            return _endereco;
        }

        public int GetQuartos()
        {
            return _quartos.Length;
        }

        public override string ToString()
        {
            return string.Format("ID: {0}, Nome: {1}, Endereço: {2}, Nº Quartos: {3}",
                _id,
                _nome,
                _endereco,
                _quartos.Length);
        }
        public int CompareTo(Hotel obj){
          return _nome.CompareTo(obj._nome);
        }
    }
}
