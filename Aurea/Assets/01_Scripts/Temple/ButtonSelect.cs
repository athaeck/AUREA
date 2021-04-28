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

    [SerializeField]
    private GameObject unlockHUD = null;

    public string selectedAurea = null;

    private GameObject parent = null;

    private bool team = true;

    private GameObject aurea;

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
                    g.transform.localPosition = new Vector3(0, 0.65f/parent.transform.parent.transform.localScale.y + g.GetComponent<Aurea>().GetAureaData().instantiateAtheight * 200, 0);
                    g.transform.localScale = g.transform.localScale * 2;
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
    
    public void Teleport()
    {
        IslandController.Instance.OpenSkyIsland();
    }

    public void Unlock()
    {
        data = Player.Instance;
        data.AddMoney(-50);
        PlayerAureaData aureaData = new PlayerAureaData();
        aureaData.aureaName = aurea.GetComponent<Aurea>().GetName();
        aureaData.aureaLevel = 1;
        data.AddAurea(aureaData);
        aurea.tag = "Aurea";
        Destroy(aurea.transform.parent.GetComponentInChildren<BoxCollider>().gameObject);
        unlockHUD.SetActive(false);
        GameObject boxcollider = new GameObject("BoxCollider");
        boxcollider.transform.parent = aurea.transform.parent.transform;
        boxcollider.transform.position = aurea.transform.parent.transform.position;
        boxcollider.AddComponent<BoxCollider>();
        boxcollider.layer = 2;
        boxcollider.tag = "Watch";
        boxcollider.GetComponent<BoxCollider>().isTrigger = true;
        boxcollider.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);
        selectAureaHUD.SetActive(true);
        select(aurea.GetComponent<Aurea>().GetName());
        TempleController.Instance.UnlockTrigger(false, null);
    }

    public void SelectedAurea(GameObject newaurea)
    {
        aurea = newaurea;
    }
}
