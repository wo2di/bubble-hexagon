using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class State 
    {
        public string name;
        public StateMachine machine;
        public State(string name, StateMachine machine)
        {
            this.name = name;
            this.machine = machine;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }

    public class Initialize : State
    {
        GameplaySM _sm;

        public Initialize(GameplaySM sm) : base("Initialize", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            if (_sm.gameplaySaveLoad.CheckGameplaySave())
            {
                Debug.Log("load!");
                _sm.gameplaySaveLoad.LoadGameplay();
            }
            _sm.ChangeState(_sm.standby);
        }
    }

    //버블이 생기고 발사를 기다리는 상태
    public class Standby : State
    {
        GameplaySM _sm;

        Bubble bubbleNow;
        Slot target;
        List<Vector3> waypoints;
        bool shoot = false;

        public Standby(GameplaySM sm) : base("Standby", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            _sm.levelManager.CheckShootCount();

            if(_sm.bubbleParent.bubble1 != null && _sm.bubbleParent.bubble2 != null)
            {
                if (_sm.bubbleParent.bubble1.transform.IsChildOf(_sm.bubbleParent.transform))
                {
                    //게임 진행중

                    _sm.bubbleParent.bubble1 = _sm.bubbleParent.bubble2;

                    //bubble2 생성
                    if (_sm.bubbleParent.bubble3 == null)
                    {
                        _sm.bubbleParent.bubble2 = _sm.bubbleFactory.SpawnRandomBubble();
                    }
                    //bubble3을 bubble2로
                    else
                    {
                        _sm.bubbleParent.bubble2 = _sm.bubbleParent.bubble3;
                        _sm.bubbleParent.bubble3 = null;
                    }
                }
                else
                {
                    //첫 로드
                }
            }
            else
            {
                //새 게임
                if (_sm.bubbleParent.GetBubblesInGrid().Count == 1)
                {
                    _sm.bubbleParent.bubble1 = _sm.bubbleFactory.SpawnRandomBubbleWhenEmpty();
                }
                else
                {
                    _sm.bubbleParent.bubble1 = _sm.bubbleFactory.SpawnRandomBubble();
                }
                _sm.bubbleParent.bubble2 = _sm.bubbleFactory.SpawnRandomBubble();
            }

            bubbleNow = _sm.bubbleParent.bubble1;
            _sm.bubbleParent.SetBubbles();
            _sm.gameplaySaveLoad.SaveGameplay();

        }

        public override void UpdateLogic()
        {
            bubbleNow = _sm.bubbleParent.bubble1;

            if (!shoot)
            {
                if (Input.GetMouseButton(0))
                {
                    _sm.rayCaster.GetSlotByRay(out target, out waypoints);
                    _sm.bubbleTrajectory?.DrawTrajectory(waypoints);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    _sm.rayCaster.GetSlotByRay(out target, out waypoints);
                    _sm.bubbleTrajectory?.ResetTrajectory();
                    if (target != null)
                    {
                        _sm.audioManager.PlaySound("bubbleshoot");
                        _sm.bubbleParent.bubble1.SetSlot(target);
                        shoot = true;
                    }
                }
            }
            ////인풋 처리
            //if (Input.GetMouseButtonUp(0) && target == null && _sm.bubbleParent.bubble1 != null)
            //{
            //    _sm.rayCaster.GetSlotByRay(out target, out waypoints);
            //    //Debug.Log("Ray!");

            //    if (target != null)
            //    {
            //        //if (target.bubble != null)
            //        //{ Debug.Log("error!"); }

            //        _sm.audioManager.PlaySound("bubbleshoot");
            //        _sm.bubbleParent.bubble1.SetSlot(target);


            //        ///////////

            //        //_sm.gridParent.ResetSlotColor();
            //        //target.GetComponent<SpriteRenderer>().color = Color.red;
            //    }
            //    else
            //    {
            //        Debug.Log("Target is Null");
            //    }
            //}


            ////디버그//////
            //if (Input.GetMouseButton(0) && target == null)
            //{
            //    Slot testTarget;
            //    List<Vector3> testWaypoints;
            //    _sm.rayCaster.GetSlotByRay(out testTarget, out testWaypoints);
            //    if (testTarget != null)
            //    {
            //        _sm.gridParent.ResetSlotColor();
            //        testTarget.GetComponent<SpriteRenderer>().color = Color.red;
            //    }
            //}
        }

        public override void UpdatePhysics()
        {
            //버블 이동
            if (target != null && shoot)
            {
                Transform tr = bubbleNow.transform;
                if(waypoints.Count != 0)
                {
                    if (Vector3.Distance(waypoints[0], tr.position) > 0.1)
                    {
                        tr.position = Vector3.MoveTowards(tr.position, waypoints[0], _sm.bubbleParent.bubbleSpeed * Time.deltaTime);
                    }
                    else
                    {
                        tr.position = waypoints[0];
                        waypoints.RemoveAt(0);
                    }
                }
                else
                {
                    //bubbleNow.FitToSlot();

                    _sm.audioManager.PlaySound("bubblecollide");
                    bubbleNow.StartCoroutine(bubbleNow.TranslateToSlot());
                    bubbleNow.transform.SetParent(_sm.bubbleParent.transform);

                    if (!bubbleNow.slot.gameObject.activeSelf)
                    {
                        _sm.ChangeState(_sm.gameOver);
                    }
                    else
                    {
                        switch (bubbleNow.GetComponent<BubbleBehaviour>())
                        {
                            case BubbleBHBomb bhBomb:
                            case BubbleBHRandom bhRandom:
                                _sm.ChangeState(_sm.applyItem);
                                break;
                            default:
                                _sm.ChangeState(_sm.bubblePop);
                                break;
                        }
                    }

                    
                }
            }
        }

        public override void Exit()
        {
            bubbleNow = null;
            target = null;
            waypoints = null;
            shoot = false;

            _sm.statistics.shootCnt.value++;
        }
    }


    
    //터트릴 버블을 터트림
    public class BubblePop : State 
    {
        GameplaySM _sm;
        public BubblePop(GameplaySM sm) : base("BubblePop", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            //터트림 확인 후 터트린다
            _sm.bubblesToPop.bubbles = new List<Bubble>();
            _sm.bubbleParent.bubble1.GetComponent<BubbleBehaviour>().OnSetToSlot();

            _sm.itemManager.AddPoint(_sm.bubblesToPop.bubbles.Count);
            _sm.popCount.value += _sm.bubblesToPop.bubbles.Count;
            _sm.StartCoroutine(PopCoroutine());
        }

        public IEnumerator PopCoroutine()
        {
            foreach(Bubble b in _sm.bubblesToPop.bubbles)
            {
                yield return new WaitForSeconds(0.1f);
                _sm.scoreManager.AddScore(10);
                _sm.audioManager.PlaySound("bubblepop");
                b.GetComponent<BubbleBehaviour>().OnPop();
            }
            _sm.ChangeState(_sm.bubbleDrop);
        }
    }

    public class ApplyItem : State
    {

        GameplaySM _sm;
        public ApplyItem(GameplaySM sm) : base("ApplyItem", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            _sm.bubbleParent.bubble1.GetComponent<BubbleBehaviour>().OnSetToSlot();
        }

        public override void UpdateLogic()
        {
            if (_sm.itemApplied)
            {
                _sm.ChangeState(_sm.bubbleDrop);
            }
        }

        public override void Exit()
        {
            _sm.itemApplied = false;
        }
    }

    //떨어트릴 버블을 떨침
    public class BubbleDrop : State 
    {
        GameplaySM _sm;
        public BubbleDrop(GameplaySM sm) : base("BubbleDrop", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            //떨어트림을 확인하여 떨어트린다
            _sm.rootBubble.GetBubblesToDrop();

            _sm.itemManager.AddPoint(_sm.bubblesToDrop.bubbles.Count * 2);
            _sm.dropCount.value += _sm.bubblesToDrop.bubbles.Count;
            _sm.StartCoroutine(DropCoroutine());
        }

        public IEnumerator DropCoroutine()
        {
            int dropCount = _sm.bubblesToDrop.bubbles.Count;
            if (dropCount > 0)
            {
                _sm.audioManager.PlaySound("bubblefall");
            }
            foreach (Bubble b in _sm.bubblesToDrop.bubbles)
            {
                b.transform.SetParent(_sm.bubbleDroppedTR);
                _sm.scoreManager.AddScore(20);
                b.GetComponent<BubbleBehaviour>().OnDrop();
                yield return new WaitForSeconds(0.03f);
            }
            //_sm.ChangeState(_sm.exitTurn);
            _sm.ChangeState(_sm.rotateGrid);

        }
    }

    //점수 계산, 특수 버블 할일 함
    public class ExitTurn : State 
    {
        GameplaySM _sm;
        public ExitTurn(GameplaySM sm) : base("ExitTurn", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            foreach(Bubble b in _sm.bubbleParent.GetBubblesInGrid())
            {
                //if (b != _sm.bubbleParent.bubble1)
                //{
                //    b.GetComponent<BubbleBehaviour>().OnExitTurn();
                //}
                
                b.GetComponent<BubbleBehaviour>().OnExitTurn();

            }

            //_sm.ChangeState(_sm.rotateGrid);
            _sm.ChangeState(_sm.standby);
        }

    }

    // 게임판 회전
    public class RotateGrid : State 
    {
        GameplaySM _sm;
        public RotateGrid(GameplaySM sm) : base("RotateGrid", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            _sm.StartCoroutine(RotateCoroutine());
        }

        public IEnumerator RotateCoroutine()
        {
            yield return new WaitForSeconds(0.3f);

            Transform tr = _sm.rotateGame.transform;
            Quaternion before = tr.rotation;
            tr.Rotate(0, 0, -60);
            Quaternion after = tr.rotation;
            tr.Rotate(0, 0, 60);

            while (Quaternion.Angle(tr.rotation, after) > 1)
            {
                _sm.rotateGame.RotateToResult(after);
                yield return null;
            }
            tr.rotation = after;

            //_sm.ChangeState(_sm.standby);
            _sm.ChangeState(_sm.exitTurn);
        }
    }

    public class GameOver : State
    {
        GameplaySM _sm;
        public GameOver(GameplaySM sm) : base("Gameover", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            _sm.scoreManager.CheckTopScore();
            _sm.playerdataSaveLoad.SaveSequence();
            _sm.gameplaySaveLoad.DeleteGameplaySave();
            _sm.gameOverEvent.Raise();
            _sm.audioManager.PlaySound("gameover");
            _sm.audioManager.TurnOffBGM();
        }
    }
}
