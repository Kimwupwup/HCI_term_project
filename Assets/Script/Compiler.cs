using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compiler : MonoBehaviour
{
    private List<GameObject> functions = new List<GameObject>();
    private bool isCompiled = false;
    private Rigidbody2D player;
    private int frameCount = 0;
    private bool isLoop = false;
    private int delayTime = 30;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if(frameCount % delayTime == 0) {
            if (isCompiled == true && functions.Count > 0) {

                if (functions[0].name == "BtnMove(Clone)") {
                    Debug.Log(functions[0].name);
                    functionMove();
                } else if (functions[0].name == "BtnJump(Clone)") {
                    Debug.Log(functions[0].name);
                    functionJump();
                } else if (functions[0].name == "BtnRotate(Clone)") {
                    Debug.Log(functions[0].name);
                    functionRotate();
                } else if (functions[0].name == "BtnLoop(Clone)") {
                    Debug.Log(functions[0].name);
                    functionLoop();
                } else if (functions[0].name == "BtnEndLoop(Clone)" && isLoop == true) {
                    Debug.Log(functions[0].name);
                    functionEndLoop();
                }
                if (isLoop) {
                    functions.Add(functions[0]);
                }
                functions.RemoveAt(0);
                frameCount = 0;
            } else {
                isCompiled = false;
                functions.Clear();
            }
        }
    }

    public void ResetView() {
        isCompiled = false;
        functions.Clear();
        player.transform.position = new Vector2(0, 0);
        player.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void Compiling() {
        if (isCompiled == true) {
            return;
        }
        GameObject code = GameObject.FindGameObjectWithTag("codePanel");
        List<GameObject> codesQueue = new List<GameObject>();
        GameObject temp;
        for (int i = 1; i < code.transform.childCount; i++) {
            codesQueue.Add(code.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < codesQueue.Count; i++) {
            for (int j = i + 1; j < codesQueue.Count; j++) {
                if (codesQueue[i].transform.position.x > codesQueue[j].transform.position.x) {
                    temp = codesQueue[i];
                    codesQueue[i] = codesQueue[j];
                    codesQueue[j] = temp;
                }
            }
        }

        for (int i = 0; i < codesQueue.Count; i++) {
            code = codesQueue[i];
            while (true) {
                functions.Add(code);
                if (code.name == "BtnLoop(Clone)") isLoop = true;
                if (code.name == "BtnEndLoop(Clone)" && isLoop == true) {
                    codesQueue.Clear();
                    isCompiled = true;
                    isLoop = false;
                    return;
                }
                
                if (code.transform.childCount < 2) {
                    break;
                }
                code = code.transform.GetChild(1).gameObject;
            }
        }
        codesQueue.Clear();
        isCompiled = true;
    }

    public void functionMove() {
        if (player.GetComponent<SpriteRenderer>().flipX == false) {
            //player.transform.position += Vector3.right * 0.3f;
            player.AddForce(Vector2.right*2, ForceMode2D.Impulse);
        } else {
            //player.transform.position += Vector3.left * 0.3f;
            player.AddForce(Vector2.left*2, ForceMode2D.Impulse);
        }
        delayTime = 30;
    }

    public void functionJump() {
        player.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        delayTime = 30;
    }

    public void functionRotate() {
        if (player.GetComponent<SpriteRenderer>().flipX == false) {
            player.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        delayTime = 30;
    }

    public void functionLoop() {
        isLoop = true;
        delayTime = 1;
    }

    public void functionEndLoop() {
        isLoop = false;
        delayTime = 1;
    }
}
