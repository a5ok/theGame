using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents a Graph used to print results in a nice way
/// </summary>
/// <remarks>Modify this AND the DataPoint prefab in the inspector to change everything you need</remarks>
public class Graph : MonoBehaviour
{
    /// <summary>
    /// The array of DataPoints used to print the results
    /// </summary>
    /// <see cref="DataPoint"/>
    public DataPoint[] Data;
    /// <summary>
    /// Prefab of the DataPoint that will be cloned for each
    /// DataPoint
    /// </summary>
    public GameObject DataPointPrefab;
    /// <summary>
    /// Padding between each bar
    /// </summary>
    public float padding;
    /// <summary>
    /// Width of a DataPoint bar
    /// </summary>
    public float barWidth = 1;
    /// <summary>
    /// Maximum height of a DataPoint bar
    /// </summary>
    public float maxScaleY = 2.5f;
    /// <summary>
    /// Distance of the Score number above the DataPoint bar
    /// </summary>
    public float distanceScoreTextToBar = 0.6f;
    /// <summary>
    /// Distance of the DateText under the DataPoint bar
    /// </summary>
    public float distanceDateTextToBar = 0.2f;
    /// <summary>
    /// Color of the Easy Difficulty DataPoint bars
    /// </summary>
    public Color EasyColor;
    /// <summary>
    /// Color of the Medium Difficulty DataPoint bars
    /// </summary>
    public Color MediumColor;
    /// <summary>
    /// Color of the Hard Difficulty DataPoint bars
    /// </summary>
    public Color HardColor;
    /// <summary>
    /// DateFormat used to print the Date
    /// </summary
    public string DateFormat = "dd/MM/yy";
    /// <summary>
    /// The array of SessionResults that will create
    /// the array of DataPoints
    /// </summary>
    public SessionResult[] sres;

    private PrefManager prefManager;
    private PlayerID actualPlayer;
    private Dictionary<GameDifficulty, Color> colorDict;

    void Start()
    {
        prefManager = GameObject.Find("PrefManager").GetComponent<PrefManager>();
        actualPlayer = prefManager.actualPlayer;
        sres = prefManager.LoadSResForActualPlayer();
        colorDict = new Dictionary<GameDifficulty, Color>()
        {
            {GameDifficulty.EASY, EasyColor },
            {GameDifficulty.MEDIUM, MediumColor },
            {GameDifficulty.HARD, HardColor },
        };
        if(sres!=null)
        {
            SpawnData();
        }
    }

    private void AddRandomSessions()
    {
        SessionResult[] testRes = TestSres();
        foreach(SessionResult s in testRes)
        {
            //Debug.Log("Sono qui");
            prefManager.AddSResForActualPlayer(s);
        }
    }

    private SessionResult[] TestSres()
    {
        SessionResult[] sres = new SessionResult[25];
        for (int i = 0; i < sres.Length; i++)
        {
            sres[i] = new SessionResult(actualPlayer, (GameDifficulty)(i % 3), (GameDifficulty)((i + 1) % 3), Random.Range(0, sres.Length));
        }
        return sres;
    }

    private float GetScoreNorm(float score, float MaxScore)
    {
        return (maxScaleY * score )/ MaxScore;
    }

    private void SpawnData()
    {
        SessionResult Max = sres[0];
        foreach(SessionResult s in sres)
        {
            if(s.CompareTo(Max)>0)
            {
                Max = s;
            }
        }
        float maxScore = Max.Score;
        //Debug.Log("Max score = " + maxScore);

        for (int i = 0; i < sres.Length; i++)
        {
            DataPoint dp = new DataPoint(i + (i * padding), GetScoreNorm(sres[i].Score, maxScore), sres[i].DifficultyEnd);
            GameObject dpgo = GameObject.Instantiate(DataPointPrefab, new Vector3(transform.position.x + dp.x* barWidth,
                                                                           transform.position.y + dp.y / 2, 0), Quaternion.identity);
            dpgo.transform.SetParent(transform);
            //scale and position of the bar
            Transform bar = dpgo.transform.GetChild(0);
            bar.localScale = new Vector3(barWidth, dp.y, dpgo.transform.localScale.z);
            Color c = colorDict[dp.gameDifficulty];
            bar.gameObject.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);

            //texts => li prende in ordine gerarchico
            TextMesh[] texts = dpgo.GetComponentsInChildren<TextMesh>();
            texts[0].text = sres[i].Score.ToString();
            texts[1].text = sres[i].GetDate().ToString(DateFormat);
            Transform scoreText = dpgo.transform.GetChild(1);

            scoreText.gameObject.transform.position = new Vector3(scoreText.transform.position.x,
                                                                    bar.transform.position.y + dp.y / 2 + distanceScoreTextToBar,
                                                                    0);
            Transform dateText = dpgo.transform.GetChild(2);
            dateText.gameObject.transform.position = new Vector3(dateText.transform.position.x,
                                                                    dateText.transform.position.y - dp.y / 2 +  0.5f - distanceDateTextToBar,
                                                                    0);
        }
    }
}
