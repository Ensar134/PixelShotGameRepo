using DG.Tweening;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        //transform.DOLocalRotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        transform.Rotate(0, 13, 0);
    }
}
