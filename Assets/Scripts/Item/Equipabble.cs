using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipabble : MonoBehaviour
{
    void Awake() {
        if(GetComponent<RectTransform>() == null) {
            throw new MissingComponentException("GameObject must have UI Image/RectTransform scripts attached to be equippable");
        }
    }
}
