using System;
using UnityEngine;
using UnityEngine.UI;

namespace Repair
{
    class Weapon : MonoBehaviour
    {
        [SerializeField] private Sprite weapon_sprite;
        [SerializeField] [Range(0f, 1600f)] private float imageSize;

        public void ChangeImageSize()
        {

        }
    }
}