using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Kristallisiert : MonoBehaviour
{
    Aurea aurea = null;
    void Start() {
        aurea = GetComponent<Aurea>();
    }
}
