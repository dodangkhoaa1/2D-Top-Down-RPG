using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goinCoin, healthGlobe, staminaGlobe;

    public void DropItems()
    {
        int randomNumber = Random.Range(1, 5);

        if(randomNumber == 1)
        {
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }else

        if(randomNumber == 2)
        {
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }else

        if (randomNumber == 3)
        {
            int randomAmountOfGold = Random.Range(1, 4);

            for (int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(goinCoin, transform.position, Quaternion.identity);
            }
        }
    }
}
