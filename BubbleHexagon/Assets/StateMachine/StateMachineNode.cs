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

    //������ ����� �߻縦 ��ٸ��� ����
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
            //���� ����
            Bubble b = _sm.bubbleFactory.SpawnBubble("color");
            bubble = b;
            _sm.bubbleParent.SetBubbleNow(b);
        }

        public override void UpdateLogic()
        {
            //��ǲ ó��
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
            //���� �̵�
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

    //������ ���ư�����
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
    
    //��Ʈ�� ������ ��Ʈ��
    public class BubblePop : State 
    {
        GameplaySM _sm;
        public BubblePop(GameplaySM sm) : base("BubblePop", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            //��Ʈ�� Ȯ�� �� ��Ʈ����
        }

        public override void UpdateLogic()
        {
            //��Ʈ���� �������� Ȯ���Ͽ� ���� ������Ʈ��
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

    //����Ʈ�� ������ ��ħ
    public class BubbleDrop : State 
    {
        GameplaySM _sm;
        public BubbleDrop(GameplaySM sm) : base("BubbleDrop", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            //����Ʈ���� Ȯ���Ͽ� ����Ʈ����
        }

        public override void UpdateLogic()
        {
            //�� ����Ʈ�ȴ��� Ȯ���Ͽ� ���� ������Ʈ��
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

    //���� ���, Ư�� ���� ���� ��
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

    // ������ ȸ��
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
