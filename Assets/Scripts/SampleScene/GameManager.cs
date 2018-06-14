using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleScene {
    public class GameManager : MonoBehaviour {
        public GameObject Prefab;
        public Sprite Sprite;

        private void Awake() {
            var strs = new StringBuilder();
            strs.AppendLine("[Sprite Info]");
            strs.AppendLine("# vertices -----------------");
            foreach (var vert in Sprite.vertices) {
                strs.AppendLine(vert.ToString());
            }
            strs.AppendLine("# uvs -----------------");
            foreach (var uv in Sprite.uv) {
                strs.AppendLine(uv.ToString());
            }
            Debug.Log(strs);
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