using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManagement : MonoBehaviour
{
    public List<Transform> IAs;
    public Transform BallPosition;

    public int stepsToMove = 3;
    public float moveDuration = 0.2f;
    public float stepDelay = 0.05f;
    public bool allowDiagonal = true;

    // Réservations de cases pour éviter les collisions
    private HashSet<Vector3Int> reservedTiles = new HashSet<Vector3Int>();


    public void LaunchCoroutine()
    {
        reservedTiles.Clear(); // reset avant de lancer une séquence
        foreach (var ia in IAs)
        {
            StartCoroutine(MoveIASequence(ia, stepsToMove));
        }
    }


    IEnumerator MoveIASequence(Transform ia, int steps)
    {
        if (ia == null || BallPosition == null) yield break;

        for (int i = 0; i < steps; i++)
        {
            Vector3Int iaGrid = Vector3Int.RoundToInt(ia.position);
            Vector3Int ballGrid = Vector3Int.RoundToInt(BallPosition.position);

            Vector3Int direction = new Vector3Int(
                Mathf.Clamp(ballGrid.x - iaGrid.x, -1, 1),
                Mathf.Clamp(ballGrid.y - iaGrid.y, -1, 1),
                Mathf.Clamp(ballGrid.z - iaGrid.z, -1, 1)
            );

            if (!allowDiagonal)
            {
                if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y) && Mathf.Abs(direction.x) >= Mathf.Abs(direction.z))
                    direction = new Vector3Int(direction.x, 0, 0);
                else if (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x) && Mathf.Abs(direction.y) >= Mathf.Abs(direction.z))
                    direction = new Vector3Int(0, direction.y, 0);
                else
                    direction = new Vector3Int(0, 0, direction.z);
            }

            if (direction == Vector3Int.zero)
                yield break;

            Vector3Int nextGrid = iaGrid + direction;

            // -------------------------------------------------------
            //  RÉSERVATION : si quelqu'un veut déjà cette case, bloqué
            // -------------------------------------------------------
            if (reservedTiles.Contains(nextGrid))
            {
                yield break; // IA reste sur place
            }
            else
            {
                reservedTiles.Add(nextGrid);
            }

            // Déplacement
            Vector3 nextWorld = nextGrid;
            yield return MoveOneTile(ia, nextWorld);

            yield return new WaitForSeconds(stepDelay);
        }

        // Une fois fini on peut nettoyer (optionnel)
        reservedTiles.Clear();
    }



    IEnumerator MoveOneTile(Transform ia, Vector3 targetPos)
    {
        Vector3 start = ia.position;
        float t = 0;

        while (t < moveDuration)
        {
            t += Time.deltaTime;
            ia.position = Vector3.Lerp(start, targetPos, t / moveDuration);
            yield return null;
        }

        ia.position = targetPos;
    }
}
