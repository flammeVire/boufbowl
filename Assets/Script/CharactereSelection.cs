using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> characters = new List<GameObject>();
    private int selectedCharacter = 0;
    public Player_Movement playerMovement;
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("D"))
        {
            selectedCharacter ++;
        }
        else if (Input.GetButtonDown("Q"))
        {
            selectedCharacter --;
        }

        if (Input.GetButtonDown("Space"))
        {
            //characters[selectedCharacter].GetComponent<>();
        }
    }
    
}
