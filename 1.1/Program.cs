using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsoleApp1
{
    // Класс XmlToLog содержит метод для преобразования строки XML-лога в объект Log
    public static class XmlToLog
    {
        public static Log ToLog(string xml)
        {
            Log log = new Log();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeList events = doc.SelectNodes("//event");

            for (int i = 0; i < events.Count; i++)
            {
                Event logEvent = new Event();

                logEvent.Date = events[i].Attributes["date"].Value;
                logEvent.Result = events[i].Attributes["result"].Value;
                logEvent.IpFrom = events[i].SelectSingleNode("ip-from").InnerText;
                logEvent.Method = events[i].SelectSingleNode("method").InnerText;
                logEvent.UrlTo = events[i].SelectSingleNode("url-to").InnerText;
                logEvent.Response = int.Parse(events[i].SelectSingleNode("response").InnerText);
                log.Events.Add(logEvent);
            }

            return log;
        }
    }

    // Класс Program содержит точку входа в приложение
    public class Program
    {
        static void Main(string[] args)
        {

            string xml = @"
            <log>
                <event date=""27/May/1999:02:32:46"" result=""success"">
                    <ip-from>195.151.62.18</ip-from>
                    <method>GET</method>
                    <url-to>/mise/</url-to>
                    <response>200</response>
                </event>
                <event date=""27/May/1999:02:41:47"" result=""success"">
                    <ip-from>195.209.248.12</ip-from>
                    <method>GET</method>
                    <url-to>soft.htm</url-to>
                    <response>200</response>
                </event>
            </log>";

            Log log = XmlToLog.ToLog(xml);

            foreach (Event logEvent in log.Events)
            {
                Console.WriteLine("Дата: {0}, результат: {1}, IP-адрес: {2}, метод: {3}, URL: {4}, код ответа:  {5}",
                                  logEvent.Date, logEvent.Result, logEvent.IpFrom, logEvent.Method, logEvent.UrlTo, logEvent.Response);
            }
        }
    }

    // Класс Log представляет лог, содержащий несколько событий
    public class Log
    {
        public List<Event> Events { get; set; } = new List<Event>();
    }

    // Класс Event представляет событие в логе
    public class Event
    {
        public string Date { get; set; }
        public string Result { get; set; }
        public string IpFrom { get; set; }
        public string Method { get; set; }
        public string UrlTo { get; set; }
        public int Response { get; set; }
    }
}
