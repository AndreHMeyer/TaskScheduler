using System;
using System.IO;

namespace EarliestDeadlineFirst
{
    public class Driver
    {
        public static void Start(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("O arquivo não foi encontrado.");
                    return;
                }

                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    // Verifica se a linha tem o formato correto
                    if (parts.Length < 4)
                    {
                        Console.WriteLine($"Formato inválido na linha: {line}");
                        continue;
                    }

                    string name = parts[0];
                    int priority, burst, deadline;

                    // Verifica se os valores de prioridade, burst e deadline são válidos
                    if (!int.TryParse(parts[1], out priority) || !int.TryParse(parts[2], out burst) || !int.TryParse(parts[3], out deadline))
                    {
                        Console.WriteLine($"Valores de prioridade, burst ou deadline inválidos na linha: {line}");
                        continue;
                    }

                    // Adiciona a tarefa à lista de tarefas do escalonador
                    ScheduleEdf.Add(name, priority, burst, deadline);
                }

                // Inicia o escalonador
                ScheduleEdf.Schedule();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}
