using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disk_manage : MonoBehaviour
{
    private bool gameWonFlag;
    private bool carring;

    public Transform bat_1;
    public Transform bat_2;
    public Transform bat_3;
    public Transform disc_1;
    public Transform disc_2;
    public Transform disc_3;
    public Transform gameWon;

    private Vector3 position_bat_1;
    private Vector3 position_bat_2;
    private Vector3 position_bat_3;

    private Stack<int> stack_bat_1;
    private Stack<int> stack_bat_2;
    private Stack<int> stack_bat_3;

    //public List<Stack<int>> list_stack_bat;
    //public List<Transform> list_disc;
    //public List<Vector3> list_bat;

    private static int total_bats = 3;
    private static int max_discs = 3;
    private static int min_discs = 3;
    private static int last_bat = 3;
    public int used_discs = min_discs;
    private int move_count;

    // Use this for initialization
    void Start()
    {
        gameWonFlag = false;
        carring = false;
        gameWon.gameObject.SetActive(false);

        bat_1.gameObject.SetActive(false);
        bat_2.gameObject.SetActive(false);
        bat_3.gameObject.SetActive(false);

        position_bat_1 = bat_1.position;
        position_bat_2 = bat_2.position;
        position_bat_3 = bat_3.position;

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
        //list_bat.Add(position_bat_1);
        //list_bat.Add(position_bat_2);
        //list_bat.Add(position_bat_3);

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
    void Update()
    {
        if (stack_bat_3.Count == used_discs) //list_stack_bat[last_bat].Count == used_discs)
        {
            gameWon.gameObject.GetComponent<TextMesh>().text = "You won!!\n" + move_count.ToString() + " moves";
            gameWon.gameObject.SetActive(true);
            gameWonFlag = true;
        }
    }

    public bool TryPop(int current_size, Vector3 current_position)
    {
        if (!gameWonFlag)
        {
            if (!carring)
            {
                //Vector3 bat_position;
                //for (int i = 0; i < total_bats; i++)
                //{
                //    bat_position = list_bat[i];
                //    if (AreClose(current_position, bat_position))
                //    {
                //        if (list_stack_bat[i].Peek() == current_size)
                //        {
                //            list_stack_bat[i].Pop();
                //            carring = true;
                //            return true;
                //        }
                //        else
                //            return false;
                //    }
                //}
                //return false;

                if (AreClose(current_position, position_bat_1))
                {
                    if (stack_bat_1.Peek() == current_size)
                    {
                        stack_bat_1.Pop();
                        carring = true;
                        return true;
                    }
                    else
                        return false;
                }
                if (AreClose(current_position, position_bat_2))
                {
                    if (stack_bat_2.Peek() == current_size)
                    {
                        stack_bat_2.Pop();
                        carring = true;
                        return true;
                    }
                    else
                        return false;
                }
                if (AreClose(current_position, position_bat_3))
                {
                    if (stack_bat_3.Peek() == current_size)
                    {
                        stack_bat_3.Pop();
                        carring = true;
                        return true;
                    }
                    else
                        return false;
                }
                return false;
            }
            return false;
        }
        return false;
    }

    public bool TryPush(int current_size, Vector3 current_position)
    {
        if (!gameWonFlag)
        {
            if (carring)
            {
                //Vector3 bat_position;
                //for (int i = 0; i < total_bats; i++)
                //{
                //    bat_position = list_bat[i];
                //    if (AreClose(current_position, bat_position))
                //    {
                //        if (list_stack_bat[i].Count == 0 || list_stack_bat[i].Peek() > current_size)
                //        {
                //            //droppedObject.position = new Vector3(bat_position.x, current_position.y, bat_position.z);
                //            list_stack_bat[i].Push(current_size);
                //            move_count++;
                //            carring = false;
                //            return true;
                //        }
                //        else
                //        {
                //            return false;
                //        }
                //    }
                //}
                //return false;

                if (AreClose(current_position, position_bat_1))
                {
                    if (stack_bat_1.Count == 0 || stack_bat_1.Peek() > current_size)
                    {
                        //droppedObject.position = new Vector3(position_bat_1.x, current_position.y, position_bat_1.z);
                        stack_bat_1.Push(current_size);
                        move_count++;
                        carring = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (AreClose(current_position, position_bat_2))
                {
                    if (stack_bat_2.Count == 0 || stack_bat_2.Peek() > current_size)
                    {
                        //droppedObject.position = new Vector3(position_bat_2.x, current_position.y, position_bat_2.z);
                        stack_bat_2.Push(current_size);
                        move_count++;
                        carring = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (AreClose(current_position, position_bat_3))
                {
                    if (stack_bat_3.Count == 0 || stack_bat_3.Peek() > current_size)
                    {
                        //droppedObject.position = new Vector3(position_bat_3.x, current_position.y, position_bat_3.z);
                        stack_bat_3.Push(current_size);
                        move_count++;
                        carring = false;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            return false;
        }
        return false;
    }

    public void BackToInitialPos()
    {
        move_count--;
    }

    public void ResetGame(bool add)
    {
        if (add)
        {
            used_discs++;
            if (used_discs > max_discs)
                used_discs = max_discs;
        }
        else
        {
            used_discs--;
            if (used_discs < min_discs)
                used_discs = min_discs;
        }
        this.Start();
    }

    private bool AreClose(Vector3 pos_disc, Vector3 pos_bat)
    {
        return pos_disc.x >= pos_bat.x * 0.95 && pos_disc.x <= pos_bat.x * 1.05 && pos_disc.z >= pos_bat.z * 0.95 && pos_disc.z <= pos_bat.z * 1.05;
    }
}
