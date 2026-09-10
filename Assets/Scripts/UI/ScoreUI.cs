using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Target target;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform player;

    [Header("Textos")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text multiplierText;

    private void Update()
    {
        scoreText.text = "SCORE: " + target.TotalScore;

        float distance = Vector3.Distance(
            player.position,
            target.transform.position
        );

        distanceText.text = "DISTANCE: " + distance.ToString("F1") + " m";

        multiplierText.text = "×" + gameManager.CurrentMultiplier;
    }
}
