using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAvatar : MonoBehaviour {
    public float offsetX;
    public float offsetY;

    public GameObject renacuajoRef;
    public GameObject ranaRef;
    public GameObject salamandraRef;

    public GameObject Actual;

    public GameObject Evolution;

    private TrigonometriaClass TrigClass;
    private float time;

    // Use this for initialization
    void Start () {
        TrigClass = new TrigonometriaClass();
        TrigClass.init();

        Actual = renacuajoRef;

        time = 0;
	}

    public void Reset()
    {
        Vector3 tmpPos = Actual.transform.position;
        tmpPos.x = -20.0f;
        tmpPos.y = 0.0f;
        Actual.transform.position = tmpPos;

        Actual = renacuajoRef;
        time = 0;

        offsetX = 0;
    }

    public void PrimeraEvolucion()
    {
        offsetX += 2.0f;
        Vector3 tmpPos = Evolution.transform.position;
        tmpPos.x = offsetX;
        Evolution.transform.position = tmpPos;
        Evolution.GetComponentInChildren<Animator>().Play("Evolucion",0,0.0f);

        GetComponent<AudioSource>().Play();

        Invoke("ChangeOne", 1.0f);
    }

    void ChangeOne()
    {
        Vector3 tmpPos = Actual.transform.position;
        tmpPos.x = -20.0f;
        tmpPos.y = 0.0f;
        Actual.transform.position = tmpPos;

        Actual = ranaRef;
    }

    public void SegundaEvolucion()
    {
        offsetX += 2.0f;
        Vector3 tmpPos = Evolution.transform.position;
        tmpPos.x = offsetX;
        Evolution.transform.position = tmpPos;
        Evolution.GetComponentInChildren<Animator>().Play("Evolucion", 0, 0.0f);

        GetComponent<AudioSource>().Play();

        Invoke("ChangeTwo", 1.0f);
    }

    void ChangeTwo()
    {
        Vector3 tmpPos = Actual.transform.position;
        tmpPos.x = -20.0f;
        tmpPos.y = 0.0f;
        Actual.transform.position = tmpPos;

        Actual = salamandraRef;
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime * 5;
        Vector3 tmpPos = Actual.transform.position;
        tmpPos.x = offsetX;
        tmpPos.y = offsetY + TrigClass.precalculatedSinRad(time) * 0.125f;
        Actual.transform.position = tmpPos;
    }

    public bool isDead()
    {
        return offsetX < -5.0f;
    }
}
