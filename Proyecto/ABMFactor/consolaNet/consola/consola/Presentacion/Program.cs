using consola;
using consola.Aplicacion;
using consola.Context;
using consola.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MigrationsAutomaticDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //FactorController factorCtr = new FactorController();
            //Valor[] valorArray = new Valor[3];


            Menu();
            int opcion = Convert.ToInt16(Console.ReadLine());

            Console.Write("");

            while (opcion != 9)
            {
                switch (opcion)
                {
                    case 1:
                        ListarFactores();
                        break;
                    case 2:
                        AgregarFactor();
                        break;
                    case 3:
                        ConsultarFactor();
                        break;
                    case 4:
                        ModificarFactor();
                        break;
                    case 5:
                        HabilitarFactor();
                        break;
                    case 6:
                        InhabilitarFactor();
                        break;
                    default:
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("Opcion no Valida");
                        Console.WriteLine("-------------------------------------");
                        break;
                };

                Menu();
                opcion = Convert.ToInt16(Console.ReadLine());
                Console.Write("");
            }
            Environment.Exit(0);
        }

        public static void Menu()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1 - Listar Factores");
            Console.WriteLine("2 - Agregar Factor");
            Console.WriteLine("3 - Consultar Factor");
            Console.WriteLine("4 - Modificar Factor");
            Console.WriteLine("5 - Habilitar Factor");
            Console.WriteLine("6 - Deshabilitar Factor");
            Console.WriteLine("9 - Salir");
        }

        public static void ListarFactores()
        {
            FactorController factorCtr = new FactorController();
            Console.WriteLine("Lista de Factores");
            Console.WriteLine("-------------------------------------");
            List<Factor> lista = factorCtr.Listar();
            foreach (var dato in lista)
            {
                string estado = "Habilitado";
                if (dato.estado == 0) estado = "Deshabilitado";
                Console.WriteLine(dato.FactorId + " - " + dato.Nombre + " - " + estado);
            }
        }

        public static void AgregarFactor()
        {
            FactorController factorCtr = new FactorController();
            Console.WriteLine("Nuevo factor");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Ingrese el nombre del factor");
            String nombre = Console.ReadLine();
            String[] descripcion = new String[3];
            int[] valor = new int[3];
            Console.WriteLine("Ingrese descripcion del primer valor");
            descripcion[0] = Console.ReadLine();
            Console.WriteLine("Ingrese valor");
            valor[0] = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Ingrese descripcion del segundo valor");
            descripcion[1] = Console.ReadLine();
            Console.WriteLine("Ingrese valor");
            valor[1] = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Ingrese descripcion del tercer valor");
            descripcion[2] = Console.ReadLine();
            Console.WriteLine("Ingrese valor");
            valor[2] = Convert.ToInt16(Console.ReadLine());

            String mensaje = factorCtr.Agragar(nombre, descripcion, valor);
            Console.WriteLine(mensaje);
        }

        public static void ConsultarFactor()
        {
            FactorController factorCtr = new FactorController();
            Console.WriteLine("Lista de Factores Habilitados");
            Console.WriteLine("-------------------------------------");
            var lista = factorCtr.Listar();
            foreach (var dato in lista)
            {
                Console.WriteLine(dato.FactorId + " - " + dato.Nombre + " - " + "Habilitado");
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Ingrese ID del factor a consultar");
            int id = Convert.ToInt16(Console.ReadLine());            
            var datosFactor = factorCtr.Consultar(id);                 

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("ID");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("# 0 " + datosFactor.Nombre);
            foreach (var datos in factorCtr.ValoresDeFactor(id))
            {
                Console.WriteLine("  #" + datos.ValorId + " - " + datos.Descripcion + " -> " + datos.valor);
            }
        }

        public static void ModificarFactor()
        {
            FactorController factorCtr = new FactorController();
            Console.WriteLine("Lista de Factores Habilitados");
            Console.WriteLine("-------------------------------------");
            var lista = factorCtr.ListarHabilitados();
            if (lista.Count != 0)
            {
                foreach (var dato in lista)
                {
                    Console.WriteLine(dato.FactorId + " - " + dato.Nombre + " - " + "Habilitado");
                }
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Ingrese ID del factor a consultar");
                int id = Convert.ToInt16(Console.ReadLine());
                Factor datosFactor = factorCtr.Consultar(id);
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("ID");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("# 0 " + datosFactor.Nombre);
                foreach (var datos in factorCtr.ValoresDeFactor(id))
                {
                    Console.WriteLine("  #" + datos.ValorId + " - " + datos.Descripcion + " -> " + datos.valor);
                }
                Console.WriteLine("Ingrese el ID de lo que va a modificar");
                Console.WriteLine("--------------------------------------");
                int opcion = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Ingrese nueva descripcion");
                string nuevoValor = Console.ReadLine();
                String mensaje = factorCtr.Modificar(nuevoValor, id, opcion);
                Console.WriteLine(mensaje);
            }
            else
            {
                Console.WriteLine("No hay factores Habilitados");
            }
            
        }

        public static void HabilitarFactor()
        {
            FactorController factorCtr = new FactorController();
            Console.WriteLine("Lista de Factores Inhabilitados");
            Console.WriteLine("-------------------------------------");
            var lista = factorCtr.ListarInhabilitados();
            if (lista.Count != 0)
            {
                foreach (var dato in lista)
                {
                    Console.WriteLine(dato.FactorId + " - " + dato.Nombre);
                }
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Ingrese el ID del factor habilitar");
                int factorID = Convert.ToInt16(Console.ReadLine());
                String mensaje = factorCtr.habilitarFactor(factorID);
                Console.WriteLine(mensaje);
            }
            else
            {
                Console.WriteLine("Lista Vacia");
            }
        }

        public static void InhabilitarFactor()
        {
            FactorController factorCtr = new FactorController();
            Console.WriteLine("Lista de Factores Habilitados");
            Console.WriteLine("-------------------------------------");
            var lista = factorCtr.ListarHabilitados();
            if (lista.Count != 0)
            {
                foreach (var dato in lista)
                {
                    Console.WriteLine(dato.FactorId + " - " + dato.Nombre);
                }
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Ingrese el ID del factor Inhabilitar");
                int factorID = Convert.ToInt16(Console.ReadLine());
                String mensaje = factorCtr.inhabilitarFactor(factorID);
                Console.WriteLine(mensaje);
            }
            else
            {
                Console.WriteLine("Lista Vacia");
            }
        }
    }

    
}
