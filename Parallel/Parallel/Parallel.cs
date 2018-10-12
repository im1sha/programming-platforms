using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel
{
    public static class Parallel
    {
        /// <summary>
        /// Class that stores data for action performing
        /// </summary>
        private class ActionData : IEnumerable
        {
            public ActionData(Action<object>[] Actions, object[] Objects)
            {
                int length = Math.Min(Actions.Length, Objects.Length);
                for (int i = 0; i < length; i++)
                {
                    Data.Add((Actions[i], Objects[i]));
                }
            }

            public readonly List<ValueTuple<Action<object>, object>> Data = 
                new List<ValueTuple<Action<object>, object>>();

            public IEnumerator GetEnumerator()
            {
                return ((IEnumerable)Data).GetEnumerator();
            }
        }

        /// <summary>
        /// Starts all delegates and waits they completed 
        /// </summary>
        /// <param name="actions">Array delegates to process</param>
        /// <param name="objects">Data to be processed by delegates</param>
        public static void WaitAll(Action<object>[] actions, object[] objects)
        {
            if (actions.Length != objects.Length)
            {
                throw new ArgumentException("Argumets should have the same length");
            }

            var data = new ActionData(actions, objects);

            using (var finished = new CountdownEvent(1))
            {
                foreach (var a in data.Data)
                {
                    finished.AddCount(); 
                    ThreadPool.QueueUserWorkItem(delegate {
                        try
                        {
                            a.Item1(a.Item2);
                        }
                        finally
                        {
                            finished.Signal(); 
                        }
                    });
                }
                finished.Signal(); 
                finished.Wait(); 
            }
        }
    }
}
