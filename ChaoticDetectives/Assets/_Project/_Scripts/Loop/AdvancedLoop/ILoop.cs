using UnityEngine;

public interface ILoop
{
    /// <summary>
    /// This method is used to tell the loopable object that a loop is being executed, 
    /// and the target loop is the loop that is next, in case they want to migrate. 
    /// </summary>
    /// <param name="targetLoop"></param>
    public void Loop(Transform targetLoop);
}