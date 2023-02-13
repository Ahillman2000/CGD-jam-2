using UnityEngine;

public class Despawn : MonoBehaviour
{
    MeshRenderer mesh_renderer;

    Material[] materials;
    
    float timeElapsed;
    float lerpDuration;
    float startValue = 0;
    float endValue = 1;
    float valueToLerp;
    float DissolveDelay = 1.5f;
    private void Awake()
    {
        mesh_renderer = gameObject.GetComponent<MeshRenderer>();

        if(mesh_renderer != null)
        {
            materials = mesh_renderer.materials;
            
        }

        lerpDuration = (Random.Range(6.6f, 14.1f) - DissolveDelay);

    }

    
    void Update()
    {
        if (materials[0].shader != null)
        {
            DissolveDelay -= Time.deltaTime;

            if (DissolveDelay <= 0)
            {
                if (timeElapsed < lerpDuration)
                {
                    valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;

                    foreach (Material mat in materials)
                    {
                        mat.SetFloat("DissolveAmount", valueToLerp);
                    }
                }
                else
                {
                    valueToLerp = endValue;
                }



                if (valueToLerp == endValue || gameObject.transform.position.y <= -50)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
