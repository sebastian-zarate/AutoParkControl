
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP2_programación_II.Entidades;
using Library2023;
using System.Numerics;

namespace TP2_programación_II.Controladores
{
    internal class nVehiculo
    {
        public static void Crear()
        {

            // metodo para crear vehiculos no registrados
            Vehiculos vehiculos = new Vehiculos();
            PlayaDeEstacionamiento playa = new PlayaDeEstacionamiento();
            Console.Write("Ingrese la patente del vehículo: ");
            vehiculos.Patente = Console.ReadLine().ToUpper();


            if (BuscarVehiculo(vehiculos.Patente) != true) // si comparamos si la patente ya existe
                                                           // si existe pasamos al menu sino seguimos con la 
                                                           //creacion del vehiculo
            {


                Console.WriteLine();
                Console.Write("Ingrese la marca del vehículo: ");
                vehiculos.Marca = Estructuras.ValidarTexto();
                Console.WriteLine();
                Console.Write("Ingrese el modelo del vehículo: ");
                vehiculos.Modelo = Estructuras.ValidarTexto();
                Console.WriteLine();
                Console.WriteLine("Ingrese año de ingreso a la playa: ");
                int año = Estructuras.NumerosEnteros();
                Console.WriteLine("Ingrese mes de ingreso a la playa: ");
                int mes = Estructuras.NumerosEnteros(1, 12);
                Console.WriteLine("Ingrese día de ingreso a la playa: ");
                int dia = Estructuras.NumerosEnteros(1, 31);

                Console.WriteLine("Ingrese hora de ingreso a la playa: ");
                int hora = Estructuras.NumerosEnteros(0, 23);
                Console.WriteLine("Ingrese minuto de ingreso a la playa: ");
                int minuto = Estructuras.NumerosEnteros(0, 59);


                vehiculos.HoraIngreso = new(año, mes, dia, hora, minuto, 00); // como guardar la hora ingresado

                Console.Clear();
                // buscamos una playa que tenga lugares libres
                Console.Write("Seleccione la playa en la que se va a estacionar el vehiculo: ");
                playa = Program.playas[nPlayaDeEstacionamiento.SeleccionarPlayaLibre()];

                nPlayaDeEstacionamiento.ValidarPosicionVehiculo(playa, vehiculos);
                vehiculos.Estacionado = true;
                playa.VehiculosEstacionados.Add(vehiculos);
                Program.vehiculo.Add(vehiculos);
             
            }
        }
        public static int SeleccionarVehiculoLibre()
        {

            // recorremos los vehiculos buscando los que no esten estacionados en alguna playa 
            List<Vehiculos> vehiculosNoEstacionados = new List<Vehiculos>();
            int contador = 0;
            foreach (Vehiculos vehiculos in Program.vehiculo)
            {
                if (vehiculos.Estacionado == false)
                { // cuando encontremos los imprimimos con patente, modelo y marca

                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
                    Console.WriteLine($"{contador + 1}. Patente: {vehiculos.Patente}");
                    Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                    Console.WriteLine($" Modelo: {vehiculos.Modelo} - Marca: {vehiculos.Marca}");
                    vehiculosNoEstacionados.Add(vehiculos);
                    contador++;
                }
            }
            Console.Write("Seleccione un vehículo: ");
            int s = Estructuras.NumerosEnteros(1,vehiculosNoEstacionados.Count)-1;
            
            // buscamos el auto en el sistema
            foreach (Vehiculos v in Program.vehiculo)
            {
                if (vehiculosNoEstacionados[s] == v)
                {
                    s = Program.vehiculo.IndexOf(v);
                    break;
                }
            }


            return s;

        }
        public static bool BuscarVehiculo(string vehic)
        {

            // metodo que devuelve un boleano si hay otro vehiculo con la misma patente
            bool estado = false;
            foreach (Vehiculos vehi in Program.vehiculo)
            {

                if (vehi.Patente == vehic)
                {
                    Console.WriteLine("Ya existe un vehículo con la patente: {0}", vehic);


                    Console.WriteLine("Presione cualquier letra para volver");
                    Console.ReadKey(false);

                    estado = true;
                    break;
                }
            }
            return estado;
        }
        public static int SeleccionarVehiculoEstacionado()
        {
            List<Vehiculos> vehiculosEstacionados = new List<Vehiculos>();
            int contador = 0;
            foreach (Vehiculos vehiculos in Program.vehiculo)
            {
                if (vehiculos.Estacionado == true)
                {

                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
                    Console.WriteLine($"{contador + 1}. Patente: {vehiculos.Patente}");
                    Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                    Console.WriteLine($" Modelo: {vehiculos.Modelo} - Marca: {vehiculos.Marca}");
                    Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                    Console.WriteLine($" Fecha de ingreso a la playa: {vehiculos.HoraIngreso}");
                    vehiculosEstacionados.Add(vehiculos);
                    contador++;
                }
            }
            Console.Write("Seleccione un vehículo:");
            int s = Estructuras.NumerosEnteros(1, Program.vehiculo.Count) - 1;
            // comparo que los vehiculos sean los mismos
            foreach (Vehiculos v in Program.vehiculo)
            {
                if (vehiculosEstacionados[s] == v)
                {
                    s = Program.vehiculo.IndexOf(v);
                    break;
                }
            }

            return s;

        }
    }
}
