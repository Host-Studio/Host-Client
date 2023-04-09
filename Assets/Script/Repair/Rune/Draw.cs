using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Repair
{
    public class Draw : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject linePrefab;

        private LineRenderer lr;
        private EdgeCollider2D col;
        private List<Vector2> points = new List<Vector2>();
        private List<GameObject> lines = new List<GameObject>();

        void OnDisable()
        {
            foreach (GameObject go in lines)
            {
                Destroy(go);
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject go = Instantiate(linePrefab, canvas.GetComponent<RectTransform>());
                lr = go.GetComponent<LineRenderer>();
                col = go.GetComponent<EdgeCollider2D>();
                points.Add(Input.mousePosition);
                lr.positionCount = 1;
                lr.SetPosition(0, points[0]);

                lines.Add(go);
            }
            if (Input.GetMouseButton(0))
            {
                Vector2 pos = Input.mousePosition;

                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
                col.points = points.ToArray();
            }
            if (Input.GetMouseButtonUp(0))
            {
                points.Clear();

                foreach (GameObject go in lines)
                {
                    Destroy(go);
                }

                lines.Clear();
            }
        }
    }

}