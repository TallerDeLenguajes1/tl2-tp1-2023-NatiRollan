using System;
using System.Collections.Generic;
namespace CadeteriaSistema;

public class Cadeteria
{
    //Campos
    private string? nombre;
    private string? telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos = new List<Pedido>();

    //Propiedades
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
    internal List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    //Constructor
    public Cadeteria(string nombre, string telefono)
    {
        Nombre = nombre;
        Telefono = telefono;
        ListadoCadetes = listadoCadetes;
    }

    //Metodos
    public void DarAltaPedido(int nro, string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        Pedido pedidoNuevo = new Pedido(nro, obs, nombre, direccion, telefono, datosReferenciaDireccion);
        ListadoPedidos.Add(pedidoNuevo);
    }

    public void AsignarCadeteAPedido(int nroPedido, int idCadete)
    {
        if (ListadoCadetes != null)
        {
            Cadete? cadEncontrado = ListadoCadetes.FirstOrDefault(cadete => cadete.Id == idCadete);
            Pedido? pedEncontrado = ListadoPedidos.FirstOrDefault(ped => ped.Nro == nroPedido);
            if (cadEncontrado != null && pedEncontrado != null)
            {
                pedEncontrado.Cadete = cadEncontrado;
            } else
            {
                Console.WriteLine("El pedido o cadete que quiere asignar no existe");
            }
        }
    }

    public void CambiarEstadoPedido(int nroPedido, int idCad, int estado)
    {
        var pedEncontrados = ListadoPedidos.Where(ped => ped.Cadete?.Id == idCad); // poner ? en cadete asegura que solo se accederá a la propiedad Id si ped.Cadete no es null.
        if (pedEncontrados != null)
        {
            Pedido? pedido = pedEncontrados.FirstOrDefault(ped => ped.Nro == nroPedido);
            if (pedido != null)
            {
                if (estado == 1)
                {
                    pedido.Estado = PedidoEstado.Entregado;
                }
                else
                {
                    pedido.Estado = PedidoEstado.Cancelado;
                }
            }
        } 
    }

    public void ReasignarPedido(Pedido pedido, int idC)
    {
        //verifico si el pedido ya tiene asignado un cadete
        foreach (var ped in ListadoPedidos)
        {
            if (ped.Cadete != null && ped == pedido)
            {
                ped.Cadete = null;
                break;
            }
        }

        // asigno el nuevo cadete al pedido
        Cadete? cadEncontrado = ListadoCadetes.FirstOrDefault(cadete => cadete.Id == idC);
        if (cadEncontrado != null)
        {
            pedido.Cadete = cadEncontrado;
        }
    }

    public float JornalACobrar(int idCadete)
    {
        var pedidosEntregados = ListadoPedidos.Where(pedido => pedido.Estado == PedidoEstado.Entregado);
        if (pedidosEntregados != null)
        {
            int pedidosCadete = pedidosEntregados.Count(ped => ped.Cadete.Id == idCadete);
            if (pedidosCadete != 0)
            {
                return pedidosCadete * 500;
            } else
            {
                return 0;
            }
        } else
        {
            return 0;
        }
    }

    public void MostrarInforme()
    {
        Console.WriteLine($"=========== Informe de {Nombre} ===============");
        Console.WriteLine($"Número de cadetes: {ListadoCadetes.Count()}");

        float totalGanado = 0;
        foreach (Cadete cadete in ListadoCadetes)
        {
            var pedidosEntregados = ListadoPedidos.Where(pedido => pedido.Estado == PedidoEstado.Entregado);
            int pedidosCadete = pedidosEntregados.Count(ped => ped.Cadete.Id == cadete.Id);
            totalGanado += JornalACobrar(cadete.Id);

            Console.WriteLine($"Cadete: {cadete.Nombre}");
            Console.WriteLine($"- Pedidos Entregados: {pedidosCadete}");
            Console.WriteLine($"- Monto ganado: ${JornalACobrar(cadete.Id)}");
            Console.WriteLine("---------------------------------------------");
        }

        Console.WriteLine($"Total Ganado: ${totalGanado}");
    }

}