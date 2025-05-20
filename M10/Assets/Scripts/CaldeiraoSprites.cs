using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CaldeiraoHover : MonoBehaviour
{
    [SerializeField] private Image caldeiraoImage;
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private Sprite spriteHover;
    [SerializeField] private Sprite spriteCraft;

    private bool spriteTemporarioAtivo = false;

    public static class DragManager
    {
        public static bool ItemSendoArrastado = false;
    }

    private RectTransform caldeiraoRect;

    void Awake()
    {
        caldeiraoRect = GetComponent<RectTransform>();
    }

    public void MostrarSpriteCraftTemporario()
    {
        StartCoroutine(SpriteTemporario());
    }

    private IEnumerator SpriteTemporario()
    {
        spriteTemporarioAtivo = true;
        caldeiraoImage.sprite = spriteCraft;
        yield return new WaitForSeconds(1.5f);
        spriteTemporarioAtivo = false;
        AtualizarSpriteHoverOuNormal();
    }

    void Update()
    {
        if (spriteTemporarioAtivo)
            return;

        AtualizarSpriteHoverOuNormal();
    }

    private void AtualizarSpriteHoverOuNormal()
    {
        Vector2 mousePos = Input.mousePosition;
        bool mouseDentro = RectTransformUtility.RectangleContainsScreenPoint(caldeiraoRect, mousePos);

        if (mouseDentro && DragManager.ItemSendoArrastado)
        {
            caldeiraoImage.sprite = spriteHover;
        }
        else
        {
            caldeiraoImage.sprite = spriteNormal;
        }
    }
}
