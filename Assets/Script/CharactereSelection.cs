using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactereSelection : MonoBehaviour
{
    List<GameObject> characters;
    [SerializeField] List<GameObject> characteres2;
    
    private int selectedCharacter = 0;
    public bool CharacterSelected = false;
    public List<List<Vector3Int>> AllMovementList = new List<List<Vector3Int>>();
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
            selectPlayer();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                characters[selectedCharacter].GetComponent<Player_Movement>().IsSelected = true;
                CharacterSelected = true;
                characters[selectedCharacter].GetComponent<Player_Movement>().ResetDesiredPosition = true;

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
        for (int i = 0; i < AllMovementList.Count; i++)
        {
            for (int j = 0; j < AllMovementList[i].Count; j++)
            {
                Vector3Int movement = AllMovementList[i][j];
                characteres2[i].transform.position += movement;
                yield return  new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }
}


