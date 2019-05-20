using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTrue : MonoBehaviour
{
    public bool isTrue;

    public void setTrue() {
        isTrue = true;
        Debug.Log(isTrue);
    }

    public void setFalse() {
        isTrue = false;
        Debug.Log(isTrue);
    }

}
