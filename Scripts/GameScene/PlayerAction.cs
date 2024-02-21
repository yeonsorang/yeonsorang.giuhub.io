using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;
    public GameManager manager;

    float h;
    float v;
    bool isHorizoMove;
    Rigidbody2D rigid;
    Animator anim;

    Vector3 dirVec;
    GameObject scanObject;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //Check Button Down & Up
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

        //Check Horizontal Move
        if (hDown)
        {
            isHorizoMove = true;
        }
        else if (vDown)
        {
            isHorizoMove = false;
        }
        else if (hUp || vUp)
        {
            isHorizoMove= h != 0;
        }



        //Animation
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetInteger("hAxisRaw", (int)h);
            anim.SetTrigger("IsChange");
            //anim.SetBool("isChange", true);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetInteger("vAxisRaw", (int)v);
            anim.SetTrigger("IsChange");
            //anim.SetBool("isChange", true);
        }
        else 
        {
            //anim.SetTrigger("IsChange");
            //anim.SetBool("isChange", false);
        }

        //Direction
        if (vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if(vDown && v == -1)
        {
            dirVec = Vector3.down;
        }
        else if(hDown && h == -1)
        {
            dirVec = Vector3.left;
        }
        else if(hDown && h == 1)
        {
            dirVec = Vector3.right;
        }

        //Scan Object
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
            //Debug.Log("This is : " + scanObject.name);
        }
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = isHorizoMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("NPC"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
