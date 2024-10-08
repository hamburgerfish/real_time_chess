using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace piecesFake
{
    public class pieceClass : MonoBehaviour
    {
        public GameObject highlight;
        public GameObject selector;
        private SpriteRenderer selectorSpriteRenderer;

        public bool canMove = false;

        public Vector2 currPos;

        public float scale = 0.16f;
        public Vector2 axisOffset = new Vector2(0.08f, 0.08f);

        public Vector2[] availPos;

        public bool white;

        public bool selected = false;
        public Vector2 destination;

        

        // Start is called before the first frame update
        void Start()
        {
            selector = this.transform.Find("Selector").gameObject;
            selectorSpriteRenderer = selector.GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void FixedUpdate()
        {

        }


        public List<Vector2> availMoveCurr = new List<Vector2>();
        private Vector2 availMoveInstance;
        private bool breakFlag;
        public void checkMove(Vector2 currPos, Vector2[][] availMoveRaw, Dictionary<string, Vector2> positionsDict, bool passThrough)
        {
            selectorSpriteRenderer.enabled = true;
            availMoveCurr.Clear();
            for (int i = 0; i < availMoveRaw.Length; i++)
            {
                breakFlag = false;
                for (int j = 0; j < availMoveRaw[i].Length; j++)
                {
                    availMoveInstance = availMoveRaw[i][j] + currPos;
                    if (!(availMoveInstance.x > 8 || availMoveInstance.y > 8 || availMoveInstance.x < 1 || availMoveInstance.y < 1))
                    {
                        if (!passThrough)
                        {
                            foreach (KeyValuePair<string, Vector2> k in positionsDict)
                            {
                                if (k.Value == availMoveInstance)
                                {
                                    breakFlag = true;
                                    break;
                                }
                            }
                            availMoveCurr.Add(availMoveInstance);
                            Instantiate(highlight, availMoveInstance*scale - axisOffset, Quaternion.identity, this.transform);
                            if (breakFlag) break;
                        }
                        else
                        {
                            availMoveCurr.Add(availMoveInstance);
                            Instantiate(highlight, availMoveInstance*scale - axisOffset, Quaternion.identity, this.transform);
                        }
                    }
                }
            }
        }


        public void kill(Dictionary<string, Vector2> positionsDict)
        {
            positionsDict.Remove(this.name);
            Destroy(this.gameObject);
        }

        private void move(Vector2 destination, Dictionary<string, Vector2> positionsDict, List<string> immuneList)
        {
            immuneList.Add(this.name);
            positionsDict[this.name] = destination;
        }

        public void act(Vector2 currPos, Vector2[][] availMoveRaw, Dictionary<string, Vector2> positionsDict, bool passThrough)
        {
            selected = !selected;
            if (selected)
            {
                checkMove(currPos, availMoveRaw, positionsDict, passThrough);
            }
            else
            {
                unHighlight();
            }
        }

        public void unHighlight()
        {
            selectorSpriteRenderer.enabled = false;
            Transform tr = this.transform;
            for (int i=0; i < tr.childCount; i++)
            {
                if (tr.GetChild(i).gameObject.CompareTag("highlight")) Destroy(tr.GetChild(i).gameObject);
            }
        }
    }
}