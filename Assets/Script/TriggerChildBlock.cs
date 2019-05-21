using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChildBlock : MonoBehaviour
{
    private GameObject objController;
    private Controller controller;

    public void Awake() {
        objController = GameObject.FindGameObjectWithTag("controller");
        controller = objController.GetComponent<Controller>();
    }

    public void SetTrue() {
        controller.SetIsCodeChild(true);
        controller.SetObjTarget(this.gameObject);
    }

    public void SetFalse() {
        controller.SetIsCodeChild(false);
        controller.SetObjTarget(null);
    }
}
