using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    private int selectedCharacter = 0;
    public bool CharacterSelected = false;
    
    

    // Update is called once per frame
    void Update()
    {
        if (CharacterSelected == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                selectedCharacter ++;
                if (selectedCharacter >= characters.Count)
                {
                    selectedCharacter = 0;
                }
                Debug.Log(characters[selectedCharacter]);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedCharacter --;
                if (selectedCharacter < 0)
                {
                    selectedCharacter = characters.Count - 1;
                }
                Debug.Log(characters[selectedCharacter]);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("selectioné");
                characters[selectedCharacter].GetComponent<Player_Movement>().IsSelected = true;
                CharacterSelected = true;
                characters[selectedCharacter].GetComponent<Player_Movement>().ResetDesiredPosition = true;
                Debug.Log(CharacterSelected);
                characters.RemoveAt(selectedCharacter);
            }

            if (characters.Count == 0)
            {
                RoundFinished();
            }
        }
    }

    void RoundFinished()
    {
        Debug.Log("Tour Fini");
    }

    
    
}
