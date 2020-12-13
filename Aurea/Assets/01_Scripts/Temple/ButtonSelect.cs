using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    [SerializeField]
    private TemplePlayerData selectData = null;

    [SerializeField]
    private FollowTarget cam = null;

    [SerializeField]
    private GameObject selectAureaHUD = null;

    [SerializeField]
    private GameObject viewAureaHUD = null;

    private string selectedAurea = null;

    private bool team = true;

    private PlayerData data = null;

    private GameObject[] all_Objs = null;

    private Vector3 position = Vector3.zero;



    public void select(string aureaName)
    {
        data = selectData.getPlayerData();
        selectedAurea = aureaName;
        List<string> squad = data.GetSquad();
        if (squad.Count == 0)
        {
            selectAureaHUD.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Aufnehmen";
            team = false;
        }
        else
        {
            for (int j = 0; j < squad.Count; j++)
            {
                if (aureaName == squad[j])
                {
                    selectAureaHUD.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Rauswerfen";
                    team = true;
                    break;
                }
                else
                {
                    selectAureaHUD.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Aufnehmen";
                    team = false;
                }
            }
        }
    }

    public void squad()
    {
        if (team)
        {
            data.RemoveAureaToSquad(selectedAurea);
            selectData.setPlayerData(data);
            select(selectedAurea);
        }
        else
        {
            
            data.AddAureaToSquad(selectedAurea);
            selectData.setPlayerData(data);
            select(selectedAurea);
        }
    }

    public void View()
    {
        all_Objs = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject g in all_Objs)
        {
            if (g.tag == "Player")
            {
                g.SetActive(false);
            }
            if (g.tag == "Aurea" || g.tag == "Locked")
            {
                selectAureaHUD.SetActive(false);
                if (g.GetComponent<Aurea>().GetName() != selectedAurea)
                {
                    g.SetActive(false);
                }
                else
                {
                    viewAureaHUD.GetComponent<ViewAurea>().HUDtext(g.GetComponent<Aurea>());
                    viewAureaHUD.SetActive(true);
                    cam.TakeTarget(g.transform);
                    cam.NewOffset(new Vector3(0, 5, -24));
                    viewAureaHUD.GetComponent<ViewAurea>().HUDtext(g.GetComponent<Aurea>());
                    viewAureaHUD.SetActive(true);
                    position = g.transform.position;
                    g.transform.localScale = new Vector3(2, 2, 2);
                    g.transform.position = Vector3.zero;
                    g.transform.LookAt(new Vector3(0, 0, -10));
                }
            }
        }
    }

    public void exitView()
    {
        viewAureaHUD.SetActive(false);
        foreach (GameObject g in all_Objs)
        {
            if (g.tag == "Player")
            {
                g.SetActive(true);
                cam.TakeTarget(g.transform);
                cam.NewOffset(new Vector3(0, 5, -15));
            }
            if (g.tag == "Aurea" || g.tag == "Locked")
            {
                selectAureaHUD.SetActive(false);
                if (g.GetComponent<Aurea>().GetName() != selectedAurea)
                {
                    
                    g.SetActive(true);
                }
                else
                {
                    viewAureaHUD.SetActive(false);
                    g.transform.localScale = new Vector3(1, 1, 1);
                    g.transform.position = position;
                    g.transform.LookAt(Vector3.zero);
                }
            }
        }
    }
}
