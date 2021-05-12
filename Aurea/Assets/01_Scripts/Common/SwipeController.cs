using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField]
    private GameObject scrollbar;

    private float scrollPosition = 0;

    private float[] position;

    void Update()
    {
        position = new float[transform.childCount];
        float distance = 1f / (position.Length - 1f);
        for(int i = 0 ; i < position.Length ; i++)
        {
            position[i] = distance * i;
        }
        scrollPosition = scrollbar.GetComponent<Scrollbar>().value;
        for(int i = 0 ;i < position.Length ; i++)
        {
            if(scrollPosition < position[i] + (distance / 2) && scrollPosition > position[i] - (distance / 2))
            {
                scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value,position[i],0.1f);
            }
        }
        for(int i = 0 ; i < position.Length ; i++)
        {
            if(scrollPosition < position[i] + (distance/2) && scrollPosition > position[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale,new Vector2(1f,1f),0.1f);
                for(int y = 0 ; y < position.Length ; y++)
                {
                    if(y != i)
                    {
                        transform.GetChild(y).localScale = Vector2.Lerp(transform.GetChild(y).localScale,new Vector2(0.8f,0.8f),0.1f);
                    }
                }
            }
        }
    }
}
