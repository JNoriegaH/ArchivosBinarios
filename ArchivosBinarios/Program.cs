using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    class ArchivosBinariosEmpleados
    {
        BinaryWriter bw = null;//flujo salida - escritura de datos
        BinaryReader br = null;//flujo entrada - lectura de datos

        //campos
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiaro;

        public void CrearArchivo(string Archivo)
        {
            //variable local metodo
            char resp;
            try
            {
                //creacion del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                //captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    NumEmp = int.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();
                    Console.Write("Direccion del Empleado: ");
                    Direccion = Console.ReadLine();
                    Console.Write("Telefono del Empleado: ");
                    Telefono = long.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados del Empleado: ");
                    DiasTrabajados = int.Parse(Console.ReadLine());
                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiaro = float.Parse(Console.ReadLine());

                    //escribe los datos al archivo
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiaro);

                    Console.Write("\n\nDeseas Almacenar otro Registro (s/n)?");

                    resp = char.Parse(Console.ReadLine());
                } while ((resp == 's') || (resp == 'S'));
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e.Message);
                Console.WriteLine("\nRuta: " + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close();// cierra el flujo - escritura
                Console.Write("\nPresione <Enter> para terminar la Escritura de Datos y regresar al Menu.");
            }
        }

        public void MostrarArchivo(string archivo)
        {
            try
            {
                //verifica si existe el archivo
                if (File.Exists(archivo))
                {
                    //creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(archivo, FileMode.Open, FileAccess.Read));

                    //despliegue de datos en pantalla
                    Console.Clear();

                    do
                    {
                        //lectura de registros mientras no llegue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiaro = br.ReadSingle();

                        //muestra los datos
                        Console.WriteLine("Numero del Empleado          : " + NumEmp);
                        Console.WriteLine("Nombre del Empleado          : " + Nombre);
                        Console.WriteLine("Direccion del Empleado       : " + Direccion);
                        Console.WriteLine("Telefono del Empleado        : " + Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado : " + DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado  : " + SalarioDiaro);
                        Console.WriteLine("SUELDO TOTAL del Empleado    : {0:C}", DiasTrabajados * SalarioDiaro);
                        Console.WriteLine("\n");
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl Archivo " + archivo + "No Existe en el Disco!!");
                    Console.Write("\nPresione <Enter> para continuar...");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.Write("\nPresione <Enter> para continuar...");
                Console.ReadKey();

            }
            finally
            {
                if (br != null) br.Close(); // cierra flujo
                Console.Write("\nPresione <Enter> para terminar la lectura de Datos y regresar al Menu");
                Console.ReadKey();
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                //declaracion variables auxiliares
                string Arch = null;
                int opcion;

                //Creacion del objeto
                ArchivosBinariosEmpleados Al = new ArchivosBinariosEmpleados();

                //Menu de opciones
                do
                {
                    Console.Clear();
                    Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                    Console.WriteLine("1.- Creacion de un Archivo.");
                    Console.WriteLine("2.- Lectura de un Archivo.");
                    Console.WriteLine("3.- Salida del Programa.");
                    Console.Write("\n Que opcion desea: ");
                    opcion = int.Parse(Console.ReadLine());

                    switch (opcion)
                    {
                        case 1:
                            //bloque de escritura
                            try
                            {
                                //captura nombre Archivo
                                Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                                Arch = Console.ReadLine();

                                //verifica si existe el archivo
                                char resp = 's';
                                if (File.Exists(Arch))
                                {
                                    Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo (s/n)?");
                                    resp = Char.Parse(Console.ReadLine());
                                }
                                if ((resp == 's') || (resp == 'S'))
                                {
                                    Al.CrearArchivo(Arch);
                                }
                            }
                            catch (IOException e)
                            {
                                Console.WriteLine("\nError: " + e.Message);
                                Console.WriteLine("\nRuta: " + e.StackTrace);
                            }
                            break;

                        case 2:
                            //bloque de lectura
                            try
                            {
                                //Captura nombre archivo
                                Console.Write("\nAlimenta el Nombre del Archivo que deseas leer: ");
                                Arch = Console.ReadLine();
                                Al.MostrarArchivo(Arch);
                            }
                            catch (IOException e)
                            {
                                Console.WriteLine("\nError: " + e.Message);
                                Console.WriteLine("\nRuta: " + e.StackTrace);
                            }
                            break;
                        case 3:
                            Console.Write("\nPresione <Enter> para Salir del Programa");
                            Console.ReadKey();
                            break;

                        default:
                            Console.WriteLine("\nEsa Opcion No Existe!!, Presione <Enter> para Continuar...");
                            Console.ReadKey();
                            break;
                    }
                } while (opcion != 3);
            }
        }
    }
}
