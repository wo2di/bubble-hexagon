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

    //버블이 생기고 발사를 기다리는 상태
    public class Standby : State
    {
        GameplaySM _sm;

        Bubble bubble;
        Slot target;
        List<Vector3> waypoints;
        public Standby(GameplaySM sm) : base("Standby", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            //버블 생성
            Bubble b = _sm.bubbleFactory.SpawnRandomBubble();
            bubble = b;
            _sm.bubbleParent.SetBubbleNow(b);
        }

        public override void UpdateLogic()
        {
            //인풋 처리
            if (Input.GetMouseButtonUp(0) && target == null && bubble != null)
            {
                _sm.rayCaster.GetSlotByRay(out target, out waypoints);
                //Debug.Log("Ray!");

                if (target != null)
                {
                    //if (target.bubble != null)
                    //{ Debug.Log("error!"); }

                    bubble.SetSlot(target);


                    ///////////

                    //_sm.gridParent.ResetSlotColor();
                    //target.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }


            //////디버그//////
            //if(Input.GetMouseButton(0) && target == null)
            //{
            //    Slot testTarget;
            //    List<Vector3> testWaypoints;
            //    _sm.rayCaster.GetSlotByRay(out testTarget, out testWaypoints);
            //    if(testTarget != null)
            //    {
            //        _sm.gridParent.ResetSlotColor();
            //        testTarget.GetComponent<SpriteRenderer>().color = Color.red;
            //    }
            //}
        }

        public override void UpdatePhysics()
        {
            //버블 이동
            if (target != null)
            {
                Transform tr = bubble.transform;
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
                    bubble.FitToSlot();
                    _sm.ChangeState(_sm.bubblePop);
                }
            }
        }

        public override void Exit()
        {
            bubble = null;
            target = null;
            waypoints = null;
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
            _sm.bubbleParent.bubbleNow.GetComponent<BubbleBehaviour>().OnSetToSlot();
            _sm.StartCoroutine(PopCoroutine());
        }

        public IEnumerator PopCoroutine()
        {
            foreach(Bubble b in _sm.bubblesToPop.bubbles)
            {
                b.GetComponent<BubbleBehaviour>().OnPop();
                yield return new WaitForSeconds(0.1f);
            }
            _sm.ChangeState(_sm.bubbleDrop);
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
            _sm.StartCoroutine(DropCoroutine());
        }

        public IEnumerator DropCoroutine()
        {
            foreach (Bubble b in _sm.bubblesToDrop.bubbles)
            {
                b.GetComponent<BubbleBehaviour>().OnDrop();
                yield return new WaitForSeconds(0.1f);
            }
            _sm.ChangeState(_sm.exitTurn);
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
                if (b != _sm.bubbleParent.bubbleNow)
                {
                    b.GetComponent<BubbleBehaviour>().OnExitTurn();
                }
            }

            _sm.ChangeState(_sm.rotateGrid);
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
        }

        public override void Exit()
        {
            base.Exit();
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

            _sm.ChangeState(_sm.standby);
        }
    }
}
