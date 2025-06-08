
using sqlComands;
using System;
using sqlComands;
class Program
{
    static public void Main(string[] args)
    {
        AgentDbContext agentDbContext = new AgentDbContext();
        Agent ahron = new Agent(1, "banan", "ahron", Status.Active, "berlin", 1);
        Agent ahron2 = new Agent(3, "ben", "yoni", Status.Injured, "madrid", 2);
        Agent ahron1 = new Agent(2, "dan", "shmeel", Status.Missing, "seu paulo", 1);
        agentDbContext.AddAgent(ahron);
        agentDbContext.AddAgent(ahron1);
        agentDbContext.AddAgent(ahron2);

        agentDbContext.GetAllAgents().ForEach(a =>
        {
            Console.WriteLine(a.status);
        });
        agentDbContext.UpdateAgentLocation(60, "gaza");
        agentDbContext.DeleteAgent(55);
    }
}