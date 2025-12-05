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
        lock (this)
        {
            characters = new List<GameObject>();
            ResetList();
        }
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
                    lock (this)
                    {
                        Debug.Log($"Space Pressed - {selectedCharacter} - {characters}");
                        if (selectedCharacter < 0 || selectedCharacter >= characters.Count)
                        {
                            Debug.LogWarning($"Invalid index in CharacterSelection.Update, with following characters : {characters} and index {selectedCharacter}");
                            return;
                        }
                        characters[selectedCharacter].GetComponent<Player_Movement>().IsSelected = true;
                        CharacterSelected = true;
                        characters[selectedCharacter].GetComponent<Player_Movement>().ResetDesiredPosition = true;

                        Debug.Log(characters[selectedCharacter].GetComponent<Player_Movement>().ResetDesiredPosition);

                        Debug.Log(CharacterSelected);
                        characters.RemoveAt(selectedCharacter);
                    }
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
            lock (this)
            {
                selectedCharacter++;
                if (selectedCharacter >= characters.Count)
                {
                    selectedCharacter = 0;
                }

                if (selectedCharacter == 0 && characters.Count == 0)
                {
                    Debug.LogWarning("Really?");
                }
                else
                {
                    Debug.Log(characters[selectedCharacter]);
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lock (this)
            {
                selectedCharacter--;
                if (selectedCharacter < 0)
                {
                    selectedCharacter = Mathf.Max(characters.Count - 1, 0);
                }

                if (selectedCharacter == 0 && characters.Count == 0)
                {
                    Debug.LogWarning("Really?");
                }
                else
                {
                    Debug.Log(characters[selectedCharacter]);
                }
                indicator.transform.position = new Vector3(characters[selectedCharacter].transform.position.x, -1.5f, characters[selectedCharacter].transform.position.z);
            }
        }
    }
    
    public void RoundFinished()
    {
        Debug.Log("Tour Fini");
        StartCoroutine(SmoothMovement());
    }

    void ResetList()
    {
        lock (this)
        {
            characters = new List<GameObject>(characteres2);
        }
    }
    IEnumerator SmoothMovement()
    {
        lock (this)
        {
            ResetList();
            var temp = new Dictionary<GameObject, List<Vector3Int>>(AllMovementList);
            foreach (var character in temp.Keys)
            {
                for (int j = 0; j < temp[character].Count; j++)
                {
                    Debug.Log("Overlap");
                    //character.transform.position -= movement;
                    Debug.Log(temp[character]);

                    Vector3Int movement = temp[character][j];
                    character.transform.position += movement;

                    Debug.Log(character);
                    if (MapManager.instance.SomethingOverlap(character))
                    {
                        Debug.Log("Overlap");
                        character.transform.position -= movement;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
            }
            AllMovementList.Clear();
            selectedCharacter = 0;
            characters[selectedCharacter].GetComponent<Player_Movement>().DesiredPosition.transform.position = characters[selectedCharacter].transform.position;
            GameManager.instance.AllPlayerHaveMoved = true;
            ResetList();
        }
        yield return null;
    }
}


