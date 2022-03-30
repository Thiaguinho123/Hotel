using SistemaDeHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
namespace SistemaDeHotel
{
    public class Sistema
    {
        private static Hotel[] _hotels = new Hotel[5];
        private static int _nHotel;
        private static List<Cliente> _clientes = new List<Cliente>();
        private static List<Quarto> _quartos = new List<Quarto>();
        private static List<Agendamento> _agendamentos = new List<Agendamento>();

        #region Hotel

        public static void HotelInserir(Hotel obj)
        {
            if (_nHotel == _hotels.Length)
                Array.Resize(ref _hotels, 2 * _hotels.Length);

            _hotels[_nHotel] = obj;
            _nHotel++;
        }

        public static Hotel[] HotelListar()
        {
            Hotel[] aux = new Hotel[_nHotel];
            Array.Copy(_hotels, aux, _nHotel);
            Array.Sort(aux);
            return aux;
        }

        public static Hotel HotelListar(int id)
        {
            foreach (var obj in _hotels)
            {
                if (obj != null && obj.GetId() == id)
                    return obj;
            }

            return null;
        }

        public static void HotelAtualizar(Hotel obj)
        {
            Hotel aux = HotelListar(obj.GetId());
            if (aux != null)
            {
                aux.SetNome(obj.GetNome());
                aux.SetEndereco(obj.GetEndereco());
                aux.SetQuartos(obj.GetQuartos());
            }
        }

        public static void HotelExcluir(Hotel obj)
        {
            int aux = HotelIndice(obj.GetId());
            if (aux != -1)
            {
                for (int i = aux; i < _nHotel; i++)
                {
                    _hotels[i] = _hotels[i + 1];
                    _nHotel--;
                }
            }
        }

        public static int HotelIndice(int id)
        {
            for (int i = 0; i < _nHotel; i++)
            {
                Hotel hotel = _hotels[i];
                if (hotel.GetId() == id)
                    return i;
            }

            return -1;
        }

        #endregion

        #region Quartos

        public static void QuartoInserir(Quarto obj)
        {
            int id = 0;
            foreach (Quarto aux in _quartos)
            {
                if (aux.Id > id)
                    id = aux.Id;
            }

            obj.Id = id + 1;
            _quartos.Add(obj);
        }

        public static List<Quarto> QuartoListar()
        {
            _quartos.Sort();
            return _quartos;
        }

        public static Quarto QuartoListar(int id)
        {
            foreach (var obj in _quartos)
            {
                if (obj.Id == id)
                    return obj;
            }

            return null;
        }

        public static void QuartoAtualizar(Quarto obj)
        {
            Quarto aux = QuartoListar(obj.Id);
            if (aux != null)
            {
                aux.QtdCamas = obj.QtdCamas;
                aux.Clientes = obj.Clientes;
            }
        }

        public static void QuartoExcluir(Quarto obj)
        {
            Quarto aux = QuartoListar(obj.Id);

            if (aux != null)
            {
                _quartos.Remove(aux);
            }
        }

        #endregion

        #region Cliente

        public static void ClienteInserir(Cliente cliente)
        {
            int id = 0;
            foreach (Cliente aux in _clientes)
            {
                if (aux.Id > id)
                    id = aux.Id;
            }

            cliente.Id = id + 1;
            _clientes.Add(cliente);
        }

        public static List<Cliente> ClienteListar()
        {
            _clientes.Sort();
            return _clientes;
        }

        public static Cliente ClienteListar(int id)
        {
            foreach (var cliente in _clientes)
            {
                if (cliente.Id == id)
                    return cliente;
            }

            return null;
        }

        public static Quarto ClienteQuartoListar(int id)
        {
            foreach (var obj in _quartos)
            {
                if (obj != null && obj.Clientes.Any(x => x.Id == id))
                {
                    return obj;
                }
            }

            return null;
        }

        public static void ClienteAtualizar(Cliente cliente)
        {
            Cliente aux = ClienteListar(cliente.Id);
            if (aux != null)
            {
                aux.Nome = cliente.Nome;
                aux.CPF = cliente.CPF;
                aux.Fone = cliente.Fone;
            }
        }

        public static void ClienteExcluir(Cliente cliente)
        {
            Cliente aux = ClienteListar(cliente.Id);

            if (aux != null)
            {
                _clientes.Remove(aux);
            }
        }

        #endregion

        #region Agendamento

        public static void AgendamentoAbrirAgenda(DateTime data)
        {
            int[] horas = { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            DateTime hoje = data.Date;
            foreach (var hora in horas)
            {
                TimeSpan horario = new TimeSpan(0, hora, 0, 0);
                Agendamento obj = new Agendamento
                {
                    DataHora = hoje + horario
                };
                AgendamentoInserir(obj);
            }
        }

        public static void AgendamentoInserir(Agendamento obj)
        {
            int id = 0;
            foreach (Agendamento aux in _agendamentos)
            {
                if (aux.Id > id)
                    id = aux.Id;
            }
            obj.Id = id + 1;
            _agendamentos.Add(obj);
        }

        public static List<Agendamento> AgendamentoListar()
        {
            _agendamentos.Sort();
            return _agendamentos;
        }

        public static List<Agendamento> AgendamentoListar(Cliente cliente)
        {
            List<Agendamento> agendamentos = new List<Agendamento>();
            foreach (Agendamento obj in _agendamentos)
            {
                if (obj.IdCliente == cliente.Id)
                    agendamentos.Add(obj);
            }
            return agendamentos;
        }

        public static Agendamento AgendamentoListar(int id)
        {
            foreach (Agendamento obj in _agendamentos)
            {
                if (obj.Id == id)
                    return obj;
            }
            return null;
        }

        public static void AgendamentoAtualizar(Agendamento obj)
        {
            Agendamento aux = AgendamentoListar(obj.Id);
            if (aux != null)
            {
                aux.DataHora = obj.DataHora;
                aux.IdCliente = obj.IdCliente;
                aux.IdQuarto = obj.IdQuarto;
            }
        }

        public static void AgendamentoExcluir(Agendamento obj)
        {
            Agendamento aux = AgendamentoListar(obj.Id);
            if (aux != null)
                _agendamentos.Remove(aux);
        }

        #endregion
        public static void ToXML() {
    XmlSerializer x = new XmlSerializer(typeof(List<Cliente>));
    StreamWriter k = new StreamWriter("Cliente.xml");
    x.Serialize(k, _clientes);
    k.Close();

    XmlSerializer x1 = new XmlSerializer(typeof(List<Quarto>));
    StreamWriter k1 = new StreamWriter("Quarto.xml");
    x1.Serialize(k1, _quartos);
    k1.Close();
    XmlSerializer x2 = new XmlSerializer(typeof(List<Agendamento>));
    StreamWriter k2 = new StreamWriter("Agendamento.xml");
    x2.Serialize(k2, _agendamentos);
    k2.Close();
    
  }

  public static void FromXML() {
    XmlSerializer x = new XmlSerializer(typeof(List<Cliente>));
    StreamReader g = new StreamReader("Cliente.xml");
    _clientes = (List<Cliente>)x.Deserialize(g);
    g.Close();

    XmlSerializer x1 = new XmlSerializer(typeof(List<Quarto>));
    StreamReader g1 = new StreamReader("Quarto.xml");
    _quartos = (List<Quarto>)x1.Deserialize(g1);
    g1.Close();
    

    XmlSerializer x2 = new XmlSerializer(typeof(List<Agendamento>));
    StreamReader g2 = new StreamReader("Agendamento.xml");
    _agendamentos = (List<Agendamento>)x2.Deserialize(g2);
    g2.Close();
  }
    }
  
}
