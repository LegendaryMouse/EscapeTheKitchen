using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheild : MonoBehaviour
{
    public float cooldown;
    public bool isCooldown;

    private Image image;
    private Player player;

    void Start()
    {
        image = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isCooldown = true;
    }
    void Update()
    {

        if(isCooldown)
        {
            image.fillAmount -=  1/cooldown * Time.deltaTime;
            if(image.fillAmount <= 0)
            {
                image.fillAmount = 1; 
                isCooldown = false;
                player.sheild.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
    public void Reset()
    {
        image.fillAmount = 1;
        isCooldown = true;
    }
    public void ReduceTime(float damage)
    {
        image.fillAmount -= damage / 15f;
    }
}
