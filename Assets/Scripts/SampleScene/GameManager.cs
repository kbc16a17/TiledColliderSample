using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleScene {
    public class GameManager : MonoBehaviour {
        public GameObject Prefab;
        public Sprite Sprite;

        private void Awake() {
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.z = 0.0f;
                Instantiate(Prefab, point, Quaternion.identity);
            }
        }
    }
}