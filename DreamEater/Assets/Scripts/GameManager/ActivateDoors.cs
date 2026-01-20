using UnityEngine;

public class ActivateDoors : MonoBehaviour
{
    [Header("Door")]
    private int _doorsUnlocked;
    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;
    [SerializeField] private GameObject door3;
    [SerializeField] private GameObject door4;
    [SerializeField] private GameObject door5;

    [Header("Enemy")]
    [SerializeField] private GameObject enemy1;
    [SerializeField] private GameObject enemy2;
    [SerializeField] private GameObject enemy3;
    [SerializeField] private GameObject enemy4;

    [Header("Triggers")]
    [SerializeField] private GameObject trigger1;
    [SerializeField] private GameObject trigger2;
    [SerializeField] private GameObject trigger3;
    [SerializeField] private GameObject trigger4;
    [SerializeField] private GameObject trigger5;

    [Header("Animators")]
    [SerializeField] private Animator[] doorAnim;
    
    private void Start()
    {
        door1.SetActive(true);
        door2.SetActive(false);
        door3.SetActive(false);
        door4.SetActive(false);
    }

    private void OnEnable()
    {
        _doorsUnlocked = GameManager.Instance.unlockedDoors;

        DeactivateTrigger();
        DeactivateAnimators();
        DeactivateEnemies();

        switch (_doorsUnlocked)
        {
            case 1:
                door1.SetActive(true);
                trigger1.SetActive(true);
                doorAnim[0].enabled = true;
                doorAnim[1].enabled = true;
                enemy1.SetActive(true);
                break;
            case 2:
                door2.SetActive(true);
                door1.SetActive(true);
                trigger2.SetActive(true);
                doorAnim[2].enabled = true;
                doorAnim[3].enabled = true;
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                break;
            case 3:
                door3.SetActive(true);
                door2.SetActive(true);
                door1.SetActive(true);
                trigger3.SetActive(true);
                doorAnim[4].enabled = true;
                doorAnim[5].enabled = true;
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                enemy3.SetActive(true);
                break;
            case 4:
                door4.SetActive(true);
                door3.SetActive(true);
                door2.SetActive(true);
                door1.SetActive(true);
                trigger4.SetActive(true);
                doorAnim[6].enabled = true;
                doorAnim[7].enabled = true;
                enemy1.SetActive(true);
                enemy2.SetActive(true);
                enemy3.SetActive(true);
                enemy4.SetActive(true);
                break;
            case 5:
                door5.SetActive(true);
                door4.SetActive(true);
                door3.SetActive(true);
                door2.SetActive(true);
                door1.SetActive(true);
                trigger5.SetActive(true);
                doorAnim[8].enabled = true;
                doorAnim[9].enabled = true;
                break;
            default:
                break;
        }
    }

    private void DeactivateTrigger()
    {
        trigger1.SetActive(false);
        trigger2.SetActive(false);
        trigger3.SetActive(false);
        trigger4.SetActive(false);
        trigger5.SetActive(false);
    }
    
    private void DeactivateAnimators()
    {
        for(int i = 0; i<doorAnim.Length; i++)
        {
            doorAnim[i].enabled = false;
        }
    }
    
    private void DeactivateEnemies()
    {
        enemy1.SetActive(false);
        enemy2.SetActive(false);
        enemy3.SetActive(false);
        enemy4.SetActive(false);
    }
}