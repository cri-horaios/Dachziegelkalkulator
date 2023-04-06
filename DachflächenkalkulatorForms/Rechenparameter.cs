using System;
using System.Globalization;
using System.Net.Mime;
using System.Windows.Media;

namespace DachflächenkalkulatorForms;

public class Rechenparameter
{
    public Rechenparameter(Eingabeparameter ep)
    {
        this.ep = ep;
    }

    private Eingabeparameter ep;
    //Rechenparameter
    public double? dachlänge;
    public double? sparrenlänge;
    public double? dachbreite;

    public void UpdateDachlänge()
    {
        if (ep.hauslaenge == 0) return;
        dachlänge = ep.hauslaenge + ep.überstandLänge ?? 0;
    }
    
    public void UpdateDachbreite()
    {
        if (ep.hausbreite == null || ep.überstandBreite == null) return;
        dachbreite = ep.hausbreite + ep.überstandBreite;
        UpdateSparrenlänge();
    }
    
    public void UpdateSparrenlänge()
    {
        if (dachbreite == 0 || dachbreite == null) return;
        if (ep.dachhöhe != null && dachbreite != null)
        {
            sparrenlänge = 
                Math.Sqrt(
                    Math.Pow((double) dachbreite, 2) 
                    + Math.Pow((double) ep.dachhöhe, 2)
                );
            ep.neigungswinkel = Math.Round(Math.Asin((double) (ep.dachhöhe / sparrenlänge)), 2);
        }
        else if (ep.neigungswinkel != null && ep.dachhöhe != null)
        {
            sparrenlänge =
                dachbreite / (2 * Math.Cos((double) ep.neigungswinkel));
            ep.dachhöhe = Math.Sin(ep.neigungswinkel * sparrenlänge ?? 0);
        }
        else sparrenlänge = 0;
        
    }
}