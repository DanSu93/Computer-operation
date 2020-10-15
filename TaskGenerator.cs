using System;
using System.Collections.Generic;

namespace Работа_ЭВМ
{
    public enum TaskType : byte
    {
        A,
        B,
        C
    }

    public interface MyTask
    {
        TaskType Name { get; }
        int ExecuteTime { get; set; }
        int StartTime { get; set; }
        bool InRange(int time);
    }

    public sealed class TaskA : MyTask
    {
        TaskType MyTask.Name => TaskType.A;

        public int ExecuteTime { get; set; }

        public int StartTime { get; set; }

        public bool InRange(int time) => time >= StartTime && time < StartTime + ExecuteTime;
    }

    public sealed class TaskB : MyTask
    {
        TaskType MyTask.Name => TaskType.B;

        public int ExecuteTime { get; set; }

        public int StartTime { get; set; }

        public bool InRange(int time) => time >= StartTime && time < StartTime + ExecuteTime;
    }

    public sealed class TaskC : MyTask
    {
        TaskType MyTask.Name => TaskType.C;

        public int ExecuteTime { get; set; }

        public int StartTime { get; set; }

        public bool InRange(int time) => time >= StartTime && time < StartTime + ExecuteTime;
    }

    public sealed class TasksQueue
    {
        readonly List<MyTask> _lstTasks = new List<MyTask>();

        public void Add(MyTask task)
        {
            if (task == null)
                return;
            _lstTasks.Add(task);
        }

        public IEnumerable<MyTask> GetTasks()
        {
            for (int i=0;i<_lstTasks.Count;i++)
            {
                for (int j=0;j<_lstTasks.Count-i-1;j++)
                {
                    if(_lstTasks[j].StartTime> _lstTasks[j+1].StartTime)
                    {
                        int temp = _lstTasks[j].StartTime;
                        _lstTasks[j].StartTime = _lstTasks[j + 1].StartTime;
                        _lstTasks[j + 1].StartTime = temp;
                    }
                }
            }
            return _lstTasks;
        }
    }
    static class TaskGenerator
    {

        public static TasksQueue GetTasks(int taskA, int taskB, int taskC, int genTimeA, int genTimeB, int genTimeC,
            int executeTimeA, int executeTimeB, int executeTimeC, int genExecuteTimeA, int genExecuteTimeB, int genExecuteTimeC, int totalHours)
        {
            if (taskA < 0 || taskB < 0 || taskC < 0 || genTimeA < 0 || genTimeB < 0 || genTimeC < 0 || executeTimeA < 0 || executeTimeB < 0 ||
                executeTimeC < 0 || genExecuteTimeA < 0 || genExecuteTimeB < 0 || genExecuteTimeC < 0 || totalHours < 0)
                throw new ArgumentException($"{nameof(GetTasks)}: Какой-либо из параметров меньше ноля.");
            TasksQueue lstTasks = new TasksQueue();
            Random rA = new Random(), rB = new Random(), rC = new Random();
            int gTimeA = taskA + rA.Next(0, genTimeA + 1) ,
                gTimeB = taskB + rB.Next(0, genTimeB + 1) ,
                gTimeC = taskC + rC.Next(0, genTimeC + 1) ;
            Random rGenTimeA = new Random(), rGenTimeB = new Random(), rGenTimeC = new Random();
            for (int k = 0, total = totalHours * 60; k  <=total; k++)
            {
                if (k == gTimeC)
                {
                    TaskC tskC = new TaskC
                    {
                        ExecuteTime = executeTimeC + rGenTimeC.Next(0, genExecuteTimeC + 1),
                        StartTime = gTimeC
                    };
                    lstTasks.Add(tskC);
                    gTimeC += taskC + rC.Next(0, genTimeC + 1);
                }
                if (k == gTimeA)
                {
                    TaskA tskA = new TaskA
                    {
                        ExecuteTime = executeTimeA + rGenTimeA.Next(0, genExecuteTimeA + 1) ,
                        StartTime = gTimeA
                    };
                    lstTasks.Add(tskA);
                    gTimeA += taskA + rA.Next(0, genTimeA + 1) ;                   
                }
                if (k == gTimeB)
                {
                    TaskB tskB = new TaskB
                    {
                        ExecuteTime = executeTimeB + rGenTimeB.Next(0, genExecuteTimeB + 1) ,
                        StartTime = gTimeB
                    };
                    lstTasks.Add(tskB);
                    gTimeB += taskB + rB.Next(0, genTimeB + 1) ;
                }
            }
            return lstTasks;
        }
    }
}