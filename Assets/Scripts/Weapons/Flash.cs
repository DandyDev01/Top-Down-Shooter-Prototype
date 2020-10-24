using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flash : MonoBehaviour
{
    [SerializeField] float fadeTime;
    Light2D flash_m;

    private void Start()
    {
        flash_m = GetComponent<Light2D>();
        StartCoroutine(Flash_m());
    }


    private IEnumerator Flash_m()
    {
        //// increase radius fast
        //for (float i = 0; i < flashRadius; i += 0.1f)
        //{
        //    flash_m.pointLightOuterRadius = i;
        //    yield return new WaitForSeconds(0.01f);
        //}

        //// make radius small slower
        //// increase radius fast
        //for (float i = flashRadius; i > 0; i -= 0.1f)
        //{
        //    flash_m.pointLightOuterRadius = i;
        //    yield return new WaitForSeconds(0.05f);
        //}

        for (float i = 1.5f; i > 0; i -= 0.05f)
        {
            flash_m.intensity = i;
            yield return new WaitForSeconds(fadeTime);
        }

        Destroy(gameObject);
    }
}
