// 
// 
// Reference: http://blog.kibotu.net/unity-2/unity-start-coroutines-main-thread-anything-else-matter
// 
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoOnMainThread : MonoBehaviour {
    	
   public readonly static Queue<Action> ExecuteOnMainThread = new Queue<Action>();
    	
   public void Update()
   {
        // dispatch stuff on main thread
    	while (ExecuteOnMainThread.Count > 0)
    	{
    		ExecuteOnMainThread.Dequeue().Invoke();
    	}
    }
}