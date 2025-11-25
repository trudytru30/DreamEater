using UnityEngine;

[System.Serializable]

public class Movement
{
    public bool canMove;
    public float speedMultiplier = 1f;//multiplicador de velocidad

    //getter y setter de canMove
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }
    
    //walk
    public void Walk()
    {
        if (!canMove)
        {
            speedMultiplier = 0f; //si es false no anda
            return;
        }
        speedMultiplier = 1f;
        
    }

    //run
    public void Run()
    {
        if (!canMove)
        {
            speedMultiplier = 0f; //si es false np cprre
            return;
        }
        speedMultiplier = 1.6f;
    }
}