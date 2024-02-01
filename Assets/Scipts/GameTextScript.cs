using DG.Tweening;
using UnityEngine;

public class GameTextScript : MonoBehaviour
{
    public Transform gameText;

    private void Start()
    {
        gameText.DOLocalMove(new Vector3(0, 0, 0), 7 * 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
