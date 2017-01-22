using UnityEngine;
using System.Collections;

public class FishParallax : MonoBehaviour {

    public float LayerSize;
    public float LayerSeparation;
    public float InitialPostionOffsetY;
    public GameObject[] layerReference;
    public int spritesPerLayer;
    private GameObject[,] spritesLayers;
    private float[] layerOffsetTime;

    public float GlobalVelocity;

    private TrigonometriaClass TrigClass;

    // Use this for initialization
    void Start() {
        TrigClass = new TrigonometriaClass();
        TrigClass.init();

        spritesLayers = new GameObject[layerReference.Length, spritesPerLayer];
        layerOffsetTime = new float[layerReference.Length];
        SetPositionScene();
    }
	
	// Update is called once per frame
	void Update () {
        LoopLayer();
        Repositioning();
    }

    private void SetPositionScene()
    {
        for (int layer = 0; layer < layerReference.Length; layer++)
        {
            layerOffsetTime[layer] = Mathf.PI * layer * 0.33f;
            for (int waveIndex = 0; waveIndex < spritesPerLayer; waveIndex++)
            {
                spritesLayers[layer, waveIndex] = Instantiate<GameObject>(layerReference[layer]);

                Vector3 tmpPos = spritesLayers[layer, waveIndex].transform.position;
                tmpPos.x = (waveIndex - 2) * 10;
                tmpPos.y = layer * LayerSeparation - InitialPostionOffsetY;
                spritesLayers[layer, waveIndex].transform.position = tmpPos;
            }
        }
    }

    private void LoopLayer()
    {
        for (int layer = 0; layer < layerReference.Length; layer++)
        {
            layerOffsetTime[layer] += Time.deltaTime * 5.0f;
            for (int waveIndex = 0; waveIndex < spritesPerLayer; waveIndex++)
            {
                Vector3 tmpPos = spritesLayers[layer, waveIndex].transform.position;
                tmpPos.x += Time.deltaTime * GlobalVelocity;
                spritesLayers[layer, waveIndex].transform.position = tmpPos;
            }
        }
    }

    /// <summary>
    /// Repositioning the assert in the scene
    /// </summary>
    private void Repositioning()
    {
        for (int layer = 0; layer < layerReference.Length; layer++)
        {
            for (int waveIndex = 0; waveIndex < spritesPerLayer; waveIndex++)
            {
                Vector3 tmpPos = spritesLayers[layer, waveIndex].transform.position;
                if (tmpPos.x > 30.0f)
                {
                    tmpPos.x = spritesLayers[layer, waveIndex - 1 < 0 ? spritesPerLayer - 1 : waveIndex - 1].transform.position.x - 40;
                }
                spritesLayers[layer, waveIndex].transform.position = tmpPos;
            }
        }
    }
}
