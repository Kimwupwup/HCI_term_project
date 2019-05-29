using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : MonoBehaviour
{
    public GameObject colorR;
    public GameObject colorG;
    public GameObject colorB;
    public GameObject preview;

    private InputField textR;
    private InputField textG;
    private InputField textB;

    private GameObject objBackground;
    private GameObject cameraBackground;

    void Awake() {
        textR = colorR.transform.GetChild(0).GetComponent<InputField>();
        textG = colorG.transform.GetChild(0).GetComponent<InputField>();
        textB = colorB.transform.GetChild(0).GetComponent<InputField>();

        if (this.CompareTag("viewColor")) {
            objBackground = GameObject.FindGameObjectWithTag("MainCamera");
            textR.text = (objBackground.GetComponent<Camera>().backgroundColor.r * 256).ToString();
            textG.text = (objBackground.GetComponent<Camera>().backgroundColor.g * 256).ToString();
            textB.text = (objBackground.GetComponent<Camera>().backgroundColor.b * 256).ToString();
            preview.GetComponent<Image>().color = objBackground.GetComponent<Camera>().backgroundColor;

        } else if (this.CompareTag("codeColor")) {
            // codePanel의 색상은 a값이 220이기때문에, viewPanel의 화면에 영향을 받는다.
            // 그래서 viewPanel의 색상도 필요로 한다.
            objBackground = GameObject.FindGameObjectWithTag("codePanel");
            cameraBackground = GameObject.FindGameObjectWithTag("MainCamera");
            textR.text = (objBackground.GetComponent<Image>().color.r * 256).ToString();
            textG.text = (objBackground.GetComponent<Image>().color.g * 256).ToString();
            textB.text = (objBackground.GetComponent<Image>().color.b * 256).ToString();
            preview.transform.parent.GetComponent<Image>().color = cameraBackground.GetComponent<Camera>().backgroundColor;
            preview.GetComponent<Image>().color = objBackground.GetComponent<Image>().color;
        }

    }

    public void ChangedValueInterupt() {
        if (string.IsNullOrEmpty(textR.text) || string.IsNullOrEmpty(textG.text) || string.IsNullOrEmpty(textB.text)) {
            return;
        }
        if (float.Parse(textR.text) > 255) {
            textR.text = "255";
        } else if (float.Parse(textG.text) > 255) {
            textG.text = "255";
        } else if (float.Parse(textB.text) > 255) {
            textB.text = "255";
        }
        preview.GetComponent<Image>().color = new Vector4(
            float.Parse(textR.text) / 256f,
            float.Parse(textG.text) / 256f,
            float.Parse(textB.text) / 256f,
            preview.GetComponent<Image>().color.a
            );

        if (preview.transform.parent.name != "preview") {
            GameObject.FindGameObjectWithTag("codeColor").transform.GetChild(3).GetComponent<Image>().color = new Vector4(
                float.Parse(textR.text) / 256f,
                float.Parse(textG.text) / 256f,
                float.Parse(textB.text) / 256f,
                preview.GetComponent<Image>().color.a
                );
        }
    }

    public void ResetValue() {
        if (this.CompareTag("viewColor")) {
            textR.text = (objBackground.GetComponent<Camera>().backgroundColor.r * 256).ToString();
            textG.text = (objBackground.GetComponent<Camera>().backgroundColor.g * 256).ToString();
            textB.text = (objBackground.GetComponent<Camera>().backgroundColor.b * 256).ToString();
            preview.GetComponent<Image>().color = objBackground.GetComponent<Camera>().backgroundColor;

        } else if (this.CompareTag("codeColor")) {
            textR.text = (objBackground.GetComponent<Image>().color.r * 256).ToString();
            textG.text = (objBackground.GetComponent<Image>().color.g * 256).ToString();
            textB.text = (objBackground.GetComponent<Image>().color.b * 256).ToString();
            preview.transform.parent.GetComponent<Image>().color = cameraBackground.GetComponent<Camera>().backgroundColor;
            preview.GetComponent<Image>().color = objBackground.GetComponent<Image>().color;
        }
    }

    public void ApplyValue() {
        if (this.CompareTag("viewColor")) {
            objBackground.GetComponent<Camera>().backgroundColor = new Vector4(
            float.Parse(textR.text) / 256f,
            float.Parse(textG.text) / 256f,
            float.Parse(textB.text) / 256f,
            objBackground.GetComponent<Camera>().backgroundColor.a
            );


        } else if (this.CompareTag("codeColor")) {
            objBackground.GetComponent<Image>().color = new Vector4(
            float.Parse(textR.text) / 256f,
            float.Parse(textG.text) / 256f,
            float.Parse(textB.text) / 256f,
            objBackground.GetComponent<Image>().color.a
            );
        }
    }
}
