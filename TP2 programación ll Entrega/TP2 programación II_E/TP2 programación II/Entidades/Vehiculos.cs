using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2_programación_II.Controladores;

namespace TP2_programación_II.Entidades
{
    internal class Vehiculos
    {

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Patente { get; set; }
        // es un atributo mas de cada vehiculo por que cuando ingresa a una playa nececitamos guradar sus datos y
        // la hora de ingreso para luego saber cuanto cobarle.  

        public DateTime HoraIngreso { get; set; } // Usamos DateTime? para permitir un valor nulo
        public int Fila { get; set; }
        public int Columna { get; set; }

        public bool? Estacionado {get; set; }   
        public Vehiculos() { }

        public Vehiculos(string patente, string marca, string modelo, DateTime horaIng, int fila, int columna, bool estacionado)
        {
            Marca = marca;
            Modelo = modelo;
            Patente = patente;
            // Este atributo lo vamos agregar por un metodo (resgitrar vehiculo) en la parte de la playa
            HoraIngreso = horaIng; // La hora de ingreso se inicializa como nula
            Fila = fila;
            Columna = columna;
            Estacionado = estacionado;
            
        }
       

    }
}
