using UnityEngine;

[System.Serializable]
public class CardStats
{
    public int grades;
    public int health;
    public int friends;
    public int money;   

    public CardStats(int grades, int health, int friends, int money)
    {
        this.grades = grades;
        this.health = health;
        this.friends = friends;
        this.money = money;
    }
    public CardStats() { }
}
