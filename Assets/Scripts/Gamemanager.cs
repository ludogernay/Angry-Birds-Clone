using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    private int _alivePiggies;
    void Start()
    {
        _alivePiggies = FindObjectsOfType<Piggies>().Length;
    }
    public void IsPiggieDied()
    {
        _alivePiggies--;
        if (_alivePiggies <= 0)
        {
            NextScene();
        }
    }
    public void NextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
