using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleController : MonoBehaviour
{
    [SerializeField]
    private GameObject slots = null;

    [SerializeField]
    private TemplePlayerData selectData = null;

    private PlayerData data = null;

    [SerializeField]
    private TempleSpiral spiral = null;

    private Vector3[] spawnPoint = null;

    private int numberAurea;

    [SerializeField]
    public AureaList aureaData = null;

    void Start()
    {
        data = selectData.getPlayerData();
        CreateSpiral();
    }

    public AureaData GetAureaData(string name)
    {
        foreach (AureaData data in aureaData.aureas)
        {
            if (data.NAME == name)
                return data;
        }

        Debug.Log("Didnt found: " + name);
        return null;
    }

    public void CreateSpiral()
    {
        List<PlayerAureaData> CapturedAurea = data.GetAurea();
        numberAurea = aureaData.aureas.Count;


        spawnPoint = spiral.Setpoints(numberAurea);
        for (int i = 0; i < numberAurea; i++)
        {
            for (int j = 0; j < CapturedAurea.Count; j++)
            {
                if (aureaData.aureas[i].NAME == CapturedAurea[j].aureaName)
                {
                    int aureaLevel = data.GetAureaLevel(CapturedAurea[j].aureaName);
                    GameObject aureaPrefab = GetAureaData(CapturedAurea[j].aureaName).levels[aureaLevel - 1].prefab;
                    Aurea aurea = Instantiate(aureaPrefab, spawnPoint[i], aureaPrefab.transform.rotation, slots.transform).GetComponent<Aurea>();
                    aurea.transform.LookAt(new Vector3(0, aurea.transform.position.y, 0));
                    Rigidbody rigid = aurea.gameObject.AddComponent<Rigidbody>();
                    rigid.useGravity = false;
                    SphereCollider collider = aurea.gameObject.AddComponent<SphereCollider>();
                    collider.center = Vector3.zero; // the center must be in local coordinates
                    collider.isTrigger = true;
                    collider.radius = 5.0f;
                    break;
                }
                if (aureaData.aureas[i].NAME != CapturedAurea[j].aureaName && j == CapturedAurea.Count - 1)
                {
                    GameObject aureaPrefab = aureaData.aureas[i].levels[0].prefab;
                    aureaPrefab.tag = "Locked";
                    Aurea aurea = Instantiate(aureaPrefab, spawnPoint[i], aureaPrefab.transform.rotation, slots.transform).GetComponent<Aurea>();
                    aurea.transform.LookAt(new Vector3(0, aurea.transform.position.y, 0));
                }
            }

        }
    }

    public void ResetIsland()
    {
        Debug.Log("Reset Temple");
    }


}
