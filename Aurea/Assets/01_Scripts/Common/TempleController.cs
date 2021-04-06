using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class TempleController : MonoBehaviour
{
    #region Singleton
    private static TempleController _instance;
    public static TempleController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TempleController>();
                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    container.AddComponent<TempleController>();
                }
            }
            return _instance;
        }
    }
    #endregion

    [SerializeField]
    private GameObject slots = null;

    [SerializeField]
    private GameObject islands = null;

    [SerializeField]
    private GameObject player = null;


    private PlayerData data = null;

    [SerializeField]
    private GameObject layout = null;

    [SerializeField]
    private int layoutstyle = 0;

    private Vector3[] spawnPoint = null;

    [SerializeField]
    private GameObject slotPrefab = null;

    private int numberAurea;

    [SerializeField]
    public AureaList aureaData = null;

   [SerializeField]

    private GameObject selectAureaHUD = null;

    [SerializeField]

    private GameObject unlockHUD = null;

    [SerializeField]

    private GameObject viewAureaHUD = null;

    [SerializeField]

    private GameObject teleportHUD = null;



    [SerializeField]

    private ButtonSelect buttonSelect = null;



    private bool watchTrigger;

    private bool unlockTrigger;

    private GameObject hitpodest;

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

        switch (layoutstyle)
        {
            case 0:
                spawnPoint = layout.GetComponent<TempleSpiral>().Setpoints(numberAurea);
                break;
            case 1:
                spawnPoint = layout.GetComponent<TempleCircle>().Setpoints(numberAurea);
                break;
            default:
                spawnPoint = layout.GetComponent<TempleSpiral>().Setpoints(numberAurea);
                break;
        }
        for (int i = 0; i < numberAurea; i++)
        {
            for (int j = 0; j < CapturedAurea.Count; j++)
            {
                if (aureaData.aureas[i].NAME == CapturedAurea[j].aureaName)
                {

                    int aureaLevel = data.GetAureaLevel(CapturedAurea[j].aureaName);
                    GameObject aureaPrefab = GetAureaData(CapturedAurea[j].aureaName).levels[aureaLevel - 1].prefab;
                    GameObject podest = Instantiate(slotPrefab, spawnPoint[i], slotPrefab.transform.rotation, slots.transform);
                    podest.transform.localScale = new Vector3(podest.transform.localScale.x / islands.transform.localScale.x, podest.transform.localScale.y / islands.transform.localScale.y, podest.transform.localScale.z / islands.transform.localScale.z);
                    Aurea aurea = Instantiate(aureaPrefab, podest.transform.position + new Vector3(0, (podest.GetComponent<MeshRenderer>().bounds.size.y + aureaData.aureas[i].instantiateAtheight), 0), aureaPrefab.transform.rotation, podest.transform).GetComponent<Aurea>();
                    GameObject boxcollider = new GameObject("BoxCollider");
                    boxcollider.transform.parent = podest.transform;
                    boxcollider.transform.position = spawnPoint[i];
                    boxcollider.AddComponent<BoxCollider>();
                    boxcollider.layer = 2;
                    boxcollider.tag = "Watch";
                    boxcollider.GetComponent<BoxCollider>().isTrigger = true;
                    boxcollider.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);
                    boxcollider.transform.localScale = new Vector3(1, 1, 1);
                    DestroyImmediate(aurea.GetComponent<BoxCollider>());
                    aurea.transform.LookAt(new Vector3(0, aurea.transform.position.y, 0));


                    break;
                }
                if (aureaData.aureas[i].NAME != CapturedAurea[j].aureaName && j == CapturedAurea.Count - 1)
                {
                    GameObject aureaPrefab = aureaData.aureas[i].levels[0].prefab;
                    GameObject podest = Instantiate(slotPrefab, spawnPoint[i], slotPrefab.transform.rotation, slots.transform);
                    podest.transform.localScale = new Vector3(podest.transform.localScale.x / islands.transform.localScale.x, podest.transform.localScale.y / islands.transform.localScale.y, podest.transform.localScale.z / islands.transform.localScale.z);
                    Aurea aurea = Instantiate(aureaPrefab, podest.transform.position + new Vector3(0,(podest.GetComponent<MeshRenderer>().bounds.size.y + aureaData.aureas[i].instantiateAtheight), 0), aureaPrefab.transform.rotation, podest.transform).GetComponent<Aurea>();
                    aurea.tag = "Locked";
                    GameObject boxcollider = new GameObject("BoxCollider");
                    boxcollider.transform.parent = podest.transform;
                    boxcollider.transform.position = spawnPoint[i];
                    boxcollider.AddComponent<BoxCollider>();
                    boxcollider.layer = 2;
                    boxcollider.tag = "Unlock";
                    boxcollider.GetComponent<BoxCollider>().isTrigger = true;
                    boxcollider.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);
                    boxcollider.transform.localScale = new Vector3(1, 1, 1);
                    DestroyImmediate(aurea.GetComponent<BoxCollider>());
                    aurea.transform.LookAt(new Vector3(0, aurea.transform.position.y, 0));
                }              
            }
        }
        slots.transform.localScale = islands.transform.localScale;
    }



    public void TakeInput(Ray ray) {

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            } 

            Aurea aurea = null;
            if (watchTrigger)
            {
                aurea = hitpodest.transform.parent.gameObject.transform.GetComponentInChildren<Aurea>();
                buttonSelect.select(aurea.GetName());
                selectAureaHUD.SetActive(true);

            }
            else
            {
                selectAureaHUD.SetActive(false);
            }


            if (unlockTrigger)
            {
                aurea = hitpodest.transform.parent.gameObject.transform.GetComponentInChildren<Aurea>();
                unlockHUD.SetActive(true);
                buttonSelect.SelectedAurea(aurea.gameObject);
                if(data.GetMoney() <= 50)
                {
                    unlockHUD.GetComponentInChildren<Button>().interactable = false;
                    unlockHUD.GetComponentInChildren<Text>().text = "You need 50 Coins";
                }
                else
                {
                    unlockHUD.GetComponentInChildren<Button>().interactable = true;
                    unlockHUD.GetComponentInChildren<Text>().text = "Entsperren";
                }

            }
            else
            {
                unlockHUD.SetActive(false);
            }

            if (hit.collider.CompareTag("Walkable"))
            {
                Vector3 movement = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                MovementController c = player.GetComponent<MovementController>();
                c.Move(movement);
            }
        }       
    }

    public void ResetIsland()
    {
        GameObject[] all_slots = null;
        all_slots = GameObject.FindGameObjectsWithTag("Podest");
        foreach (GameObject slots in all_slots)
        {
                DestroyImmediate(slots);
        }
        data = Player.Instance;
        slots.transform.localScale = Vector3.one;
        CreateSpiral();



        Debug.Log("Reset Temple");
    }
    
    public void WatchTrigger(bool newtrigger, GameObject newhitpodest)
    {
        watchTrigger = newtrigger;
        hitpodest = newhitpodest;
    }

    public void UnlockTrigger(bool newtrigger, GameObject newhitpodest)
    {
        unlockTrigger = newtrigger;
        hitpodest = newhitpodest;
    }

    public void teleport(bool trigger)
    {
        if (trigger)
        {
            teleportHUD.SetActive(true);
        }
        else
        {
            teleportHUD.SetActive(false);
        }
    }

}
