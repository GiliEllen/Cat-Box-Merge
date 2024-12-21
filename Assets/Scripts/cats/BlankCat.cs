using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankCat : Cat
{
    void Awake() {
        catId = 0;
        isClickable = false;
    }
}
