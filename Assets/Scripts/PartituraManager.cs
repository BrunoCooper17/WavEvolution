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

        // MOVE THE NOTES (WAVE)
        {
            Vector3 tmpPos = Partituras[PartituraActual].transform.position;
            tmpPos.x -= velocity * Time.deltaTime;
            Partituras[PartituraActual].transform.position = tmpPos;
        }

        // CHECK IF THE WAVE (PARTITURE) HAS ENDED
        int notes = Partituras[PartituraActual].transform.childCount;
        bool bLoadNext = true;
        for (int index = 0; index < notes; index++)
        {
            if(Partituras[PartituraActual].transform.GetChild(index).position.x > -7.0f)
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

    protected void LoadPartiture()
    {
        Vector3 tmpPos = Partituras[lastPartiture].transform.position;
        tmpPos.x = -10000;
        Partituras[lastPartiture].transform.position = tmpPos;

        tmpPos = Partituras[PartituraActual].transform.position;
        tmpPos.x = 10.0f;
        Partituras[PartituraActual].transform.position = tmpPos;

        bWaitNextWave = false;
    }
}
