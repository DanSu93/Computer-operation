using System;
using System.Collections.Generic;
using System.Linq;

namespace Работа_ЭВМ
{
    public struct Results
    {
        public int A, B, C;
        public double P;
        public uint CountA, CountB, CountC;

        public uint TotalCount => CountA + CountB + CountC;

        public int TotalTime => A + B + C;
    }

    /// <summary>
    /// 
    /// </summary>
    static class Sheduler
    {
        public static Results Run(TasksQueue tasks, int totalHours)
        {
            if (tasks == null)
                throw new ArgumentNullException();
            List<MyTask> tskA = new List<MyTask>(), tskB = new List<MyTask>(), tskC = new List<MyTask>();
            bool taskA, taskB, taskC=false;
            MyTask[] tsks = tasks.GetTasks().ToArray();
            if (tsks.Length <= 0)
                return new Results();
            Results results = new Results();
            for (int k = 0, c = 0, total = totalHours * 60; c < tsks.Length && k <= total; k++)
            {              
                MyTask task = tsks[c];
                taskA= Check(tskC, task.StartTime);
                taskB= Check(tskC, task.StartTime);
                if (Check(tskA, task.StartTime) || Check(tskB, task.StartTime) || Check(tskC, task.StartTime))
                    taskC = true;
                    else taskC = false;
                if (task.StartTime == k - 1)
                   k--;
                if (task == null || task.StartTime != k)
                {

                    if (!Check(tskA, k) && !Check(tskB, k) && !Check(tskC, k))
                        results.P++;
                    continue;
                }
               c++;              
                switch (task.Name)
                {
                    case TaskType.A:
                        if ((taskA|| taskB)&&taskC||total- k < task.ExecuteTime)
                            continue;
                        results.A += task.ExecuteTime;
                        results.CountA++;
                        tskA.Add(task);
                        break;
                    case TaskType.B:
                        if ((taskA || taskB) && taskC || total - k < task.ExecuteTime)
                            continue;
                        results.B += task.ExecuteTime;
                        results.CountB++;
                        tskB.Add(task);
                        break;
                    case TaskType.C:
                        if (taskA||taskB||taskC||total - k < task.ExecuteTime)
                            continue;
                        results.C += task.ExecuteTime;
                        results.CountC++;
                        tskC.Add(task);
                        break;
                    default:
                        results.P++;
                        break;
                }
            }
            return results;
        }
        static public bool Check(List<MyTask> tsk, int task)
        {
            for (int i = 0; i < tsk.Count; i++)
            {
                if (tsk[i].InRange(task))
                    return true;
            }
            return false;
        }
    }
}