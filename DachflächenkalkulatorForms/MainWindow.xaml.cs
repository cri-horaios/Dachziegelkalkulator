using System;
using System.Collections.Generic;
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
        public double hausbreite;
        public double hauslänge;
        public double überstandLänge;
        public double überstandBreite;
        public double dachhöhe;
        public double neigungswinkel;
        public double ziegelProReihe;
        public double reihen;
        public double eindeckbreite;
        public double eindeckhöhe;
        public double dachziegelProReihe;
        public double dachlänge;
        public double sparrenlänge;
        public double dachbreite;
        public double dachfläche;

        private void txt_Hauslänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Hauslänge.Text)) return;
            hauslänge = Convert.ToDouble(txt_Hauslänge.Text);
            updateDachlänge();
        }

        private void txt_Hausbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Hausbreite.Text)) return;
            hausbreite = Convert.ToDouble(txt_Hausbreite.Text);
            updateDachbreite();
        }

        private void txt_ÜberstandLänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ÜberstandLänge.Text)) return;
            überstandLänge = Convert.ToDouble(txt_ÜberstandLänge.Text);
            updateDachlänge();
        }

        private void txt_ÜberstandBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ÜberstandBreite.Text)) return;
            überstandBreite = Convert.ToDouble(txt_ÜberstandBreite.Text);
            updateDachbreite();
        }

        private void txt_Dachhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Dachhöhe.Text)) return;
                dachhöhe = Convert.ToDouble(txt_Dachhöhe.Text);
            txt_Neigungswinkel.Text = Math.Sin(dachhöhe / sparrenlänge).ToString();
            UpdateDachfläche();        
        }

        private void txt_Neigungswinkel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Neigungswinkel.Text)) return;
                neigungswinkel = Convert.ToDouble(txt_Neigungswinkel.Text);
            UpdateDachfläche();
        }
        
        private void txt_Eindeckbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Eindeckbreite.Text)) return;
            eindeckbreite = Convert.ToDouble(txt_Eindeckbreite.Text);
            updateDachziegelProReihe();
            //TODO: Updateaufrufe!
        }
        private void txt_Eindeckhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Eindeckhöhe.Text))
                eindeckhöhe = Convert.ToDouble(txt_Eindeckhöhe.Text);
            updateReihen();
            //TODO: Updateaufrufe!
        }

        private void txt_MengeProReihe_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (string.IsNullOrEmpty(txt_MengeProReihe.Text))
            ziegelProReihe = Convert.ToDouble(txt_MengeProReihe.Text);
            //txt_MengeProReihe.Text = ziegelProReihe.ToString();
            updateDachziegelGesamt();
        }
                
        private void txt_AnzahlReihen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_AnzahlReihen.Text)) return;
                reihen = Convert.ToDouble(txt_AnzahlReihen.Text);
            updateDachziegelGesamt();
        }

        private void txt_Dachfläche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Dachfläche.Text)) return;
                updateDachziegelGesamt();
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
       
        private void updateDachbreite()
        {
            if (hausbreite != 0 && überstandBreite != 0)
                dachbreite = hausbreite + überstandBreite;
            updateSparrenlänge();
        }

        private void updateDachlänge()
        {
            if (hauslänge == 0 || überstandLänge == 0)
                dachlänge = hauslänge + überstandLänge;
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
                        + Math.Pow(Convert.ToDouble(dachhöhe), 2));
            }
            else if (neigungswinkel != 0)
            {
                sparrenlänge = 
                    dachbreite / (2 * Math.Cos(neigungswinkel));
            }
            else sparrenlänge = 0;
            updateReihen();
        }

        private void updateReihen()
        {
            reihen = eindeckhöhe / sparrenlänge;
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
            txt_DachziegelGesamt.Text = (reihen * dachziegelProReihe).ToString();
        }
    }
}