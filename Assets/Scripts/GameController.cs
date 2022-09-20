using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;
    public static GameController self
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameController>();

            return instance;
        }
    }


    [SerializeField] private TextMeshProUGUI startText = null;
    [SerializeField] private TextMeshProUGUI endText = null;

    public static bool StartGame => self.startGame;
    private bool startGame = false;

    public static bool End => self.end;
    private bool end = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (!startGame)
            {
                startGame = true;

                startText.gameObject.SetActive(false);
            }

            if (end)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    public static void Finish()
    {
        self.end = true;

        self.endText.gameObject.SetActive(true);
    }
}
