using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour
{
    //�o���ʒu�̕ϐ�
    private float positionX;
    //private float positionY;

    //�p�[�����m�C�Y�ɕω���^����ϐ�
    private float rdm;

    //���𓖂Ă����ɂ��̏�Ɏc�点�邽�߂̐ݒ�ϐ�
    public float moveSetX;
    //public float moveSetY;
    private float firstMoveSet;

    //�����X�s�[�h
    public float noiseSpeedLow;
    public float noiseSpeedHigh;

    //���������������Ƃ𒲂ׂ�ϐ�
    public bool hitCheck;
    //�����O�ꂽ���Ƃ𒲂ׂ�ϐ�
    public bool outCheck;

    // Start is called before the first frame update
    void Start()
    {
        //�����ʒu�ݒ�ϐ��B�X�N���[���̕��̃T�C�Y���Ń����_���Ȓl���Ƃ�A�X�P�[���Ԃ��k�߂邽�߂�30�Ŋ����Ă���B
        positionX = Random.Range(-Screen.width / 2, Screen.width / 2) / 30;

        //�ŏ��ɐݒ肵��fuwaSet�̒l���L�����Ă���
        firstMoveSet = moveSetX;

        //�p�[�����m�C�Y�p�ϐ�
        rdm = Random.Range(noiseSpeedLow, noiseSpeedHigh);

        hitCheck = false;
        outCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        //�O�̍��W�ƍ��̍��W��ۑ�����ϐ���錾
        Vector3 oldPos = transform.position;
        Vector3 newPos = transform.position;
        //���΂������Ɍ������邩�E�Ɍ������邩���`�F�b�N����ϐ���錾
        float check = 0.0f;

        //���C�g�������������Ɏ��R�ɒ�~����悤�ɏ��X�Ɍ��炵�Ă����Ă���
        if (hitCheck == true && moveSetX > 0)
        {
            moveSetX -= 0.2f;

        }
        else
        {
            hitCheck = false;
        }

        //���C�g���O�ꂽ���Ɏ��R�Ƀp�[�����m�C�Y�ɖ߂��悤�ɏ��X�ɑ����Ă����Ă���
        if (outCheck == true && moveSetX < firstMoveSet)
        {
            moveSetX += 0.01f;
            
        }
        else
        {
            outCheck = false;
        }

        //�p�[�����m�C�Y�œ������w��
        newPos.x = positionX + (Mathf.PerlinNoise(Time.time / rdm, 0) - 0.5f) * moveSetX;


        //�������̃p�[�����m�C�Y�̒l�����̂܂܉�]�Ɏg�����ƂŁA�i�s�����Ɖ�]��������v����
        check = newPos.x - oldPos.x;
        Vector3 rotation = this.transform.eulerAngles;
        rotation.z = -check * 100f;
        if (0 <= check)
        {
            transform.Rotate(rotation);
        }
        else
        {
            transform.Rotate(rotation);
        }
        transform.position = newPos;

    }
}