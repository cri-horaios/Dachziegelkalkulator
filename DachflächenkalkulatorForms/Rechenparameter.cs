using System;
using System.Globalization;
using System.Net.Mime;
using System.Windows.Controls;
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
        
    }
    
    public void UpdateSparrenlänge(bool dachhöheReadOnly)
    {
        if (dachbreite == 0 || dachbreite == null) return;
        if (ep.dachhöhe != null && !dachhöheReadOnly)
        {
            sparrenlänge = 
                Math.Sqrt(
                    Math.Pow((double) dachbreite / 2 , 2) 
                    + Math.Pow((double) ep.dachhöhe, 2)
                );
            ep.neigungswinkel = Math.Round(RadiansToDegrees(Math.Atan((double)(ep.dachhöhe/(dachbreite/2)))), 2);
        }
        else if (ep.neigungswinkel != null && dachhöheReadOnly)
        {
            sparrenlänge = (dachbreite/2) / Math.Cos((double)((Math.PI / 180) * ep.neigungswinkel));
            ep.dachhöhe = Math.Round((double)(dachbreite/2 * Math.Tan((double)(Math.PI / 180 * ep.neigungswinkel))),2);
        }
    }

    double DegreesToRadians(double? degrees)
    {
        return (degrees * (Math.PI / 180.0)) ?? 0;
    }
    double RadiansToDegrees(double? radian)
    {
        return (radian * (180.0 / Math.PI)) ?? 0 ;
    }
}