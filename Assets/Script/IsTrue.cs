using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IsTrue : MonoBehaviour
{

    public bool isTrue;
    public bool isChild;
    public GameObject target = null;

    public void setTrueChild() {
        isChild = true;
        target = this.gameObject;
        Debug.Log("isChild : " + isChild);
        Debug.Log("tag : " + target.tag);
    }

    public void setFalseChild() {
        isChild = false;
        target = null;
        Debug.Log("isChild : " + isChild);
    }

    public void setTrue() {
        isTrue = true;
        Debug.Log(isTrue);
    }

    public void setFalse() {
        isTrue = false;
        Debug.Log(isTrue);
    }
}
