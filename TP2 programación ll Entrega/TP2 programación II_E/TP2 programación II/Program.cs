using System.ComponentModel.Design;
using TP2_programación_II.Controladores;
using TP2_programación_II.Entidades;
using Library2023;
using System.Security.Cryptography;
using System.IO;
using System.Text;
//GRUPO: Ctrl-Shift-elite
// Integrantes: Benner Alan, Girard Nicolás, Maldonado Mateo y Zárate Sebastián.
// 

namespace TP2_programación_II
{
    internal class Program
    {
        public static List<PlayaDeEstacionamiento> playas;
        public static List<Vehiculos> vehiculo;
        public static List<Cobros> cobros;  

        static void Main(string[] args)
        {
            playas = new List<PlayaDeEstacionamiento>();
            vehiculo = new List<Vehiculos>();
            cobros = new List<Cobros>();
    
            
            Datos();
            Menu();
           
        }
        public static void Menu()
        {
            // menu principal del programa
            Console.Clear();
            string[] opciones = { "Gestionar playas", "Ingresar entrada Vehículo", "Gestionar salida vehículo","Imprimir dibujo de la playa","Ordenar playa por disponibilidad","Ordenar playa por cobros", "Salir" };
            Console.WriteLine();
            Estructuras.DibujarMenu("Menú", opciones);
            Console.WriteLine();
            Console.WriteLine("Seleccione un opción: ");
            int seleccion = Estructuras.NumerosEnteros(1, opciones.Length);
            switch (seleccion)
            {
                case 1: Console.Clear(); nPlayaDeEstacionamiento.Menu(); Menu(); break;
                case 2: Console.Clear(); nPlayaDeEstacionamiento.IngresoVehiculo(); Menu(); break;
                case 3: Console.Clear(); nCobros.Menu(); Menu(); break;
                case 4:
                    Console.Clear();//  imprime a las playas con sus vehiculos estacionados en el lugar que se agrego
                    int i = nPlayaDeEstacionamiento.Seleccionar();
                    Console.Clear();
                    nPlayaDeEstacionamiento.Recuadro(playas[i].Nombre, nPlayaDeEstacionamiento.CargarMatriz(i));
                    Console.ReadKey();
                    Menu();
                    break;
                case 5: Console.Clear(); CantidadVehiculo(); OpcionBlocNotas(2); Console.SetOut(Console.Out); Console.ReadKey(); Menu(); break;
                case 6: Console.Clear(); OrdenarXCobros(); OpcionBlocNotas(1); Console.SetOut(Console.Out); Console.ReadKey(); Menu(); break;
             
                case 7: break;
            }

        }

        public static void Datos()
        {
            
            // Cargamos los vehiculos 
            vehiculo.Add(new Vehiculos("PLN 279", "Toyota", "Etios", new DateTime(2023, 09, 20, 15, 30, 0), 0, 0, true));// vehiculo 0
            vehiculo.Add(new Vehiculos("DCQ 245", "Mercedez-Benz", "Clase C Sedán AMG", new DateTime(2023, 09, 19, 17, 30, 0), 1, 1, true));// vehiculo 1
            vehiculo.Add(new Vehiculos("ABA 812", "Chevrolet", "Corsa", new DateTime(2023, 09, 20, 16, 00, 0), 0, 2, true));// vehiculo 2
            vehiculo.Add(new Vehiculos("PJK 279", "Toyota", "Etios", new DateTime(2023, 09, 20, 15, 30, 0), 2, 0, true));// vehiculo 3
            vehiculo.Add(new Vehiculos("QCQ 235", "Mercedez-Benz", "Clase C Sedán AMG", new DateTime(2023, 09, 19, 17, 30, 0), 2, 1, true));// vehiculo 4
            vehiculo.Add(new Vehiculos("GHA 241", "Volks Wagen", "Vento", new DateTime(2023, 09, 20, 16, 00, 0), 2, 2, false));// vehiculo 5

            vehiculo.Add(new Vehiculos("XYZ 123", "Ford", "Focus", new DateTime(2023, 09, 21, 14, 45, 0), 1, 0, false)); // vehiculo 6  
            vehiculo.Add(new Vehiculos("LMN 456", "Honda", "Civic", new DateTime(2023, 09, 21, 10, 15, 0), 0, 1, true)); //vehiculo  7
            vehiculo.Add(new Vehiculos("JKL 789", "Nissan", "Altima", new DateTime(2023, 09, 22, 11, 30, 0), 1, 2, false)); // vehiculo  8
            vehiculo.Add(new Vehiculos("ABC 567", "Toyota", "Camry", new DateTime(2023, 09, 23, 9, 20, 0), 2, 0, true)); // vehiculo  9

            vehiculo.Add(new Vehiculos("EFG 789", "Honda", "Accord", new DateTime(2023, 09, 24, 13, 45, 0), 0, 2, true)); // vehiculo 10
            vehiculo.Add(new Vehiculos("MNO 456", "Ford", "Mustang", new DateTime(2023, 09, 24, 16, 30, 0), 1, 0, false)); // vehiculo 11
            vehiculo.Add(new Vehiculos("RST 123", "Chevrolet", "Malibu", new DateTime(2023, 09, 25, 11, 15, 0), 2, 1, true)); // vehiculo 12
            vehiculo.Add(new Vehiculos("UVW 789", "Nissan", "Maxima", new DateTime(2023, 09, 25, 9, 50, 0), 1, 2, true)); // vehiculo 13



            // Importamos las playas y  las agregamos
            importarPlayas();
            playas.Add(new PlayaDeEstacionamiento(1, "The Brothers", 5, 5, 1000));
            playas.Add(new PlayaDeEstacionamiento(2, "The dogs", 10, 6, 500));
            playas.Add(new PlayaDeEstacionamiento(3, "Radiadores Springs", 8, 4, 250));

            // agregamos vehiculos 
            playas[0].VehiculosEstacionados.Add(vehiculo[0]);
            playas[1].VehiculosEstacionados.Add(vehiculo[1]);
            playas[1].VehiculosEstacionados.Add(vehiculo[2]);
            playas[5].VehiculosEstacionados.Add(vehiculo[3]);
            playas[2].VehiculosEstacionados.Add(vehiculo[4]);
            playas[2].VehiculosEstacionados.Add(vehiculo[7]);
            playas[0].VehiculosEstacionados.Add(vehiculo[9]);
            playas[1].VehiculosEstacionados.Add(vehiculo[10]);
            playas[2].VehiculosEstacionados.Add(vehiculo[12]);
            playas[0].VehiculosEstacionados.Add(vehiculo[13]);


            // ORDENAR POR COBROS

            cobros.Add(new Cobros(1, vehiculo[0], playas[0], 4000, new(2023, 9, 23, 15, 10, 00)));
            cobros.Add(new Cobros(2, vehiculo[1], playas[2], 3000, new(2023, 9, 23, 15, 30, 00)));
            cobros.Add(new Cobros(3, vehiculo[0], playas[2], 4000, new(2023, 10, 23, 14, 30, 00)));
            cobros.Add(new Cobros(4, vehiculo[2], playas[1], 1000, new(2023, 9, 23, 15, 00, 00)));

            cobros.Add(new Cobros(5, vehiculo[7], playas[4], 4000, new(2023, 9, 23, 15, 10, 00)));
            cobros.Add(new Cobros(6, vehiculo[6], playas[3], 500, new(2023, 9, 23, 15, 30, 00)));
            cobros.Add(new Cobros(7, vehiculo[10], playas[2], 5000, new(2023, 10, 23, 14, 30, 00)));
            cobros.Add(new Cobros(8, vehiculo[12], playas[5], 1000, new(2023, 9, 23, 15, 00, 00)));
            // agregamos los cobros a las playas
            playas[0].CobrosRealizados.Add(cobros[0]);
            playas[2].CobrosRealizados.Add(cobros[1]);
            playas[2].CobrosRealizados.Add(cobros[2]);
            playas[5].CobrosRealizados.Add(cobros[3]);
            playas[0].CobrosRealizados.Add(cobros[4]);
            playas[4].CobrosRealizados.Add(cobros[5]);
            playas[2].CobrosRealizados.Add(cobros[6]);
            playas[3].CobrosRealizados.Add(cobros[7]);
           


        }
        public static void importarPlayas()
        // Metodo paara importar las playas del archivo txt
        // Con StreamReader podemos crear y leer el texto de txt respetando los parametros que les pasamos
        {
            StreamReader sr = new StreamReader("playas.txt");
            string line = sr.ReadLine();
            while (line != null)
            {

                string[] datos = line.Split(';');
                //                                       id - nombre de la playa- fila - columna - tarifa  
                playas.Add(new PlayaDeEstacionamiento(int.Parse(datos[0]), (datos[1]), int.Parse(datos[2]), int.Parse(datos[3]), float.Parse(datos[4])));
                line = sr.ReadLine();
            }
            sr.Close();
        }


        public static List<PlayaDeEstacionamiento> CantidadVehiculo()
        {

            // metodo de ordenamiento por la cantidad de lugares libres de mayor a menor
            playas = playas.OrderByDescending(playa =>  (playa.CapacidadMaximaFila * playa.CapacidadMaximaColumna) - playa.VehiculosEstacionados.Count).ToList();
            return playas;

        }

        public static List<PlayaDeEstacionamiento> OrdenarXCobros()
        {
            // metodo de ordenamiento  de mayor a menor recaudacion
            nPlayaDeEstacionamiento.CalcularCobros(); //calcula el total de cobros en base  a los montos de las salidas que tubo
            playas = playas.OrderByDescending(p => p.TotalCobros).ToList();
            return playas;
        }
        public static void OpcionBlocNotas(int num)
        {
            
            // imprime el informe en consola
            string contenidoInforme = GenerarInforme(num);
            Console.WriteLine(contenidoInforme);
       
    
            // si es 1 lo imprime en el bloc de notas
            Console.WriteLine("¿Le interesaría guardar el informe en el bloc de notas? (1. Sí // 2. No)");
            int opcionGuardarInforme = Estructuras.NumerosEnteros(1, 2);

            if (opcionGuardarInforme == 1)
            {
                // Define el nombre del archivo de salida
                string nombreArchivo = "informes.txt";

                // Añade el contenido al archivo existente en lugar de crear uno nuevo
                File.AppendAllText(nombreArchivo, contenidoInforme);

                Console.WriteLine("El informe ha sido añadido al archivo " + nombreArchivo);
            }
            else
            {
                Console.WriteLine("El informe no se guardó en el bloc de notas");
            }
        }

        public static string GenerarInforme(int num)
        {
            // Inicializa un StringBuilder para construir el informe.
            StringBuilder informe = new StringBuilder();

            // Recorre cada playa de estacionamiento.
            foreach (PlayaDeEstacionamiento pl in playas)
            {
                // Agrega un salto de línea al informe.
                informe.AppendLine();

                // Inicializa una lista para almacenar la información de cobros.
                List<(string, string, string)> lista = new List<(string, string, string)>();

                // Inicializa una variable para el total de cobros.
                float total = 0;

                // Recorre los cobros realizados en la playa actual.
                foreach (Cobros cobr in pl.CobrosRealizados)
                {
                    // Extrae información relevante de cada cobro.
                    string patente = cobr.Vehiculo.Patente;
                    string monto = cobr.Monto.ToString();
                    string fechaHora = cobr.FechaHora.ToString();

                    // Actualiza el total acumulado.
                    total = total + cobr.Monto;

                    // Agrega la información a la lista.
                    lista.Add((patente, monto, fechaHora));
                }

                // Calcula la cantidad de lugares libres en la playa.
                int lugaresLibres = ((pl.CapacidadMaximaColumna * pl.CapacidadMaximaFila) - pl.VehiculosEstacionados.Count());

                // Llama al método para dibujar el informe.
                DibujarInforme(pl.Nombre, lista, total, lugaresLibres, num, informe);
            }

            // Retorna el informe completo en formato de cadena.
            return informe.ToString();
        }

        public static void DibujarInforme(string titulo, List<(string, string, string)>? opciones, float? cobro = null, int? lugares = null, int? op = null, StringBuilder informe = null)
        {
            // Calcula la longitud máxima necesaria para presentar la información.
            int longitudMaxima = 0;

            // Itera sobre las opciones para determinar la longitud máxima.
            foreach (var opcion in opciones)
            {
                int longitudOpcion = opcion.Item1.Length + opcion.Item2.Length + opcion.Item3.Length + 30;

                if (longitudOpcion > longitudMaxima)
                {
                    longitudMaxima = longitudOpcion;
                }
            }

            // Ajusta la longitud máxima para asegurar una presentación adecuada.
            longitudMaxima += 50;
            if (longitudMaxima > 43)
            {
                longitudMaxima = 43 + 25;
            }

            // Agrega al informe los elementos del marco superior.
            informe.AppendLine("╔" + new string('═', longitudMaxima) + "╗");
            informe.Append("║");
            informe.Append(titulo.PadLeft((longitudMaxima + titulo.Length) / 2).PadRight(longitudMaxima));
            informe.AppendLine("║");
            informe.AppendLine("╠" + new string('═', longitudMaxima) + "╣");

            // Verifica el tipo de operación para determinar cómo presentar la información.
            if (op == 1)
            {
                // Si es una lista de opciones numeradas.
                int indice = 1;

                foreach (var opcion in opciones)
                {
                    // Construye el contenido de cada opción.
                    string numeroOpcion = indice.ToString().PadLeft(3);
                    string contenidoOpcion = $"{opcion.Item1}: {"$" + opcion.Item2}, {"FECHA " + opcion.Item3}";

                    // Agrega la opción al informe.
                    informe.Append("║ ");
                    informe.Append(numeroOpcion + ". Patente " + contenidoOpcion.PadRight(longitudMaxima - 14));
                    informe.AppendLine("║");

                    // Agrega un marco entre las opciones.
                    if (indice <= opciones.Count)
                    {
                        informe.AppendLine("╟" + new string('═', longitudMaxima) + "╢");
                    }

                    // Incrementa el índice.
                    indice++;
                }

                // Agrega el total de cobros al informe.
                informe.Append("║");
                string montoTotal = $"   Monto total recaudado ${cobro} de la playa {titulo}";
                informe.Append(montoTotal.PadLeft((montoTotal.Length)).PadRight(longitudMaxima));
                informe.AppendLine("║");

                // Agrega el marco inferior al informe.
                informe.AppendLine("╚" + new string('═', longitudMaxima) + "╝");
            }
            else if (op == 2)
            {
                // Si es información sobre lugares libres.
                string montoTotal = $"   La playa {titulo} tiene {lugares} lugares libres  ";
                informe.Append("║");
                informe.Append(montoTotal.PadLeft((montoTotal.Length)).PadRight(longitudMaxima));
                informe.AppendLine("║");

                // Agrega el marco inferior al informe.
                informe.AppendLine("╚" + new string('═', longitudMaxima) + "╝");
            }
        }


    }




}
