using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compiler : MonoBehaviour
{
    private List<GameObject> functions = new List<GameObject>();
    private bool isCompiled = false;
    private Rigidbody2D player;
    private int frameCount = 0;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        if(frameCount % 30 == 0) {
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
                }
                functions.RemoveAt(0);
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
        while(true) {
            if (code.transform.childCount < 2) {
                isCompiled = true;
                return;
            }
            functions.Add(code.transform.GetChild(1).gameObject);
            code = code.transform.GetChild(1).gameObject;
        }
    }

    public void functionMove() {
        if (player.GetComponent<SpriteRenderer>().flipX == false) {
            //player.transform.position += Vector3.right * 0.3f;
            player.AddForce(Vector2.right*2, ForceMode2D.Impulse);
        } else {
            //player.transform.position += Vector3.left * 0.3f;
            player.AddForce(Vector2.left*2, ForceMode2D.Impulse);
        }
    }

    public void functionJump() {
        player.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }

    public void functionRotate() {
        if (player.GetComponent<SpriteRenderer>().flipX == false) {
            player.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }
}
