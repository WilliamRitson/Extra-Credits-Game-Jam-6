
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public float delayTime = 1.5f;

    private bool hovering = false;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        StartCoroutine(ActivateTooltipAfterDelay());
    }

    private IEnumerator ActivateTooltipAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        if (hovering)
            tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        tooltip.SetActive(false);
    }
}
