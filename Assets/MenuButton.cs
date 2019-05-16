using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    
    public float speed = 1;
    private GameObject menuPanel;
    private GameObject codePanel;

    private bool isSet = false;
    private bool isUnset = false;

    void Start() {
        menuPanel = GameObject.FindGameObjectWithTag("menuPanel");
        codePanel = GameObject.FindGameObjectWithTag("codePanel");
    }
    void Update() {
        if (isSet == true) {
            menuPanel.transform.position = Vector2.Lerp(menuPanel.transform.position, new Vector2(270, menuPanel.transform.position.y), Time.deltaTime * speed);
            codePanel.transform.position = Vector3.Lerp(codePanel.transform.position, new Vector2(codePanel.transform.position.x, 600), Time.deltaTime * speed);
        }
        if (isUnset == true) {
            menuPanel.transform.position = Vector2.Lerp(menuPanel.transform.position, new Vector2(-280, menuPanel.transform.position.y), Time.deltaTime * speed);
            codePanel.transform.position = Vector3.Lerp(codePanel.transform.position, new Vector2(codePanel.transform.position.x, 0), Time.deltaTime * speed);
        }
    }

    public void BtnMenuOnClick() {
        isSet = true;
        isUnset = false;
    }

    public void OtherOnClick() {
        isUnset = true;
        isSet = false;
    }
}
