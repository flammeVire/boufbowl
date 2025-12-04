using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereSelection : MonoBehaviour
{
    [SerializeField] List<GameObject> characters;
    [SerializeField] List<GameObject> characteres2;
    
    private int selectedCharacter = 0;
    public bool CharacterSelected = false;
    public List<List<Vector3Int>> AllMovementList = new List<List<Vector3Int>>();



    // Update is called once per frame
    void Update()
    {
        if (CharacterSelected == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                selectedCharacter++;
                if (selectedCharacter >= characters.Count)
                {
                    selectedCharacter = 0;
                }

                Debug.Log(characters[selectedCharacter]);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                selectedCharacter--;
                if (selectedCharacter < 0)
                {
                    selectedCharacter = characters.Count - 1;
                }

                Debug.Log(characters[selectedCharacter]);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
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

    IEnumerator SmoothMovement()
    {
        for (int i = 0; i < AllMovementList.Count; i++)
        {
            for (int j = 0; j < AllMovementList[i].Count; j++)
            {
                Vector3Int movement = AllMovementList[i][j];
                characteres2[i].transform.position += movement;
            }
        }
        yield return null;
    }
}


