using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewAurea : MonoBehaviour
{

    [SerializeField]
    private GameObject aureaName = null;

    [SerializeField]
    private GameObject skills = null;

    [SerializeField]
    private GameObject werte = null;
    
    public void HUDtext(Aurea aurea)
    {
        aureaName.GetComponent<Text>().text = aurea.GetName();
        skills.GetComponent<Text>().text = skillList(aurea);
        werte.GetComponent<Text>().text = werteList(aurea);


    }

    public string skillList(Aurea aurea)
    {
        string skillnames = "";
        
        for (int i = 0; i < aurea.GetSkills().Count; i++)
        {

            skillnames += aurea.GetSkills()[i].GetName() + "\n";
        }
        return skillnames;
    }

    public string werteList(Aurea aurea)
    {
        string wertetext = "";
        wertetext += "Level: " + aurea.GetLevel() + "\n";
        wertetext += "Lebenspunkte: " + aurea.GetLifePointsMax() + "\n";
        wertetext += "Magischer Schaden: " + aurea.GetMagicalDamage() + "\n";
        wertetext += "Magischer Schutz: " + aurea.GetMagicalDefence() + "\n";
        wertetext += "Schaden: " + aurea.GetPhysicalDamage() + "\n";
        wertetext += "Schutz: " + aurea.GetPhysicalDefence();
        return wertetext;
    }
}
