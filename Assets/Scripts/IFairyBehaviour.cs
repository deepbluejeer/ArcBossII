using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Action();

public interface IFairyBehaviour
{
    Action CurrentAction { get; set; }

    void Enter(Fairy fairy);
    void GetReady();
    void Wait();
    void Execute();
    void Domino();
    void Exit();
}
