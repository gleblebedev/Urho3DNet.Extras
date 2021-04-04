using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Urho3DNet.Actions
{
    public class ActionManager : IDisposable
    {
        private static Object[] tmpKeysArray = new Object[128];

        private readonly Dictionary<Object, HashElement> targets = new Dictionary<Object, HashElement>();
        private bool currentTargetSalvaged;
        private HashElement currentTarget;
        private bool targetsAvailable;

        ~ActionManager()
        {
            Dispose(false);
        }

        public void RemoveAllActions()
        {
            if (!targetsAvailable)
                return;

            var count = targets.Count;
            if (tmpKeysArray.Length < count) tmpKeysArray = new Object[tmpKeysArray.Length * 2];

            targets.Keys.CopyTo(tmpKeysArray, 0);

            for (var i = 0; i < count; i++) RemoveAllActionsFromTarget(tmpKeysArray[i]);
        }

        public void RemoveAction(ActionState actionState)
        {
            if (actionState == null || actionState.OriginalTarget == null) return;

            var target = actionState.OriginalTarget;
            HashElement element;
            if (targets.TryGetValue(target, out element))
            {
                var i = element.ActionStates.IndexOf(actionState);

                if (i != -1) RemoveActionAtIndex(i, element);
            }
        }

        public void CancelActiveActions()
        {
            //this might be very slow when debugged is attached.
            foreach (var target in targets)
            {
                var tcs = target.Value.ActionStates.LastOrDefault();
                var taskSourceState = tcs as TaskSourceState;
                if (taskSourceState != null)
                {
                    taskSourceState.Cancel();
                    continue;
                }

                var seqState = tcs as SequenceState;
                seqState?.Cancel();
            }
        }

        public ActionState AddAction(BaseAction action, Object target, bool paused = false)
        {
            Debug.Assert(action != null);
            Debug.Assert(target != null);

            HashElement element;
            if (!targets.TryGetValue(target, out element))
            {
                element = new HashElement();
                element.Paused = paused;
                element.Target = target;
                targets.Add(target, element);
                targetsAvailable = true;
            }

            ActionAllocWithHashElement(element);
            var isActionRunning = false;
            foreach (var existingState in element.ActionStates)
                if (existingState.Action == action)
                {
                    isActionRunning = true;
                    break;
                }

            Debug.Assert(!isActionRunning, "Action is already running for this target.");
            var state = action.StartAction(target);
            element.ActionStates.Add(state);

            return state;
        }

        public void RemoveAction(int tag, Object target)
        {
            Debug.Assert(tag != (int) ActionTag.Invalid);
            Debug.Assert(target != null);

            // Early out if we do not have any targets to search
            if (targets.Count == 0)
                return;

            HashElement element;
            if (targets.TryGetValue(target, out element))
            {
                var limit = element.ActionStates.Count;
                var tagFound = false;

                for (var i = 0; i < limit; i++)
                {
                    var actionState = element.ActionStates[i];

                    if (actionState.Action.Tag == tag && actionState.OriginalTarget == target)
                    {
                        RemoveActionAtIndex(i, element);
                        tagFound = true;
                        break;
                    }
                }
            }
        }

        /// <param name="target"></param>
        /// <summary>Pauses the specified target.</summary>
        /// <remarks></remarks>
        public void PauseTarget(Object target)
        {
            HashElement hashElement;
            if (!targets.TryGetValue(target, out hashElement))
                return;
            hashElement.Paused = true;
        }

        /// <param name="target"></param>
        /// <summary>Resumes the actions on the specified node.</summary>
        /// <remarks></remarks>
        public void ResumeTarget(Object target)
        {
            HashElement hashElement;
            if (!targets.TryGetValue(target, out hashElement))
                return;
            hashElement.Paused = false;
        }

        public void RemoveAllActionsFromTarget(Object target)
        {
            if (target == null) return;

            HashElement element;
            if (targets.TryGetValue(target, out element))
            {
                if (element.ActionStates.Contains(element.CurrentActionState) && !element.CurrentActionSalvaged)
                    element.CurrentActionSalvaged = true;

                element.ActionStates.Clear();

                if (currentTarget == element)
                    currentTargetSalvaged = true;
                else
                    DeleteHashElement(element);
            }
        }

        public void Update(float dt)
        {
            if (!targetsAvailable)
                return;

            var count = targets.Count;

            while (tmpKeysArray.Length < count) tmpKeysArray = new Node[tmpKeysArray.Length * 2];

            targets.Keys.CopyTo(tmpKeysArray, 0);

            for (var i = 0; i < count; i++)
            {
                HashElement elt;
                if (!targets.TryGetValue(tmpKeysArray[i], out elt)) continue;

                elt.Target.Dispose();
                if (elt.Target.IsExpired)
                    targets.Remove(elt.Target);

                currentTarget = elt;
                currentTargetSalvaged = false;

                if (!currentTarget.Paused)
                    // The 'actions' may change while inside this loop.
                    for (currentTarget.ActionIndex = 0;
                        currentTarget.ActionIndex < currentTarget.ActionStates.Count;
                        currentTarget.ActionIndex++)
                    {
                        currentTarget.CurrentActionState = currentTarget.ActionStates[currentTarget.ActionIndex];
                        if (currentTarget.CurrentActionState == null) continue;
                        if (currentTarget.Target.IsExpired) break;

                        currentTarget.CurrentActionSalvaged = false;

                        currentTarget.CurrentActionState.Step(dt);

                        if (currentTarget.CurrentActionSalvaged)
                        {
                            // The currentAction told the node to remove it. To prevent the action from
                            // aidentally deallocating itself before finishing its step, we retained
                            // it. Now that step is done, it's safe to release it.

                            //currentTarget->currentAction->release();
                        }
                        else if (currentTarget.CurrentActionState.IsDone)
                        {
                            currentTarget.CurrentActionState.Stop();

                            var actionState = currentTarget.CurrentActionState;
                            // Make currentAction nil to prevent removeAction from salvaging it.
                            currentTarget.CurrentActionState = null;
                            RemoveAction(actionState);
                        }

                        currentTarget.CurrentActionState = null;
                    }

                // only delete currentTarget if no actions were scheduled during the cycle (issue #481)
                if (currentTargetSalvaged && currentTarget.ActionStates.Count == 0) DeleteHashElement(currentTarget);
            }

            currentTarget = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void RemoveActionAtIndex(int index, HashElement element)
        {
            var action = element.ActionStates[index];

            if (action == element.CurrentActionState && !element.CurrentActionSalvaged)
                element.CurrentActionSalvaged = true;

            element.ActionStates.RemoveAt(index);

            // update actionIndex in case we are in tick. looping over the actions
            if (element.ActionIndex >= index) element.ActionIndex--;

            if (element.ActionStates.Count == 0)
            {
                if (currentTarget == element)
                    currentTargetSalvaged = true;
                else
                    DeleteHashElement(element);
            }
        }

        internal void RemoveAction(BaseAction action, Object target)
        {
            if (action == null || target == null)
                return;

            HashElement element;
            if (targets.TryGetValue(target, out element))
            {
                var limit = element.ActionStates.Count;
                var actionFound = false;

                for (var i = 0; i < limit; i++)
                {
                    var actionState = element.ActionStates[i];

                    if (actionState.Action == action && actionState.OriginalTarget == target)
                    {
                        RemoveActionAtIndex(i, element);
                        actionFound = true;
                        break;
                    }
                }
            }
        }

        internal void ActionAllocWithHashElement(HashElement element)
        {
            if (element.ActionStates == null) element.ActionStates = new List<ActionState>();
        }

        internal void DeleteHashElement(HashElement element)
        {
            element.ActionStates.Clear();
            targets.Remove(element.Target);
            element.Target = null;
            targetsAvailable = targets.Count > 0;
        }

        private void Dispose(bool i)
        {
            RemoveAllActions();
        }

        internal class HashElement
        {
            public int ActionIndex;
            public List<ActionState> ActionStates;
            public ActionState CurrentActionState;
            public bool CurrentActionSalvaged;
            public bool Paused;
            public Object Target;
        }
    }
}