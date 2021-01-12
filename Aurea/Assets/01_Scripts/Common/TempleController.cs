using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TempleController : MonoBehaviour
{
    [SerializeField]
    private GameObject slots = null;

    [SerializeField]
    private Rigidbody player = null;


    private PlayerData data = null;

    [SerializeField]
    private TempleSpiral spiral = null;

    private Vector3[] spawnPoint = null;

    private int numberAurea;

    [SerializeField]
    public AureaList aureaData = null;

   [SerializeField]
    private GameObject selectAureaHUD = null;

    [SerializeField]
    private GameObject viewAureaHUD = null;

    [SerializeField]
    private ButtonSelect buttonSelect = null;

    private bool trigger = false;

    void Start()
    {

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
                    Debug.Log("spawn Aurea");
                    int aureaLevel = data.GetAureaLevel(CapturedAurea[j].aureaName);
                    GameObject aureaPrefab = GetAureaData(CapturedAurea[j].aureaName).levels[aureaLevel - 1].prefab;
                    Aurea aurea = Instantiate(aureaPrefab, spawnPoint[i], aureaPrefab.transform.rotation, slots.transform).GetComponent<Aurea>();
                    aurea.transform.LookAt(new Vector3(0, aurea.transform.position.y, 0));
                    SphereCollider collider = aurea.gameObject.AddComponent<SphereCollider>();
                    collider.center = new Vector3(0, -10, 0);
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

    public void TakeInput(Ray ray) {

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
                
            Aurea hero = null;
            if (trigger)
            {
                if (hit.collider.CompareTag("Aurea") && !viewAureaHUD.activeSelf)
                {
                    hero = hit.collider.GetComponent<Aurea>();
                    buttonSelect.select(hero.GetName());
                    selectAureaHUD.GetComponent<FollowTarget>().TakeTarget(hero.transform);

                    selectAureaHUD.SetActive(true);
                }
                else
                {
                    selectAureaHUD.SetActive(false);
                }
            }
            if (hit.collider.CompareTag("Walkable"))
            {
                player.MovePosition(hit.point);
            }
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aurea"))
        {
            trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Aurea"))
        {
            trigger = false;
            selectAureaHUD.SetActive(false);
        }
    }



    public void ResetIsland()
    {
        Aurea[] all_aurea = null;
        all_aurea = GameObject.FindObjectsOfType<Aurea>();

        foreach (Aurea aurea in all_aurea)
        {
                DestroyImmediate(aurea.gameObject);
        }

        data = Player.Instance;
        CreateSpiral();

        Debug.Log("Reset Temple");
    }


}
