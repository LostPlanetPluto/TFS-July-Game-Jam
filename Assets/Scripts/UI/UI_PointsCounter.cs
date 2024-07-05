using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.onAddPoint += DisplayScore;
        }
    }

    private void DisplayScore()
    {
        if (GameManager.instance == null) return;

        pointsText.text = GameManager.instance.GetPoints().ToString();
    }

    private void OnDestroy()
    {
        if (GameManager.instance == null) return;

        GameManager.instance.onAddPoint -= DisplayScore;
    }
}
