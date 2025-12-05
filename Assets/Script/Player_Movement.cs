using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject DesiredPosition;
    [SerializeField] private int NBMovement;
    [SerializeField] private int MaxMovement;
    public List<Vector3Int> MovementList;
    public Transform RedGoalLine;
    public Transform BlueGoalLine;
    public bool IsSelected = false;
    public bool ResetDesiredPosition = false;
    public CharactereSelection CharactereSelection;
    private Vector3Int TestPosition;
    public Vector3Int MaxArea;
    public Vector3Int MinArea;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        MovementList = new List<Vector3Int>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NBMovement < MaxMovement && IsSelected)
        {
            if(ResetDesiredPosition)
            {
                Debug.Log("Reset Desired Position");
                TestPosition = new Vector3Int((int) PlayerTransform.position.x, (int)PlayerTransform.position.y, (int)PlayerTransform.position.z);
                Debug.Log(DesiredPosition.transform.position);
                ResetDesiredPosition = false;
            }
            
            //Deplacement de du déplacement désiré (flèche)
            if (Input.GetKeyDown(KeyCode.D)) { // Right
                
                TestPosition += new Vector3Int(1, 0, 0);
                MovementList.Add(new Vector3Int(1, 0, 0));
            } else if (Input.GetKeyDown(KeyCode.W)) { // Up
                TestPosition += new Vector3Int(0, 0, 1);
                MovementList.Add(new Vector3Int(0, 0, 1));
            } else if (Input.GetKeyDown(KeyCode.A)) { // Left
                TestPosition += new Vector3Int(-1, 0, 0);
                MovementList.Add(new Vector3Int(-1, 0, 0));
            } else if (Input.GetKeyDown(KeyCode.S)) { // Down
                TestPosition += new Vector3Int(0, 0, -1);
                MovementList.Add( new Vector3Int(0, 0, -1));
            }

            if (TestPosition.x > MinArea.x ||
                TestPosition.x < MaxArea.x ||
                TestPosition.y > MinArea.y ||
                TestPosition.y < MaxArea.y)
            {
                DesiredPosition.transform.position = TestPosition;
            }
            
            

            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < MovementList.Count; i++)
                {
                    //PlayerTransform.position += MovementList[i];
                    //VerifPlayerToDesired();
                    IsSelected = false;
                    CharactereSelection.CharacterSelected = false;
                }
                CharactereSelection.AllMovementList.Add(gameObject, MovementList);
                CharactereSelection.NbPlayerValided ++;
                MovementList = new List<Vector3Int>();
            }
            
        }
        
        GoingThroughGoal();
    }
    

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
           Debug.Log("But de l'equipe rouge");
       }
       else if (this.gameObject.CompareTag("Blue Team") && PlayerTransform.position.x >= RedGoalLine.position.x)
       {
           Debug.Log("But de l'equipe Bleue");
       }
   }
}
