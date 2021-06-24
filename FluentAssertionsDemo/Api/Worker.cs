using System;
using System.Collections.Generic;

namespace FluentAssertionsDemo.Api
{
    public class Worker
    {
        public bool IsWorking { get; set; }
        
        public IList<Job> Jobs { get; init; }

        public Worker()
        {
            Jobs = new List<Job>();
        }

        public void AddJobs(params Job[] jobs)
        {
            if (jobs.Length <= 0)
                throw new ArgumentException("Must add at least one job");
            
            foreach(Job job in jobs)
                Jobs.Add(job);
        }

        public bool Work(bool shouldWork)
        {
            IsWorking = Jobs.Count > 0 && shouldWork;
            return IsWorking;
        }
    }
}