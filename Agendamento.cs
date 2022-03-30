using System;
using System.Collections.Generic;
namespace SistemaDeHotel.Models
{
    public class Agendamento : IComparable<Agendamento>{
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int IdCliente { get; set; }
        public int IdQuarto { get; set; }

        public override string ToString()
        {
            var disponivel = IdCliente == 0 ? " - Disponível" : " - Ocupado";
            return $"{Id} {DataHora:dd/MM/yyyy HH:mm}" + disponivel;
        }
        public int CompareTo(Agendamento obj){
          return Id.CompareTo(obj.Id);
        }
    }
}
