using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanTest : MonoBehaviour
{
    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "Next";

    // 初期化メソッド
    void Start()
    {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
    }

    // 1フレームに1回コールされる
    void Update()
    {

        // 矢印下ボタンを押下している
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // WaitからRunに遷移する
            this.animator.SetBool(key_isRun, true);
        }
        else
        {
            // RunからWaitに遷移する
            this.animator.SetBool(key_isRun, false);
        }

    }
}
