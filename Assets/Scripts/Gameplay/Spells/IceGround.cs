using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IceGround : MonoBehaviour
{
    public float fadeDuration;
    public float i = 2;
    public SpriteRenderer sr;


    private void Update()
    {
        i -= Time.deltaTime / fadeDuration;

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, i);
        if (i < 0) Destroy(this.gameObject);
    }
}
