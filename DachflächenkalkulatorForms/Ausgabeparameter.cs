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
        
        if (rp.dachlänge == 0 || rp.sparrenlänge == 0) return;
        dachfläche = 2 * (rp.dachlänge * rp.sparrenlänge);
        UpdateReihen();
        UpdateDachziegelProReihe();
    }

    public void UpdateDachziegelProReihe()
    {
        dachziegelProReihe = rp.dachlänge / ep.eindeckbreite;
        UpdateDachziegelGesamt();
    }
    
    public void UpdateReihen()
    {
        if (ep.eindeckhöhe ==  null  || rp.sparrenlänge == null || rp.sparrenlänge == 0) return;
        reihen = ep.eindeckhöhe / rp.sparrenlänge;
        UpdateDachziegelGesamt();
    }

    public void UpdateDachziegelGesamt()
    {
        if (reihen == null || dachziegelProReihe == null) return;
        dachziegelGesamt = reihen * dachziegelProReihe;
        
    }
}