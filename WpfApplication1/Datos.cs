using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class Datos
    {
        static List<string> equiposOriginal = new List<string>();

        static List<int> listaLocales = new List<int>();
        static List<int> listaVisitantes = new List<int>();

        static List<Partido> jornada = new List<Partido>();

        public static List<string> EquiposOriginal
        {
            get
            {
                return equiposOriginal;
            }

            set
            {
                equiposOriginal = value;
            }
        }

        public static List<int> ListaLocales
        {
            get
            {
                return listaLocales;
            }

            set
            {
                listaLocales = value;
            }
        }

        public static List<int> ListaVisitantes
        {
            get
            {
                return listaVisitantes;
            }

            set
            {
                listaVisitantes = value;
            }
        }

        public static List<Partido> Jornada
        {
            get
            {
                return jornada;
            }

            set
            {
                jornada = value;
            }
        }
        //Cargar equipos en lista
        public static void cargarEquipos(string path)
        {
            EquiposOriginal.Clear();
            StreamReader reader = new StreamReader(path, Encoding.Default);
            while (reader.Peek() > -1)
            {
                EquiposOriginal.Add(reader.ReadLine());
            }
            reader.Close();
        }
        //Generacion de listas auxiliares (local y visitante)
        public static void generarListasAuxiliares()
        {
            for (int i = 1; i <= EquiposOriginal.Count - 2; i++)
            {
                if (i % 2 == 0)
                {
                    ListaVisitantes.Add(i);
                }
                else
                {
                    ListaLocales.Add(i);
                }
            }
            ListaLocales.Add(EquiposOriginal.Count);
            ListaVisitantes.Add(EquiposOriginal.Count - 1);
        }
        //mover las listas de la primera jornada
        public static void moverListasPrimeraJornada()
        {
            for (int i = 1; i <= Datos.EquiposOriginal.Count - 2; i++)
            {
                if (i % 2 == 0)
                {
                    Datos.Jornada.Last().EquipoVisitante = Datos.EquiposOriginal.ElementAt(i - 1);
                }
                else
                {
                    Datos.Jornada.Add(new Partido(Datos.EquiposOriginal.ElementAt(i - 1), ""));
                }
            }

            Datos.Jornada.Add(new Partido(Datos.EquiposOriginal.Last(), Datos.EquiposOriginal.ElementAt(Datos.EquiposOriginal.LastIndexOf(Datos.EquiposOriginal.Last()) - 1)));
        }
        //mover las listas para las jornadas pares
        public static void moverListasPares()
        {
            ListaLocales.Insert(0, listaLocales.Last());
            ListaLocales.RemoveAt(listaLocales.Count - 1);
            getListas();
        }
        //mover las listas para las jornadas impares
        public static void moverListasImpares()
        {
            ListaVisitantes.Insert(ListaVisitantes.Count, ListaLocales.Last());
            ListaLocales.RemoveAt(listaLocales.Count - 1);
            listaLocales.Add(listaLocales.FirstOrDefault());
            listaLocales.RemoveAt(0);
            listaLocales.Insert(0,ListaVisitantes.FirstOrDefault());
            ListaVisitantes.RemoveAt(0);
            getListas();
        }
        //equiparacion de listas auxiliares con la lista original
        public static void getListas()
        {
            Datos.Jornada.Clear();

            int local;
            int visitante;
            string uno = "";
            string dos = "";

            for (int i = 0; i < EquiposOriginal.Count / 2; i++)
            {
                local = ListaLocales.ElementAt(i);
                visitante = ListaVisitantes.ElementAt(i);

                foreach (string s in EquiposOriginal)
                {
                    string[] indices = s.Split('-');

                    if (local.ToString() == indices[0])
                    {
                        uno = s;
                    }
                    else if (visitante.ToString() == indices[0])
                    {
                        dos = s;
                    }
                }
                Datos.Jornada.Add(new Partido(uno, dos));
            }

        }
    }

    public class Partido
    {
        string equipoLocal;
        string equipoVisitante;

        public Partido(string equipoLocal, string equipoVisitante)
        {
            this.equipoLocal = equipoLocal;
            this.equipoVisitante = equipoVisitante;
        }
        public Partido()
        {

        }
        public string EquipoLocal
        {
            get
            {
                return equipoLocal;
            }

            set
            {
                equipoLocal = value;
            }
        }

        public string EquipoVisitante
        {
            get
            {
                return equipoVisitante;
            }

            set
            {
                equipoVisitante = value;
            }
        }
    }
}
