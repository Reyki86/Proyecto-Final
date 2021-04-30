using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static double new_balance;
        static float new_ratioInteres;
        static int new_plazoMeses;
        static char repeat = 'y';

        static DateTime localDate = DateTime.Now;
        static System.Data.DataTable tablaDatos = new DataTable();

        public static void Main(string[] args)
        {
            while (repeat == 'y')
            {
                CrearTablaDatos();

                PedirDatos();

                Usuario user1 = new Usuario(new_balance, new_ratioInteres, new_plazoMeses);

                user1.CalcularCuotaMensual();

                user1.CalcularInteresesMensuales();

                MostrarTabla();

                do
                { 
                    Console.WriteLine("Desea calcular otro prestamo? (y/n)");
                    repeat = char.Parse(Console.ReadLine());
                }
                while (repeat != 'y' && repeat != 'n');
                
            }

        }

        class Usuario
        {
            private float ratioInteres;
            private double balance;
            private double interesMensual;
            private double cuotaMensual;
            private double capital;
            private int plazoMeses;

            public Usuario(double balance, float ratioInteres, int plazoMeses)
            {
                this.balance = balance;
                this.ratioInteres = ratioInteres;
                this.plazoMeses = plazoMeses;
            }
            public void CalcularCuotaMensual()
            {
                cuotaMensual = ((balance * ratioInteres) * Math.Pow(1.0 + ratioInteres, plazoMeses)) / (Math.Pow(1.0 + ratioInteres, plazoMeses) - 1.0);

            }

            public void CalcularInteresesMensuales()
            {
                for (int i = 1; i <= plazoMeses; i++)
                {
                    interesMensual = balance * ratioInteres;
                    capital = cuotaMensual - interesMensual;

                    balance = balance - capital;

                    tablaDatos.Rows.Add(i, localDate.AddMonths(i).ToString("dd/MMM/yyyy"), Math.Round(cuotaMensual, 2), Math.Round(capital, 2), Math.Round(interesMensual, 2), Math.Round(balance, 2));
                }
            }
        }        

        static void CrearTablaDatos()
        {
            tablaDatos.Reset();

            tablaDatos.Columns.Add("Pago", typeof(int));
            tablaDatos.Columns.Add("Fecha", typeof(string));
            tablaDatos.Columns.Add("Cuota", typeof(double));
            tablaDatos.Columns.Add("Capital", typeof(double));
            tablaDatos.Columns.Add("Interes", typeof(double));
            tablaDatos.Columns.Add("Balance", typeof(double));
        }

        static void PedirDatos()
        {
            Console.Write("Introduzca la cantidad a pagar:");
            new_balance = double.Parse(Console.ReadLine());


            Console.Write("Introduzca el % de interes anual:");
            new_ratioInteres = float.Parse(Console.ReadLine());
            new_ratioInteres /= 1200;
            
            Console.Write("Introduzca el plazo a pagar en meses:");
            new_plazoMeses = Int32.Parse(Console.ReadLine());



        }
        static void MostrarTabla()
        {
            foreach (DataRow drow in tablaDatos.Rows)
                Console.WriteLine("Pago    Fechas de Pago      Cuota      Capital      Interes      Balance\n{0}      {1}        {2}    {3}      {4}       {5}", drow["Pago"], drow["Fecha"], drow["Cuota"], drow["Capital"], drow["Interes"], drow["Balance"]);

        }
    }
}
