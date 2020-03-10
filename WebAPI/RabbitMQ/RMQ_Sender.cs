using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_Sender.
    /// </summary>
    public sealed class RMQ_Sender
    {
        #region Instances

        private static volatile RMQ_Sender _instance;
        private static readonly object syncRoot = new object();

        private string host_name;
        private string username;
        private string password;
        private string virtuel_host;
        private ushort requested_heartbeat;
        private bool automatic_recovery;
        private TimeSpan network_recovery_interval;
        private int port;
        private string queue;
        private bool durable;
        private bool exclusive;
        private bool auto_delete;
        private IDictionary<string, object> arguments;
        private string exchange;
        private string routing_key;
        private bool basic_properties_persistatnt;
        private string[] main_arguments;

        #endregion

        /// <summary>
        /// This is the conctructor for the class RMQ_Sender.
        /// </summary>
        private RMQ_Sender()
        {
            host_name = "localhost";
            username = "guest";
            password = "guest";
            virtuel_host = "Task_Queue";
            requested_heartbeat = 30;
            automatic_recovery = true;
            network_recovery_interval = new TimeSpan(0, 0, 5);
            port = 5672;
            queue = "Order_Handler";
            durable = true;
            exclusive = false;
            auto_delete = false;
            arguments = null;
            exchange = "";
            routing_key = "Order_Handler";
            basic_properties_persistatnt = true;
            main_arguments = null;
        }

        #region Get Functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_Sender.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_Sender GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_Sender();
                    }
                }
            }

            return _instance;
        }

        /// <summary>
        /// Returns the value of the instance host_name.
        /// </summary>
        /// <returns>string</returns>
        public string GetHostName()
        {
            return host_name;
        }

        /// <summary>
        /// Returns the value of the instance username.
        /// </summary>
        /// <returns>string</returns>
        public string GetUsername()
        {
            return username;
        }

        /// <summary>
        /// Returns the value of the instance password.
        /// </summary>
        /// <returns>string</returns>
        public string GetPassword()
        {
            return password;
        }

        /// <summary>
        /// Returns the value of the instance virtuel_host.
        /// </summary>
        /// <returns>string</returns>
        public string GetVirtualHost()
        {
            return virtuel_host;
        }

        /// <summary>
        /// Returns the value of the instance requested_heartbeat.
        /// </summary>
        /// <returns>ushort</returns>
        public ushort GetRequestedHeartbeat()
        {
            return requested_heartbeat;
        }

        /// <summary>
        /// Returns the value of the instance automatic_recovery.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetAutomaticRecovery()
        {
            return automatic_recovery;
        }

        /// <summary>
        /// Returns the value of the instance network_recovery_interval.
        /// </summary>
        /// <returns>TimeSpan</returns>
        public TimeSpan GetNetworkRecoveryInterval()
        {
            return network_recovery_interval;
        }

        /// <summary>
        /// Returns the value of the instance port.
        /// </summary>
        /// <returns>int</returns>
        public int GetPort()
        {
            return port;
        }

        /// <summary>
        /// Returns the value of the instance queue.
        /// </summary>
        /// <returns>string</returns>
        public string GetQueue()
        {
            return queue;
        }

        /// <summary>
        /// Returns the value of the instance durable.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetDurable()
        {
            return durable;
        }

        /// <summary>
        /// Returns the value of the instance exclusive.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetExclusive()
        {
            return exclusive;
        }

        /// <summary>
        /// Returns the value of the instance auto_delete.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetAutoDelete()
        {
            return auto_delete;
        }

        /// <summary>
        /// Returns the value of the instance host_name.
        /// </summary>
        /// <returns>IDictionary(string, object)</returns>
        public IDictionary<string, object> GetArguments()
        {
            return arguments;
        }

        /// <summary>
        /// Returns the value of the instance exchange.
        /// </summary>
        /// <returns>string</returns>
        public string GetExchange()
        {
            return exchange;
        }

        /// <summary>
        /// Returns the value of the instance routing_key.
        /// </summary>
        /// <returns>string</returns>
        public string GetRoutingKey()
        {
            return routing_key;
        }

        /// <summary>
        /// Returns the value of the instance basic_properties_persistatnt.
        /// </summary>
        /// <returns>string</returns>
        public bool GetBasicPropertiesPersistant()
        {
            return basic_properties_persistatnt;
        }

        /// <summary>
        /// Returns the value of the instance main_arguments.
        /// </summary>
        /// <returns>string[]</returns>
        public string[] GetArgs()
        {
            return main_arguments;
        }

        #endregion

        #region Set Functions

        /// <summary>
        /// Changes the value of the instance host_name.
        /// </summary>
        /// <param name="host_name"></param>
        public void SetHostName(string host_name)
        {
            this.host_name = host_name;
        }

        /// <summary>
        /// Changes the value of the instance username.
        /// </summary>
        /// <param name="username"></param>
        public void SetUsername(string username)
        {
            this.username = username;
        }

        /// <summary>
        /// Changes the value of the instance password.
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            this.password = password;
        }

        /// <summary>
        /// Changes the value of the instance virtuel_host.
        /// </summary>
        /// <param name="virtuel_host"></param>
        public void SetVirtuelHost(string virtuel_host)
        {
            this.virtuel_host = virtuel_host;
        }

        /// <summary>
        /// Changes the value of the instance requested_heartbeat.
        /// </summary>
        /// <param name="requested_heartbeat"></param>
        public void SetRequestedHeartbeat(ushort requested_heartbeat)
        {
            this.requested_heartbeat = requested_heartbeat;
        }

        /// <summary>
        /// Changes the value of the instance automatic_recovery.
        /// </summary>
        /// <param name="automatic_recovery"></param>
        public void SetAutomaticRecovery(bool automatic_recovery)
        {
            this.automatic_recovery = automatic_recovery;
        }

        /// <summary>
        /// Changes the value of the instance network_recovery_interval.
        /// </summary>
        /// <param name="network_recovery_interval"></param>
        public void SetNetworkRecoveryInterval(TimeSpan network_recovery_interval)
        {
            this.network_recovery_interval = network_recovery_interval;
        }

        /// <summary>
        /// Changes the value of the instance port.
        /// </summary>
        /// <param name="port"></param>
        public void SetPort(int port)
        {
            this.port = port;
        }

        /// <summary>
        /// Changes the value of the instance queue.
        /// </summary>
        /// <param name="queue"></param>
        public void SetQueue(string queue)
        {
            this.queue = queue;
        }

        /// <summary>
        /// Changes the value of the instance durable.
        /// </summary>
        /// <param name="durable"></param>
        public void SetDurable(bool durable)
        {
            this.durable = durable;
        }

        /// <summary>
        /// Changes the value of the instance exclusive.
        /// </summary>
        /// <param name="exclusive"></param>
        public void SetExclusive(bool exclusive)
        {
            this.exclusive = exclusive;
        }

        /// <summary>
        /// Changes the value of the instance auto_delete.
        /// </summary>
        /// <param name="auto_delete"></param>
        public void SetAutoDelete(bool auto_delete)
        {
            this.auto_delete = auto_delete;
        }

        /// <summary>
        /// Changes the value of the instance arguments.
        /// </summary>
        /// <param name="arguments"></param>
        public void SetArguments(IDictionary<string, object> arguments)
        {
            this.arguments = arguments;
        }

        /// <summary>
        /// Changes the value of the instance exchange.
        /// </summary>
        /// <param name="exchange"></param>
        public void SetExchange(string exchange)
        {
            this.exchange = exchange;
        }

        /// <summary>
        /// Changes the value of the instance routing_key.
        /// </summary>
        /// <param name="routing_key"></param>
        public void SetRoutingKey(string routing_key)
        {
            this.routing_key = routing_key;
        }

        /// <summary>
        /// Changes the value of the instance basic_properties_persistatnt.
        /// </summary>
        /// <param name="basic_properties_persistatnt"></param>
        public void SetBasicPropertiesPersistant(bool basic_properties_persistatnt)
        {
            this.basic_properties_persistatnt = basic_properties_persistatnt;
        }

        /// <summary>
        /// Changes the value of the instance main_arguments.
        /// </summary>
        /// <returns>string[]</returns>
        public void SetArgs(string[] args)
        {
            this.main_arguments = args;
        }

        #endregion

        #region message functions

        /// <summary>
        /// convert information into a message string.
        /// </summary>
        /// <param name="informations"></param>
        /// <returns>string</returns>
        public string ConvertToMessage(List<string> informations)
        {
            string result = "";
            string seperator = "$$$";

            foreach (string info in informations)
            {
                result += info;
                result += seperator;
            }

            return result;
        }

        #endregion

        #region RabbitMQ Functions

        /// <summary>
        /// Sends message to a message queue.
        /// </summary>
        /// <param name="informations"></param>
        /// <returns>string</returns>
        public string SendMessage(List<string> informations)
        {
            string result;

            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = GetHostName(),
                    UserName = GetUsername(),
                    Password = GetPassword(),
                    VirtualHost = GetVirtualHost(),
                    RequestedHeartbeat = GetRequestedHeartbeat(),
                    AutomaticRecoveryEnabled = GetAutomaticRecovery(),
                    NetworkRecoveryInterval = GetNetworkRecoveryInterval(),
                    Port = GetPort()
                };
                
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: GetQueue(), 
                        durable: GetDurable(), 
                        exclusive: GetExclusive(), 
                        autoDelete: GetAutoDelete(), 
                        arguments: GetArguments());
                    
                    //var message = GetMessage(informations);
                    var message = ConvertToMessage(informations);
                    var body = Encoding.UTF8.GetBytes(message);

                    Console.WriteLine("Message: " + message);
                    Console.WriteLine("Body: " + body);
                    
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = GetBasicPropertiesPersistant();
                    
                    channel.BasicPublish(exchange: GetExchange(), routingKey: GetRoutingKey(), basicProperties: properties, body: body);
                }

                result = "Success";
            }

            catch (Exception exception)
            {
                result = exception.Message;
            }

            return result;
        }

        /// <summary>
        /// Gets the message to be used to send to the queue.
        /// </summary>
        /// <param name="informations"></param>
        /// <returns>string</returns>
        private string GetMessage(List<string> informations)
        {
            return ((GetArgs().Length > 0) ? string.Join(" ", GetArgs()) : ConvertToMessage(informations));

        }

        #endregion
    }
}