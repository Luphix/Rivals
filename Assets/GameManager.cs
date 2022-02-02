using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    public static int FireLvP1 = 0;
    public static int FireLvP2 = 0;

    public static int IceLvP1 = 0;
    public static int IceLvP2 = 0;

    public static int WindLvP1 = 0;
    public static int WindLvP2 = 0;

    public static float HP1 = 600;
    public static float HP2 = 600;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Vulcao");
    }
}
