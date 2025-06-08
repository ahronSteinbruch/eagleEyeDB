using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace sqlComands
{
    internal class AgentDbContext
    {
        string coon = "user=root;server=localhost;database=eagleeyedb;port=3306;";
        private MySqlConnection coonect;

        public AgentDbContext()
        {
            try
            {
                coonect = new MySqlConnection(coon);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }

        public void AddAgent(Agent agent)
        {
            string command = "INSERT INTO `agents` (`codeName`, `realName`, `status`, `location`, `missionsCompleted`) " +
                             "VALUES (@codeName, @realName, @status, @location, @missionsCompleted)";
            try
            {
                coonect.Open();
                MySqlCommand cmd = new MySqlCommand(command, coonect);
                cmd.Parameters.AddWithValue("@codeName", agent.CodeName);
                cmd.Parameters.AddWithValue("@realName", agent.RealName);
                cmd.Parameters.AddWithValue("@status", agent.status); // ENUM as string
                cmd.Parameters.AddWithValue("@location", agent.location);
                cmd.Parameters.AddWithValue("@missionsCompleted", agent.MissionsCompleted);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AddAgent error: " + ex.Message);
            }
            finally
            {
                coonect.Close();
            }
        }

        public List<Agent> GetAllAgents()
        {
            List<Agent> agents = new List<Agent>();
            string command = "SELECT * FROM agents";
            MySqlDataReader reader = null;

            try
            {
                coonect.Open();
                MySqlCommand cmd = new MySqlCommand(command, coonect);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string statusStr = reader.GetString("status");
                    if (!Enum.TryParse(statusStr, out Status status))
                    {
                        Console.WriteLine($"Invalid status '{statusStr}' for agent ID {reader.GetInt32("id")}");
                        continue;
                    }

                    Agent agent = new Agent(
                        reader.GetInt32("id"),
                        reader.GetString("codeName"),
                        reader.GetString("realName"),
                        status,
                        reader.GetString("location"),
                        reader.GetInt32("missionsCompleted")
                    );

                    agents.Add(agent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetAllAgents error: " + ex.Message);
            }
            finally
            {
                reader?.Close();
                coonect.Close();
            }

            return agents;
        }

        public void UpdateAgentLocation(int agentId, string newLocation)
        {
            string command = "UPDATE `agents` SET `location` = @location WHERE id = @agentId";
            try
            {
                coonect.Open();
                MySqlCommand cmd = new MySqlCommand(command, coonect);
                cmd.Parameters.AddWithValue("@agentId", agentId);
                cmd.Parameters.AddWithValue("@location", newLocation);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateAgentLocation error: " + ex.Message);
            }
            finally
            {
                coonect.Close();
            }
        }

        public void DeleteAgent(int agentId)
        {
            string command = "DELETE FROM `agents` WHERE id = @agentId"; // fixed: DELETE, not DELATE
            try
            {
                coonect.Open();
                MySqlCommand cmd = new MySqlCommand(command, coonect);
                cmd.Parameters.AddWithValue("@agentId", agentId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteAgent error: " + ex.Message);
            }
            finally
            {
                coonect.Close();
            }
        }
    }
}
