using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI text;
    float timer = 0;

    bool timing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timing) {
            timer += Time.deltaTime;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);

            string timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
            text.text = "Your Time: " + timerText;
        }
    }

    public void StartTimer() {
        timing = true;
    }

    public void StopTimer(){
        timing = false;
    }
}
