using System;

public enum Difficulty
{
    Easy, Medium, Hard
}

public class Options
{
    public static Difficulty difficulty = Difficulty.Medium;

    public static int GetCatHealth()
    {
        switch (difficulty)
        {
             case Difficulty.Easy:
                 return 20;
             case Difficulty.Medium:
                 return 30;
             case Difficulty.Hard:
                 return 40;
             default:
                 throw new ArgumentOutOfRangeException();
        }
    }
}
