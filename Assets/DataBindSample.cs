using System.Buffers;
using MinimalUtility.DataBind;
using UnityEngine;

namespace DefaultNamespace
{
    public class DataBindSample
    {
        private readonly DataBindView _view;

        private readonly Color32 _color = new Color32(255, 0, 0, 255);

        private readonly int _score = 100;

        public DataBindSample(DataBindView view)
        {
            _view = view;
        }

        public void Bind()
        {
            _view.Bind((color: _color, score: _score));
        }
    }
}