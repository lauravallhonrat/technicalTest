using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tangelo.Test.Components
{
    public class SpinnerComponent : MonoBehaviour, IActionComponent
    {
        // QUESTION 1: Implement here the rotation
        public void DoAction()
        {
            throw new System.NotImplementedException();
        }

        void Update()
        {
            // QUESTION 2: This is an obsolete method. We want to do it using DOTween and out of the Update.
            transform.RotateAround(new Vector3(0, 1, 0), 90);
        }
    }
}