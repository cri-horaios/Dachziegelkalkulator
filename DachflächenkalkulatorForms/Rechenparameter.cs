using System;
using System.Globalization;
using System.Net.Mime;
using System.Windows.Media;

namespace DachflächenkalkulatorForms;

public class Rechenparameter : MainWindow
{
    //Rechenparameter
    public double? dachlänge;
    public double? sparrenlänge;
    public double? dachbreite;

    public void updateDachlänge()
    {
        if (ep.hauslaenge == 0) return;
        rp.dachlänge = ep.hauslaenge + ep.überstandLänge ?? 0;
        ap.UpdateDachfläche();
    }
    
    public void updateDachbreite()
    {
        if (ep.hausbreite == null || ep.überstandBreite == null) return;
        rp.dachbreite = ep.hausbreite + ep.überstandBreite;
    }
    
    public void UpdateSparrenlänge()
    {
        if (rp.dachbreite == 0 || rp.dachbreite == null) return;
        
        if (ep.dachhöhe == null) undoReadOnly(txt_Neigungswinkel);
        else if (ep.neigungswinkel == null) undoReadOnly(txt_Dachhöhe);
        
        if (Nicht0undNull(ep.dachhöhe) && Nicht0undNull(rp.dachbreite))
        {
            rp.sparrenlänge = 
                Math.Sqrt(
                    Math.Pow((double) rp.dachbreite, 2) 
                    + Math.Pow((double) ep.dachhöhe, 2)
                );
            ep.neigungswinkel = Math.Round(Math.Asin((double) (ep.dachhöhe / rp.sparrenlänge)), 2);
            txt_Neigungswinkel.Text = ep.neigungswinkel.ToString();
            makeReadOnly(txt_Neigungswinkel);
        }
        else if (ep.neigungswinkel != null && ep.dachhöhe != null)
        {
            rp.sparrenlänge =
                rp.dachbreite / (2 * Math.Cos((double) ep.neigungswinkel));
            ep.dachhöhe = Math.Sin(ep.neigungswinkel * rp.sparrenlänge ?? 0);
            txt_Dachhöhe.Text = ep.dachhöhe.ToString();
            makeReadOnly(txt_Dachhöhe);
        }
        else rp.sparrenlänge = 0;
        ap.UpdateReihen();
    }

    private static bool Nicht0undNull(double? ep)
    {
        return ep != 0 && ep != null;
    }
}