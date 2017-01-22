using UnityEngine;
using System.Collections;

public class PartituraManager : MonoBehaviour {
    // NOTES IN THE PARTITURE COMPONENT
    public GameObject[] PartiturasRef;
    private GameObject[] Partituras;

    // SELECT THE INITIAL WAVE
    public int PartituraActual = 0;
    private int lastPartiture = 0;

    // VELOCITY OF THE SONG
    public float velocity = 60.0f;

    private bool bWaitNextWave = false;

    public GameObject test;

	// Use this for initialization
	void Start () {
        // INSTANCIATE THE PARTITURES
        {
            Partituras = new GameObject[PartiturasRef.Length];
            for (int index = 0; index < PartiturasRef.Length; index++)
            {
                Partituras[index] = Instantiate<GameObject>(PartiturasRef[index]);
                Vector3 tmpPos = Partituras[index].transform.position;
                tmpPos.x = -10000;
                Partituras[index].transform.position = tmpPos;
            }
        }

        // SETS THE FIRST WAVE
        LoadPartiture();
	}
	
	// Update is called once per frame
	void Update () {
        if (bWaitNextWave) return;

        float[] indexNote = { -100, -100 };

        // CHECK IF ONE NOTE IS PRESSED
        {
            int currentNoteIndex = 0;
            int toucheDetected = Input.touchCount < 3 ? Input.touchCount : 2;
            for (int i = 0; i < toucheDetected; ++i)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchConvert = Camera.main.ScreenToWorldPoint(touch.position);
                    touchConvert.z = 0;
                    test.transform.position = touchConvert;

                    if(touchConvert.x < -4.5f && touchConvert.x > -6.5f )
                    {
                        indexNote[currentNoteIndex] = touchConvert.y;
                    }
                }
            }
        }

        // MOVE THE NOTES (WAVE)
        {
            Vector3 tmpPos = Partituras[PartituraActual].transform.position;
            tmpPos.x -= velocity * Time.deltaTime;
            int notes = Partituras[PartituraActual].transform.childCount;
            for (int index = 0; index < notes; index++)
            {
                Transform tmpGO = Partituras[PartituraActual].transform.GetChild(index);
                Note tmpNote = tmpGO.GetComponent<Note>();
                if (!tmpNote.IsPlayed() && tmpGO.position.x >= -6.5f && tmpGO.position.x <= -4.5f)
                {
                    for (int noteIndex = 0; noteIndex < 2; noteIndex++)
                    {
                        if (Mathf.Abs(tmpGO.position.y - indexNote[0]) < 1.0f)
                        {
                            tmpNote.PlaySong(true);
                        }
                    }

                    //tmpNote.PlaySong(true);
                }
            }

            Partituras[PartituraActual].transform.position = tmpPos;
        }

        {
            // CHECK IF THE WAVE (PARTITURE) HAS ENDED
            int notes = Partituras[PartituraActual].transform.childCount;
            bool bLoadNext = true;
            for (int index = 0; index < notes; index++)
            {
                if (Partituras[PartituraActual].transform.GetChild(index).position.x > -7.0f)
                {
                    bLoadNext = false;
                }
            }

            if (bLoadNext)
            {
                lastPartiture = PartituraActual;
                PartituraActual++;
                if (PartituraActual >= PartiturasRef.Length)
                {
                    PartituraActual = 0;
                }

                Invoke("LoadPartiture", 5.0f);
                bWaitNextWave = true;
            }
        }
    }

    protected void LoadPartiture()
    {
        Vector3 tmpPos = Partituras[lastPartiture].transform.position;
        tmpPos.x = -10000;
        Partituras[lastPartiture].transform.position = tmpPos;

        tmpPos = Partituras[PartituraActual].transform.position;
        tmpPos.x = 10.0f;
        Partituras[PartituraActual].transform.position = tmpPos;

        bWaitNextWave = false;

        int notes = Partituras[PartituraActual].transform.childCount;
        for (int index = 0; index < notes; index++)
        {
            Partituras[PartituraActual].transform.GetChild(index).GetComponent<Note>().Init();
        }
    }
}
