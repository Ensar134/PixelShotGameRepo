using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ImageAnimScript : MonoBehaviour
{
    public RectTransform bossLevelText;

    private void OnEnable()
    {
        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        bossLevelText.DOAnchorPos(new Vector2(0, 400), 0.5f);
        yield return new WaitForSecondsRealtime(2f);
        bossLevelText.DOAnchorPos(new Vector2(1500, 400), 0.5f);
    }
}
