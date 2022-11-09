using System;
using System.Collections.Generic;
using System.Text;

namespace StartApp.Models
{
    public class TabataExercise
    {
        public List<Tabata> TabataList { get; set; }

        public int Sets { get; set; }

        public string Name { get; set; }

        public int RestBetweenSets { get; set; }

        public int Cycles { get; set; }

     
        public int TotalTime
        { 
            get 
            {
                if (TabataList.Count == 0)
                    throw new NullReferenceException();

                int totalTime = 0;

                foreach(Tabata tabata in TabataList)
                {
                    totalTime = tabata.RestTime + tabata.WorkTime;
                }
                totalTime = Cycles != 0 ? totalTime * Cycles : totalTime;
                totalTime = Sets != 0 ? totalTime * Sets : totalTime;
                totalTime = RestBetweenSets != 0 ? totalTime + ((Sets - 1) * RestBetweenSets) : totalTime;
                return totalTime;
            }
        }
    }
}
