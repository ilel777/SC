using UnityEngine;

public class PlayerAttack : Attack
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    public override void FireBolt()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            Debug.Log("Space pressed");
            base.FireBolt();
        }
    }

    public override GameObject PrepareNewBolt()
    {
        return base.PrepareNewBolt();
    }

}
