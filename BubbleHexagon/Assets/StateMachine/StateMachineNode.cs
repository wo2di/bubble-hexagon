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

        Bubble bubbleNow;
        Slot target;
        List<Vector3> waypoints;
        public Standby(GameplaySM sm) : base("Standby", sm)
        {
            _sm = sm;
        }

        public override void Enter()
        {
            _sm.levelManager.CheckShootCount();

            if (_sm.bubbleParent.bubble2 == null)
            {
                if (_sm.bubbleParent.GetBubblesInGrid().Count == 1)
                {
                    bubbleNow = _sm.bubbleFactory.SpawnRandomBubbleWhenEmpty();
                }
                else
                {
                    bubbleNow = _sm.bubbleFactory.SpawnRandomBubble();
                }
                _sm.bubbleParent.bubble1 = bubbleNow;
            }
            else
            {
                bubbleNow = _sm.bubbleParent.bubble2;
                _sm.bubbleParent.bubble1 = bubbleNow;
            }

            if (_sm.bubbleParent.bubble3 == null)
            {
                _sm.bubbleParent.bubble2 = _sm.bubbleFactory.SpawnRandomBubble();
            }
            else
            {
                _sm.bubbleParent.bubble2 = _sm.bubbleParent.bubble3;
                _sm.bubbleParent.bubble3 = null;
            }

            _sm.bubbleParent.SetBubbles();

        }

        public override void UpdateLogic()
        {
            bubbleNow = _sm.bubbleParent.bubble1;
            //��ǲ ó��
            if (Input.GetMouseButtonUp(0) && target == null && _sm.bubbleParent.bubble1 != null)
            {
                _sm.rayCaster.GetSlotByRay(out target, out waypoints);
                //Debug.Log("Ray!");

                if (target != null)
                {
                    //if (target.bubble != null)
                    //{ Debug.Log("error!"); }

                    _sm.audioManager.PlaySound("bubbleshoot");
                    _sm.bubbleParent.bubble1.SetSlot(target);


                    ///////////

                    //_sm.gridParent.ResetSlotColor();
                    //target.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }


            //////�����//////
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
            //���� �̵�
            if (target != null)
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
                    _sm.ChangeState(_sm.bubblePop);
                }
            }
        }

        public override void Exit()
        {
            bubbleNow = null;
            target = null;
            waypoints = null;

            _sm.statistics.shootCnt.value++;
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
            _sm.bubblesToPop.bubbles = new List<Bubble>();
            _sm.bubbleParent.bubble1.GetComponent<BubbleBehaviour>().OnSetToSlot();

            _sm.itemManager.AddPoint(_sm.bubblesToPop.bubbles.Count);
            _sm.StartCoroutine(PopCoroutine());
        }

        public IEnumerator PopCoroutine()
        {
            foreach(Bubble b in _sm.bubblesToPop.bubbles)
            {
                yield return new WaitForSeconds(0.1f);
                _sm.audioManager.PlaySound("bubblepop");
                b.GetComponent<BubbleBehaviour>().OnPop();
            }
            _sm.ChangeState(_sm.bubbleDrop);
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
            _sm.rootBubble.GetBubblesToDrop();

            _sm.itemManager.AddPoint(_sm.bubblesToDrop.bubbles.Count * 2);
            _sm.StartCoroutine(DropCoroutine());
        }

        public IEnumerator DropCoroutine()
        {
            foreach (Bubble b in _sm.bubblesToDrop.bubbles)
            {
                b.GetComponent<BubbleBehaviour>().OnDrop();
                yield return new WaitForSeconds(0.1f);
            }
            //_sm.ChangeState(_sm.exitTurn);
            _sm.ChangeState(_sm.rotateGrid);

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
}
