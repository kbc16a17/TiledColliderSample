using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SampleScene {
    public class TileScript : MonoBehaviour {
        public enum ReferenceSizeType {
            Renderer,
            Scale,
        }

        [Header(@"Objects")]
        public GameObject Prefab;
        public Transform LeftTransform;
        public Transform RightTransform;

        [Header(@"Properties")]
        public ReferenceSizeType SizeType = ReferenceSizeType.Renderer;
        [Range(10, 100)]
        public int DivideNumX = 10;
        [Range(10, 100)]
        public int DivideNumY = 10;

        private Vector3 prefabSize {
            get {
                if (SizeType == ReferenceSizeType.Renderer) {
                    return Prefab.GetComponent<Renderer>().bounds.size;
                }
                return Prefab.transform.localScale;
            }
        }

        private Vector3 leftTransformSize => LeftTransform.GetComponent<Renderer>().bounds.size;
        private Vector3 rightTransformSize => RightTransform.GetComponent<Renderer>().bounds.size;

        private void Awake() {
            var topLeft = LeftTransform.position + (leftTransformSize / 2);
            var bottomRight = RightTransform.position - (rightTransformSize / 2);
            var distance = (bottomRight - topLeft);
            var size = new Vector2(distance.x / DivideNumX, distance.y / DivideNumY);
            var unit = new Vector2((distance.x - size.x) / (DivideNumX - 1), (distance.y - size.y) / (DivideNumY - 1));
            var rate = new Vector2(size.x / prefabSize.x, size.y / prefabSize.y);

            for (int j = 0; j < DivideNumY; j++) {
                var y = (size.y / 2) + topLeft.y + unit.y * j;
                for (int i = 0; i < DivideNumX; i++) {
                    var x = (size.x / 2) + topLeft.x + unit.x * i;
                    var position = new Vector3(x, y);
                    var obj = Instantiate(Prefab, position, Quaternion.identity);
                    obj.transform.localScale = new Vector3(obj.transform.localScale.x * rate.x, obj.transform.localScale.y * rate.y);
                }
            }
        }
    }
}