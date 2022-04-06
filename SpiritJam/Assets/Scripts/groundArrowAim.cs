using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundArrowAim : MonoBehaviour
{
    private playerController playerController;
    private mouseController mouseController;

    public GameObject aimTargetOG;
    public GameObject aimMiddlePointOG;
    [Space]
    public float aimSizeModifier = 0.2f;
    public float aimMinSize = 0.3f;
    public float aimMaxSize = 1.2f;
    [Space]
    public float middleSizeModifier = 0.2f;
    public float middleMinSize = 0.3f;
    public float middleMaxSize = 1.2f;

    private GameObject aimTarget;
    private GameObject aimMiddlePoint1;
    private GameObject aimMiddlePoint2;
    private GameObject aimMiddlePoint3;

    [HideInInspector]
    public float maxDistance = 20f;

    void Awake()
    {
        playerController = FindObjectOfType<playerController>();
        playerController.gameObject.TryGetComponent<mouseController>(out mouseController);

        aimTarget = Instantiate(aimTargetOG, this.transform.position, Quaternion.identity);

        aimMiddlePoint1 = Instantiate(aimMiddlePointOG, this.transform.position, Quaternion.identity);
        aimMiddlePoint2 = Instantiate(aimMiddlePointOG, this.transform.position, Quaternion.identity);
        aimMiddlePoint3 = Instantiate(aimMiddlePointOG, this.transform.position, Quaternion.identity);
    }


    void Update()
    {
        Vector3 currentPos = this.transform.position;
        Vector3 groundPos = playerController.groudPos;

        Vector3 posDifference = currentPos - groundPos;
        Vector3 direction = posDifference.normalized;

        float distance = Mathf.Min(maxDistance, posDifference.magnitude);
        Vector3 endPos = groundPos + direction * distance;

        aimTarget.transform.position = Vector3.Lerp(aimTarget.transform.position, endPos, mouseController.posIndicatorLerpSpeed * Time.deltaTime);

        Vector3 middleTargetPos1 = Vector3.Lerp(groundPos, endPos, 0.25f);
        Vector3 middleTargetPos2 = Vector3.Lerp(groundPos, endPos, 0.5f);
        Vector3 middleTargetPos3 = Vector3.Lerp(groundPos, endPos, 0.75f);

        aimMiddlePoint1.transform.position = Vector3.Lerp(aimMiddlePoint1.transform.position, middleTargetPos1, mouseController.posIndicatorLerpSpeed * 0.5f * Time.deltaTime);
        aimMiddlePoint2.transform.position = Vector3.Lerp(aimMiddlePoint2.transform.position, middleTargetPos2, mouseController.posIndicatorLerpSpeed * 0.55f * Time.deltaTime);
        aimMiddlePoint3.transform.position = Vector3.Lerp(aimMiddlePoint3.transform.position, middleTargetPos3, mouseController.posIndicatorLerpSpeed * 0.6f * Time.deltaTime);

        var aimDistToPlayer = Mathf.Clamp(distance * aimSizeModifier, aimMinSize, aimMaxSize);
        var aimTargetScale = new Vector3(aimDistToPlayer, aimDistToPlayer, aimDistToPlayer);
        aimTarget.transform.localScale = aimTargetScale;

        var middleDistToPlayer1 = Mathf.Clamp(Vector3.Distance(aimMiddlePoint1.transform.position, groundPos) * middleSizeModifier, middleMinSize, middleMaxSize);
        var middleTargetScale1 = new Vector3(middleDistToPlayer1, middleDistToPlayer1, middleDistToPlayer1);
        aimMiddlePoint1.transform.localScale = middleTargetScale1;

        var middleDistToPlayer2 = Mathf.Clamp(Vector3.Distance(aimMiddlePoint2.transform.position, groundPos) * middleSizeModifier, middleMinSize, middleMaxSize);
        var middleTargetScale2 = new Vector3(middleDistToPlayer2, middleDistToPlayer2, middleDistToPlayer2);
        aimMiddlePoint2.transform.localScale = middleTargetScale2;

        var middleDistToPlayer3 = Mathf.Clamp(Vector3.Distance(aimMiddlePoint3.transform.position, groundPos) * middleSizeModifier, middleMinSize, middleMaxSize);
        var middleTargetScale3 = new Vector3(middleDistToPlayer3, middleDistToPlayer3, middleDistToPlayer3);
        aimMiddlePoint3.transform.localScale = middleTargetScale3;

        //Debug.DrawLine(groundPos, endPos);
    }

    private void OnEnable() {
        aimTarget.SetActive(true);

        aimMiddlePoint1.SetActive(true);
        aimMiddlePoint2.SetActive(true);
        aimMiddlePoint3.SetActive(true);

        aimTarget.transform.position = this.transform.position;

        aimMiddlePoint1.transform.position = this.transform.position;
        aimMiddlePoint2.transform.position = this.transform.position;
        aimMiddlePoint3.transform.position = this.transform.position;
    }

    private void OnDisable() {
        aimTarget.SetActive(false);

        aimMiddlePoint1.SetActive(false);
        aimMiddlePoint2.SetActive(false);
        aimMiddlePoint3.SetActive(false);
    }
}
