using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TP2_programación_II.Controladores;
using TP2_programación_II.Entidades;
using System.Numerics;
using Library2023;

namespace TP2_programación_II.Controladores
{
    internal class nCobros
    {
        public static void Crear()
        {
            bool estado = true;
            while (estado)
            {
                Console.Clear();
                Cobros cobros = new Cobros();
                cobros.Id = Program.cobros.Count() + 1;
                // buscamos y selecionamos el vehiculo que este estacionado en alguna playa
                cobros.Vehiculo = Program.vehiculo[nVehiculo.SeleccionarVehiculoEstacionado()];

                Console.WriteLine("Ingrese año de salida de la playa: ");
                int año = Estructuras.NumerosEnteros();
                Console.WriteLine("Ingrese mes de salida de la playa: ");
                int mes = Estructuras.NumerosEnteros(1, 12);
                Console.WriteLine("Ingrese día de salida de la playa: ");
                int dia = Estructuras.NumerosEnteros(1, 31);
                Console.WriteLine("Ingrese hora de salida de la playa: ");
                int hora = Estructuras.NumerosEnteros(0, 24);
                Console.WriteLine("Ingrese minuto de salida de la playa: ");
                int minuto = Estructuras.NumerosEnteros(0, 59);
                cobros.FechaHora = new(año, mes, dia, hora, minuto, 00);

                DateTime ingreso = new DateTime();
                DateTime salida = new DateTime();
                float tarifa = 0;

                //recorro las playas
                foreach (PlayaDeEstacionamiento p in Program.playas)
                {
                    //mientras  haya vehículos en la playa
                    if (p.VehiculosEstacionados.Count > 0)
                    {
                        //recorro las lista de vehículos que esten en la playa
                        foreach (Vehiculos v in p.VehiculosEstacionados)
                        {
                            // accedo a los atributos de mi vehículo a través de lista de vehículos que estoy recorriendo en la playa
                            if (cobros.Vehiculo == v)
                            {
                                tarifa = p.TarifaPorHora;
                                ingreso = v.HoraIngreso;
                                //Console.WriteLine(ingreso);
                                salida = cobros.FechaHora;
                            }
                        }
                    }
                }

                // en base a la hora ingresada y la tarifa de la playa calculo el monto a cobrar al vehículo
                if (salida < ingreso)
                {
                    Console.WriteLine("Los datos de la fecha de salida no pueden ser menores a los de entrada!!");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    float monto = calculoHora(ingreso, salida, tarifa);
                    cobros.Monto = monto;

                    //recorro las playas
                    foreach (PlayaDeEstacionamiento playa in Program.playas)
                    {
                        //mientras  haya vehículos en la playa
                        if (playa.VehiculosEstacionados.Count > 0)
                        {
                            //recorro las lista de vehículos que esten en mi sistema
                            foreach (Vehiculos vehic in playa.VehiculosEstacionados)
                            {
                                if (cobros.Vehiculo == vehic)
                                {
                                    //el vehículo ya no está más estacionado, lo remuevo el vehículo de la playa, agrego objeto cobro a la playa
                                    vehic.Estacionado = false;
                                    playa.VehiculosEstacionados.Remove(vehic);
                                    playa.CobrosRealizados.Add(cobros);
                                    //playa.TotalCobros += monto;
                                    break;
                                }
                            }
                        }
                    }
                    Program.cobros.Add(cobros);
                    break;
                }

            }
        }

        public static float calculoHora(DateTime horaEntrada, DateTime horaSalida, float tarifaDePlaya)
        {
            //metodo para calcular el monto a cobrar por las horas que estuvo
            float total = 0;  //horas
            float totalMinutos = 0;
            float totaldias = 0;

            TimeSpan diferencia = horaSalida - horaEntrada;

            totaldias = diferencia.Days;
           
            totaldias *= 24;
            
            
            total = diferencia.Hours;                //asigno la diferencia de horas
           
            totalMinutos = diferencia.Minutes;      //la diferencia en minutos
            totalMinutos /= 60;                     //la diferencia en minutos la convierto en decimal
            
            total += totalMinutos + totaldias;              //sumo la horas totales más los minutos totales como parte decimal
            
            total *= tarifaDePlaya;             //multiplico por la tarifa por hora
            
            
            Console.ReadLine();
            return total;
        }
        public static void Imprimir()
        {
            //método para imprimir  los cobros realizados

            foreach (Cobros cobros in Program.cobros)
            {
                Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
                Console.WriteLine($"{Program.cobros.IndexOf(cobros) + 1}. Patente: {cobros.Vehiculo.Patente}");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine($" Id del cobro: {cobros.Id} - Hora de salida: {cobros.FechaHora}");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine($" Cobrado: ${cobros.Monto} ");

            }
     
        }
   

        public static void Menu()
        {
            string[] opciones = new string[] { "Crear", "Listar", "Volver" };
            Console.Clear();
            Estructuras.DibujarMenu("Gestión de salida de vehículos", opciones);
           
            Console.Write("Seleccione opción: ");

            int seleccion = Estructuras.NumerosEnteros(1, opciones.Length);
            Console.WriteLine();
            switch (seleccion)
            {
                case 1: Console.Clear(); Crear(); Console.Clear(); Menu(); break;
                case 2: Imprimir(); Console.ReadKey(); Console.Clear(); Menu(); break;
                case 3: break;

            }
        }

    
    }
}

