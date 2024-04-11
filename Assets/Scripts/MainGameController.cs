using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController: MonoBehaviour
{
    void Start()
    {
        // We can load level scenes additively so we have multiple scenes loaded at once
        SceneManager.LoadScene("Example Minigame", LoadSceneMode.Additive);
    }
}