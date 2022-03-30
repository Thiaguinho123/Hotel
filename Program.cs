using SistemaDeHotel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
namespace SistemaDeHotel
{
    internal class Program
    {
        private static Cliente _clienteLogin = null;
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            Console.WriteLine("Bem-vindo ao Sistema de Gestão de Hotel");

            int op = 0;
            int perfil = 0;
            do
            {
                try
                {
                    if (perfil == 0)
                    {
                        op = 0;
                        perfil = MenuUsuario();
                    }
                    if (perfil == 1)
                    {
                        op = MenuAdmin();
                        switch (op)
                        {
                            case 1: HotelInserir(); break;
                            case 2: HotelListar(); break;
                            case 3: HotelAtualizar(); break;
                            case 4: HotelExcluir(); break;

                            case 5: QuartoInserir(); break;
                            case 6: QuartoListar(); break;
                            case 7: QuartoAtualizar(); break;
                            case 8: QuartoExcluir(); break;

                            case 9: ClienteInserir(); break;
                            case 10: ClienteListar(); break;
                            case 11: ClienteAtualizar(); break;
                            case 12: ClienteExcluir(); break;

                            case 13: AgendamentoAbrirAgenda(); break;
                            case 14: AgendamentoConsultarAgenda(); break;
                            case 15: Sistema.ToXML();break;
                            case 16: Sistema.FromXML();break;
                            case 99: perfil = 0; break;
                        }
                    }

                    if (perfil == 2 && _clienteLogin == null)
                    {
                        op = MenuClienteLogin();
                        switch (op)
                        {
                            case 1: ClienteLogin(); break;
                            case 99: perfil = 0; break;

                        }
                    }
                    if (perfil == 2 && _clienteLogin != null)
                    {
                        op = MenuClienteLogout();
                        switch (op)
                        {
                            case 1: ClienteAgendarQuarto(); break;
                            case 2: ClienteListarQuarto(); break;
                            case 99: ClienteLogout(); break;
                        }
                    }
                }
                catch (Exception)
                {
                    op = -1;
                    throw;
                }
            } while (op != 0);
        }

        public static void ClienteLogin()
        {
            Console.WriteLine("----- Login do Cliente -----");
            ClienteListar();
            Console.Write("Informe o código do cliente para logar: ");
            int id = int.Parse(Console.ReadLine());
            // Procurar o cliente com o id informado
            _clienteLogin = Sistema.ClienteListar(id);
        }

        public static void ClienteLogout()
        {
            Console.WriteLine("----- Logout do Cliente -----");
            _clienteLogin = null;
        }

        public static void ClienteAgendarQuarto()
        {
            Console.WriteLine("----- Agendar um quarto de hospedes -----");
            //Listar Quartos disponíveis
            Sistema.QuartoListar();

            // Dados do quarto
            Console.Write("Informe o id do quarto: ");
            int idQuarto = int.Parse(Console.ReadLine());

            // Lista os clientes
            ClienteListar();
            Console.Write("Informe o id do cliente: ");
            int idCliente = int.Parse(Console.ReadLine());

            DateTime data = DateTime.Today;
            Console.Write("Informe a data <enter para hoje>: ");
            string s = Console.ReadLine();
            if (s != "")
                data = DateTime.Parse(s);


            Agendamento obj = new Agendamento
            {
                DataHora = data,
                IdCliente = idCliente,
                IdQuarto = idQuarto
            };
            Sistema.AgendamentoInserir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void ClienteListarQuarto()
        {
            Console.WriteLine("----- Listar meus quartos -----");

            // Lista meu quarto
            ClienteListar();
            Console.Write("Informe o id do cliente: ");
            int idCliente = int.Parse(Console.ReadLine());
            var cliente = Sistema.ClienteQuartoListar(idCliente);
            Console.WriteLine(cliente);
            Console.WriteLine("------------------------------------------");
        }

        
        public static int MenuUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1 - Entrar como Administrador");
            Console.WriteLine("2 - Entrar como Cliente");
            Console.WriteLine("0 - Fim");
            Console.WriteLine("----------------------------------");
            Console.Write("Informe uma opção: ");
            int op = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return op;
        }

        public static int MenuAdmin()
        {
            Console.WriteLine();
            Console.WriteLine("----- Escolha uma opção! -----");
            Console.WriteLine("01 - Inserir um novo hotel");
            Console.WriteLine("02 - Listar os hotéis cadastradas");
            Console.WriteLine("03 - Atualizar um hotel");
            Console.WriteLine("04 - Excluir um hotel");
            Console.WriteLine("05 - Inserir um novo quarto");
            Console.WriteLine("06 - Listar os quartos cadastrados");
            Console.WriteLine("07 - Atualizar um quarto");
            Console.WriteLine("08 - Excluir um quarto");
            Console.WriteLine("09 - Inserir um novo cliente");
            Console.WriteLine("10 - Listar os clientes cadastrados");
            Console.WriteLine("11 - Atualizar um cliente");
            Console.WriteLine("12 - Excluir um cliente");
            Console.WriteLine("13 - Abrir Agenda");
            Console.WriteLine("14 - Consultar Agenda");
            Console.WriteLine("-----------<DADOS>---------------");
            Console.WriteLine("15 - Salvar dados de hotéis");
            Console.WriteLine("16 - Abrir dados de hotéis");
            Console.WriteLine("------------------------------");
            Console.WriteLine("99 - Voltar ao menu anterior");
            Console.WriteLine("00 - Finalizar o sistema");
            Console.WriteLine("------------------------------");
            Console.Write("Opção: ");
            int op = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return op;
        }

        public static int MenuClienteLogin()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("01 - Login");
            Console.WriteLine("99 - Voltar");
            Console.WriteLine("00 - Fim");
            Console.WriteLine("----------------------------------");
            Console.Write("Informe uma opção: ");
            int op = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return op;
        }

        public static int MenuClienteLogout()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Bem vindo(a), " + _clienteLogin.Nome);
            Console.WriteLine("----------------------------------");
            Console.WriteLine("01 - Agendar um quarto");
            Console.WriteLine("02 - Listar meu quarto");
            Console.WriteLine("99 - Logout");
            Console.WriteLine("0  - Fim");
            Console.WriteLine("----------------------------------");
            Console.Write("Informe uma opção: ");
            int op = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return op;
        }

        
        public static void HotelInserir()
        {
            Console.WriteLine("----- Inserir um novo hotel -----");

            // Dados do hotel
            Console.Write("Informe o id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe o endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("Informe a quantidade de quartos: ");
            int qtd = int.Parse(Console.ReadLine());

            Hotel obj = new Hotel(id, nome, endereco, qtd);
            // Inserir o hotel no sistema.
            Sistema.HotelInserir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void HotelListar()
        {
            Console.WriteLine("----- Listar de hotéis cadastrados -----");
            foreach (Hotel obj in Sistema.HotelListar())
                Console.WriteLine(obj);
            Console.WriteLine("------------------------------------------");
        }

        public static void HotelAtualizar()
        {
            Console.WriteLine("----- Atualizar um hotel -----");

            // Dados do hotel
            Console.Write("Informe o id do hotel a ser atualizado: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Informe o novo nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe o novo endereço: ");
            string endereco = Console.ReadLine();

            Console.Write("Informe a nova quantidade de quartos: ");
            int qtd = int.Parse(Console.ReadLine());

            Hotel obj = new Hotel(id, nome, endereco, qtd);

            // Atualizar o hotel no sistema
            Sistema.HotelAtualizar(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void HotelExcluir()
        {
            Console.WriteLine("----- Excluir um hotel -----");

            // Dados do hotel
            Console.Write("Informe o id do hotel a ser excluído: ");
            int id = int.Parse(Console.ReadLine());
            string nome = "";
            string endereco = "";
            int qtd = 0;

            Hotel obj = new Hotel(id, nome, endereco, qtd);
            // Excluir o hotel no sistema
            Sistema.HotelExcluir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }


        public static void QuartoInserir()
        {
            Console.WriteLine("----- Inserir um novo quarto de hospedes -----");

            // Dados do quarto
            Console.Write("Informe o id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Informe a qtd de camas: ");
            int qtdCamas = int.Parse(Console.ReadLine());

            // Lista os clientes
            ClienteListar();
            Console.Write("Informe a qtd de clientes nesse quarto: ");
            int qtdCliente = int.Parse(Console.ReadLine());

            List<Cliente> clientes = new List<Cliente>();
            for (int i = 1; i <= qtdCliente; i++)
            {
                Console.Write($"Informe o id do cliente #{i}: ");
                int idCliente = int.Parse(Console.ReadLine());
                var cliente = Sistema.ClienteListar(idCliente);
                clientes.Add(cliente);
            }
            Quarto obj = new Quarto(id, qtdCamas, clientes);
            Sistema.QuartoInserir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void QuartoListar()
        {
            Console.WriteLine("----- Listar os quartos cadastrados -----");
            foreach (Quarto obj in Sistema.QuartoListar())
            {
                Console.WriteLine($"{obj}");
            }
            Console.WriteLine("------------------------------------------");
        }

        public static void QuartoAtualizar()
        {
            Console.WriteLine("----- Atualizar um quarto -----");
            // Dados do pet
            Console.Write("Informe o id do quarto a ser atualizado: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Informe a qtd de camas: ");
            int qtdCamas = int.Parse(Console.ReadLine());

            // Lista os clientes
            ClienteListar();
            Console.Write("Informe a qtd de clientes nesse quarto: ");
            int qtdCliente = int.Parse(Console.ReadLine());

            List<Cliente> clientes = new List<Cliente>();
            for (int i = 1; i <= qtdCliente; i++)
            {
                Console.Write($"Informe o id do cliente #{i}: ");
                int idCliente = int.Parse(Console.ReadLine());
                var cliente = Sistema.ClienteListar(idCliente);
                clientes.Add(cliente);
            }

            Quarto obj = new Quarto(id, qtdCamas, clientes);
            Sistema.QuartoAtualizar(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void QuartoExcluir()
        {
            Console.WriteLine("----- Excluir um quarto -----");
            // Dados do quarto
            Console.Write("Informe o id do quarto a ser excluído: ");
            int id = int.Parse(Console.ReadLine());

            Quarto obj = new Quarto(id);
            Sistema.QuartoExcluir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }


        public static void ClienteInserir()
        {
            Console.WriteLine("----- Inserir um novo cliente -----");
            // Dados do cliente
            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe o CPF: ");
            string cpf = Console.ReadLine();

            Console.Write("Informe o fone: ");
            string fone = Console.ReadLine();

            Console.Write("Informe a data de nascimento: ");
            bool dataSucesso = DateTime.TryParse(Console.ReadLine(), out var dataNascimento);

            Console.Write("Informe o sexo (1: masculino, 2: feminino): ");
            ESexo sexo = Enum.Parse<ESexo>(Console.ReadLine());

            Cliente obj = new Cliente(nome, cpf, fone, dataNascimento, sexo);
            Sistema.ClienteInserir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void ClienteListar()
        {
            Console.WriteLine("----- Listar os clientes cadastrados -----");
            foreach (Cliente obj in Sistema.ClienteListar())
                Console.WriteLine(obj);
            Console.WriteLine("------------------------------------------");
        }

        public static void ClienteAtualizar()
        {
            Console.WriteLine("----- Atualizar um cliente -----");
            // Dados do cliente
            Console.Write("Informe o id do cliente a ser atualizado: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe o CPF: ");
            string cpf = Console.ReadLine();

            Console.Write("Informe o fone: ");
            string fone = Console.ReadLine();

            Console.Write("Informe a data de nascimento: ");
            bool dataSucesso = DateTime.TryParse(Console.ReadLine(), out var dataNascimento);

            Console.Write("Informe o sexo (1: masculino, 2: feminino): ");
            ESexo sexo = Enum.Parse<ESexo>(Console.ReadLine());

            Cliente obj = new Cliente(id, nome, cpf, fone, dataNascimento, sexo);
            Sistema.ClienteAtualizar(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }

        public static void ClienteExcluir()
        {
            Console.WriteLine("----- Excluir um cliente -----");
            // Dados do cliente
            Console.Write("Informe o id do cliente a ser excluído: ");
            int id = int.Parse(Console.ReadLine());

            Cliente obj = new Cliente(id);
            Sistema.ClienteExcluir(obj);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }


        public static void AgendamentoAbrirAgenda()
        {
            Console.WriteLine("----- Abrir Agenda -----");
            DateTime data = DateTime.Today;
            Console.Write("Informe a data <enter para hoje>: ");
            string s = Console.ReadLine();

            if (s != "")
                data = DateTime.Parse(s);
            // Abrir Agenda
            Sistema.AgendamentoAbrirAgenda(data);
            Console.WriteLine("----- Operação realizada com sucesso -----");
        }
        public static void AgendamentoConsultarAgenda()
        {
            Console.WriteLine("----- Consultar Agenda -----");
            foreach (Agendamento obj in Sistema.AgendamentoListar())
                Console.WriteLine(obj);
            Console.WriteLine("------------------------------------------");
        }
    }
}

