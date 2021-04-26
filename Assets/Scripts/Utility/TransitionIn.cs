using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionIn : MonoBehaviour
{
    public GameObject transitionImage;
    public SpriteRenderer sr;
    public float i;
    public float duration = 0.5f;

    public static bool transitionDone;

    private void Start()
    {
        i = 0;
        transitionImage.SetActive(true);
        transitionDone = false;
    }

    private void Update()
    {
        if (i > 1) transitionDone = true;
        else transitionDone = false;

        i += Time.deltaTime / duration;
        sr.color = new Color(0, 0, 0, i);
    }
}
