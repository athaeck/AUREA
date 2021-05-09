using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IslandController : MonoBehaviour
{
    #region Singleton
    private static IslandController _instance;
    public static IslandController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<IslandController>();
                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    container.AddComponent<IslandController>();
                }
            }
            return _instance;
        }
    }
    #endregion

    private SkyIslandController _skyIsland;
    public SkyIslandController skyIsland
    {
        get
        {
            if (_skyIsland == null)
                FindIslands();
            return _skyIsland;
        }
        private set
        {
            _skyIsland = value;
        }
    }

    private TempleController _temple;
    public TempleController temple
    {
        get
        {
            if (_temple == null)
                FindIslands();
            return _temple;
        }
        private set
        {
            _temple = value;
        }
    }

    private FightController _fight;
    public FightController fight
    {
        get
        {
            if (_fight == null)
                FindIslands();
            return _fight;
        }
        private set
        {
            _fight = value;
        }
    }

    [SerializeField]
    private Island _activeIsland;
    public Island activeIsland
    {
        get
        {
            return _activeIsland;
        }
        private set
        {
            _activeIsland = value;
        }
    }

    private void Awake()
    {
        ChangeActiveIsland(activeIsland);
    }

    public void FindIslands()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        foreach (GameObject rootObject in rootObjects)
        {
            IslandController.Instance.FindIslandsRecursive(rootObject);
        }
    }

    void FindIslandsRecursive(GameObject root)
    {

        SkyIslandController skyIslandController = root.GetComponent<SkyIslandController>();
        TempleController templeController = root.GetComponent<TempleController>();
        FightController fightController = root.GetComponent<FightController>();

        if (skyIslandController)
            _skyIsland = skyIslandController;

        if (templeController)
            _temple = templeController;

        if (fightController)
            _fight = fightController;

        if (_skyIsland && _temple && _fight)
            return;

        foreach (Transform child in root.transform)
        {
            FindIslandsRecursive(child.gameObject);
        }
    }

    public void ChangeActiveIsland(Island _island)
    {
        if (!_skyIsland || !_temple || !_fight)
            IslandController.Instance.FindIslands();

        switch (_island)
        {
            case Island.SkyIsland:
                IslandController.Instance.skyIsland.gameObject.SetActive(true);
                IslandController.Instance.skyIsland.ResetIsland();

                IslandController.Instance.temple.gameObject.SetActive(false);
                IslandController.Instance.fight.gameObject.SetActive(false);
                break;
            case Island.TempleOfDoom:
                IslandController.Instance.temple.gameObject.SetActive(true);
                IslandController.Instance.temple.ResetIsland();

                IslandController.Instance.skyIsland.gameObject.SetActive(false);
                IslandController.Instance.fight.gameObject.SetActive(false);
                break;
            case Island.ChickenFight:
                IslandController.Instance.fight.gameObject.SetActive(true);
                IslandController.Instance.fight.ResetIsland();

                IslandController.Instance.skyIsland.gameObject.SetActive(false);
                IslandController.Instance.temple.gameObject.SetActive(false);
                break;
        }
        activeIsland = _island;
        CameraController.Instance?.ChangeIsland(activeIsland, Player.Instance.IsArOn());
    }

    public void OpenSkyIsland()
    {
        ChangeActiveIsland(Island.SkyIsland);
    }
    public void OpenTemple()
    {
        ChangeActiveIsland(Island.TempleOfDoom);
    }
    public void OpenFight()
    {
        ChangeActiveIsland(Island.ChickenFight);
    }
}
