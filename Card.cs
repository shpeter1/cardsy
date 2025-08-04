public class Card {
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public int Value;
    public Suit CardSuit;
    public bool IsSpecial;

    public Card(int value, Suit suit) {
        Value = value;
        CardSuit = suit;
        IsSpecial = (value == 2 || value == 4 || value == 7 || value == 10);
    }
}
