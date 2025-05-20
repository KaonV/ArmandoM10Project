using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SlidingPanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    private Coroutine closeRoutine;
    private bool isPointerInside = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;

        // Para fechamento programado se ainda estiver aguardando
        if (closeRoutine != null)
        {
            StopCoroutine(closeRoutine);
            closeRoutine = null;
        }

        animator.SetBool("Aberto", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerInside = false;

        // Espera um tempo antes de fechar, evitando falso positivo
        closeRoutine = StartCoroutine(DelayedClose());
    }

    private IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(1f); // pequeno delay
        if (!isPointerInside)
        {
            animator.SetBool("Aberto", false);
        }
    }
}
