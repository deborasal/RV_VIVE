using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickMoveDropScript06 : MonoBehaviour
{
    private bool moveableObjectGrabbed = false;

    public LayerMask whatIsMoveableObject;
    public LayerMask whatIsGround;

    private Ray movebleObjectRay;
    private RaycastHit movebleObjectHit;
    private RaycastHit groundHit;
    private RaycastHit mousePosHit;

    private Transform movebleObject;
    public Transform cursorPointer;
    public Transform bat_1;
    public Transform bat_2;
    public Transform bat_3;
    public Transform disc_1;
    public Transform disc_2;
    public Transform disc_3;
    public Transform gameWon;

    private Vector3 mousePosToGround;
    private Vector3 position_bat_1;
    private Vector3 position_bat_2;
    private Vector3 position_bat_3;
    private Vector3 initial_position;
    //private float fixed_Z;

    private Stack<int> stack_bat_1;
    private Stack<int> stack_bat_2;
    private Stack<int> stack_bat_3;

    //public List<Stack<int>> list_stack_bat;
    //public List<Transform> list_disc;
    //public List<Transform> list_bat;

    private static int total_bats = 3;
    private static int total_discs = 3;
    private static int last_bat = 3;
    public float distance;
    public float smooth;
    public int used_discs;
    private int move_count;

    // Use this for initialization
    void Start ()
    {
        gameWon.gameObject.SetActive(false);

        movebleObjectHit = new RaycastHit();
        groundHit = new RaycastHit();
        bat_1.gameObject.SetActive(false);
        bat_2.gameObject.SetActive(false);
        bat_3.gameObject.SetActive(false);

        position_bat_1 = bat_1.position;
        position_bat_2 = bat_2.position;
        position_bat_3 = bat_3.position;
        //fixed_Z = position_bat_1.z;

        stack_bat_1 = new Stack<int>();
        stack_bat_2 = new Stack<int>();
        stack_bat_3 = new Stack<int>();

        //list_stack_bat = new List<Stack<int>>();
        //list_stack_bat.Add(stack_bat_1);
        //list_stack_bat.Add(stack_bat_2);
        //list_stack_bat.Add(stack_bat_3);

        //list_disc = new List<Transform>();
        //list_disc.Add(disc_1);
        //list_disc.Add(disc_2);
        //list_disc.Add(disc_3);

        //list_bat = new List<Transform>();
        //list_bat.Add(bat_1);
        //list_bat.Add(bat_2);
        //list_bat.Add(bat_3);

        //for (int i = 0; i < total_discs; i++)
        //    list_disc[i].gameObject.SetActive(false);

        //used_discs = 3;

        //for (int i = 0; i < used_discs; i++)
        //{
        //    list_disc[i].gameObject.SetActive(true);
        //    list_stack_bat[0].Push(list_disc[i].GetComponent<properties>().GetSize());
        //}

        stack_bat_1.Push(disc_1.GetComponent<properties>().GetSize());
        stack_bat_1.Push(disc_2.GetComponent<properties>().GetSize());
        stack_bat_1.Push(disc_3.GetComponent<properties>().GetSize());

        move_count = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        movebleObjectRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (moveableObjectGrabbed)
            {
                DropMoveableObject();
            }
            else
            {
                FindGrabMoveableObject();
            }
        }

        if (moveableObjectGrabbed)
        {
            ÙpdateObjectPosition();
            if (movebleObject.GetComponent<properties>().GetTouched())
            {
                //DropMoveableObject();
            }
        }
		
        if (stack_bat_3.Count == used_discs) //list_stack_bat[last_bat].Count == used_discs)
        {
            cursorPointer.gameObject.SetActive(false);
            gameWon.gameObject.GetComponent<TextMesh>().text = "You won!!\n"+move_count.ToString()+" moves";
            gameWon.gameObject.SetActive(true);
        }
	}

    void FindGrabMoveableObject()
    {
        if(Physics.Raycast(movebleObjectRay,out movebleObjectHit, distance, whatIsMoveableObject))
        {
            if (TryPop(movebleObjectHit.transform))
            {
                movebleObject = movebleObjectHit.transform;
                moveableObjectGrabbed = true;
                initial_position = movebleObject.position;
                movebleObject.GetComponent<Rigidbody>().useGravity = false;
                movebleObject.GetComponent<properties>().SetBeingCarried(true);
                //movebleObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
                //movebleObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
                cursorPointer.gameObject.SetActive(false);
            }
        }
    }

    void ÙpdateObjectPosition()
    {
        Vector3 reference = Camera.main.transform.position + (Camera.main.transform.forward * distance);
        movebleObject.position = Vector3.Lerp(movebleObject.position, reference, Time.deltaTime * smooth);
        //cursorPointer.position;
    }

    void DropMoveableObject()
    {
        if (movebleObject != null)
        {
            if (!TryPush(movebleObject))
            {
                movebleObject.position = initial_position;
                TryPush(movebleObject);
            }
            if (movebleObject.position.x >= initial_position.x * 0.9 && movebleObject.position.x <= initial_position.x * 1.1)
            {
                move_count--;
            }
            //movebleObject.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezePositionX;
            //movebleObject.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezePositionZ;
            movebleObject.GetComponent<Rigidbody>().useGravity = true;
            movebleObject.GetComponent<properties>().SetBeingCarried(false);
            movebleObject = null;
            moveableObjectGrabbed = false;
            cursorPointer.gameObject.SetActive(true);
        }
    }

    bool TryPop(Transform hitObject)
    {

        Vector3 aux = hitObject.position;
        //Vector3 bat_position;
        //for (int i = 0; i < total_bats; i++)
        //{
        //    bat_position = list_bat[i].position;
        //    if (aux.x >= bat_position.x * 0.9 && aux.x <= bat_position.x * 1.1)
        //    {
        //        if (list_stack_bat[i].Peek() == hitObject.GetComponent<properties>().GetSize())
        //        {
        //            list_stack_bat[i].Pop();
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //}
        //return false;

        if (aux.x >= position_bat_1.x * 0.9 && aux.x <= position_bat_1.x * 1.1)
        {
            if (stack_bat_1.Peek() == hitObject.GetComponent<properties>().GetSize())
            {
                stack_bat_1.Pop();
                return true;
            }
            else
                return false;
        }
        if (aux.x >= position_bat_2.x * 0.9 && aux.x <= position_bat_2.x * 1.1)
        {
            if (stack_bat_2.Peek() == hitObject.GetComponent<properties>().GetSize())
            {
                stack_bat_2.Pop();
                return true;
            }
            else
                return false;
        }
        if (aux.x >= position_bat_3.x * 0.9 && aux.x <= position_bat_3.x * 1.1)
        {
            if (stack_bat_3.Peek() == hitObject.GetComponent<properties>().GetSize())
            {
                stack_bat_3.Pop();
                return true;
            }
            else
                return false;
        }
        return false;
    }

    bool TryPush(Transform droppedObject)
    {
        Vector3 aux = droppedObject.position;
        //Vector3 bat_position;
        //for (int i = 0; i < total_bats; i++)
        //{
        //    bat_position = list_bat[i].position;
        //    if (aux.x >= bat_position.x * 0.9 && aux.x <= bat_position.x * 1.1)
        //    {
        //        if (list_stack_bat[i].Count == 0 || list_stack_bat[i].Peek() > droppedObject.GetComponent<properties>().GetSize())
        //        {
        //            droppedObject.position = new Vector3(bat_position.x, aux.y, bat_position.z);
        //            list_stack_bat[i].Push(droppedObject.GetComponent<properties>().GetSize());
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //return false;

        if (aux.x >= position_bat_1.x * 0.9 && aux.x <= position_bat_1.x * 1.1)
        {
            if (stack_bat_1.Count == 0 || stack_bat_1.Peek() > droppedObject.GetComponent<properties>().GetSize())
            {
                droppedObject.position = new Vector3(position_bat_1.x, aux.y, position_bat_1.z);
                stack_bat_1.Push(droppedObject.GetComponent<properties>().GetSize());
                move_count++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (aux.x >= position_bat_2.x * 0.9 && aux.x <= position_bat_2.x * 1.1)
        {
            if (stack_bat_2.Count == 0 || stack_bat_2.Peek() > droppedObject.GetComponent<properties>().GetSize())
            {
                droppedObject.position = new Vector3(position_bat_2.x, aux.y, position_bat_2.z);
                stack_bat_2.Push(droppedObject.GetComponent<properties>().GetSize());
                move_count++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (aux.x >= position_bat_3.x * 0.9 && aux.x <= position_bat_3.x * 1.1)
        {
            if (stack_bat_3.Count == 0 || stack_bat_3.Peek() > droppedObject.GetComponent<properties>().GetSize())
            {
                droppedObject.position = new Vector3(position_bat_3.x, aux.y, position_bat_3.z);
                stack_bat_3.Push(droppedObject.GetComponent<properties>().GetSize());
                move_count++;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
