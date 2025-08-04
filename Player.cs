using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public List<Card> Hand = new List<Card>();
    public List<Card> FaceUp = new List<Card>();
    public List<Card> FaceDown = new List<Card>();

    public void Draw(Card card) {
        Hand.Add(card);
    }

    public void Play(List<Card> cards) {
        // TODO: בדיקת חוקיות והעברת קלפים לשולחן
    }
}
