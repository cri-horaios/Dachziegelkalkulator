using System;

namespace DachflächenkalkulatorForms;

public class Ausgabeparameter
{
    public Ausgabeparameter(Eingabeparameter eingabeparameter, Rechenparameter rechenparameter)
    {
        ep = eingabeparameter; 
        rp = rechenparameter;
    }

    private readonly Eingabeparameter ep;
    private Rechenparameter rp;
    //Ergebnisparameter
    public double? dachfläche;
    public double? reihen;
    public double? dachziegelProReihe;
    public double? dachziegelGesamt;
    
    public void UpdateDachfläche()
    {
        
        if (rp.dachlänge == null || rp.sparrenlänge == null) return;
        dachfläche = 2 * ((rp.dachlänge/100 * rp.sparrenlänge/100));
        UpdateReihen();
        UpdateDachziegelProReihe();
    }

    public void UpdateDachziegelProReihe()
    {
        if (rp.dachlänge == null || ep.eindeckbreite == null) return;
        dachziegelProReihe = Math.Ceiling((double)(rp.dachlänge / ep.eindeckbreite));
        UpdateDachziegelGesamt();
    }

    public void UpdateReihen()
    {
        if (rp.sparrenlänge == null || ep.eindeckhöhe ==  null || ep.eindeckhöhe == 0) return;
        reihen = Math.Ceiling((double)(rp.sparrenlänge / ep.eindeckhöhe));
        UpdateDachziegelGesamt();
    }

    public void UpdateDachziegelGesamt()
    {
        if (reihen == null || dachziegelProReihe == null) return;
        dachziegelGesamt = 2 * reihen * dachziegelProReihe;
        
    }
}