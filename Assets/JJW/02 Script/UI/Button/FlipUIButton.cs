using DG.Tweening;
using UnityEngine;

namespace JJW._02_Script.UI
{
    public class FlipUIButton : MonoBehaviour
    {
        [SerializeField] private RectTransform[] targets;
        [SerializeField] private float duration = 0.4f;

        private bool _opened = false;

        private Vector2[] _originOffsets;

        private void Awake()
        {
            _originOffsets = new Vector2[targets.Length];
            for (int i = 0; i < targets.Length; i++)
                _originOffsets[i] = targets[i].offsetMin;
        }

        public void Flip()
        {
            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                var origin = _originOffsets[i];

                DOTween.Kill(target);

                Sequence seq = DOTween.Sequence();

                if (!_opened)
                {
                    seq.Append(DOTween.To(
                        () => target.offsetMin,
                        v => target.offsetMin = v,
                        new Vector2(origin.x, 0),
                        duration));

                    seq.Append(DOTween.To(
                        () => target.offsetMin,
                        v => target.offsetMin = v,
                        new Vector2(-330, 0),
                        duration));
                }
                else
                {
                    seq.Append(DOTween.To(
                        () => target.offsetMin,
                        v => target.offsetMin = v,
                        new Vector2(origin.x, 0),
                        duration));

                    seq.Append(DOTween.To(
                        () => target.offsetMin,
                        v => target.offsetMin = v,
                        origin,
                        duration));
                }
            }

            _opened = !_opened;
        }
    }
}
