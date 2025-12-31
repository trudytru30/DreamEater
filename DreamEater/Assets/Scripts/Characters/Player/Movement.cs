/*
 Gestiona el movimiento del player 
 Se llaman desde playerController2
*/

[System.Serializable]

public class Movement
{
    public bool canMove=true;
    public float speedMultiplier = 1f;//multiplicador de velocidad

    //getter y setter de canMove
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }
    
    public void Walk()
    {
        if (!canMove)
        {
            speedMultiplier = 0f; //si es false no anda
            return;
        }
        speedMultiplier = 1f;
        
    }
    
    public void Run()
    {
        if (!canMove)
        {
            speedMultiplier = 0f; //si es false np corre
            return;
        }
        speedMultiplier = 1.3f;
    }
}