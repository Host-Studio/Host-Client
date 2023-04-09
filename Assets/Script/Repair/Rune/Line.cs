using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    public class Line : MonoBehaviour
    {
        public EdgeCollider2D col;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Contains("Point"))
            {
                if(!col.gameObject.GetComponent<Image>().enabled)
                {
                    col.gameObject.GetComponent<Image>().enabled = true;
                    col.transform.parent.parent.parent.GetComponent<Rune>().EnabledPoint();
                }
            }
        }
    }
}