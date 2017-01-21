using UnityEngine;
using System.Collections;

public class AmbientParallax : MonoBehaviour {

    public GameObject[] WavesRef;
    public int WavesPerLayer;
    private GameObject[,] WavesLayers;
    private float[] WavesOffsetTime;

    public float GlobalVelocity;

    private TrigonometriaClass TrigClass;

	// Use this for initialization
	void Start () {
        TrigClass = new TrigonometriaClass();
        TrigClass.init();
        // INSTANTIATE THE WAVES
        {
            WavesLayers = new GameObject[WavesRef.Length, WavesPerLayer];
            WavesOffsetTime = new float[WavesRef.Length];
            for(int layer = 0; layer < WavesRef.Length; layer++)
            {
                WavesOffsetTime[layer] = Mathf.PI * layer;
                for(int waveIndex = 0; waveIndex<WavesPerLayer; waveIndex++)
                {
                    WavesLayers[layer, waveIndex] = Instantiate<GameObject>(WavesRef[layer]);

                    Vector3 tmpPos = WavesLayers[layer, waveIndex].transform.position;
                    tmpPos.x = (waveIndex - 2) * 7;
                    tmpPos.y = layer * 0.85f - 4.0f;
                    WavesLayers[layer, waveIndex].transform.position = tmpPos;
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
        // MOVE THE WAVES
        {
            for (int layer = 0; layer < WavesRef.Length; layer++)
            {
                WavesOffsetTime[layer] += Time.deltaTime * 5.0f;
                for (int waveIndex = 0; waveIndex < WavesPerLayer; waveIndex++)
                {
                    Vector3 tmpPos = WavesLayers[layer, waveIndex].transform.position;
                    tmpPos.x -= Time.deltaTime * GlobalVelocity + +TrigClass.precalculatedSinRad(WavesOffsetTime[layer]) * 0.05f;

                    if(tmpPos.x < -15.0f)
                    {
                        tmpPos.x = WavesLayers[layer, waveIndex-1 < 0 ? WavesPerLayer-1 : waveIndex-1].transform.position.x + 7;
                    }

                    tmpPos.y = layer * 0.85f - 4.0f + TrigClass.precalculatedSinRad(WavesOffsetTime[layer]) * 0.25f;
                    WavesLayers[layer, waveIndex].transform.position = tmpPos;
                }
            }
        }
    }
}
