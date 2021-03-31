using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizationController : MonoBehaviour
{
    [SerializeField]
    GameObject damagePopup = null;

    Aurea aurea = null;
    void Start()
    {
        if (IslandController.Instance.fight.training) return;
        aurea = GetComponent<Aurea>();
        
        aurea.TookDamage += GotHit;
    }

    void GotHit(int _dmg)
    {
        if (IslandController.Instance.fight.training) return;
        if (!Player.Instance.AnimationsOn())
            return;

        DamagePopup newPopup = Instantiate(damagePopup, this.transform.position, Quaternion.identity).GetComponent<DamagePopup>();
        newPopup.Setup(_dmg);
    }
}
