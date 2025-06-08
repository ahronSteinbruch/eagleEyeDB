using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlComands
{
    public enum Status
    {
        Active,
        Injured,
        Missing,
        Retired
    }

    internal class Agent
    {
        public int id;
        public string CodeName;
        public string RealName;
        public Status status;
        public string location;
        public int MissionsCompleted;
        public Agent(int id, string codeName, string realName, Status s, string location, int missionsCompleted)
        {
            this.id = id;
            this.CodeName = codeName;
            this.RealName = realName;
            this.location = location;
            this.status = s;
            this.MissionsCompleted = missionsCompleted; 
        }
    }
}
