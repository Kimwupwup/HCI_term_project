using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private GameObject tmpButton;
    private bool isInCodePanel = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData) {
        tmpButton = Instantiate(this, Input.mousePosition, Quaternion.identity).gameObject;
        
        tmpButton.transform.SetParent(GameObject.FindGameObjectWithTag("canvas").transform);

    }

    public void OnDrag(PointerEventData eventData) {
        tmpButton.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!isInCodePanel) {
            Destroy(tmpButton);
        }
    }

    public void IsTrue() {
        isInCodePanel = true;
        Debug.Log(isInCodePanel);
    }

    public void IsFalse() {
        isInCodePanel = false;
        Debug.Log(isInCodePanel);

    }
}
