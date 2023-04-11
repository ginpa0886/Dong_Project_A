using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static _Enums;

public class Card_Prefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("TEXT")]
    [SerializeField] Text m_CardIngameType_Text;
    [SerializeField] Text m_CardName_Text;
    [SerializeField] Text m_CardCost_Text;
    [SerializeField] Text m_CardDesc_Text;    

    [Space(10f)]
    [Header("IMAGE")]
    [SerializeField] Image m_CardName_Img;
    [SerializeField] Image m_Card_Img;

    [Space(10f)]
    [Header("BUTTTON")]
    [SerializeField] Button m_Card_Btn;

    [Space(10f)]
    [Header("ANIMATOR")]
    [SerializeField] Animator m_Card_Animator;

    Card_Data m_CardData;

    public Card_Data Get_CardData { get { return m_CardData; } }

    WaitForSeconds m_Delete;

    void Start()
    {
        m_Card_Btn.onClick.AddListener(Use_Card);
        m_Delete = new WaitForSeconds(0.1f);
    }

    public void Set_CardData(Card_Data c_Data)
    {
        this.gameObject.SetActive(true);
        m_CardData = c_Data;
        Update_CardUI(m_CardData);
    }

    #region CARD UI
    void Update_CardUI(Card_Data c_Data)
    {
        if(c_Data == null)
        {
            return;
        }

        m_CardIngameType_Text.text = c_Data.Get_IngameType.ToString();
        m_CardName_Text.text       = c_Data.Get_Name.ToString();
        m_CardCost_Text.text       = c_Data.Get_Cost.ToString();
        m_CardDesc_Text.text       = c_Data.Get_Desc.ToString();

        m_CardName_Img.sprite = null;
        m_Card_Img.sprite     = null;
    }

    #endregion

    #region Interface
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Card_Btn.image.color = m_Card_Btn.colors.highlightedColor;
        m_Card_Animator.Play("Card_Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Card_Btn.image.color = m_Card_Btn.colors.normalColor;
        m_Card_Animator.Play("Card_Exit");
    }

    #endregion

    void Use_Card()
    {
        // 차례에 대한 검사 진행
        if(GameManager.Instance.InGame.Turn.Request_UserPlaySomething() == false)
        {
            Debug.Log("지금 내 차례가 아니야...");
            return;
        }

        // 코스트에 대한 검사 진행
        if (GameManager.Instance.InGame.Play.Request_UseEnergy(m_CardData.Get_Cost) == true)
        {
            GameManager.Instance.InGame.Cards.Abandon(m_CardData);
            return;
        }

        Debug.Log("에너지가 부족해...");
        return;
    }

    public void Delete_CardData()
    {
        if(m_DeleteCoroutine != null)
        {
            StopCoroutine(m_DeleteCoroutine);
        }
        m_DeleteCoroutine = StartCoroutine(Co_Delete_CardData());
    }

    Coroutine m_DeleteCoroutine;

    IEnumerator Co_Delete_CardData()
    {
        m_Card_Animator.Play("Card_Exit");

        yield return m_Delete;

        m_CardData = null;
        this.gameObject.SetActive(false);
    }
}
