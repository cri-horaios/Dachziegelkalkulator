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

        protected Rechenparameter rp = new();
        protected Ausgabeparameter ap = new();
        protected Eingabeparameter ep = new();
        
        ///////////////////////////////////////
        //          Rechenparameter          //
        ///////////////////////////////////////
        private void txt_Hauslänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.hauslaenge = checkNullAndConvertToDouble(txt_Hauslänge) * 1000;
            rp.UpdateDachlänge();
        }

        private void txt_Hausbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.hausbreite = checkNullAndConvertToDouble(txt_Hausbreite) * 1000;
            rp.UpdateDachbreite();
        }


        private void txt_ÜberstandLänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.überstandLänge = checkNullAndConvertToDouble(txt_ÜberstandLänge);
            rp.UpdateDachlänge();
        }

        private void txt_ÜberstandBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.überstandBreite = checkNullAndConvertToDouble(txt_ÜberstandBreite);
            rp.UpdateDachbreite();
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

        ///////////////////////////////////////
        //         Allg. Helferlein          //
        ///////////////////////////////////////
        private double? checkNullAndConvertToDouble(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return null;
            
            return Convert.ToDouble(textBox.Text);
        }

        protected void makeReadOnly(TextBox textbox)
        {
            textbox.IsReadOnly = true;
            textbox.Background = Brushes.LightGray;
        }

        protected void undoReadOnly(TextBox textbox)
        {
            textbox.IsReadOnly = false;
            textbox.Background = Brushes.White;
        }
    }
}