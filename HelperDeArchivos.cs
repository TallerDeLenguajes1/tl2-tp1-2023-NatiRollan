using System;
using System.Collections.Generic;
using System.IO;
namespace CadeteriaSistema;

public class HelperDeArchivos
{
    public List<string[]> LeerArchivoCsv(string nombreDeArchivo)
    {
        List<string[]> LecturaDelArchivo = new List<string[]>();
        string linea = "";

        if (File.Exists(nombreDeArchivo))
        {
            FileStream archivo = new FileStream(nombreDeArchivo, FileMode.Open);
            StreamReader strReader = new StreamReader(archivo);

            while ((linea = strReader.ReadLine()) != null)
            {
                string[] fila = linea.Split(',');
                LecturaDelArchivo.Add(fila);
            }
            strReader.Close();
        }
        else
        {
            Console.WriteLine($"El archivo {nombreDeArchivo} no existe");
            return null;
        }

        return LecturaDelArchivo;
    }

    public List<Cadete> ConversorDeCadete(List<string[]> Filas)
    {
        List<Cadete> MisCadetes = new List<Cadete>();
        foreach (string[] fila in Filas)
        {
            Cadete cadete = new Cadete(int.Parse(fila[0]), fila[1], fila[2], fila[3]);
            MisCadetes.Add(cadete);
        }
        return MisCadetes;
    }

    public Cadeteria ConversorDeCadeteria(List<string[]> Filas, List<Cadete> listadoCadetes)
    {
        Cadeteria? MiCadeteria = null;
        foreach (string[] fila in Filas)
        {
            MiCadeteria = new Cadeteria(fila[0], fila[1], listadoCadetes);
        }
        
        return MiCadeteria;
    }
}