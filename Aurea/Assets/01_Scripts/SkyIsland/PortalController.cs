using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Difficulty _difficulty;

    [SerializeField]
    private List<GameObject> _currentDifficulty = null;

    [SerializeField]
    private Sprite _emptystar = null;

    [SerializeField]
    private Sprite _fullStar = null;

    [SerializeField]
    private Text _description = null;

    private int availableFullStars = 1;

    private string description = "";

    private Island island;

    private void Start()
    {
        Init();
    }
    void Update()
    {
        Init();
    }
    private void Init()
    {
        _difficulty = Player.Instance.GetDifficulty();
        SetAvailableFullStars();
        SetStars();
        if(_description != null)
        {
            _description.text = description;
        }
    }
    public void SetDescription(string des,Island land)
    {
        description = des;
        
        island = land;
    }

    public void Teleport()
    {
        switch(island)
        {
            case Island.TempleOfDoom:
                IslandController.Instance.OpenTemple();
                break;
            case Island.ChickenFight:
                IslandController.Instance.OpenFight();
                break;
            default:
                IslandController.Instance.OpenFight();
                break;
        }
    }
    private void SetStars()
    {
        if(_currentDifficulty != null && _emptystar != null && _fullStar != null)
        {
            int length = _currentDifficulty.Count;
             for(int i = 0 ; i <= availableFullStars ; i++)
             {
              _currentDifficulty[i].GetComponent<Image>().sprite = _fullStar;
             }
                 if(length - availableFullStars > 0)
                 {
                   for(int y = 2 ; y >  availableFullStars ; y--)
                       {
                           _currentDifficulty[y].GetComponent<Image>().sprite = _emptystar;
                       }
                 }
        }

    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }
    private void SetAvailableFullStars()
    {
        switch(_difficulty)
        {
            case Difficulty.Easy:
                availableFullStars = 0;
                break;
            case Difficulty.Medium:
                availableFullStars = 1;
                break;
            default:
                availableFullStars = 2;
                break;
        }
    }
}
