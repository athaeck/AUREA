using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewAurea : MonoBehaviour
{

    [SerializeField]
    private GameObject aureaName = null;

    [SerializeField]
    private GameObject[] skills = null;

    [SerializeField]
    private GameObject werte = null;
    
    public void HUDtext(Aurea aurea)
    {
        aureaName.GetComponent<Text>().text = aurea.GetName();
        
        for (int i = 0; i < aurea.GetSkills().Count; i++)
        {
            skills[i].GetComponent<Text>().text = skillList(aurea, i);
        }
        werte.GetComponent<Text>().text = werteList(aurea);


    }

    public string skillList(Aurea aurea, int count)
    {
        string skillnames = "";
            skillnames += aurea.GetSkills()[count].GetName();
        return skillnames;
    }

    public string werteList(Aurea aurea)
    {
        string wertetext = "";
        wertetext += "Lebenspunkte: " + aurea.GetLifePointsMax() + "\n";
        wertetext += "Magischer Schaden: " + aurea.GetMagicalDamage() + "\n";
        wertetext += "Magischer Schutz: " + aurea.GetMagicalDefence() + "\n";
        wertetext += "Schaden: " + aurea.GetPhysicalDamage() + "\n";
        wertetext += "Schutz: " + aurea.GetPhysicalDefence();
        return wertetext;
    }
}
