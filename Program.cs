// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
namespace CadeteriaSistema;

internal class Program
{
    private static void Main(string[] args)
    {
        List<Cadete> ListaCadetes = new List<Cadete>();
        Cadeteria? cadeteria = null;

        Console.WriteLine("Seleccione el tipo de archivo que desea leer: ");
        Console.WriteLine("1 - CSV");
        Console.WriteLine("2 - Json");
        int.TryParse(Console.ReadLine(), out int tipo);

        if (tipo == 1)
        {
            AccesoCSV helpCsv = new AccesoCSV();
            ListaCadetes = helpCsv.LeerArchivoCadete("Cadetes.csv");
            cadeteria = helpCsv.LeerArchivoCadeteria("Cadeteria.csv");
        } else
        {
            AccesoJSON helpJson = new AccesoJSON();
            ListaCadetes = helpJson.LeerArchivoCadete("Cadetes.json");
            cadeteria = helpJson.LeerArchivoCadeteria("Cadeteria.json");
        } 

        cadeteria.ListadoCadetes = ListaCadetes; //Cargo el listado obtenido de cadetes a mi cadeteria

        string? opcion;
        
        do
        {
            Console.WriteLine("--------------- Menu ---------------");
            Console.WriteLine("1. Dar de alta pedido");
            Console.WriteLine("2. Asignar pedido a cadete");
            Console.WriteLine("3. Cambiar estado de pedido");
            Console.WriteLine("4. Reasignar pedido a otro cadete");
            Console.WriteLine("5. Mostrar Informe");
            Console.WriteLine("6. Salir");
            opcion = Console.ReadLine();
            
            switch (opcion)
            {
                case "1":
                    cadeteria.DarAltaPedido(1, "con papas extras", "Natalia", "Av. Belgrano 800", "3815555555", "Casa esquina");
                    cadeteria.DarAltaPedido(2, "sin condimentos", "Martina", "San Juan 967", "3814444444", "Porton con rejas");
                    cadeteria.DarAltaPedido(3, "sin picante", "Carlos", "Buenos Aires 387", "3816666666", "No funciona el timbre");
                    cadeteria.DarAltaPedido(4, "Sin observaciones", "Nicolas", "Av. Belgrano 839", "3817777777", "Piso 8 B");                    
                    break;
                case "2":
                    Console.WriteLine("Ingrese numero de pedido: ");
                    int.TryParse(Console.ReadLine(), out int nroPed);
                    Console.WriteLine("Ingrese ID del cadete: ");
                    int.TryParse(Console.ReadLine(), out int nroCad);

                    cadeteria.AsignarCadeteAPedido(nroPed, nroCad); 
                    break;
                case "3":
                    Console.WriteLine("Ingrese numero de pedido: ");
                    int.TryParse(Console.ReadLine(), out int pedidoNro);
                    Console.WriteLine("Ingrese ID del cadete: ");
                    int.TryParse(Console.ReadLine(), out int cadeteId);
                    Console.WriteLine("Ingrese 1 si el pedido fue entregado por el cadete o 2 si fue cancelado: ");
                    int.TryParse(Console.ReadLine(), out int opc);

                    cadeteria.CambiarEstadoPedido(pedidoNro, cadeteId, opc);
                    break;
                case "4":
                    Console.WriteLine("Ingrese ID del nuevo cadete: ");
                    int.TryParse(Console.ReadLine(), out int id);
                    Console.WriteLine("Ingrese numero del pedido que quiere reasignar: ");
                    int.TryParse(Console.ReadLine(), out int nro);
                    Pedido? ped = cadeteria.ListadoPedidos.FirstOrDefault(ped => ped.Nro == nro);

                    cadeteria.ReasignarPedido(ped, id);
                    break;
                case "5":
                    cadeteria.MostrarInforme();
                    break;
                default:
                    break;
            }

        } while (opcion != "6");
    }
}
