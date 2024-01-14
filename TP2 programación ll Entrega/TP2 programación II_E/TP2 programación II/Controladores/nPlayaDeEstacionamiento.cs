using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

using Library2023;
using TP2_programación_II.Entidades;

namespace TP2_programación_II.Controladores
{
    internal class nPlayaDeEstacionamiento
    {
        public static void Crear()
        {
            // metodo para crear la playa y las agregamos a la lista general
            PlayaDeEstacionamiento playas = new PlayaDeEstacionamiento();

            Console.Write("Ingrese un nombre de playa: ");
            playas.Nombre = Estructuras.ValidarTexto();// comparamos sino existe otra igual a la ingresada
            if (BuscarPlaya(playas.Nombre) != true)
            {

                Console.WriteLine();
                Console.Write("Ingrese el número de fila máxima: ");
                playas.CapacidadMaximaFila = Estructuras.NumerosEnteros();
                Console.WriteLine();
                Console.Write("Ingrese el número de columnas máximas de la playa: ");
                playas.CapacidadMaximaColumna = Estructuras.NumerosEnteros();
                Console.WriteLine();
                Console.Write("Ingrese la tarifa por hora de la playa: ");
                playas.TarifaPorHora = Estructuras.NumerosEnteros();

                Program.playas.Add(playas);
            }
        
        }

        public static void Imprimir()

        {
          
      
            // imprimir el nombre de las playas, cantidad de lugares, autos acargo, lugares libres y total recudado de la playa
            foreach (PlayaDeEstacionamiento playa in Program.playas)
            {
                Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
                Console.WriteLine("{0} - {1}", Program.playas.IndexOf(playa) + 1, playa.Nombre);
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine("Capacidad maxima: {0} lugares", playa.CapacidadMaximaColumna * playa.CapacidadMaximaFila);
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine($"Tarifa de la playa: ${playa.TarifaPorHora}");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine($"Patentes a cargo de la playa: {playa.PatentesAutos}");
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine($"Lugares libres: {(playa.CapacidadMaximaColumna * playa.CapacidadMaximaFila) - playa.VehiculosEstacionados.Count()}");
                //con este método obtengo el valor del total de cobros por playa
                CalcularCobros();
                Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                Console.WriteLine($"Total Recaudado: ${playa.TotalCobros}");
            }
        }

        public static int Seleccionar()
        {
            // metodo que llama al metodo imprimir() playas y podemos selecionar una playa
            Console.WriteLine();
            Imprimir();
            Console.Write("Seleccione una playa: ");
            int s = Estructuras.NumerosEnteros();

            return s - 1;
        }
        public static void Eliminar()
        {
            // metodo eliminar playa
            int i = Seleccionar();
            Program.playas.RemoveAt(i);
        }
        public static void Modificar(int i)
        {   
            // Metodo para modificar alguna playa selecionada anteriormente
            Console.WriteLine();
            string[] opciones = new string[] { "Nombre", "Fila", "Columnas", "Tarifa por hora ", "Volver" };
            Console.Clear();
            Estructuras.DibujarMenu("Modificar", opciones);
            Console.Write("Seleccione opción a modificar: ");
            
            int seleccion = Estructuras.NumerosEnteros(1, opciones.Length);
            Console.WriteLine();
            switch (seleccion)
            {
                case 1: // modifico el nombre de la playa
                    
                    Console.Clear();
                    Console.Write("Ingrese nuevo nombre para la playa {0}: ", Program.playas[i].Nombre);
                    Program.playas[i].Nombre = Estructuras.ValidarTexto(); Modificar(i); break;

                case 2:// modifico la cantidad de filas de la plya
                    Console.Write("Ingrese nueva cantidad de filas para la playa {0}: ", Program.playas[i].Nombre);
                    Program.playas[i].CapacidadMaximaFila = Estructuras.NumerosEnteros(); Modificar(i);
                    break;

                case 3://modifico la cantidad de columna de la plya
                    Console.Write("Ingrese nueva cantidad de columnas para la playa {0}: ", Program.playas[i].Nombre);
                    Program.playas[i].CapacidadMaximaColumna = Estructuras.NumerosEnteros(); Modificar(i); break;
                
                case 4: //modifico la tarifa
                    Console.Write("Ingrese nueva tarifa para la playa {0}: ", Program.playas[i].Nombre);
                    Program.playas[i].TarifaPorHora = Estructuras.NumerosEnteros(); Modificar(i); break;

                case 5:  Console.Clear(); break;




            }
        }
        public static void Menu()
        {
            // Metodo general de las playas de estacionamiento  
            string[] opciones = new string[] { "Crear", "Modificar", "Eliminar", "Listar", "Volver" };
            Console.Clear();
            Estructuras.DibujarMenu("Playas", opciones);

            Console.Write("Seleccione opción: ");

            int seleccion = Estructuras.NumerosEnteros(1, opciones.Length);
            Console.WriteLine();
            switch (seleccion)
            {
                case 1: Console.Clear(); Crear(); Console.Clear();  Menu(); break;
                case 2: Console.Clear(); Modificar(Seleccionar()); Console.Clear();  Menu(); break;
                case 3:
                    Console.Clear();
                    if (Program.playas.Count > 0)
                    { Eliminar(); }
                    else
                    {
                        Console.WriteLine("No existen datos a eliminar"); Console.ReadKey(true);
                    }; Console.Clear(); Menu(); break;
                case 4: Console.Clear(); Imprimir(); Console.ReadKey(); Console.Clear(); Menu(); break;
                case 5:  break;

            }
        }
        public static bool BuscarPlaya(string play)
        {
            // Metodo para comparar que no haya ninguna playa con el mismo nombre 
            bool estado = false;
            foreach (PlayaDeEstacionamiento pl in Program.playas)
            {
                if (pl.Nombre.ToUpper() == play.ToUpper()) // comparamos el nombre de la playa nueva 
                {
                    Console.WriteLine("Ya existe una playa con el nombre: {0}", play);

                    Console.WriteLine("Presione cualquier letra para volver");
                    Console.ReadKey(false);
                    estado = true;

                    break;
                }
            }
            return estado;

        }

        public static void IngresoVehiculo()
        {
            // meteto de ingresar vehiculo existentes a la playa
            
            Vehiculos vehiculo = new Vehiculos();
            PlayaDeEstacionamiento playa = new PlayaDeEstacionamiento();

            string[] opciones = new string[] { "Vehiculo nuevo", "Vehiculo ya cargado al sistema", "Volver" };
            Console.Clear();
            Estructuras.DibujarMenu("Ingresar vehículos", opciones);
            Console.Write("Seleccione Opción: ");
            int seleccion = Estructuras.NumerosEnteros(1, opciones.Length);
            Console.WriteLine();

            if (seleccion == 1)
            {
                // si no existe se crea y agrega
                Console.Clear();
                //utilizar vehiculo NUEVO
                nVehiculo.Crear();
                vehiculo = Program.vehiculo[Program.vehiculo.Count - 1];

            }
            if (seleccion == 2)
            {
                // si existe se agrega a la playa que desee
                bool estado = false;
                //para auto ya ingresado

                foreach (Vehiculos ve in Program.vehiculo)
                {
                    if (ve.Estacionado == false)
                    {
                        estado = true;
                        seleccion = 2;
                        break;
                    }
                    else
                    {
                        seleccion = 3;
                    }
                }

                
                vehiculo = Program.vehiculo[nVehiculo.SeleccionarVehiculoLibre()];
                Console.Clear();
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
                vehiculo.HoraIngreso = new(año, mes, dia, hora, minuto, 00);

                Console.Clear();
                Console.Write("Seleccione la playa en la que se va a estacionar el vehículo: ");
                playa = Program.playas[SeleccionarPlayaLibre()]; // seleciona la playa aingresar el vehiculo
                
                ValidarPosicionVehiculo(playa, vehiculo);// valida si en ese lugar hay vehiculo o la posicion ingresada es nula 
                vehiculo.Estacionado = true;// cambiamos su atributo de estacionado que sea true 
                playa.VehiculosEstacionados.Add(vehiculo);

            }
        }
        
        public static int SeleccionarPlayaLibre()
        {
            int contador = 0;
            List<PlayaDeEstacionamiento> playasLibres = new List<PlayaDeEstacionamiento>();
            // metodo para selecionar playa que tenga lugares libres
            Console.WriteLine();
            foreach (PlayaDeEstacionamiento playa in Program.playas)
            {     
                if (playa.VehiculosEstacionados.Count < playa.CapacidadMaximaColumna * playa.CapacidadMaximaFila)
                {

                    Console.SetCursorPosition(Console.CursorLeft + 3, Console.CursorTop);
                    Console.WriteLine("{0} - {1}", contador + 1, playa.Nombre);
                    Console.SetCursorPosition(Console.CursorLeft + 8, Console.CursorTop);
                    // nos dice la capacidad  de lugares que tiene libre
                    Console.WriteLine($"Capacidad disponible: {(playa.CapacidadMaximaColumna * playa.CapacidadMaximaFila - playa.VehiculosEstacionados.Count)} lugares");
                    playasLibres.Add(playa);
                    contador++;
                }
            }
            // selecionar la playa
            Console.Write("Seleccione una playa: ");
            int s = Estructuras.NumerosEnteros(1, playasLibres.Count) - 1;

            foreach (PlayaDeEstacionamiento p in Program.playas)
            {
                if (playasLibres[s] == p)
                {
                    s = Program.playas.IndexOf(p);
                    break;
                }
            }
            return s;
        }

        public static void CalcularCobros()
        {

            // metodo para calcular la cantidad recaudado por playa
            //recorro las playas de mi sistema
            for (int i = 0; i < Program.playas.Count; i++)
            {
                float total = 0;
                // accedo a las que tengan cobros realizados mayores a 0
                if (Program.playas[i].CobrosRealizados.Count > 0)
                {
                    //recorro cada cobro realizado por una playa
                    for (int j = 0; j < Program.playas[i].CobrosRealizados.Count; j++)
                    {
                        //sumo cada cobro realizado a mi variable total
                        total += Program.playas[i].CobrosRealizados[j].Monto;
                    }
                }
                //al atributo de la lista de program le asigno el valor de la variable total
                Program.playas[i].TotalCobros = total;
            }


        }

        public static char[,] CargarMatriz(int indice)
        {

            // metodo cargar matrix con los vehiculos estacionados
            PlayaDeEstacionamiento playa = Program.playas[indice]; 

            char[,] m = new char[playa.CapacidadMaximaFila, playa.CapacidadMaximaColumna];

            if(playa.VehiculosEstacionados.Count > 0) 
            {
                
                foreach (Vehiculos v in playa.VehiculosEstacionados)
                {
                    m[v.Fila, v.Columna] = 'X';
                    
                }
            }
            

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    if (m[i, j] != 'X')
                    {
                        m[i, j] = '-';
                    }
                    
                }
            }
            return m;
        }

        public static void ValidarPosicionVehiculo(PlayaDeEstacionamiento playa, Vehiculos vehiculo)
        {
            //bool para controlar el bucle
            bool superposicion = true;

            // imprime la matriz por pantalla para mostrar los lugares disponibles en esta
            while (superposicion)
            {
                Console.Clear();
                Recuadro(playa.Nombre, CargarMatriz(Program.playas.IndexOf(playa)));
                Console.CursorLeft = 0;
                Console.Write("\nSeleccione la fila para estacionar el vehículo: ");
                vehiculo.Fila = Estructuras.NumerosEnteros(1, playa.CapacidadMaximaFila) - 1;
                Console.Write("Seleccione la columna para estacionar el vehículo: ");
                vehiculo.Columna = Estructuras.NumerosEnteros(1, playa.CapacidadMaximaColumna) - 1;
                int contador = 0;
                foreach (Vehiculos v in playa.VehiculosEstacionados)
                {

                    if (v.Fila == vehiculo.Fila && v.Columna == vehiculo.Columna)
                    {
                        contador += 1;
                    }


                }
                //si no coincide con ningun auto que superposicion sea falso
                if (contador == 0)
                {
                    superposicion = false;
                }
                else if (contador == 1)
                {
                    Console.WriteLine("Ya hay un auto estacionado en esa posición");
                    Console.WriteLine("Presione cualquier tecla para volver a ingresar la posición del vehículo");
                    Console.ReadKey(true);
                }

            }

        }

        public static void Recuadro(string titulo, char[,] matriz)
        {


            //                 0    1    2    3    4    5    6    7    8    9
            char[] cuadro = { '╔', '║', '═', '╗', '╚', '╝', '╠', '╣', '╩', '╦' };

            // excepcion es verdadera cuando el titulo es mas largo que el ancho de la matriz
            bool excepcion = false;
            int altura = matriz.GetLength(0) + 5;
            int ancho;
            int sangriaTitulo = 6; //sangrias por defecto
            int sangriaMatriz = 6;
            int anchoMatriz = matriz.GetLength(1) * 3;
            //si el ancho de la matriz menor al largo del titulo
            if ((anchoMatriz) <= titulo.Length)
            {
                ancho = (titulo.Length + 3 * 2) + sangriaTitulo;
                sangriaMatriz = ((ancho) - (anchoMatriz)) / 2;
                excepcion = true;
            }

            else
            //si el ancho de la matriz mayor al largo del titulo
            {
                ancho = ((anchoMatriz) + 2 * 3) + 1;
                sangriaTitulo = ((ancho) - (titulo.Length)) / 2;
            }
            int bordeLeft = 8;
            int bordeTop = 4;
            Console.SetCursorPosition(bordeLeft, bordeTop);

            for (int i = 0; i <= altura; i++)
            {
                if (i == 0) //borde arriba
                {
                    Console.Write(cuadro[0]);
                    for (int j = 1; j < ancho - 1; j++)
                    {
                        Console.Write(cuadro[2]);
                    }
                    Console.Write(cuadro[3]);
                    Console.CursorLeft = bordeLeft;
                    Console.CursorTop += 1;

                }

                if (i == 1) //titulo
                {
                    Console.Write(cuadro[1]);
                    Console.CursorLeft = Console.CursorLeft + ancho - 2;
                    Console.Write(cuadro[1]);
                    Console.CursorLeft = bordeLeft + sangriaTitulo;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write($"{titulo}");
                    Console.ResetColor();
                    Console.CursorTop += 1;
                    Console.CursorLeft = bordeLeft;
                }

                if (i == 2) //renglon despues de titulo
                {
                    Console.Write(cuadro[6]);
                    for (int j = 1; j < ancho - 1; j++)
                    {
                        Console.Write(cuadro[2]);
                    }
                    Console.Write(cuadro[7]);
                    Console.CursorLeft = bordeLeft;
                    Console.CursorTop += 1;
                }

                if (i == altura - 1) //bordes de abajo
                {
                    Console.Write(cuadro[4]);
                    for (int j = 1; j < ancho - 1; j++)
                    {
                        Console.Write(cuadro[2]);
                    }
                    Console.Write(cuadro[5]);
                    Console.CursorLeft = bordeLeft;
                    Console.CursorTop += 1;

                }

                if (i == 3) //enumerar e imprimir matriz
                {

                    Console.CursorLeft = bordeLeft;
                    Console.Write(cuadro[1]);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    if (excepcion == false)
                    {
                        Console.CursorLeft = bordeLeft + sangriaMatriz;
                    }
                    if (excepcion == true)
                    {
                        Console.CursorLeft = bordeLeft + sangriaTitulo;
                    }
                    for (int j = 1; j <= matriz.GetLength(1); j++)
                    {
                        if (j < 10)
                        {
                            Console.Write($"{j}  ");
                        }
                        else
                        {
                            Console.Write($"{j} ");
                        }

                    }

                    for (int j = 1; j <= matriz.GetLength(0); j++)
                    {
                        Console.CursorLeft = bordeLeft + 3;
                        Console.CursorTop += 1;
                        Console.Write($"{j} ");
                        Console.ResetColor();
                        for (int k = 0; k < matriz.GetLength(1); k++)
                        {
                            if (matriz[j - 1, k] == 'X')
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                            else if (matriz[j - 1, k] != 'X')
                            {
                                Console.ResetColor();
                            }


                            if (j < 10) Console.Write($" {matriz[j - 1, k]} ");

                            else Console.Write($"{matriz[j - 1, k]}  ");
                        }
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    Console.CursorTop = bordeTop + i;
                    Console.ResetColor();
                    Console.CursorLeft = bordeLeft + ancho - 1;
                    Console.Write(cuadro[1]);
                    Console.CursorLeft = bordeLeft;
                    Console.CursorTop += 1;
                }

                if (i > 3 && i < altura - 1)
                {
                    Console.Write(cuadro[1]);
                    Console.CursorLeft = Console.CursorLeft + ancho - 2;
                    Console.Write(cuadro[1]);
                    Console.CursorLeft = bordeLeft;
                    Console.CursorTop += 1;
                }



            }


        }

    }
}
