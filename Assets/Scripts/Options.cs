using System;

public enum Difficulty
{
    Easy, Medium, Hard, Hyper
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
            case Difficulty.Hyper:
                return 50;
             default:
                 throw new ArgumentOutOfRangeException();
        }
    }
}
