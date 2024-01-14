using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2_programación_II.Entidades
{
    internal class Cobros
    {
        public int Id { get; set; }
        public Vehiculos Vehiculo { get; set; }
        public PlayaDeEstacionamiento Playa {get; set; }
        public DateTime FechaHora { get; set; }// de salida del vehiculo
        public float Monto { get; set; }// Monto que se cobro        



        public Cobros() { }
        public Cobros(int id, Vehiculos vehiculo, PlayaDeEstacionamiento playa, float monto, DateTime fechaHora)
        {
            Id = id;    
            Vehiculo = vehiculo;
            Playa = playa;
            Monto = monto;
            FechaHora = fechaHora;
            
        }

     
    }
}
