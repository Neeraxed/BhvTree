using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool gamePaused = false;

    public static bool GamePaused { get; private set; }

    private void Update()
    {
        if (GamePaused != gamePaused)
            GamePaused = gamePaused;
    }
}
