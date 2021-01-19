using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CrystalVisualizationController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> crystals = new List<GameObject>();

    [SerializeField]
    private Material activeMaterial = null;

    [SerializeField]
    private Material inactiveMaterial = null;

    private PlayerController player = null;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        ActiveCrystals(player.GetAPLeft());
        player.ChangedAP += ActiveCrystals;
    }

    public void ActiveCrystals(int num)
    {
        for (int i = 0; i < crystals.Count; i++)
        {
            crystals[i].GetComponent<MeshRenderer>().material = i < num ? activeMaterial : inactiveMaterial;
        }
    }
}
