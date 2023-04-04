using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace DachflächenkalkulatorForms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public double hausbreite = 0;
        public double hauslaenge = 0;
        public double? überstandLänge = 0;
        public double? überstandBreite = 0;
        public double dachhöhe = 0;
        public double neigungswinkel = 0;
        public double eindeckbreite = 0;
        public double eindeckhöhe = 0;

        public double dachfläche = 0;
        public double ziegelProReihe = 0;
        public double reihen = 0;
        public double dachziegelProReihe = 0;

        public double dachlänge = 0;
        public double sparrenlänge = 0;
        public double dachbreite = 0;

        private void txt_Hauslänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Hauslänge.Text))
                {
                    txt_Hauslänge.Text = "0";
                    return;
                }
                hauslaenge = meterInCentimeter(Convert.ToDouble(txt_Hauslänge.Text));
                updateDachlänge();
                debugoutput();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txt_Hausbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Hausbreite.Text))
                {
                    txt_Hausbreite.Text = "0";
                    return;
                }
                hausbreite = meterInCentimeter(Convert.ToDouble(txt_Hausbreite.Text));
                updateDachbreite();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void txt_ÜberstandLänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                überstandLänge = Convert.ToDouble(
                    string.IsNullOrWhiteSpace(txt_ÜberstandLänge.Text)
                        ? 0
                        : txt_ÜberstandLänge.Text);

                updateDachlänge();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txt_ÜberstandBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                überstandBreite = Convert.ToDouble(
                    string.IsNullOrWhiteSpace(txt_ÜberstandBreite.Text)
                    ? 0
                    : txt_ÜberstandBreite.Text);

                updateDachbreite();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txt_Dachhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_Dachhöhe.Text) || txt_Dachhöhe.Text.Equals("0"))
                {
                    txt_Neigungswinkel.IsReadOnly = false;
                    txt_Neigungswinkel.Background = Brushes.White;
                }
                else
                {
                    txt_Neigungswinkel.IsReadOnly = true;
                    txt_Neigungswinkel.Background = Brushes.LightGray;
                    double.TryParse(txt_Dachhöhe.Text, out dachhöhe);
                }

                updateSparrenlänge();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txt_Neigungswinkel_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txt_Neigungswinkel.Text) || txt_Neigungswinkel.Text.Equals("0"))
                {
                    txt_Dachhöhe.IsReadOnly = false;
                    txt_Dachhöhe.Background = Brushes.White;
                }
                else
                {
                    txt_Dachhöhe.IsReadOnly = true;
                    txt_Dachhöhe.Background = Brushes.LightGray;
                    neigungswinkel = Convert.ToDouble(txt_Neigungswinkel.Text);
                }

                updateSparrenlänge();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txt_Eindeckbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_Eindeckbreite.Text)) return;

                eindeckbreite = Convert.ToDouble(txt_Eindeckbreite.Text);
                updateDachziegelProReihe();
                //TODO: Updateaufrufe!

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void txt_Eindeckhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            try { 
            if (string.IsNullOrEmpty(txt_Eindeckhöhe.Text)) return;

            eindeckhöhe = Convert.ToDouble(txt_Eindeckhöhe.Text);
            updateReihen();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //TODO: Updateaufrufe!
        }

        private void txt_MengeProReihe_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_MengeProReihe.Text)) return;

                ziegelProReihe = Convert.ToDouble(txt_MengeProReihe.Text);
                updateDachziegelGesamt();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
                
        private void txt_AnzahlReihen_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_AnzahlReihen.Text)) return;

                reihen = Convert.ToDouble(txt_AnzahlReihen.Text);
                updateDachziegelGesamt();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void txt_Dachfläche_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_Dachfläche.Text)) return;

                dachfläche = Convert.ToDouble(txt_Dachfläche.Text);
                updateDachziegelGesamt();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        private void txt_DachziegelGesamt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_DachziegelGesamt.Text)) return;
        }        
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        //////////////Update-Berechnungen/////////////////
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
       
        
        
        
        
        
        //////////////////////////////////////////////////
        ////////////////Rechenparameter///////////////////
        //////////////////////////////////////////////////
        
        private void updateDachbreite()
        {
            if (hausbreite == 0) return;
            dachbreite = hausbreite + überstandBreite ?? 0;
            updateSparrenlänge();
        }

        private void updateDachlänge()
        {
            if (hauslaenge == 0) return;
            dachlänge = hauslaenge + überstandLänge ?? 0;
            UpdateDachfläche();
        }
        private void updateSparrenlänge()
        {
            if (dachbreite == 0) return;
            if (dachhöhe != 0)
            {
                sparrenlänge = 
                    Math.Sqrt(
                        Math.Pow(dachbreite, 2) 
                        + Math.Pow(dachhöhe, 2)
                        );
                txt_Neigungswinkel.Text = Math.Round(Math.Asin(dachhöhe / sparrenlänge), 2).ToString();
            }
            else if (neigungswinkel != 0)
            {
                sparrenlänge =
                    dachbreite / (2 * Math.Cos(neigungswinkel));
                txt_Dachhöhe.Text = Math.Sin(neigungswinkel * sparrenlänge).ToString();
            }
            else sparrenlänge = 0;
            updateReihen();
        }
        ////////////////////////////////////////////////////
        ////////////////Ausgabeparameter///////////////////
        //////////////////////////////////////////////////
        private void updateReihen()
        {
            if (eindeckhöhe ==  0  || sparrenlänge == 0) return;
            reihen = eindeckhöhe / sparrenlänge;
            txt_AnzahlReihen.Text = reihen.ToString();
            updateDachziegelGesamt();
        }

        private void updateDachziegelProReihe()
        {
            dachziegelProReihe = dachlänge / eindeckbreite;
            txt_MengeProReihe.Text = dachziegelProReihe.ToString();
            updateDachziegelGesamt();
        }

        private void UpdateDachfläche()
        {
            if (dachlänge == 0 || sparrenlänge == 0) return;
            dachfläche = 2 * (dachlänge * sparrenlänge);
            txt_Dachfläche.Text = dachfläche.ToString();
            updateReihen();
            updateDachziegelProReihe();
        }

        private void updateDachziegelGesamt()
        {
            if (reihen == 0 || dachziegelProReihe == 0) return;
            var dachziegelGesamt = reihen * dachziegelProReihe;
            txt_DachziegelGesamt.Text = dachziegelGesamt.ToString();
        }

        private double meterInCentimeter(double meter)
        {
            return Math.Round(meter * 1000, 1);
        }

        private void debugoutput()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"==============Eingabeparameter==============");
            sb.AppendLine($"Hauslänge = {hauslaenge}");
            sb.AppendLine($"Hausbreite = {hausbreite}");
            sb.AppendLine($"Überstand Länge = {überstandLänge}");
            sb.AppendLine($"Überstand Breite = {überstandBreite}");
            sb.AppendLine($"Dachhöhe = {dachhöhe}");
            sb.AppendLine($"Neigungswinkel = {neigungswinkel}");
            sb.AppendLine($"Eindeckbreite = {eindeckbreite}");
            sb.AppendLine($"Eindeckhöhe = {eindeckhöhe}");
            sb.AppendLine($"==============Rechenparameter==============");
            sb.AppendLine($"Sparrenlänge = {sparrenlänge}");
            sb.AppendLine($"Dachbreite = {dachbreite}");
            sb.AppendLine($"Dachlänge = {dachlänge}");
            sb.AppendLine($"Dachlänge = {dachlänge}");
            sb.AppendLine($"==============Ausgabewerte==============");
            sb.AppendLine($"Dachziegel pro Reihe = {dachziegelProReihe}");
            sb.AppendLine($"Reihen = {reihen}");
            sb.AppendLine($"Dachfläche = {dachfläche}");
            Console.WriteLine(sb.ToString());
        }
    }
}