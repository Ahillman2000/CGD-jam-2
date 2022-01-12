using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    public IEnumerator Shake(bool shaking, float magnitude)
    {
        Vector3 OriginalPos = transform.localPosition;

        if(shaking)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, OriginalPos.z);

            yield return null;
        }
        else
        {
            transform.localPosition = OriginalPos;
        }
    }
}
