using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    private void Update()
    {
        Destroy(this.gameObject,0.2f);
    }
}
