using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DachflächenkalkulatorForms
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static Eingabeparameter ep = new();
        private static Rechenparameter rp = new(ep);
        private Ausgabeparameter ap = new(ep, rp);
        
        ///////////////////////////////////////
        //          Rechenparameter          //
        ///////////////////////////////////////
        private void txt_Hauslänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ep.hauslaenge.Equals(checkNullAndConvertToDouble(txt_Hauslänge) * 100)) return;
            ep.hauslaenge = checkNullAndConvertToDouble(txt_Hauslänge) * 100;
            rp.UpdateDachlänge();
            ap.UpdateDachfläche();
            updateAusgabeparameterUI();
        }

        private void txt_Hausbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ep.hausbreite.Equals(checkNullAndConvertToDouble(txt_Hausbreite) * 100)) return;
            ep.hausbreite = checkNullAndConvertToDouble(txt_Hausbreite) * 100;
            rp.UpdateDachbreite();
            rp.UpdateSparrenlänge(txt_Dachhöhe.IsReadOnly);
            ap.UpdateDachfläche();
            updateAllForms();
        }


        private void txt_ÜberstandLänge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ep.überstandLänge.Equals(checkNullAndConvertToDouble(txt_ÜberstandLänge))) return;
            ep.überstandLänge = checkNullAndConvertToDouble(txt_ÜberstandLänge);
            rp.UpdateDachlänge();
            ap.UpdateDachfläche();
            updateAusgabeparameterUI();
        }

        private void txt_ÜberstandBreite_TextChanged(object sender, TextChangedEventArgs e)
        {            
            if(ep.überstandBreite.Equals(checkNullAndConvertToDouble(txt_ÜberstandBreite))) return;
            ep.überstandBreite = checkNullAndConvertToDouble(txt_ÜberstandBreite);
            rp.UpdateDachbreite();
            rp.UpdateSparrenlänge(txt_Dachhöhe.IsReadOnly);
            ap.UpdateDachfläche();
            updateAllForms();
        }

        private void txt_Dachhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ep.dachhöhe.Equals(checkNullAndConvertToDouble(txt_Dachhöhe)))return;
            ep.dachhöhe = checkNullAndConvertToDouble(txt_Dachhöhe);
            if (txt_Dachhöhe.IsReadOnly) return;
            if (ep.dachhöhe == null) undoReadOnly(txt_Neigungswinkel);
            else makeReadOnly(txt_Neigungswinkel);
            rp.UpdateSparrenlänge(txt_Dachhöhe.IsReadOnly);
            ap.UpdateReihen();
            updateAllForms();
        }

        private void txt_Neigungswinkel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ep.neigungswinkel.Equals(checkNullAndConvertToDouble(txt_Neigungswinkel))) return;
            ep.neigungswinkel = checkNullAndConvertToDouble(txt_Neigungswinkel);
            if (txt_Neigungswinkel.IsReadOnly) return;
            if (ep.neigungswinkel == null) undoReadOnly(txt_Dachhöhe);
            else makeReadOnly(txt_Dachhöhe);
            rp.UpdateSparrenlänge(txt_Dachhöhe.IsReadOnly);
            ap.UpdateReihen();
            updateAllForms();
        }
        
        private void txt_Eindeckbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ep.eindeckbreite.Equals(checkNullAndConvertToDouble(txt_Eindeckbreite))) return;
            ep.eindeckbreite = checkNullAndConvertToDouble(txt_Eindeckbreite);
            ap.UpdateDachziegelProReihe();
            updateAusgabeparameterUI();
        }

        private void txt_Eindeckhöhe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ep.eindeckhöhe.Equals(checkNullAndConvertToDouble(txt_Eindeckhöhe)))return;
            ep.eindeckhöhe = checkNullAndConvertToDouble(txt_Eindeckhöhe);
            ap.UpdateReihen();
            updateAusgabeparameterUI();
        }
        
        ///////////////////////////////////////
        //          Ausgabefelder            //
        ///////////////////////////////////////
        

        private void txt_MengeProReihe_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ap.dachziegelProReihe.Equals(checkNullAndConvertToDouble(txt_MengeProReihe))) return;
            ap.dachziegelProReihe = checkNullAndConvertToDouble(txt_MengeProReihe);
            updateAusgabeparameterUI();
        }

        private void txt_AnzahlReihen_TextChanged(object sender, TextChangedEventArgs e)
        { 
            if(ap.reihen.Equals(checkNullAndConvertToDouble(txt_AnzahlReihen)))return;
            ap.reihen = checkNullAndConvertToDouble(txt_AnzahlReihen);
            updateAusgabeparameterUI();
        }

        private void txt_Dachfläche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ap.dachfläche.Equals(checkNullAndConvertToDouble(txt_Dachfläche))) return;
            ap.dachfläche = checkNullAndConvertToDouble(txt_Dachfläche);
            updateAusgabeparameterUI();
        }

        private void txt_DachziegelGesamt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(ap.dachziegelGesamt.Equals(checkNullAndConvertToDouble(txt_DachziegelGesamt)))return;
            ap.dachziegelGesamt = checkNullAndConvertToDouble(txt_DachziegelGesamt);
            updateAusgabeparameterUI();
        }

        ///////////////////////////////////////
        //         Allg. Helferlein          //
        ///////////////////////////////////////
        private double? checkNullAndConvertToDouble(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text)) return null;
            double rückgabe;
            if (double.TryParse(textBox.Text, out rückgabe)) return rückgabe;
            return null;
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
            txt_Hauslänge.Text = (ep.hauslaenge / 100).ToString();
            txt_Hausbreite.Text = (ep.hausbreite / 100).ToString();
            txt_ÜberstandLänge.Text = ep.überstandLänge.ToString();
            txt_ÜberstandBreite.Text = ep.überstandBreite.ToString();
            txt_Dachhöhe.Text = ep.dachhöhe.ToString();
            txt_Neigungswinkel.Text = ep.neigungswinkel.ToString();
            txt_Eindeckbreite.Text = ep.eindeckbreite.ToString();
            txt_Eindeckhöhe.Text = ep.eindeckhöhe.ToString();
        }
        
        private void updateAusgabeparameterUI()
        {
            txt_MengeProReihe.Text = (ap.dachziegelProReihe ?? 0).ToString(CultureInfo.CurrentCulture);
            txt_AnzahlReihen.Text = (ap.reihen ?? 0).ToString(CultureInfo.CurrentCulture);
            txt_Dachfläche.Text = Math.Ceiling(ap.dachfläche ?? 0).ToString(CultureInfo.CurrentCulture);
            txt_DachziegelGesamt.Text = Math.Ceiling(ap.dachziegelGesamt ?? 0).ToString(CultureInfo.CurrentCulture);
        }
        
        private void updateAllForms()
        {
            updateEingabeparameterUI();
            updateAusgabeparameterUI();
        }
    }
}