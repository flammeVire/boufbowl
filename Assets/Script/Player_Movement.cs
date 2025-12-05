using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] public GameObject DesiredPosition;
    [SerializeField] private int NBMovement;
    [SerializeField] private int MaxMovement;
    public List<Vector3Int> MovementList;
    public Transform RedGoalLine;
    public Transform BlueGoalLine;
    public bool IsSelected = false;
    public bool ResetDesiredPosition = false;
    public CharactereSelection CharactereSelection;
    private Vector3Int TestPosition;
    public Vector3Int MaxArea = new Vector3Int(10, 5, 0);
    public Vector3Int MinArea = new Vector3Int(-10, -5, 0);
    public Scoring scoring;
    public ScriptableObject scriptableObject;
    public bool choixBouger = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        MovementList = new List<Vector3Int>();
        OnDiagonal = false;
    }
    bool OnDiagonal = false;
    // Update is called once per frame
    void Update()
    {
        if (NBMovement <= MaxMovement && IsSelected)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OnDiagonal = true;
            }
            if (ResetDesiredPosition)
            {
                Debug.Log("Reset Desired Position");
                TestPosition = new Vector3Int((int)PlayerTransform.position.x, (int)PlayerTransform.position.y, (int)PlayerTransform.position.z);
                Debug.Log(DesiredPosition.transform.position);
                ResetDesiredPosition = false;
            }

            //Deplacement de du déplacement désiré (flèche)
            if (!OnDiagonal)
            {
                if (Input.GetKeyDown(KeyCode.D))
                { // Right
                    TestPosition += new Vector3Int(1, 0, 0);
                    MovementList.Add(new Vector3Int(1, 0, 0));
                }
                else if (Input.GetKeyDown(KeyCode.W))
                { // Up
                    TestPosition += new Vector3Int(0, 0, 1);
                    MovementList.Add(new Vector3Int(0, 0, 1));
                }
                else if (Input.GetKeyDown(KeyCode.A))
                { // Left
                    TestPosition += new Vector3Int(-1, 0, 0);
                    MovementList.Add(new Vector3Int(-1, 0, 0));
                }
                else if (Input.GetKeyDown(KeyCode.S))
                { // Down
                    TestPosition += new Vector3Int(0, 0, -1);
                    MovementList.Add(new Vector3Int(0, 0, -1));
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.D))
                { // Right

                    TestPosition += new Vector3Int(1, 0, -1);
                    MovementList.Add(new Vector3Int(1, 0, -1));
                }
                else if (Input.GetKeyDown(KeyCode.W))
                { // Up
                    TestPosition += new Vector3Int(1, 0, 1);
                    MovementList.Add(new Vector3Int(1, 0, 1));
                }
                else if (Input.GetKeyDown(KeyCode.A))
                { // Left
                    TestPosition += new Vector3Int(-1, 0, 1);
                    MovementList.Add(new Vector3Int(-1, 0, 1));
                }
                else if (Input.GetKeyDown(KeyCode.S))
                { // Down
                    TestPosition += new Vector3Int(-1, 0, -1);
                    MovementList.Add(new Vector3Int(-1, 0, -1));
                }
            }


            if (TestPosition.x >= MinArea.x &&
                TestPosition.x <= MaxArea.x &&
                TestPosition.z >= MinArea.z &&
                TestPosition.z <= MaxArea.z)
            {
                applyDesiredPosition(TestPosition);
            }

            if (Input.GetKeyDown(KeyCode.E) || NBMovement == MaxMovement)
            {
                for (int i = 0; i < MovementList.Count; i++)
                {
                    //PlayerTransform.position += MovementList[i];
                    //VerifPlayerToDesired();
                    IsSelected = false;
                    CharactereSelection.CharacterSelected = false;
                }
                lock (CharactereSelection)
                {
                    bool alreadyRegister = CharactereSelection.AllMovementList.ContainsKey(gameObject);
                    if (alreadyRegister)
                    {
                        Debug.LogWarning($"Following player : {gameObject} is already in CharactereSelection.AllMovementList : {CharactereSelection.AllMovementList}");
                        CharactereSelection.AllMovementList.Remove(gameObject);
                    }
                    CharactereSelection.AllMovementList.Add(gameObject, MovementList);
                    if (!alreadyRegister)
                    {
                        CharactereSelection.NbPlayerValided++;
                    }
                }
                MovementList = new List<Vector3Int>();
                NBMovement = 0;
            }

        }
        GoingThroughGoal();
        choixBouger = false;


    }
    
    private void applyDesiredPosition(Vector3 position)
    {
        if (DesiredPosition.transform.position == position) return;

        DesiredPosition.transform.position = position;
        NBMovement++;
    }
    

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Centre du rectangle
        Vector3 center = new Vector3(
            (MinArea.x + MaxArea.x) * 0.5f,
            transform.position.y, // même hauteur que ton objet
            (MinArea.y + MaxArea.y) * 0.5f
        );

        // Taille du rectangle
        Vector3 size = new Vector3(
            Mathf.Abs(MaxArea.x - MinArea.x),
            0.1f,
            Mathf.Abs(MaxArea.y - MinArea.y)
        );

        Gizmos.DrawWireCube(center, size);
    }*/

    
    
    bool VerifPlayerToDesired()
    {
        bool xOK = Mathf.Abs(PlayerTransform.position.x - DesiredPosition.transform.position.x) < 0.1f;
        bool yOK = Mathf.Abs(PlayerTransform.position.y - DesiredPosition.transform.position.y) < 0.1f;

        if (xOK && yOK)
        {
            IsSelected = false;
            FindObjectOfType<CharactereSelection>().CharacterSelected = false;
            return true;
        }
        return false;
    }
    
    

   void GoingThroughGoal()
   {
       if (this.gameObject.CompareTag("Red Team") && PlayerTransform.position.x >= BlueGoalLine.position.x)
       {
           Debug.Log(this.gameObject.GetComponents<MonoBehaviour>());
           if (this.gameObject.GetComponent<PassePlayer>().statCharacter.isHaveBall == true)
           {
               Debug.Log("ball dans le goal rouge");
               scoring.RedGetPoint();
           }
           Debug.Log("But de l'equipe rouge");
       }
       else if (this.gameObject.CompareTag("Blue Team") && PlayerTransform.position.x >= RedGoalLine.position.x)
       {
           if (this.gameObject.GetComponent<PassePlayer>().statCharacter.isHaveBall == true)
           {
               Debug.Log("ball dans le goal bleu");
               scoring.BlueGetPoint();
           }
           Debug.Log("But de l'equipe Bleue");
       }
   }
}
