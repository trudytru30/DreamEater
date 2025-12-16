using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class PuertasNiveles : MonoBehaviour
{
    [SerializeField] private int nextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        GameManager.Instance.ChangeNextLevelLoading(nextScene);
        SceneManager.LoadScene("LoadingScreen");
    } 
}
