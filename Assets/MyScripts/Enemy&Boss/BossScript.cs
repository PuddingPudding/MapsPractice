using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{   //召喚怪物需要音效 整體戰鬥需要BGM 火坑改一下位子 火坑的狀態改一下，剛開始不會有火，打敗後才會冒出火焰  過關的結束轉場
    public AudioSource BOSSBGM;
    public float HPMax = 300;
    public GameObject target;
    public GameObject fireBallCandidate;
    public GameObject[] FBThrowPosList; //BOSS扔出火球時的位子
    public GameObject[] fireBallPosList; //將生成火球的幾個位子記下，在這些空物件的地方生成火球
    public float fireBallGeneratePeriod = 1.5f;
    public float fireBallFeverPeriod = 0.8f;
    public float fireBallCounter = -4;
    public GameObject enemyCandidate;

    public SpriteRenderer[] enemyPosList1; //生成敵人的幾個位子
    public SpriteRenderer[] enemyPosList2_1; //第二階段的敵人生成位子(隱形怪)
    public TranslucentScript[] translucentEnemyList;
    public SpriteRenderer[] enemyPosList2_2; //第二階段的敵人生成位子(硬硬怪)
    public HightLight[] strongEnemyList;

    public float enemyGeneratePeriod = 2.5f;
    public float enemyCounter = -4;
    public GameObject fireCircleCandidate;
    public float fireCircleHight = 91.5f;
    public float fireCircleGeneratePeriod = 5f;
    public float fireCircleCounter = 0;
    public ParticleSystem flameOnHead;
    public AudioSource flameBurstNoise;
    public float flameBurst = 2f;

    private float CurrentHP = 300;
    private float MinimumHitPeriod = 1f;
    private float HitCounter = 0;
    private Animator animator;

    private float freezeTime = 5f;
    private bool readyAngry = true;
    private float freezeBGMTime = 8f;

    private bool translucentSummoning = false; //表示正在召喚
    private bool strongSummoning = false;
    private bool feverMode = false; //最後的爆走模式

    //吐心臟部分//////////
    private bool immueMode = false;
    private bool heartDropEvent = true; //心臟掉出的事件，在半血掉出心臟後，該事件被設為false
    public GameObject bossHeart;
    public GameObject immueShield;
    public GameObject flames;

    // Use this for initialization
    void Start()
    {
        CurrentHP = HPMax;
        animator = this.GetComponent<Animator>();
        bossHeart.SetActive(false);
        flames.SetActive(false);//先將兩旁的火堆熄滅
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeTime > 0 && this.readyAngry)
        {
            freezeTime -= Time.deltaTime;
            immueMode = true;
        }
        else if (this.readyAngry)
        {
            animator.SetTrigger("Preperation");
            readyAngry = false;
        }

        if (HitCounter >= 0)
        {
            HitCounter -= Time.deltaTime;
        }

        if (this.immueMode)//處於無敵狀態
        {
            immueShield.SetActive(true);
            immueShield.transform.eulerAngles -= new Vector3(0, 120 * Time.deltaTime, 0);
        }
        else
        {
            immueShield.SetActive(false);
        }


        if (CurrentHP > HPMax - 300) //在被扣掉300血之前，都會是一直甩火球
        {
            if (fireBallCounter <= fireBallGeneratePeriod)
            {
                fireBallCounter += Time.deltaTime;
            }
            else
            {
                fireBallCounter -= fireBallGeneratePeriod;
                animator.SetTrigger("FireBallAtk");
            }
        }

        if (this.translucentSummoning) //BOSS正在召喚隱形怪的時候，進入以下判斷
        {
            int deadCount = 0;
            for(int i = 0; i < translucentEnemyList.Length; i++)
            {
                if(translucentEnemyList[i].GetComponent<EnemyScript>().CurrentHP <= 0)
                {
                    deadCount++;
                }
            }
            if(deadCount == translucentEnemyList.Length)
            {
                this.translucentSummoning = false;
                this.strongEnemySpawn();
            }
        }

        if(this.strongSummoning)
        {
            int deadCount = 0;
            for(int i = 0; i < strongEnemyList.Length; i++)
            {
                if (strongEnemyList[i].GetComponent<EnemyScript>().CurrentHP <= 0)
                {
                    deadCount++;
                }
            }
            if (deadCount == strongEnemyList.Length)
            {
                this.strongSummoning = false;
                this.immueMode = false;
            }
        }

        if (this.feverMode)
        {
            if (fireBallCounter <= fireBallFeverPeriod)
            {
                fireBallCounter += Time.deltaTime;
            }
            else
            {
                fireBallCounter -= fireBallFeverPeriod;
                this.fireBallSpawn();
            }
            if (fireCircleCounter <= fireCircleGeneratePeriod)
            {
                fireCircleCounter += Time.deltaTime;
            }
            else
            {
                fireCircleCounter -= fireCircleGeneratePeriod;
                this.fireCircleSpawn();
                Invoke("fireCircleEnd", 4.3f);
            }
        }
    }

    public void BattleBegin()
    {
        this.immueMode = false;
        BOSSBGM.Play();
    }

    public void fireBallThrow(int num) //每次扔火球會製造兩發，由於左右手不同，需得知第一還是第二
    {
        GameObject newFireBall = GameObject.Instantiate(fireBallCandidate);
        newFireBall.transform.position = FBThrowPosList[num].transform.position;
        newFireBall.GetComponent<FireBallScript>().InitAndShoot(this.target);
    }

    public void fireBallSpawn()
    {
        GameObject newFireBall = GameObject.Instantiate(fireBallCandidate);
        int numTemp = Random.Range(0, fireBallPosList.Length);
        newFireBall.transform.position = fireBallPosList[numTemp].transform.position;
        newFireBall.GetComponent<FireBallScript>().InitAndShoot(this.target);
    }

    public void enemySpawn()
    {
        for (int i = 0; i < enemyPosList1.Length; i++)
        {
            GameObject newEnemy = GameObject.Instantiate(enemyCandidate);
            newEnemy.transform.position = enemyPosList1[i].transform.position - new Vector3(0, 1.2f, 0);
            newEnemy.GetComponent<EnemyScriptVerDestory>().enabled = false;
            newEnemy.transform.DOMoveY(1.2f, 2f).SetRelative(true).OnComplete(() =>
            {
                newEnemy.GetComponent<EnemyScriptVerDestory>().enabled = true;
                newEnemy.GetComponent<EnemyScriptVerDestory>().FollowTarget = target;
            });

            SpriteRenderer SR = enemyPosList1[i]; //由於用Dotween的話，當秒數讀完，i也跑掉了，所以需要先把該SpriteRenderer放到暫存
            DOTween.To(() => SR.color, x => SR.color = x, Color.white, 2f).OnComplete(() =>
            {
                DOTween.To(() => SR.color, x => SR.color = x, Color.clear, 3f);
            });
        }
    }

    public void translucentEnemySpawn()
    {
        if (!this.translucentSummoning)
        {
            this.translucentSummoning = true;
            for (int i = 0; i < enemyPosList2_1.Length; i++)
            {
                SpriteRenderer SR = enemyPosList2_1[i]; //由於用Dotween的話，當秒數讀完，i也跑掉了，所以需要先把該SpriteRenderer放到暫存
                DOTween.To(() => SR.color, x => SR.color = x, Color.green, 2f).OnComplete(() =>
                {
                    DOTween.To(() => SR.color, x => SR.color = x, Color.clear, 3f);
                });

                translucentEnemyList[i].gameObject.SetActive(true);
                translucentEnemyList[i].transform.position = enemyPosList2_1[i].transform.position - new Vector3(0, 1.2f, 0);
                TranslucentScript TS = translucentEnemyList[i];
                TS.BecomeNormal();
                TS.transform.DOMoveY(1.2f, 2f).SetRelative(true).OnComplete(() =>
                {
                    TS.GetComponent<EnemyScript>().enabled = true;
                    TS.GetComponent<EnemyScript>().FollowTarget = target;
                    TS.BecomeTranslucent();
                });
            }
        }
    }

    public void strongEnemySpawn()
    {
        if (!this.strongSummoning)
        {
            this.strongSummoning = true;
            for (int i = 0; i < enemyPosList2_2.Length; i++)
            {
                SpriteRenderer SR = enemyPosList2_2[i]; //由於用Dotween的話，當秒數讀完，i也跑掉了，所以需要先把該SpriteRenderer放到暫存
                DOTween.To(() => SR.color, x => SR.color = x, Color.yellow, 3f).OnComplete(() =>
                {
                    DOTween.To(() => SR.color, x => SR.color = x, Color.clear, 3f);
                });

                strongEnemyList[i].gameObject.SetActive(true);
                strongEnemyList[i].transform.position = enemyPosList2_2[i].transform.position - new Vector3(0, 3.6f, 0);
                HightLight SE = strongEnemyList[i];
                SE.transform.DOMoveY(3.6f, 3f).SetRelative(true).OnComplete(() =>
                {
                    SE.GetComponent<EnemyScript>().enabled = true;
                    SE.GetComponent<EnemyScript>().FollowTarget = target;
                });
            }
        }
    }

    public void fireCircleSpawn()
    {
        animator.SetBool("Charging", true);
        GameObject newFireCircle = GameObject.Instantiate(fireCircleCandidate);
        newFireCircle.transform.position = new Vector3(target.transform.position.x, fireCircleHight, target.transform.position.z);
    }
    public void fireCircleEnd()
    {
        animator.SetBool("Charging", false);
        animator.SetTrigger("Explode");
    }

    public void Hit(float value)
    {
        if (HitCounter <= 0 && !immueMode)
        {
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;
            if (CurrentHP == HPMax - 100 || CurrentHP == HPMax - 200)
            {
                enemySpawn();
            }
            if (CurrentHP == HPMax - 250)
            {
                translucentEnemySpawn();
                this.immueMode = true;
            }
            if(CurrentHP == HPMax - 300)
            {
                this.feverMode = true;
            }
            if (CurrentHP <= 0 && this.heartDropEvent) //這一段死亡時，加個死掉的音效
            {
                this.feverMode = false;
                this.heartDropEvent = false;
                animator.SetBool("Charging",false);
                animator.SetBool("Fall", true);
                flames.SetActive(true);
            }
        }
    }

    public void BuryTheBody()
    {
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-2f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-2f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
            });
        });
    }

    public IEnumerator Angry() //開場的時候王會生氣一下，讓頭上的火焰燒起來
    {
        float startSizeTemp = this.flameOnHead.startSize;
        float startSpeedTemp = this.flameOnHead.startSpeed;
        float lifeTimeTemp = this.flameOnHead.startLifetime;
        flameBurstNoise.Play();
        this.flameOnHead.startSize *= flameBurst;
        this.flameOnHead.startSpeed *= flameBurst;
        this.flameOnHead.startLifetime *= flameBurst;
        yield return new WaitForSeconds(0.8f);
        this.flameOnHead.startSize = startSizeTemp;
        this.flameOnHead.startSpeed = startSpeedTemp;
        this.flameOnHead.startLifetime = lifeTimeTemp;
    }

    public void HeartFall()
    {
        bossHeart.SetActive(true);
        bossHeart.GetComponent<BossHeartScript>().Reduction();
    }
}
