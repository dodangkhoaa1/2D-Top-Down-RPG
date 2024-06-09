using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    private void Awake()
    {
        if(PlayerController.Instance == null) {
            int characterOption = Prefs.characterSelectedOption;
            Instantiate(prefabs[characterOption], transform.position, Quaternion.identity); //spawn character
        }
        Destroy(gameObject);//destroy point to spawn player
        Debug.Log(DatabaseKey.UnlockCharacter);
    }
}
