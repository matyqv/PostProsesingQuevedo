using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InterfaceRecursos : MonoBehaviour
{

    public  float scores;
    public TextMeshProUGUI score;

    [SerializeField] Image[] Vidas; 
    [SerializeField] int HP=3;

    public static event Action GameOver;
    // Start is called before the first frame update
    void Start()
    {
        EnemyScript.SumaScore += SumarScore;
        Bala.SumarScoreBala+= SumarScore;
        Bala.RestarScoreBala+= RestarScore;
        EnemyScript.restaVida += RestarVida;
        PwUpMecanica.RecibeBotiquin += SumarVida;
    }

    // Update is called once per frame
    void Update()
    {
        coordinarRecursos();
    }

    void coordinarRecursos()
    {
        score.text = "" + Mathf.RoundToInt(scores);
    }

    void SumarScore()
    {
        scores += 10;
    }
    void RestarScore()
    {
        scores -= 10;
    }

    void RestarVida()
    {
        HP -= 1;
        if (HP > 2) { Vidas[2].enabled = true; } else { Vidas[2].enabled = false; }
        if (HP > 1) { Vidas[1].enabled = true; } else { Vidas[1].enabled = false; }
        if (HP > 0) { Vidas[0].enabled = true; }
        else
        {
            Vidas[0].enabled = false;
            GameOver?.Invoke();
        }
    }
    void SumarVida()
    {
        HP += 1;
        if (HP > 2) { Vidas[2].enabled = true; } else { Vidas[2].enabled = false; }
        if (HP > 1) { Vidas[1].enabled = true; } else { Vidas[1].enabled = false; }
        if (HP > 0) { Vidas[0].enabled = true; } else { Vidas[0].enabled = false; }
    }

    private void OnDisable()
    {
        EnemyScript.SumaScore -= SumarScore;
        EnemyScript.restaVida -= RestarVida;
        Bala.SumarScoreBala -= SumarScore;
        Bala.RestarScoreBala -= RestarScore;
        PwUpMecanica.RecibeBotiquin -= SumarVida;
    }
}
