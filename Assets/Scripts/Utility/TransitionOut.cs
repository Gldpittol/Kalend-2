using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionOut : MonoBehaviour
{
    public GameObject transitionImage;
    public SpriteRenderer sr;
    public float i;
    public float duration = 0.5f;

    private void Start()
    {
        i = 1;
        transitionImage.SetActive(true);
    }

    private void Update()
    {
        if (i < 0) Destroy(this.gameObject);

        i -= Time.deltaTime / duration;
        sr.color = new Color(0, 0, 0, i);
    }
}
