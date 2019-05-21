using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject tmpButton;
    private GameObject objController;
    private Controller controller;
    private GameObject objTarget;

    public void Awake() {
        objController = GameObject.FindGameObjectWithTag("controller");
        controller = objController.GetComponent<Controller>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (this.CompareTag("button") == true) {
            tmpButton = Instantiate(this, Input.mousePosition, Quaternion.identity).gameObject;
            tmpButton.tag = "clone";
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            tmpButton.GetComponent<Image>().raycastTarget = false;
        } else {
            tmpButton = this.gameObject;
            tmpButton.transform.parent.GetComponent<Image>().raycastTarget = true;
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            tmpButton.GetComponent<Image>().raycastTarget = false;
        }        
    }

    public void OnDrag(PointerEventData eventData) {
        tmpButton.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        
        objTarget = controller.GetObjTarget();
        if (objTarget != null)
            Debug.Log(objTarget.name);
        bool isChild = controller.GetIsCodeChild();

        if (isChild && objTarget.CompareTag("child")) {
            tmpButton.transform.position = objTarget.transform.position;
            tmpButton.transform.SetParent(objTarget.transform);
            tmpButton.GetComponent<Image>().raycastTarget = true;
            objTarget.GetComponent<Image>().raycastTarget = false;
            return;
        }

        bool isTrue = controller.GetIsCodePanel();

        if (!isTrue) {
            Destroy(tmpButton);
            Debug.Log("Destroy");
        } else {
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("codePanel").transform);
            tmpButton.GetComponent<Image>().raycastTarget = true;
        }
    }
}
