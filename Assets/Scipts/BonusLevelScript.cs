using UnityEngine;
using DG.Tweening;
using System.Collections;

public class BonusLevelScript : MonoBehaviour
{
    public RectTransform bonusLevelText;
    public ParticleSystem confetiParticle;

    private void OnEnable()
    {
        ObjectPooler.Instance.SpawnFromPoolSecond("ConfettiParticle", gameObject.transform.position, Quaternion.identity);
        StartCoroutine(WaitForEnd());
    }

    IEnumerator WaitForEnd()
    {
        bonusLevelText.DOAnchorPos(new Vector2(0, 400), 0.5f);
        yield return new WaitForSecondsRealtime(2f);
        bonusLevelText.DOAnchorPos(new Vector2(1500, 400), 0.5f);
    }
}