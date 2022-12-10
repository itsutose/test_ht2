using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �^�[�Q�b�g�ɐU������X�N���v�g
/// </summary>
internal class LookAtTarget : MonoBehaviour
{
    // ���g��Transform
    [SerializeField] private Transform _self;
    private Vector3 objrot;

    // �^�[�Q�b�g��Transform
    [SerializeField] private Transform _target;

    private void Update()
    {
        // �^�[�Q�b�g�̕����Ɏ��g����]������
        _self.LookAt(_target);

        objrot = transform.eulerAngles;
        objrot.x = objrot.x + 180;
        objrot.z = objrot.z + 180;
        transform.eulerAngles = objrot;

    }
}