using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    public Animator transitionAnimator;

    void Awake()
    {
        StartCoroutine(DoTransition());
    }

    private IEnumerator DoTransition()
    {
        yield return new WaitForSeconds(2f);
        GameManager.singleton.isPaused = false;
    }
}
