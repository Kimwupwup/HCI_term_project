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
            
            if (tmpButton.name == "BtnDelay(Clone)" || 
                tmpButton.name == "BtnCnt=(Clone)" ||
                tmpButton.name == "BtnCount(Clone)") {
                tmpButton.transform.GetChild(0).gameObject.SetActive(true);
            }
        } else {
            tmpButton = this.gameObject;

            if (tmpButton.name == "BtnCount(Clone)") {
                if (tmpButton.transform.parent.GetChild(1).CompareTag("condition")) {
                    tmpButton.transform.parent.GetChild(1).gameObject.SetActive(true);
                }
            } else {
                if (tmpButton.transform.parent.GetChild(0).CompareTag("child")) {
                    tmpButton.transform.parent.GetChild(0).gameObject.SetActive(true);
                } else if (tmpButton.transform.parent.GetChild(1).CompareTag("child")) {
                    tmpButton.transform.parent.GetChild(1).gameObject.SetActive(true);
                }
            }
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            tmpButton.GetComponent<Image>().raycastTarget = false;
        }        
    }

    public void OnDrag(PointerEventData eventData) {
        tmpButton.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        
        bool isChild = controller.GetIsCodeChild();

        if (isChild) {
            objTarget = controller.GetObjTarget();

            if (objTarget != null) {
                Debug.Log(objTarget.transform.parent.name);
            }

            if (objTarget.CompareTag("condition") && tmpButton.name == "BtnCount(Clone)") {
                float temp = tmpButton.GetComponent<RectTransform>().rect.width;
                temp = objTarget.GetComponent<RectTransform>().rect.width - temp;

                tmpButton.transform.position =
                    new Vector3(objTarget.transform.position.x - (temp / 2),
                    objTarget.transform.position.y + 10, 0);

                tmpButton.transform.SetParent(objTarget.transform.parent);
                tmpButton.GetComponent<Image>().raycastTarget = true;
                objTarget.SetActive(false);
                controller.SetObjTarget(null);
                controller.SetIsCodeChild(false);
                return;
            }

            if (objTarget.CompareTag("child") && tmpButton.name != "BtnCount(Clone)") {
                float temp = tmpButton.GetComponent<RectTransform>().rect.width;
                temp = objTarget.GetComponent<RectTransform>().rect.width - temp;

                tmpButton.transform.position =
                    new Vector3(objTarget.transform.position.x - (temp / 2),
                    objTarget.transform.position.y, 0);

                tmpButton.transform.SetParent(objTarget.transform.parent);
                tmpButton.GetComponent<Image>().raycastTarget = true;
                objTarget.SetActive(false);
                controller.SetObjTarget(null);
                controller.SetIsCodeChild(false);
                return;
            }
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
