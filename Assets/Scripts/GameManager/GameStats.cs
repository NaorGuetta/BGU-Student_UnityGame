using UnityEngine;

public class GameStats
{
    private int _maxStatValue = 32;
    private int _startingValue = 15;

    public int Health { get; private set; }
    public int Money { get; private set; }
    public int Friends { get; private set; }
    public int Grades { get; private set; }

    public float HealthPercentage => (float)Health / _maxStatValue;
    public float MoneyPercentage => (float)Money / _maxStatValue;
    public float FriendsPercentage => (float)Friends / _maxStatValue;
    public float GradesPercentage => (float)Grades / _maxStatValue;

    public void ApplyModification(CardStats mod)
    {
        Health = ClampValue(Health + mod.health);
        Money = ClampValue(Money + mod.money);
        Friends = ClampValue(Friends + mod.friends);
        Grades = ClampValue(Grades + mod.grades);
        Debug.Log("Health " + Health);
        Debug.Log("Money " + Money);
        Debug.Log("Friends " + Friends);
        Debug.Log("Grades " + Grades);

    }

    public void ResetStats(int numOfGames)
    {
        _startingValue = _startingValue + numOfGames;
        ApplyStartingValues();
    }

    private void ApplyStartingValues()
    {
        Health = ClampValue(_startingValue);
        Money = ClampValue(_startingValue);
        Friends = ClampValue(_startingValue);
        Grades = ClampValue(_startingValue);
    }

    private int ClampValue(int value)
    {
        return Mathf.Clamp(value, 0, _maxStatValue);
    }

    public int CheckStatThreshold()
    {
        if (Health <= 0)
            return 1; // Health is below or equal to 0
        if (Money <= 0)
            return 2; // Money is below or equal to 0
        if (Friends <= 0)
            return 3; // Friends is below or equal to 0
        if (Grades <= 0)
            return 4; // Grades is below or equal to 0

        return -1; // All stats are above 0
    }
}
