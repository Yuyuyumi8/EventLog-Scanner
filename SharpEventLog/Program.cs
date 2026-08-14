using System;
using System.Diagnostics;
using System.Linq;

namespace SharpEventLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Author: Unknown");
            Console.WriteLine();

            ProcessLogEntries(4624, "Success");
            ProcessLogEntries(4625, "Failure");
        }

        /// <summary>
        /// Processes Windows Security Event Log entries for a given Event ID.
        /// </summary>
        /// <param name="eventId">The event ID (e.g., 4624 for logon success, 4625 for failure)</param>
        /// <param name="status">Human-readable status label ("Success" or "Failure")</param>
        public static void ProcessLogEntries(int eventId, string status)
        {
            Console.WriteLine($"\r\n========== SharpEventLog -> Event ID {eventId} ({status}) ==========\r\n");

            var log = new EventLog("Security");
            var entries = log.Entries.Cast<EventLogEntry>()
                                   .Where(entry => entry.InstanceId == eventId);

            foreach (var entry in entries)
            {
                string message = entry.Message;
                string ipAddress = ExtractField(message, "Source Network Address:\t", "\tSource Port:");
                string username = ExtractField(
                    ExtractField(message, "New Logon:\r\n", "\r\nProcess Information:"),
                    "Account Name:\t\t", 
                    "Account Domain:\t\t"
                );

                // Skip if IP is too short (likely local or malformed)
                if (ipAddress.Length < 7) continue;

                // Clean up whitespace and control characters
                username = CleanWhitespace(username);
                ipAddress = CleanWhitespace(ipAddress);

                Console.WriteLine("\r\n-----------------------------------");
                Console.WriteLine($"Time: {entry.TimeGenerated}");
                Console.WriteLine($"Status: {status}");
                Console.WriteLine($"Username: {username}");
                Console.WriteLine($"Remote IP: {ipAddress}");
            }
        }

        /// <summary>
        /// Extracts a substring between two delimiters.
        /// </summary>
        public static string ExtractField(string source, string start, string end)
        {
            int startIndex = source.IndexOf(start);
            if (startIndex == -1) return string.Empty;

            string afterStart = source.Substring(startIndex + start.Length);
            int endIndex = afterStart.IndexOf(end);
            if (endIndex == -1) return string.Empty;

            return afterStart.Substring(0, endIndex);
        }

        /// <summary>
        /// Removes common whitespace and control characters from a string.
        /// </summary>
        public static string CleanWhitespace(string input)
        {
            return input.Replace("\r", "")
                        .Replace("\n", "")
                        .Replace("\t", "")
                        .Replace(" ", "");
        }
    }
}