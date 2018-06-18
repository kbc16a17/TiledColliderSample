using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using SampleScene.Extensions;

namespace SampleScene {
    [DefaultExecutionOrder(1)]
    [RequireComponent(typeof(CreateTile))]
    public class CreateTileAndGrouping : MonoBehaviour {
        [SerializeField]
        private Text text;

        private new Renderer renderer;
        private ColliderGroup group;

        private void Awake() {
            renderer = GetComponent<Renderer>();

            // 作ったタイルの子供達のColliderHelperを取得して配列にする
            var colliders = GetComponent<CreateTile>().children
                .Select(o => o.GetComponent<ColliderHelper>())
                .ToArray();
            group = new ColliderGroup(colliders);
        }

        public void Update() {
            renderer.enabled = group.EnteredAll;
            text.text = group.EnterCount + " / " + group.Children.Length;
        }
    }
}