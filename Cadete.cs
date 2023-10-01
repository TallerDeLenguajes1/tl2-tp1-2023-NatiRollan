using System;
using System.Collections.Generic;
namespace CadeteriaSistema;

public class Cadete
{
    //Campos
    private int id;
    private string? nombre;
    private string? direccion;
    private string? telefono;
    private List<Pedido> listadoPedidos;

    //Propiedades
    public int Id { get => id; set => id = value; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Direccion { get => direccion; set => direccion = value; }
    public string? Telefono { get => telefono; set => telefono = value; }
    internal List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

    //Constructor
    public Cadete (int id, string nombre, string direccion, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        ListadoPedidos = new List<Pedido>();
    }

    //Metodos
    public void AgregarPedido(Pedido pedido)
    {
        ListadoPedidos.Add(pedido);
    }

    public float JornalACobrar()
    {
        int pedidosEntregados = ListadoPedidos.Count(pedido => pedido.Estado == PedidoEstado.Entregado);
        return pedidosEntregados * 500;
    }
}