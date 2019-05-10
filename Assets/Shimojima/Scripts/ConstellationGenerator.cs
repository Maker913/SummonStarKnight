using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationGenerator : MonoBehaviour
{
    public GameObject[] constellation;
    private int constellationLength;
    private float time;
    private int turn;

    void Start()
    {
        constellationLength = constellation.Length;
        turn = 1;
    }

    
    void Update()
    {
        time += Time.deltaTime;

        if (turn == 1 && time >= 3.00f)
        {
            int cons = Random.Range(0, constellationLength);
            GameObject consPre = Instantiate(constellation[cons], new Vector3(transform.localPosition.x,
                                                                              transform.localPosition.y + 2, 
                                                                              transform.localPosition.z),
                                                                              Quaternion.identity);
            turn = 2;
        }
    }
}
