using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour, IPointerClickHandler {
    public Image frontImage;     // יש לגרור פה את התמונה של צד הקלף
    public Image backImage;      // ויש את התמונה של הגב
    [HideInInspector] public Card data;
    [HideInInspector] public GameManager manager;
    public bool isFaceUp;

    public void Initialize(Card cardData, GameManager mgr, bool startFaceUp) {
        data = cardData;
        manager = mgr;
        isFaceUp = startFaceUp;
        Refresh();
    }

    public void Refresh() {
        frontImage.sprite = manager.GetCardSprite(data.Value, data.CardSuit);
        frontImage.gameObject.SetActive(isFaceUp);
        backImage.gameObject.SetActive(!isFaceUp);
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (manager.CanSelect(this)) {
            manager.ToggleSelect(this);
        }
    }
}
