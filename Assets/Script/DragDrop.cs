using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject tmpButton;
    private GameObject codePanel;

    public void Awake() {
        codePanel = GameObject.FindGameObjectWithTag("codePanel");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (this.CompareTag("button") == true) {
            tmpButton = Instantiate(this, Input.mousePosition, Quaternion.identity).gameObject;
            tmpButton.tag = "clone";
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            tmpButton.GetComponent<Image>().raycastTarget = false;
        } else {
            tmpButton = this.gameObject;
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);
            tmpButton.GetComponent<Image>().raycastTarget = false;
        }        
    }

    public void OnDrag(PointerEventData eventData) {
        tmpButton.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        bool isTrue;
        isTrue = codePanel.GetComponent<IsTrue>().isTrue;

        if (!isTrue) {
            Destroy(tmpButton);
            Debug.Log("Destroy");
        } else {
            tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("codePanel").transform);
            tmpButton.GetComponent<Image>().raycastTarget = true;
        }
    }
}
