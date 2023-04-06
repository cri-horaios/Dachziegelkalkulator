using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected static Eingabeparameter ep = new();
        protected static Rechenparameter rp = new Rechenparameter(ep);
        protected Ausgabeparameter ap = new Ausgabeparameter(ep, rp);

        

        ///////////////////////////////////////
        //          Rechenparameter          //
        ///////////////////////////////////////
        private void txt_Hauslänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.hauslaenge = checkNullAndConvertToDouble(txt_Hauslänge) * 1000;
            rp.UpdateDachlänge();
            ap.UpdateDachfläche();
            updateAusgabeparameterUI();;
        }

        private void txt_Hausbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.hausbreite = checkNullAndConvertToDouble(txt_Hausbreite) * 1000;
            rp.UpdateDachbreite();
            updateAllForms();
        }


        private void txt_ÜberstandLänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.überstandLänge = checkNullAndConvertToDouble(txt_ÜberstandLänge);
            rp.UpdateDachlänge();
            ap.UpdateDachfläche();
            updateAusgabeparameterUI();
        }

        private void txt_ÜberstandBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.überstandBreite = checkNullAndConvertToDouble(txt_ÜberstandBreite);
            rp.UpdateDachbreite();
            updateAllForms();
        }

        private void txt_Dachhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.dachhöhe = checkNullAndConvertToDouble(txt_Dachhöhe);
            if (ep.dachhöhe == null) undoReadOnly(txt_Neigungswinkel);
            else makeReadOnly(txt_Neigungswinkel);
            rp.UpdateSparrenlänge();
            ap.UpdateReihen();
            updateAllForms();
        }

        private void txt_Neigungswinkel_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.neigungswinkel = checkNullAndConvertToDouble(txt_Neigungswinkel);
            if (ep.neigungswinkel == null) undoReadOnly(txt_Dachhöhe);
            else makeReadOnly(txt_Dachhöhe);
            rp.UpdateSparrenlänge();
            ap.UpdateReihen();
            updateAllForms();
        }
        
        private void txt_Eindeckbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.eindeckbreite = checkNullAndConvertToDouble(txt_Eindeckbreite);
            ap.UpdateDachziegelProReihe();
            updateAusgabeparameterUI();
        }

        private void txt_Eindeckhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ep.eindeckhöhe = checkNullAndConvertToDouble(txt_Eindeckhöhe);
            ap.UpdateReihen();
            updateAusgabeparameterUI();
        }
        
        ///////////////////////////////////////
        //          Ausgabefelder            //
        ///////////////////////////////////////
        

        private void txt_MengeProReihe_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.dachziegelProReihe = checkNullAndConvertToDouble(txt_MengeProReihe);
            updateAusgabeparameterUI();
        }

        private void txt_AnzahlReihen_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.reihen = checkNullAndConvertToDouble(txt_AnzahlReihen);
            updateAusgabeparameterUI();
        }

        private void txt_Dachfläche_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.dachfläche = checkNullAndConvertToDouble(txt_Dachfläche);
            updateAusgabeparameterUI();
        }

        private void txt_DachziegelGesamt_TextChanged(object sender, TextChangedEventArgs e)
        {
            ap.dachziegelGesamt = checkNullAndConvertToDouble(txt_DachziegelGesamt);
            updateAusgabeparameterUI();
        }

        ///////////////////////////////////////
        //         Allg. Helferlein          //
        ///////////////////////////////////////
        private double? checkNullAndConvertToDouble(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return null;
            
            return Convert.ToDouble(textBox.Text);
        }

        private void makeReadOnly(TextBox textbox)
        {
            textbox.IsReadOnly = true;
            textbox.Background = Brushes.LightGray;
        }

        private void undoReadOnly(TextBox textbox)
        {
            textbox.IsReadOnly = false;
            textbox.Background = Brushes.White;
        }
        
        private void updateEingabeparameterUI()
        {
            txt_Dachhöhe.Text = ep.dachhöhe.ToString();
            txt_Eindeckbreite.Text = ep.eindeckbreite.ToString();
            txt_Eindeckhöhe.Text = ep.eindeckhöhe.ToString();
            txt_Hausbreite.Text = ep.hausbreite.ToString();
            txt_Neigungswinkel.Text = ep.neigungswinkel.ToString();
            txt_Hauslänge.Text = ep.hauslaenge.ToString();
            txt_ÜberstandBreite.Text = ep.überstandBreite.ToString();
            txt_ÜberstandLänge.Text = ep.überstandLänge.ToString();
        }
        
        private void updateAusgabeparameterUI()
        {
            txt_Dachfläche.Text = Math.Ceiling(ap.dachfläche ?? 0).ToString(CultureInfo.CurrentCulture);
            txt_AnzahlReihen.Text = Math.Ceiling(ap.reihen ?? 0).ToString(CultureInfo.CurrentCulture);
            txt_MengeProReihe.Text = Math.Ceiling(ap.dachziegelProReihe ?? 0).ToString(CultureInfo.CurrentCulture);
            txt_DachziegelGesamt.Text = Math.Ceiling(ap.dachziegelGesamt ?? 0).ToString(CultureInfo.CurrentCulture);
        }
        
        private void updateAllForms()
        {
            updateEingabeparameterUI();
            updateAusgabeparameterUI();
        }
    }
}