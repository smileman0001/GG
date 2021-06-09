using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanel : MonoBehaviour
{
    public Transform box;
    public CanvasGroup Back;

    private void OnEnable()
    {
        Back.alpha = 0;
        Back.LeanAlpha(1, 0.5f);

        box.localPosition = new Vector2(-239.25f, +Screen.height);
        box.LeanMoveLocalY(0, -100f).setEaseOutExpo().delay = 0.1f;
    }


    public void CloseDialog()
    {
        Back.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(Complete);
    }

    void Complete()
    {
        gameObject.SetActive(false);
    }
}