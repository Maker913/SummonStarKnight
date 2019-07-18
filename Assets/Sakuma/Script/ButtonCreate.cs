using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonObj;

    [SerializeField]
    private GameObject ButtonParent;

    [SerializeField]
    private GameObject game;

    [SerializeField]
    private GameObject dtest;

    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        int data = 0;
        for(int i = 0; i < StageCobtroller.Technique.Length; i++)
        {
            if(StageCobtroller .Technique[i] != -1)
            {
                data++;
            }
        }

        for(int i = 0; i < data; i++)
        {
            
            float brock = ButtonParent.GetComponent<RectTransform>().sizeDelta.x+400;
            
            float brockSize =brock/( data + 1);
            Debug.Log(brockSize);
            GameObject Obj= Instantiate(
                ButtonObj,
                new Vector3 (
                    ButtonParent.transform.position .x - (brock / 2) + (brockSize * (i+1)),
                    ButtonParent.transform.position.y,
                    ButtonParent.transform.position.z
                ),
                Quaternion.identity,
                ButtonParent.transform
            );
            Obj.GetComponent<ButtonNum>().Num = StageCobtroller.Technique[i];
            if (sprites[StageCobtroller.Technique[i]] == null)
            {
                Obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = game.GetComponent<GameController>().technique[StageCobtroller.Technique[i]].Name;
            }
            else
            {
                Obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                Obj.GetComponent<Image>().sprite = sprites[StageCobtroller.Technique[i]];
            }
            Obj.GetComponent<Button>().onClick.AddListener(() => { dtest.GetComponent<SumonBoard>().Sumon(Obj); });
        }





    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
