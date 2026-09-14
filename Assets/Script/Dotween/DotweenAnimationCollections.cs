using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[System.Serializable]
public enum AnimationType
{
    None,
    ScaleUp,
    ScaleRotation
}

public class DotweenAnimationCollections : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Setting Scale Value")]
    [SerializeField] private float targetScaleValue = 1.2f;
    [SerializeField] private float durationAnimation = 0.2f;

    [Header("Setting Rotate Value")]
    [SerializeField] private float targetRotateValue = 15f;

    [Header("Setting Animation")]
    [SerializeField] AnimationType animationType;
    [SerializeField] bool onHover;
    [SerializeField] bool onLoop;

    private Vector3 startScale;
    private Quaternion startRotation;

    private void Awake()
    {
        // Simpan nilai awal di Awake agar lebih aman
        startScale = transform.localScale;
        startRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        // Jika mode Loop diaktifkan, jalankan loop saat objek aktif
        if (onLoop)
        {
            //PlayLoopAnimation();
        }
    }

    private void OnDisable()
    {
        // Bersihkan tween jika objek di-disable agar tidak memory leak
        transform.DOKill();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onHover && !onLoop)
        {
            PlayAnimationDotween(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onHover && !onLoop)
        {
            PlayAnimationDotween(false);
        }
    }


    public void PlayAnimationDotween(bool x)
    {
        transform.DOKill();
        switch (animationType)
        {
            case AnimationType.None:
                break;
            case AnimationType.ScaleUp:
                ScaleUpAnimation(x);
                break;
            case AnimationType.ScaleRotation:
                ScaleRotationAnimation(x);
                break;
        }
    }


    private void ScaleUpAnimation(bool x)
    {
        if (x)
        {
            transform.DOScale(startScale * targetScaleValue, durationAnimation).SetUpdate(true);
        }
        else
        {
            transform.DOScale(startScale, durationAnimation).SetUpdate(true);
        }
    }

    private void ScaleRotationAnimation(bool x)
    {
        if (x)
        {
            Sequence s = DOTween.Sequence();
            float randomZ = Random.Range(-targetRotateValue, targetRotateValue);

            s.Append(transform.DOScale(startScale * targetScaleValue, durationAnimation))
             .Join(transform.DORotate(new Vector3(0, 0, randomZ), durationAnimation))
             .SetUpdate(true);
        }
        else
        {
            Sequence s = DOTween.Sequence();

            // Kembalikan skala DAN rotasi ke posisi awal
            s.Append(transform.DOScale(startScale, durationAnimation))
             .Join(transform.DORotateQuaternion(startRotation, durationAnimation))
             .SetUpdate(true);
        }
    }

}
