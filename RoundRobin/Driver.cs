using System;
using System.IO;

namespace RoundRobin
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

                    //Verifica se a linha tem o formato correto
                    if (parts.Length < 3)
                    {
                        Console.WriteLine($"Formato inválido na linha: {line}");
                        continue;
                    }

                    string name = parts[0];
                    int priority, burst;

                    //Verifica se os valores de prioridade e burst são válidos
                    if (!int.TryParse(parts[1], out priority) || !int.TryParse(parts[2], out burst))
                    {
                        Console.WriteLine($"Valores de prioridade ou burst inválidos na linha: {line}");
                        continue;
                    }

                    //Adiciona a tarefa à lista de tarefas do escalonador
                    ScheduleRr.Add(name, priority, burst);
                }

                //Inicia o escalonador
                ScheduleRr.Schedule();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
    }
}