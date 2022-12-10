using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターゲットに振り向くスクリプト
/// </summary>
internal class LookAtTarget : MonoBehaviour
{
    // 自身のTransform
    [SerializeField] private Transform _self;
    private Vector3 objrot;

    // ターゲットのTransform
    [SerializeField] private Transform _target;

    private void Update()
    {
        // ターゲットの方向に自身を回転させる
        _self.LookAt(_target);

        objrot = transform.eulerAngles;
        objrot.x = objrot.x + 180;
        objrot.z = objrot.z + 180;
        transform.eulerAngles = objrot;

    }
}