namespace DachflächenkalkulatorForms;

public class Ausgabeparameter : MainWindow
{
    //Ergebnisparameter
    public double? dachfläche;
    public double? reihen;
    public double? dachziegelProReihe;
    public double? dachziegelGesamt;
    
    public void UpdateDachfläche()
    {
        if (rp.dachlänge == 0 || rp.sparrenlänge == 0) return;
        ap.dachfläche = 2 * (rp.dachlänge * rp.sparrenlänge);
        txt_Dachfläche.Text = ap.dachfläche.ToString();
        ap.UpdateReihen();
        ap.UpdateDachziegelProReihe();
    }

    public void UpdateDachziegelProReihe()
    {
        ap.dachziegelProReihe = rp.dachlänge / ep.eindeckbreite;
        txt_MengeProReihe.Text = ap.dachziegelProReihe.ToString();
        UpdateDachziegelGesamt();
    }
    
    public void UpdateReihen()
    {
        if (ep.eindeckhöhe ==  null  || rp.sparrenlänge == null || rp.sparrenlänge == 0) return;
        ap.reihen = ep.eindeckhöhe / rp.sparrenlänge;
        txt_AnzahlReihen.Text = ap.reihen.ToString();
        UpdateDachziegelGesamt();
    }

    public void UpdateDachziegelGesamt()
    {
        if (ap.reihen == null || ap.dachziegelProReihe == null) return;
        ap.dachziegelGesamt = ap.reihen * ap.dachziegelProReihe;
        txt_DachziegelGesamt.Text = ap.dachziegelGesamt.ToString();
    }
}