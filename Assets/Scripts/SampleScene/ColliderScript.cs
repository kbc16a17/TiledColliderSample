using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleScene {
    [RequireComponent(typeof(SpriteRenderer))]
    public class ColliderScript : MonoBehaviour {
        private new SpriteRenderer renderer;
        private int touchesCount = 0;

        private void Awake() {
            renderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            touchesCount++;
            renderer.enabled = true;
        }

        private void OnTriggerExit2D(Collider2D collision) {
            touchesCount--;
            if (touchesCount == 0) {
                renderer.enabled = false;
            }
        }
    }
}