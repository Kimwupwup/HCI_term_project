using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compiler : MonoBehaviour
{
    private List<GameObject> functions = new List<GameObject>();
    private List<int> loopIndex = new List<int>();
    private List<int> endLoopIndex = new List<int>();

    private bool isCompiled = false;
    private Rigidbody2D player;
    
    private int frameCount = 0;
    private int delayTime = 1;
    private int currentIndex = 0;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if(frameCount % delayTime == 0) {
            if (isCompiled == true && currentIndex < functions.Count) {
                Debug.Log(currentIndex);
                if (functions[currentIndex].name == "BtnMove(Clone)") {
                    //Debug.Log(functions[currentIndex].name);
                    functionMove();
                } else if (functions[currentIndex].name == "BtnJump(Clone)") {
                    //Debug.Log(functions[currentIndex].name);
                    functionJump();
                } else if (functions[currentIndex].name == "BtnRotate(Clone)") {
                    //Debug.Log(functions[currentIndex].name);
                    functionRotate();
                } else if (functions[currentIndex].name == "BtnLoop(Clone)") {
                    //Debug.Log(functions[currentIndex].name);
                    functionLoop();
                } else if (functions[currentIndex].name == "BtnEndLoop(Clone)") {
                    //Debug.Log(functions[currentIndex].name);
                    functionEndLoop();
                } else if (functions[currentIndex].name == "BtnDelay(Clone)") {
                    string n = functions[currentIndex].transform.GetChild(0).GetComponent<InputField>().text;
                    float temp;
                    if (string.IsNullOrEmpty(n)) {
                        temp = 2;
                    } else {
                        temp = float.Parse(n);
                    }
                    functionDelay(temp);
                }
                currentIndex++;
                frameCount = 0;
            } else {
                currentIndex = 0;
                delayTime = 1;
                isCompiled = false;
                loopIndex.Clear();
                endLoopIndex.Clear();
                functions.Clear();
            }
        }
        
    }

    public void ResetView() {
        currentIndex = 0;
        delayTime = 1;
        isCompiled = false;
        functions.Clear();
        loopIndex.Clear();
        endLoopIndex.Clear();
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
        
        // Sort multiple code lines
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

                if (code.name == "BtnLoop(Clone)") {
                    loopIndex.Add(functions.Count - 1);
                } 
                if (code.name == "BtnEndLoop(Clone)") {
                    endLoopIndex.Add(functions.Count - 1);
                }

                if (code.transform.childCount < 2 && code.name != "BtnDelay(Clone)") {
                    break;
                } else if (code.transform.childCount < 3 && code.name == "BtnDelay(Clone)") {
                    break;
                }

                if (code.name == "BtnDelay(Clone)") {
                    code = code.transform.GetChild(2).gameObject;
                } else {
                    code = code.transform.GetChild(1).gameObject;
                }
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
        delayTime = 1;
    }

    public void functionEndLoop() {
        for (int i = 0; i < endLoopIndex.Count; i++) {
            if (currentIndex == endLoopIndex[i]) {
                currentIndex = loopIndex[loopIndex.Count - 1 - i] - 1;
            }
        }
        delayTime = 1;
    }

    public void functionDelay(float delay) {
        if (delay == 0) {
            delayTime = 1;
        } else {
            delayTime = (int)(60 * delay);
        }
    }
}
