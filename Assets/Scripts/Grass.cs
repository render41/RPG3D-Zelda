using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    #region Variables
    [SerializeField] private ParticleSystem fxGrassCut;
    #endregion
    
    #region Get Hit
    private void GetHit(int amount)
    {
        bool isCut = false;
        if (!isCut)
        {
            this.transform.localScale = Vector3.one;
            fxGrassCut.Emit(100);
            isCut = true;
        }
    }
    #endregion
}
