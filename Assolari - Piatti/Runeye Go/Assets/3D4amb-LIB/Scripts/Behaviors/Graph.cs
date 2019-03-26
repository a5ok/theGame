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
    /// The array of SessionResults that will create
    /// the array of DataPoints
    /// </summary>
    public SessionResult[] sres;
    

    private PrefManager prefManager;
    private PlayerID actualPlayer;
    private Dictionary<GameDifficulty, Color> colorDict;

    private SessionResult[] sresLevel1;
    private SessionResult[] sresLevel2;
    private SessionResult[] sresLevel3;
    private SessionResult[] sresLevel4;
    private SessionResult[] sresMaxByLevel;
    private SessionResult[] sresStock;
    private SessionResult[] sresReal;

    void Start()
    {
        prefManager = GameObject.Find("PrefManager").GetComponent<PrefManager>();
        actualPlayer = prefManager.actualPlayer;

        sres = prefManager.LoadSResForActualPlayer();

        sresStock = new SessionResult[4];
        sresStock[0] = new SessionResult(actualPlayer, GameDifficulty.EASY, 1);
        sresStock[1] = new SessionResult(actualPlayer, GameDifficulty.EASY, 2);
        sresStock[2] = new SessionResult(actualPlayer, GameDifficulty.EASY, 3);
        sresStock[3] = new SessionResult(actualPlayer, GameDifficulty.EASY, 4);

        if(sres == null)
            sresReal = sresStock;
        else
        {
            int lenght = sres.Length + 4;
            sresReal = new SessionResult[lenght];
            sresStock.CopyTo(sresReal, 0);
            sres.CopyTo(sresReal, 4);
        }

        sresMaxByLevel = new SessionResult[4];
        colorDict = new Dictionary<GameDifficulty, Color>()
        {
            {GameDifficulty.EASY, EasyColor },
            {GameDifficulty.MEDIUM, MediumColor },
            {GameDifficulty.HARD, HardColor },
        };

        DivideByLevel(sresReal);



            
        


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
        if (MaxScore == 0)
            return 0;
        else
            return (maxScaleY * score )/ MaxScore;
    }

    private void SpawnData()
    {
        SessionResult Max = sresReal[0];
        foreach(SessionResult s in sresReal)
        {
            if(s.CompareTo(Max)>0)
            {
                Max = s;
            }
        }
        float maxScore = Max.Score;
        //Debug.Log("Max score = " + maxScore);

        for (int i = 0; i < sresMaxByLevel.Length; i++)
        {
            DataPoint dp = new DataPoint(i + (i * padding), GetScoreNorm(sresMaxByLevel[i].Score, maxScore), sresMaxByLevel[i].DifficultyEnd);
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
            texts[0].text = sresMaxByLevel[i].Score.ToString();
            texts[1].text = "Level " + (i + 1);
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

    private void DivideByLevel(SessionResult[] sr)
    {
        foreach(SessionResult session in sr)
        {
            switch(session.Level)
            {
                case 1:
                    {
                        if(sresLevel1 == null)
                        {
                            sresLevel1 = new SessionResult[1];
                            sresLevel1[0] = session;
                        }
                        else
                        {
                            SessionResult[] arrayT = new SessionResult[sresLevel1.Length + 1];
                            sresLevel1.CopyTo(arrayT, 0);
                            arrayT[arrayT.Length - 1] = session;
                            sresLevel1 = new SessionResult[arrayT.Length];
                            arrayT.CopyTo(sresLevel1, 0);
                        }
                        break;
                    }
                case 2:
                    {
                        if (sresLevel2 == null)
                        {
                            sresLevel2 = new SessionResult[1];
                            sresLevel2[0] = session;
                        }
                        else
                        {
                            SessionResult[] arrayT = new SessionResult[sresLevel2.Length + 1];
                            sresLevel2.CopyTo(arrayT, 0);
                            arrayT[arrayT.Length - 1] = session;
                            sresLevel2 = new SessionResult[arrayT.Length];
                            arrayT.CopyTo(sresLevel2, 0);
                        }
                        break;
                    }
                case 3:
                    {
                        if (sresLevel3 == null)
                        {
                            sresLevel3 = new SessionResult[1];
                            sresLevel3[0] = session;
                        }
                        else
                        {
                            SessionResult[] arrayT = new SessionResult[sresLevel3.Length + 1];
                            sresLevel3.CopyTo(arrayT, 0);
                            arrayT[arrayT.Length - 1] = session;
                            sresLevel3 = new SessionResult[arrayT.Length];
                            arrayT.CopyTo(sresLevel3, 0);
                        }
                        break;
                    }
                case 4:
                    {
                        if (sresLevel4 == null)
                        {
                            sresLevel4 = new SessionResult[1];
                            sresLevel4[0] = session;
                        }
                        else
                        {
                            SessionResult[] arrayT = new SessionResult[sresLevel4.Length + 1];
                            sresLevel4.CopyTo(arrayT, 0);
                            arrayT[arrayT.Length - 1] = session;
                            sresLevel4 = new SessionResult[arrayT.Length];
                            arrayT.CopyTo(sresLevel4, 0);
                        }
                        break;
                    }
            }
        }

        GetMaxVector();
    }

    private void GetMaxVector()
    {
        int max1 = sresLevel1[0].Score;
        int max2 = sresLevel2[0].Score;
        int max3 = sresLevel3[0].Score;
        int max4 = sresLevel4[0].Score;

        SessionResult sresMax1 = sresLevel1[0];
        SessionResult sresMax2 = sresLevel2[0];
        SessionResult sresMax3 = sresLevel3[0];
        SessionResult sresMax4 = sresLevel4[0];


        foreach (SessionResult sr in sresLevel1)
        {
            if (sr.Score > max1)
            {
                max1 = sr.Score;
                sresMax1 = sr;
            }
        }

        foreach (SessionResult sr in sresLevel2)
        {
            if (sr.Score > max2)
            {
                max2 = sr.Score;
                sresMax2 = sr;
            }
        }

        foreach (SessionResult sr in sresLevel3)
        {
            if (sr.Score > max3)
            {
                max3 = sr.Score;
                sresMax3 = sr;
            }
        }

        foreach (SessionResult sr in sresLevel4)
        {
            if (sr.Score > max4)
            {
                max4 = sr.Score;
                sresMax4 = sr;
            }
        }

        sresMaxByLevel[0] = sresMax1;
        sresMaxByLevel[1] = sresMax2;
        sresMaxByLevel[2] = sresMax3;
        sresMaxByLevel[3] = sresMax4;

        SpawnData();

    }
}
