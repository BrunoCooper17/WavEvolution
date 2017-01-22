using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {
    public int state = 0;

    public GameObject PartituraRef;
    public GameObject GameOverRef;
    public GameObject WinRef;
    public MoveAvatar Avatar;
    
    private GameObject partituraManager;

	// Use this for initialization
	void Start () {
        Invoke("ChangeToMenuState", 4.5f);
	}
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case 1:
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Ended)
                    {
                        Invoke("StartGame", 0.5f);

                        GetComponent<AudioSource>().Play();
                    }
                }
                break;

                // GAME
            case 2:
                if(Avatar.isDead())
                {
                    state = 4;
                    Vector3 tmpPos = GameOverRef.transform.position;
                    tmpPos.y = 0;
                    GameOverRef.transform.position = tmpPos;

                    Avatar.offsetX = 0;
                    Avatar.Reset();

                    Destroy(partituraManager);
                    GameObject[] tmpDestroy = GameObject.FindGameObjectsWithTag("Destroy");
                    for(int index=0; index < tmpDestroy.Length; index++)
                    {
                        Destroy(tmpDestroy[index]);
                    }

                    Invoke("GameOver",5);
                }
                if(partituraManager.GetComponent<PartituraManager>().bWin)
                {
                    state = 4;
                    Vector3 tmpPos = WinRef.transform.position;
                    tmpPos.y = 0;
                    WinRef.transform.position = tmpPos;


                    Avatar.offsetX = 0;
                    Avatar.Reset();

                    Destroy(partituraManager);
                    GameObject[] tmpDestroy = GameObject.FindGameObjectsWithTag("Destroy");
                    for (int index = 0; index < tmpDestroy.Length; index++)
                    {
                        Destroy(tmpDestroy[index]);
                    }

                    Invoke("Winner", 5);
                }
                break;
        }
    }

    void StartGame()
    {
        state = 2;

        Vector3 tmpPos = transform.position;
        tmpPos.y = 50;
        transform.position = tmpPos;

        partituraManager = Instantiate<GameObject>(PartituraRef);
        partituraManager.GetComponent<PartituraManager>().avatarRef = Avatar;
    }

    void GameOver()
    {
        Vector3 tmpPos = transform.position;
        tmpPos.y = 0;
        transform.position = tmpPos;

        tmpPos = GameOverRef.transform.position;
        tmpPos.y = 50;
        GameOverRef.transform.position = tmpPos;

        state = 1;
    }

    void ChangeToMenuState()
    {
        state = 1;
    }

    void Winner()
    {
        Vector3 tmpPos = transform.position;
        tmpPos.y = 0;
        transform.position = tmpPos;

        tmpPos = WinRef.transform.position;
        tmpPos.y = 50;
        WinRef.transform.position = tmpPos;

        state = 1;
    }
}
