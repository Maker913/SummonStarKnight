using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationGenerator : MonoBehaviour
{
    public GameObject[] constellation;
    [SerializeField]
    private string[] register;
    private int[,] attackConstellation;
    private int constellationLength;
    private float time;
    private int turn;

    void Start()
    {
        constellationLength = constellation.Length;
        attackConstellation = new int[register.Length, 14];
        Register();
        turn = 1;
    }

    
    void Update()
    {
        time += Time.deltaTime;

        if (turn == 1 && time >= 3.00f)
        {
            
            turn = 2;
        }
    }

    private void Register()
    {
        for (int i = 0; i < register.Length; i++)
        {
            if (register[i] != "")
            {
                string[] tmp = register[i].Split(',');
                int[] tmp2 = new int[tmp.Length];
                for (int j = 0; j < tmp.Length; j++)
                {
                    tmp2[j] = int.Parse(tmp[j]);
                    attackConstellation[i, j] = tmp2[j];
                }
            }
        }

        for (int i = 0; i < 14; i++)
        {
            if (attackConstellation[14,i] != 0)
            {
                constellation[attackConstellation[14, i] - 1].SetActive(true);
            }
        }

    }
}
