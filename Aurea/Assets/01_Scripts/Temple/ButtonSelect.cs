using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{

    [SerializeField]
    private Camera cam = null;

    [SerializeField]
    private GameObject selectAureaHUD = null;

    [SerializeField]
    private GameObject viewAureaHUD = null;

    private string selectedAurea = null;

    private Vector3 oldpos = new Vector3(0, 0, 0);
    private Quaternion oldrot = new Quaternion();

    [SerializeField]
    private Vector3 newpos = new Vector3(0, 0, 0);

    private GameObject parent = null;

    private bool team = true;

    private PlayerData data = null;

    private GameObject[] all_Objs = null;

    private Vector3 position = Vector3.zero;



    public void select(string aureaName)
    {
        data = Player.Instance;
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
            select(selectedAurea);
        }
        else if(data.GetSquad().Count < 5)
        { 
            data.AddAureaToSquad(selectedAurea);
            select(selectedAurea);
        }
    }

    public void View()
    {
        all_Objs = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject g in all_Objs)
        {
            if (g.tag == "Player" || g.tag == "Podest")
            {
                g.SetActive(false);
            }
            if (g.tag == "Aurea" || g.tag == "Locked" )
            {
                selectAureaHUD.SetActive(false);
                if (g.GetComponent<Aurea>().GetName() != selectedAurea)
                {
                    g.SetActive(false);
                }
                else
                {
                    parent = g.transform.parent.gameObject;
                    g.transform.parent = g.transform.parent.transform.parent;
                    viewAureaHUD.GetComponent<ViewAurea>().HUDtext(g.GetComponent<Aurea>());
                    viewAureaHUD.SetActive(true);
                    viewAureaHUD.GetComponent<ViewAurea>().HUDtext(g.GetComponent<Aurea>());
                    viewAureaHUD.SetActive(true);
                    position = g.transform.position;
                    g.transform.localScale = g.transform.localScale * 2;
                    g.transform.position = new Vector3(0,1,0);
                    g.transform.LookAt(new Vector3(0, g.transform.position.y, -10));

                    cam.GetComponent<FollowTarget>().TakeTarget(g.transform);
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
                cam.GetComponent<FollowTarget>().TakeTarget(g.transform);
            }
            if (g.tag == "Podest")
            {
                g.SetActive(true);
            }
            if (g.tag == "Aurea" || g.tag == "Locked")
            {
                if (g.GetComponent<Aurea>().GetName() != selectedAurea)
                {
                    
                    g.SetActive(true);
                }
                else
                {
                    g.transform.parent = parent.transform;
                    viewAureaHUD.SetActive(false);
                    selectAureaHUD.SetActive(true);
                    g.transform.localScale = new Vector3(1, 1, 1);
                    g.transform.position = position;
                    g.transform.LookAt(new Vector3(0, g.transform.position.y, 0));
                }
            }
        }
    }
    
    public void teleport()
    {
        IslandController.Instance.OpenSkyIsland();
    }
}
