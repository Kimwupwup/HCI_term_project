using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private bool isCodePanel;
    private bool isCodeChild;
    private GameObject objTarget;

    public bool GetIsCodePanel() {
        return isCodePanel;
    }

    public void SetIsCodePanel(bool isCodePanel) {
        this.isCodePanel = isCodePanel;
    }

    public bool GetIsCodeChild() {
        return isCodeChild;
    }

    public void SetIsCodeChild(bool isCodeChild) {
        this.isCodeChild = isCodeChild;
    }

    public GameObject GetObjTarget() {
        return objTarget;
    }

    public void SetObjTarget(GameObject objTarget) {
        this.objTarget = objTarget;
    }
}
