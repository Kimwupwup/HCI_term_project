using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    
    public float speed = 1;

    private GameObject menuPanel;
    private GameObject codePanel;
    private GameObject pausePanel;

    private Vector3 posPausePanel;

    private float menuPanelWidth;
    private float codePanelHeight;

    private bool isSetMenu = false;
    private bool isSetCodePanel = false;
    private bool isSetViewPanel = false;

    void Awake() {
        pausePanel = GameObject.FindGameObjectWithTag("pausePanel");
        posPausePanel = pausePanel.transform.position;
        pausePanel.SetActive(false);
    }

    void Start() {
        menuPanel = GameObject.FindGameObjectWithTag("menuPanel");
        codePanel = GameObject.FindGameObjectWithTag("codePanel");

        menuPanelWidth = menuPanel.GetComponent<RectTransform>().sizeDelta.x;
        codePanelHeight = codePanel.GetComponent<RectTransform>().sizeDelta.y;
    }
    void Update() {

        // 메뉴창 켜기
        if (isSetMenu == true) {
            menuPanel.transform.position = Vector2.Lerp(menuPanel.transform.position, new Vector2(menuPanelWidth / 2, menuPanel.transform.position.y), Time.deltaTime * speed);
        }

        // 메뉴창 끄기
        if (isSetMenu == false) {
            menuPanel.transform.position = Vector2.Lerp(menuPanel.transform.position, new Vector2(-menuPanelWidth / 2, menuPanel.transform.position.y), Time.deltaTime * speed);
        }

        // 코드창 키우기
        if (isSetCodePanel == true) {
            codePanel.transform.position = Vector3.Lerp(codePanel.transform.position, new Vector2(codePanel.transform.position.x, codePanelHeight / 3), Time.deltaTime * speed);
        }

        // 초기 상태
        if (isSetCodePanel == false && isSetViewPanel == false) {
            codePanel.transform.position = Vector3.Lerp(codePanel.transform.position, new Vector2(codePanel.transform.position.x, 0), Time.deltaTime * speed);
        }

        // 뷰창 키우기
        if (isSetViewPanel == true) {
            codePanel.transform.position = Vector3.Lerp(codePanel.transform.position, new Vector2(codePanel.transform.position.x, -codePanelHeight / 3), Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pausePanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            pausePanel.SetActive(true);
        }
    }

    public void TurnOnMenu() {

        // 메뉴창, 코드창 키우기(뷰창 줄이기)
        isSetMenu = true;
        isSetCodePanel = true;
        isSetViewPanel = false;
    }

    public void TurnOffMenu() {

        // 이미 메뉴창이 꺼져있을 경우
        if (isSetMenu == false) {
            
            // 뷰창 키우기(코드창 줄이기)
            if (isSetViewPanel == false) {
                isSetViewPanel = true;
                isSetCodePanel = false;
            } else {    // 원래 상태로
                isSetViewPanel = false;
                isSetCodePanel = false;
            }
        } else {    // 원래 상태로 (메뉴창 끄기)
            isSetMenu = false;
            isSetCodePanel = false;
        }
    }

    public void SizeUpDownCodePanel() {

        // 코드창 키우기(뷰창 줄이기)
        if (isSetCodePanel == false) {
            isSetCodePanel = true;
            isSetViewPanel = false;
        } else {    // 원래 상태로
            isSetCodePanel = false;
            isSetViewPanel = false;
        }
    }

    public void IsExit() {
        pausePanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        pausePanel.SetActive(true);
    }

    public void ConfirmExit() {
        Application.Quit();
    }

    public void CancelExit() {
        pausePanel.GetComponent<RectTransform>().position = posPausePanel;
        pausePanel.SetActive(false);
    }
}
