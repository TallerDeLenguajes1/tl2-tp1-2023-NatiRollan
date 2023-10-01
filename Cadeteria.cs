using System;
using System.Collections.Generic;
namespace CadeteriaSistema;

public class Cadeteria
{
    //Campos
    private string? nombre;
    private string? telefono;
    private List<Cadete> listadoCadetes;

    //Propiedades
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
    internal List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }

    //Constructor
    public Cadeteria(string nombre, string telefono, List<Cadete> listadoCadetes)
    {
        Nombre = nombre;
        Telefono = telefono;
        ListadoCadetes = listadoCadetes;
    }

    //Metodos
    public Pedido DarAltaPedido(int nro, string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        Pedido pedidoNuevo = new Pedido(nro, obs, nombre, direccion, telefono, datosReferenciaDireccion);
        return pedidoNuevo;
    }

    public void AsignarPedidoACadete(Pedido pedidoNuevo, int idCadete)
    {
        if (ListadoCadetes != null)
        {
            Cadete? cadEncontrado = ListadoCadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            if (cadEncontrado != null)
            {
                cadEncontrado.AgregarPedido(pedidoNuevo);
            }
        }
    }

    public void CambiarEstadoPedido(int nroPedido, int idCad, int estado)
    {
        Cadete? cadEncontrado = ListadoCadetes.FirstOrDefault(cad => cad.Id == idCad);
        if (cadEncontrado != null)
        {
            Pedido? pedEncontrado = cadEncontrado.ListadoPedidos.FirstOrDefault(ped => ped.Nro == nroPedido);
            if (pedEncontrado != null)
            {
                if (estado == 1)
                {
                    pedEncontrado.Estado = PedidoEstado.Entregado;
                }
                else
                {
                    pedEncontrado.Estado = PedidoEstado.Cancelado;
                }
            }
        }
    }

    public void ReasignarPedido(Pedido pedido, int idC)
    {
        //verifico si el pedido ya esta asignado a otro cadete
        foreach (var otroCadete in ListadoCadetes)
        {
            if (otroCadete.ListadoPedidos.Contains(pedido))
            {
                otroCadete.ListadoPedidos.Remove(pedido);
                break;
            }
        }

        // asigno el pedido al nuevo cadete
        Cadete? cadEncontrado = ListadoCadetes.FirstOrDefault(cadete => cadete.Id == idC);
        if (cadEncontrado != null)
        {
            cadEncontrado.ListadoPedidos.Add(pedido);
        }
    }



    public void MostrarInforme()
    {
        Console.WriteLine($"=========== Informe de {Nombre} ===============");
        Console.WriteLine($"Número de cadetes: {ListadoCadetes.Count()}");

        float totalGanado = 0;
        foreach (Cadete cadete in ListadoCadetes)
        {
            int pedidosEntregados = cadete.ListadoPedidos.Count(pedido => pedido.Estado == PedidoEstado.Entregado);
            totalGanado += cadete.JornalACobrar();

            Console.WriteLine($"Cadete: {cadete.Nombre}");
            Console.WriteLine($"- Pedidos Entregados: {pedidosEntregados}");
            Console.WriteLine($"- Monto ganado: ${cadete.JornalACobrar()}");
            Console.WriteLine("---------------------------------------------");
        }

        Console.WriteLine($"Total Ganado: ${totalGanado}");

    }
}