using UnityEngine;
using System.Collections;

public class TrigonometriaClass
{
    private int tableSize = 4096;//65536/4; // or 1024
    private float[] mSin;
	private float[] mCos;
    private float[] mSinRad;
    private float[] mCosRad;
    private float salto;
	private const float MAX_RAD = 2 * Mathf.PI;

	// Use this for initialization
	public void init ()
	{
		// PRE CALCULATE EVERY VALUE WE COULD NEED FOR EVERY FUNCTION
		mCos = new float[tableSize];
		mSin = new float[tableSize];
        mCosRad = new float[tableSize];
        mSinRad = new float[tableSize];
        salto = tableSize / (2 * Mathf.PI);

		float angle = 0.0f;
        float angleDiff = 2 * Mathf.PI / tableSize;
        for (int i=0; i<tableSize; i++) {
            mSinRad[i] = Mathf.Sin(angle);
            mCosRad[i] = Mathf.Cos(angle);
            mSin [i] = mSinRad[i] * Mathf.Rad2Deg;
			mCos [i] = mCosRad[i] * Mathf.Rad2Deg;
			angle += angleDiff;
		}
	}

    public float precalculatedSinRad(float rad)
    {
        if (rad < 0.0f)
        {
            do
            {
                rad += 2 * Mathf.PI;
            } while (rad < 0.0f);
        }
        else if (rad > 2 * Mathf.PI)
        {
            do
            {
                rad -= 2 * Mathf.PI;
            } while (rad > 2 * Mathf.PI);
        }
        return mSinRad[(int)(rad * salto)];
    }

    public float precalculatedSinDeg (float rad)
	{
		if (rad < 0.0f) {
			do {
				rad += 2 * Mathf.PI;
			} while(rad < 0.0f);
		} else if (rad > 2 * Mathf.PI) {
			do {
				rad -= 2 * Mathf.PI;
			} while(rad > 2*Mathf.PI);
		}
		return mSin [(int)(rad * salto)];
	}

	public float precalculatedCosDeg (float rad)
	{
		if (rad < 0.0f) {
			do {
				rad += 2 * Mathf.PI;
			} while(rad < 0.0f);
		} else if (rad > 2 * Mathf.PI) {
			do {
				rad -= 2 * Mathf.PI;
			} while(rad > 2*Mathf.PI);
		}
		return mCos [(int)(rad * salto)];
	}

    public float precalculatedCosRad(float rad)
    {
        if (rad < 0.0f)
        {
            do
            {
                rad += 2 * Mathf.PI;
            } while (rad < 0.0f);
        }
        else if (rad > 2 * Mathf.PI)
        {
            do
            {
                rad -= 2 * Mathf.PI;
            } while (rad > 2 * Mathf.PI);
        }
        return mCosRad[(int)(rad * salto)];
    }
}
