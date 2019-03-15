using System;

public static class ActionExtensions
{
    public static void CallActionIfNotNull(this Action action)
    {
        if (action != null)
            action();
    }

    public static void CallActionIfNotNull<T>(this Action<T> action, T value)
    {
        if (action != null)
            action(value);
    }
}
