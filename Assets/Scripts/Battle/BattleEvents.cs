using System;
using SnIProductions;

public static class GameEvents
{
    public static Action OnBattleStarted;
    public static Action OnTurnStarted;
    public static Action OnTurnEnded;

    public static Action<BattleResult> OnBattleEnded;

    public static Action<Card> OnCardPlayed;
    public static Action OnDeckShuffled;
}