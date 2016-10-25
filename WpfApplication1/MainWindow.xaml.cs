using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApplication1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int contJornada = 0;

        private void button_seleccionar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.ShowDialog();
            textBox_seleccionar.Text = abrir.FileName;

            //Añadir equipos a la lista
            Datos.cargarEquipos(textBox_seleccionar.Text);

            foreach (string c in Datos.EquiposOriginal)
            {
                this.listBox_equipos.Items.Add(c);
            }

            //Genero listas auxiliares
            Datos.generarListasAuxiliares();
        }

        private void button_generarXML_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Partido>));
            TextWriter textWriter = new StreamWriter(@"C:\Users\DAM220\Desktop\jornada" + contJornada + ".xml");
            serializer.Serialize(textWriter, Datos.Jornada);
            textWriter.Close();

            //Pruebas en casa
            //XmlSerializer serializer = new XmlSerializer(typeof(List<Partido>));
            //TextWriter textWriter = new StreamWriter(@"C:\Users\David\Desktop\jornada" + contJornada + ".xml");
            //serializer.Serialize(textWriter, Datos.Jornada);
            //textWriter.Close();
        }

        private void button_calcular_Click(object sender, RoutedEventArgs e)
        {
            contJornada++;
            //Comprobacion si ya se han generado todas las jornadas
            if (contJornada > Datos.EquiposOriginal.Count / 2)
            {
                MessageBox.Show("Ya se han generado todas las jornadas.");
            }
            //Jornada 1
            else if (contJornada == 1)
            {
                this.listBox_jornadas.Items.Clear();
                this.listBox_jornadas.Items.Add("Jornada " + contJornada);
                Datos.moverListasPrimeraJornada();

                //Mostrar jornada en la lista
                foreach (Partido p in Datos.Jornada)
                {
                    this.listBox_jornadas.Items.Add(p.EquipoLocal + "-" + p.EquipoVisitante);
                }
            }
            else if (contJornada % 2 == 0)
            {
                this.listBox_jornadas.Items.Clear();

                this.listBox_jornadas.Items.Add("Jornada " + contJornada);
                Datos.moverListasPares();

                //Mostrar jornada en la lista
                foreach (Partido p in Datos.Jornada)
                {
                    this.listBox_jornadas.Items.Add(p.EquipoLocal + "-" + p.EquipoVisitante);
                }
            }
            else if (contJornada %2 != 0)
            {
                this.listBox_jornadas.Items.Clear();

                this.listBox_jornadas.Items.Add("Jornada " + contJornada);
                Datos.moverListasImpares();

                //Mostrar jornada en la lista
                foreach (Partido p in Datos.Jornada)
                {
                    this.listBox_jornadas.Items.Add(p.EquipoLocal + "-" + p.EquipoVisitante);
                }
            }
        }
    }
}
