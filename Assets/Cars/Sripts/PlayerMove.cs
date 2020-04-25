using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    // Start is called before the first frame update
    // 0: Punch
    // 1: Cut
    // 2: Leaf
    public GameObject BlueHand;
    public GameObject OrangeHand;
    // variable of sprites Hand
    public static int priorityofBlue;
    public static int priorityofOrange;
    private int[] priority1 = { 0, 1, 2 };
    private int[] priority2 = { 2, 0, 1 };
    bool Change;
    public static float t = 0;
    int a = 0;
    void Start()
    {
        priorityofBlue = priority1[Random.Range(0, priority1.Length)];
        priorityofOrange = priority2[Random.Range(0, priority2.Length)];
        Change = false;

        if (GameController.score >= 2)
        {
            this.transform.rotation.eulerAngles.Set(234, 234, 34543);
            a = GameController.score / 2;
        }

        Time.timeScale = 1 + 0.04f * a;
        t = Time.timeScale;

    }

    // Update is called once per frame
    void Update()
    {
        //
        {
            //if Blue Hand is at left move it to right and if its right moves it to left.
            //-0.75, -2.25
            if (GameController.BlueAtLeft)
            {
                BlueHand.transform.position = Vector3.Lerp(BlueHand.transform.position,
                    new Vector3(-(float)(Camera.main.orthographicSize * Screen.width / Screen.height / 4), -3.5f, 0),
                    0.3f);
            }
            else
            {
                BlueHand.transform.position = Vector3.Lerp(BlueHand.transform.position,
                    new Vector3(-(float)(Camera.main.orthographicSize * Screen.width / Screen.height / 4 * 3), -3.5f, 0),
                    0.3f);
            }
            //if Orange Hand is at left move it to right and if its right moves it to left.
            if (GameController.OrangeAtLeft)
            {
                OrangeHand.transform.position = Vector3.Lerp(OrangeHand.transform.position,
                    new Vector3((float)(Camera.main.orthographicSize * Screen.width / Screen.height / 4 * 3), -3.5f, 0),
                    0.3f);
            }
            else
            {
                OrangeHand.transform.position = Vector3.Lerp(OrangeHand.transform.position,
                    new Vector3((float)(Camera.main.orthographicSize * Screen.width / Screen.height / 4), -3.5f, 0),
                    0.3f);
            }
        }
        if (GameController.score % 10 == 0 && Change && GameController.score != 0)
        {
            priorityofBlue = priority1[Random.Range(0, priority1.Length)];
            priorityofOrange = priority2[Random.Range(0, priority2.Length)];
            Change = false;
            t = t + 0.04f;

            Time.timeScale = t;
        }
        if (GameController.score % 10 == 1)
        {
            Change = true;
        }
        if (GameController.score % 2 == 0 && Change && GameController.score != 0)
        {
            Change = false;
            t = t + 0.04f;

            Time.timeScale = t;
        }
        if (GameController.score % 2 == 1)
        {
            Change = true;
        }
    }
}