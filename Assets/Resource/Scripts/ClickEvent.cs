using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerEnterHandler
    , IPointerExitHandler
{
    [SerializeField] GameObject Target_Dialogue;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] Player3d_Planet playercamera;
    public bool CanClick = false;

    public LoveGameManager lovegameManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        lovegameManager = FindObjectOfType<LoveGameManager>();
    }
    public void ClickSlime()
    {
        if (CanClick)
        {
            //최초에 한번만 클릭할수있게하고,클릭하면 대화창 뜨고(카메라 조작안되게)
            Target_Dialogue.SetActive(true);
            playercamera.IsMoved = false;
            CanClick = false;
            lovegameManager.activeConversationIndex++;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CanClick)
        {
            //클릭 가능한 경우에만 호버효과뜨게
            GetComponent<SpriteRenderer>().color = new Color(200f / 255f, 250f / 255f, 250f / 255f);
            Debug.Log("Enter");
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        Debug.Log("Exit");
    }
}
