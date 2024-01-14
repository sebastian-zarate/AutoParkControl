using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_programación_II.Entidades
{
    internal class PlayaDeEstacionamiento
    {
        public int Id { get; set; }// agrege nuevo el id 
        public string Nombre { get; set; }

        // Podemos generar un matriz para poner luego la ubicacion de cada vehiculo
        // Para sacar la capacidad total que puede tener la playa multiplicamos CapacidadMaximaFila por CapacidadMaximaColumna
        public int CapacidadMaximaFila { get; set; }
        public int CapacidadMaximaColumna { get; set; }

        // Monto en pesos u otra moneda por hora u otra unidad de tiempo 
        public float TarifaPorHora { get; set; }

        public float TotalCobros {get; set; }
        public List<Vehiculos> VehiculosEstacionados { get;  set; }
        public List<Cobros> CobrosRealizados { get; private set; }

        public string PatentesAutos { get => ConcatenarAutosXpatentes(); }
        public PlayaDeEstacionamiento()
        {
            VehiculosEstacionados = new List<Vehiculos>();
            CobrosRealizados = new List<Cobros>();
        }
        public PlayaDeEstacionamiento(int id, string nombre, int fila, int columna, float tarifaPorHora)
        {
            Id = id;
            Nombre = nombre;
            CapacidadMaximaFila = fila;
            CapacidadMaximaColumna = columna;
            TarifaPorHora = tarifaPorHora;
            VehiculosEstacionados = new List<Vehiculos>();
            CobrosRealizados = new List<Cobros>();
           

        }

        string ConcatenarAutosXpatentes()
        {
            string a = "";
            foreach (Vehiculos vehiculo in VehiculosEstacionados)
            {
                a = a + vehiculo.Patente;
                if (VehiculosEstacionados.IndexOf(vehiculo) < VehiculosEstacionados.Count - 1)
                {
                    a = a + " - ";
                }
            }

            return a;
        }

    }
}
