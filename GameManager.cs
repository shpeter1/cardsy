using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {
    [Header("Prefabs & Sprites")]
    public GameObject cardPrefab;
    public Sprite[] valueSprites;        // סדר: 3,4,5,...,A,2
    public Sprite heartsSpriteSheet;     // או מערך של 4 ספרייטים לפי סדר הסוויט
    public Transform playerHandArea;
    public Transform playerFaceUpArea;
    public Transform playerFaceDownArea;
    public Text statusText;
    public Text deckCountText;

    List<Card> deck;
    List<CardUI> selected = new List<CardUI>();

    void Start() {
        InitDeck();
        Shuffle(deck);
    }

    public void OnStartButton() {
        DealInitial();
        UpdateUI();
        statusText.text = "התורך";
    }

    void InitDeck() {
        deck = new List<Card>();
        foreach (Card.Suit s in System.Enum.GetValues(typeof(Card.Suit)))
            for (int v = 3; v <= 14; v++)  // נניח 11=J,12=Q,13=K,14=A,2=15
                deck.Add(new Card(v == 15? 2 : v, s));
    }

    void Shuffle<T>(List<T> list) {
        for (int i = list.Count - 1; i > 0; i--) {
            int j = Random.Range(0, i + 1);
            var tmp = list[i]; list[i] = list[j]; list[j] = tmp;
        }
    }

    void DealInitial() {
        // 3 סגורים, 3 פתוחים, 3 ביד
        for (int i = 0; i < 3; i++) {
            SpawnCard(deck.Pop(), playerFaceDownArea, false);
            SpawnCard(deck.Pop(), playerFaceUpArea, true);
            SpawnCard(deck.Pop(), playerHandArea, true);
        }
    }

    Card Pop(this List<Card> list) {
        var c = list[0];
        list.RemoveAt(0);
        return c;
    }

    void SpawnCard(Card cardData, Transform parent, bool faceUp) {
        GameObject go = Instantiate(cardPrefab, parent);
        var ui = go.GetComponent<CardUI>();
        ui.Initialize(cardData, this, faceUp);
    }

    public Sprite GetCardSprite(int value, Card.Suit suit) {
        // לדוגמה מחזיר ספרייט לפי value בלבד; תוכל למפות גם לפי suit
        int index = value - 3; // אם 3→0,...,A(14)→11,2(15)→12
        return valueSprites[index];
    }

    public bool CanSelect(CardUI ui) {
        // בודק אם חוקי (אותו ערך וכו')
        if (selected.Count == 0) return true;
        return selected.All(x => x.data.Value == ui.data.Value);
    }

    public void ToggleSelect(CardUI ui) {
        if (selected.Contains(ui)) {
            selected.Remove(ui);
            ui.GetComponent<Image>().color = Color.white;
        } else {
            selected.Add(ui);
            ui.GetComponent<Image>().color = Color.yellow;
        }
    }

    public void OnPlayButton() {
        // העבר הנבחרים לערימת התרכז
        foreach (var ui in selected) {
            Destroy(ui.gameObject);
            // כאן תוסיף לעיבוד הלוגי: put in pile, בדיקת חוקיות, refill hand וכו'
        }
        selected.Clear();
        UpdateUI();
    }

    void UpdateUI() {
        deckCountText.text = deck.Count.ToString();
        // תוכל להוסיף רענון נוסף במידת הצורך
    }
}
