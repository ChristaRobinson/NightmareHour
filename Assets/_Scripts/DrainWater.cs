using System.Collections;
using UnityEngine;

public class DrainWater : MonoBehaviour
{
    [SerializeField] float lowerDuration = 5f;
    [SerializeField] float shrinkDuration = 5f;
    [SerializeField] float targetY = -0.36f;

    public bool isDrained { get; private set; }
    bool draining;

    
    public void StartDraining()
    {
        if (draining || isDrained) return;
        draining = true;
        StartCoroutine(DrainRoutine());
    }

    IEnumerator DrainRoutine()
    {
        yield return StartCoroutine(LowerRoutine());
        yield return StartCoroutine(ShrinkRoutine());
        isDrained = true;
        draining = false;
    }

    IEnumerator LowerRoutine()
    {
        float startY = transform.localPosition.y;
        for (float t = 0f; t < lowerDuration; t += Time.deltaTime)
        {
            Vector3 p = transform.localPosition;
            p.y = Mathf.Lerp(startY, targetY, t / lowerDuration);
            transform.localPosition = p;
            yield return null;
        }
        Vector3 end = transform.localPosition;
        end.y = targetY;
        transform.localPosition = end;
    }
// use gloabal scale

    IEnumerator ShrinkRoutine()
    {
        Vector3 startScale = transform.lossyScale;
        for (float t = 0f; t < shrinkDuration; t += Time.deltaTime)
        {
            transform.localScale = Vector3.Scale(transform.localScale, Vector3.Lerp(Vector3.one, Vector3.zero, t / shrinkDuration));
            yield return null;
        }
        //transform.lossyScale = Vector3.zero;
    }
}
