using System;
namespace CadeteriaSistema;

enum PedidoEstado
{
    Pendiente,
    Entregado,
    Cancelado
}

public class Pedido
{
    //campos
    private int nro;
    private string? observacion;
    private Cliente cliente;
    private PedidoEstado estado;

    //propiedades
    public int Nro { get => nro; set => nro = value; }
    public string? Observacion { get => observacion; set => observacion = value; }
    internal Cliente Cliente { get => cliente; set => cliente = value; }
    internal PedidoEstado Estado { get => estado; set => estado = value; }

    //constructor
    public Pedido (int nro, string obs, string nombre, string direccion, string telefono, string datosReferenciaDireccion)
    {
        Nro = nro;
        Observacion = observacion;
        Estado = PedidoEstado.Pendiente;
        Cliente = new Cliente(nombre, direccion, telefono, datosReferenciaDireccion);
    }

    //metodos
    public void VerDireccionCliente()
    {
        Console.WriteLine($"Direccion del cliente: {Cliente.Direccion}");
    }

    public void VerDatosCliente()
    {
        Console.WriteLine($"Nombre del cliente: {Cliente.Nombre}");
        Console.WriteLine($"Telefono del cliente: {Cliente.Telefono}");
    }
}