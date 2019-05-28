using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour
{
    public GameObject colorR;
    public GameObject colorG;
    public GameObject colorB;

    private InputField textR;
    private InputField textG;
    private InputField textB;

    private GameObject objBackground;

    void Awake() {
        textR = colorR.transform.GetChild(0).GetComponent<InputField>();
        textG = colorG.transform.GetChild(0).GetComponent<InputField>();
        textB = colorB.transform.GetChild(0).GetComponent<InputField>();

        if (this.CompareTag("viewColor")) {
            objBackground = GameObject.FindGameObjectWithTag("MainCamera");
            textR.text = (objBackground.GetComponent<Camera>().backgroundColor.r * 256).ToString();
            textG.text = (objBackground.GetComponent<Camera>().backgroundColor.g * 256).ToString();
            textB.text = (objBackground.GetComponent<Camera>().backgroundColor.b * 256).ToString();
        } else if (this.CompareTag("codeColor")) {
            objBackground = GameObject.FindGameObjectWithTag("codePanel");
            textR.text = (objBackground.GetComponent<Image>().color.r * 256).ToString();
            textG.text = (objBackground.GetComponent<Image>().color.g * 256).ToString();
            textB.text = (objBackground.GetComponent<Image>().color.b * 256).ToString();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
