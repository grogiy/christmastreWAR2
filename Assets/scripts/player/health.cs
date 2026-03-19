using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    public Slider hp;
    // Update is called once per frame
    void Update()
    {
        hp.value -= Time.deltaTime;
    }
}
