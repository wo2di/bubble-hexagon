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
            Bubble b = _sm.bubbleFactory.SpawnBubble("color");
            bubble = b;
            _sm.bubbleParent.SetBubbleNow(b);
        }

        public override void UpdateLogic()
        {
            //인풋 처리
            if(Input.GetMouseButtonUp(0) && target == null)
            {
                _sm.rayCaster.GetSlotByRay(out target, out waypoints);

                if(target !=null)
                {
                    bubble.SetSlot(target);
                }
            }
        }

        public override void UpdatePhysics()
        {
            //버블 이동
            if(target != null)
            {
                Transform tr = bubble.transform;
                if(waypoints.Count != 0)
                {
                    if (Vector3.Distance(waypoints[0], tr.position) > 0.1)
                    {
                        tr.position = Vector3.MoveTowards(tr.position, waypoints[0], 15 * Time.deltaTime);
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
                    _sm.ChangeState(_sm.standby);
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

    //버블이 날아가는중
    public class BubbleFired : State 
    {
        GameplaySM _sm;
        public BubbleFired(GameplaySM sm) : base("BubbleFired", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            base.Enter();
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
        }

        public override void UpdateLogic()
        {
            //터트림이 끝났는지 확인하여 다음 스테이트로
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
        }

        public override void UpdateLogic()
        {
            //다 덜어트렸는지 확인하여 다음 스테이트로
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
            base.Enter();
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
            base.Enter();
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
}
