using System;

/*
 Subject del patrón Observer para notificar cambios de estado en los switches.
*/
public static class SwitchSubject
{
    public static event Action<Switch> OnSwitchStateChanged;

    public static void Notify(Switch changedSwitch)
    {
        OnSwitchStateChanged?.Invoke(changedSwitch);
    }
}