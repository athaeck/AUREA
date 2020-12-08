using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Island
{
    SkyIsland,
    TempleOfDoom,
    ChickenFight
}

public class IslandController : MonoBehaviour
{
    #region Singleton
    public static IslandController _instance;
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

    private Island activeIsland = Island.SkyIsland;
    private SkyIslandController skyIsland = null;
    private TempleController temple = null;
    private FightController fight = null;

    public void FindIslands()
    {
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[i];

            SkyIslandController skyIslandController = gameObject.GetComponent<SkyIslandController>();
            TempleController templeController = gameObject.GetComponent<TempleController>();
            FightController fightController = gameObject.GetComponent<FightController>();

            if (skyIslandController)
                skyIsland = skyIslandController;

            if (templeController)
                temple = templeController;

            if (fightController)
                fight = fightController;
        }
    }

    public void ChangeActiveIsland(Island _island)
    {
        if(!skyIsland || !temple || !fight)
            FindIslands();
        
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
    }
}
