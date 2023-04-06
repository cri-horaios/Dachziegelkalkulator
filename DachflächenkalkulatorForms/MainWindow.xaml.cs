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

        public Rechenparameter rp = new Rechenparameter();
        public Ausgabeparameter ap = new Ausgabeparameter();
        public Eingabeparameter ep = new Eingabeparameter();
        
        ///////////////////////////////////////
        //          Rechenparameter          //
        ///////////////////////////////////////
        private void txt_Hauslänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.hauslaenge = checkNullAndConvertToDouble(txt_Hauslänge) * 1000;
            rp.updateDachlänge();
        }

        private void txt_Hausbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.hausbreite = checkNullAndConvertToDouble(txt_Hausbreite) * 1000;
            rp.updateDachbreite();
        }


        private void txt_ÜberstandLänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.überstandLänge = checkNullAndConvertToDouble(txt_ÜberstandLänge);
            rp.updateDachlänge();
        }

        private void txt_ÜberstandBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.überstandBreite = checkNullAndConvertToDouble(txt_ÜberstandBreite);
            rp.updateDachbreite();
        }

        private void txt_Dachhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.dachhöhe = checkNullAndConvertToDouble(txt_Dachhöhe);
            rp.UpdateSparrenlänge();
        }

        private void txt_Neigungswinkel_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.neigungswinkel = checkNullAndConvertToDouble(txt_Neigungswinkel);
            rp.UpdateSparrenlänge();
        }
        
        ///////////////////////////////////////
        //          Ausgabefelder            //
        ///////////////////////////////////////
        private void txt_Eindeckbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.eindeckbreite = checkNullAndConvertToDouble(txt_Eindeckbreite);
            ap.UpdateDachziegelProReihe();
            //TODO: Updateaufrufe!
        }

        private void txt_Eindeckhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.eindeckhöhe = checkNullAndConvertToDouble(txt_Eindeckhöhe);
            ap.UpdateReihen();
            //TODO: Updateaufrufe!
        }

        private void txt_MengeProReihe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.dachziegelProReihe = checkNullAndConvertToDouble(txt_MengeProReihe);
            ap.UpdateDachziegelGesamt();
        }

        private void txt_AnzahlReihen_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.reihen = checkNullAndConvertToDouble(txt_AnzahlReihen);
            ap.UpdateDachziegelGesamt();
        }

        private void txt_Dachfläche_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.dachfläche = checkNullAndConvertToDouble(txt_Dachfläche);
            ap.UpdateDachziegelGesamt();
        }

        private void txt_DachziegelGesamt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.dachziegelGesamt = checkNullAndConvertToDouble(txt_DachziegelGesamt);
        }

        // Check Null and return Double
        private double? checkNullAndConvertToDouble(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return null;
            
            return Convert.ToDouble(textBox.Text);
        }

        public void makeReadOnly(TextBox textbox)
        {
            textbox.IsReadOnly = true;
            textbox.Background = Brushes.LightGray;
        }

        public void undoReadOnly(TextBox textbox)
        {
            textbox.IsReadOnly = false;
            textbox.Background = Brushes.White;
        }

        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        ////////////////Update-Parameter//////////////////
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////
        //////////////////////////////////////////////////

        //////////////////////////////////////////////////
        ////////////////Rechenparameter///////////////////
        //////////////////////////////////////////////////

        // private void updateDachbreite(ep.hausbreite)
        // {
        //     if (hausbreite == 0) return;
        //     dachbreite = hausbreite + überstandBreite ?? 0;
        //     updateSparrenlänge();
        // }

        // private void updateDachlänge()
        // {
        //     if (hauslaenge == 0) return;
        //     dachlänge = hauslaenge + überstandLänge ?? 0;
        //     UpdateDachfläche();
        // }

        // public double updateSparrenlänge(Eingabeparameter ep, Rechenparameter rp, Ausgabeparameter ap)
        // {
        //     if (rp.dachbreite == 0) return 0;
        //     if (ep.dachhöhe != 0)
        //     {
        //         sparrenlänge = 
        //             Math.Sqrt(
        //                 Math.Pow(rp.dachbreite, 2) 
        //                 + Math.Pow(ep.dachhöhe, 2)
        //             );
        //         txt_Neigungswinkel.Text = Math.Round(Math.Asin(ep.dachhöhe / sparrenlänge), 2).ToString();
        //     }
        //     else if (ep.neigungswinkel != 0)
        //     {
        //         sparrenlänge =
        //             rp.dachbreite / (2 * Math.Cos(ep.neigungswinkel));
        //         txt_Dachhöhe.Text = Math.Sin(ep.neigungswinkel * sparrenlänge).ToString();
        //     }
        //     else sparrenlänge = 0;
        //     updateReihen();
        //     return sparrenlänge;
        // }

        ////////////////////////////////////////////////////
        ////////////////Ausgabeparameter///////////////////
        //////////////////////////////////////////////////
        // public void updateReihen()
        // {
        //     if (eindeckhöhe ==  0  || sparrenlänge == 0) return;
        //     reihen = eindeckhöhe / sparrenlänge;
        //     txt_AnzahlReihen.Text = reihen.ToString();
        //     updateDachziegelGesamt();
        // }

        // private void updateDachziegelProReihe()
        // {
        //     dachziegelProReihe = dachlänge / eindeckbreite;
        //     txt_MengeProReihe.Text = dachziegelProReihe.ToString();
        //     updateDachziegelGesamt();
        // }

        // private void UpdateDachfläche()
        // {
        //     if (dachlänge == 0 || sparrenlänge == 0) return;
        //     dachfläche = 2 * (dachlänge * sparrenlänge);
        //     txt_Dachfläche.Text = dachfläche.ToString();
        //     updateReihen();
        //     updateDachziegelProReihe();
        // }

        // private void updateDachziegelGesamt()
        // {
        //     if (reihen == 0 || dachziegelProReihe == 0) return;
        //     var dachziegelGesamt = reihen * dachziegelProReihe;
        //     txt_DachziegelGesamt.Text = dachziegelGesamt.ToString();
        // }

    }
}