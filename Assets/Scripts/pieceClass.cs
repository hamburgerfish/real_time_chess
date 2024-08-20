using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace pieces
{
    public class pieceClass : MonoBehaviour
    {

        public bool canMove = false;

        public Vector2 currPos;

        public float scale = 0.16f;

        public Vector2[] availPos;

        public bool white;

        public bool selected = false;

        public bool kill = false;


        

        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate()
        {

        }


        private List<Vector2> availMoveCurr = new List<Vector2>();
        private Vector2 availMoveInstance;
        public void checkMove(Vector2 currPos, Vector2[][] availMoveRaw, List<Vector2> positionsList, bool passThrough)
        {
            for (int i = 0; i < availMoveRaw.Length; i++)
            {
                for (int j = 0; j < availMoveRaw[i].Length; j++)
                {
                    availMoveInstance = availMoveRaw[i][j] + currPos;
                    if (availMoveInstance.x >= 7.5f * scale || availMoveInstance.y >= 7.5f * scale || availMoveInstance.x <= -0.5f * scale || availMoveInstance.y <= -0.5f * scale)
                    {
                        if (!passThrough)
                        {
                            //for (int k = 0; k < positionsList.Length; k++)
                            foreach (Vector2 k in positionsList)
                            {
                                availMoveCurr.Add(availMoveInstance);
                                if (k == availMoveInstance)
                                {
                                    break;
                                }
                            }
                        }
                        else availMoveCurr.Add(availMoveInstance);
                    }
                }
            }
        }


        //private void 

    }
}