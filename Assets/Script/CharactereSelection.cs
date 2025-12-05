using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereSelection : MonoBehaviour
{
    List<GameObject> characters;
    [SerializeField] List<GameObject> characteres2;

    [SerializeField]public GameObject indicator;

    private int selectedCharacter = 0;
    public bool CharacterSelected = false;
    public Dictionary<GameObject, List<Vector3Int>> AllMovementList = new();
    public int NbPlayerValided;

    void Start()
    {
        characters = new List<GameObject>();
        ResetList();
    }
    // Update is called once per frame
    void Update()
    {
        if (CharacterSelected == false)
        {
            if (GameManager.instance.canMove)
            {

                selectPlayer();

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Space Pressed");
                    characters[selectedCharacter].GetComponent<Player_Movement>().IsSelected = true;
                    CharacterSelected = true;
                    characters[selectedCharacter].GetComponent<Player_Movement>().ResetDesiredPosition = true;

                    Debug.Log(characters[selectedCharacter].GetComponent<Player_Movement>().ResetDesiredPosition);

                    Debug.Log(CharacterSelected);
                    characters.RemoveAt(selectedCharacter);
                }

                if (NbPlayerValided == 5)
                {
                    RoundFinished();
                    NbPlayerValided = 0;
                }
            }
        }
    }

    void selectPlayer()
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
        else if (Input.GetKeyDown(KeyCode.A))
        {
            selectedCharacter--;
            if (selectedCharacter < 0)
            {
                selectedCharacter = characters.Count - 1;
            }

            Debug.Log(characters[selectedCharacter]);
        }
        indicator.transform.position = new Vector3(characters[selectedCharacter].transform.position.x, -1.5f, characters[selectedCharacter].transform.position.z);
    }
    
    public void RoundFinished()
    {
        Debug.Log("Tour Fini");
        StartCoroutine(SmoothMovement());
    }

    void ResetList()
    {
        characters = new List<GameObject>(characteres2);
    }
    IEnumerator SmoothMovement()
    {
        ResetList();
        foreach (var character in AllMovementList.Keys)
        {
            for (int j = 0; j < AllMovementList[character].Count; j++)
            {
                Debug.Log(AllMovementList[character]);

                Vector3Int movement = AllMovementList[character][j];
                character.transform.position += movement;

                Debug.Log(character);
                if (MapManager.instance.SomethingOverlap(character))
                {
                    Debug.Log("Overlap");
                    //character.transform.position -= movement;
                }
                yield return  new WaitForSeconds(0.5f);
            }
        }
        GameManager.instance.AllPlayerHaveMoved = true;
        ResetList();
        yield return null;
    }
}


