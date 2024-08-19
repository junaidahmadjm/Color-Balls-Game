using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public GameObject MainMenuUI;
    public bool gameState;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        gameState = false;
    }

    public void startTheGame()
    {
        gameState = true;
        MainMenuUI.SetActive(false);
        GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>().Play();
    }
}
