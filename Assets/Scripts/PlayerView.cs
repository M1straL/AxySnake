using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerView : MonoBehaviour
    {
        private Stack<GameObject> _body;
        private GameObject _bodyPrefab;

        private Ctx _ctx;

        private GameObject _headPrefab;

        private Camera _playerCamera;

        private PlayerEntity _playerEntity;
        private GameObject _playerRoot;
        private GameObject _tailPrefab;

        private void Start()
        {
            _playerCamera = gameObject.GetComponent<Camera>();
            _body = new Stack<GameObject>();

            Init(new GameObject()); //TODO add root transform
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.root.CompareTag("LevelEnemy")) _ctx.IsLevelFailed.Execute();

            if (other.transform.root.CompareTag("PlayerBody"))
            {
            }
        }

        public void SetCtx(Ctx ctx)
        {
            _ctx = ctx;
        }

        public void Move(Vector3 movementDirection)
        {
            transform.Translate(movementDirection, Space.Self);
        }

        public void Rotate(Vector3 rotationVector)
        {
            transform.Rotate(rotationVector);
        }

        public struct Ctx
        {
            public ReactiveCommand IsLevelFailed;
        }


        #region Сделай потом

        public void Init(GameObject root)
        {
            _headPrefab.transform.SetParent(root.transform);
            _bodyPrefab.transform.SetParent(root.transform);
            _tailPrefab.transform.SetParent(root.transform);

            _ctx.IsLevelFailed = new ReactiveCommand();
        }

        public void Reduce(GameObject contactedGO)
        {
            while (_body.TryPop(out var body))
            {
                if (body == contactedGO) break;
                body.gameObject.SetActive(false);
            }
        }


        public void Grow()
        {
            var body = Instantiate(_bodyPrefab, _playerRoot.transform, true);
            if (!_body.TryPeek(out var lastBody)) body.transform.position = _headPrefab.transform.position;

            var lastBodyMeshBounds = lastBody.GetComponent<MeshRenderer>().bounds;
            body.transform.position = lastBody.transform.position + lastBodyMeshBounds.size;

            _body.Push(_bodyPrefab);
        }

        #endregion
    }
}