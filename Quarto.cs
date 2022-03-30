using System;
using System.Collections.Generic;

namespace  SistemaDeHotel.Models
{
    public class Quarto: IComparable<Quarto>
    {
        public Quarto(int id)
        {
            Id = id;
        }

        public Quarto(int id, int qtdCamas, List<Cliente> clientes)
        {
            Id = id;
            QtdCamas = qtdCamas;
            Clientes = clientes;
        }

        public int Id { get; set; }
        public int QtdCamas { get; set; }
        public List<Cliente> Clientes { get; set; }

        public override string ToString()
        {
            return String.Format("Quarto Nº: {0}, QtdCamas: {1} Nº de Hospedes: {2}",
                Id,
                QtdCamas,
                Clientes.Count);
        }
       public Quarto(){
    
  }
      public int CompareTo(Quarto obj){
        return Id.CompareTo(obj.Id);
      }
    }
}
