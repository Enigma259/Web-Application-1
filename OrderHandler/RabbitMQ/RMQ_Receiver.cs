using ClassLibrary1.Controller;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OrderHandler.RabbitMQ
{
    /// <summary>
    /// This is the class RMQ_Receiver.
    /// </summary>
    public sealed class RMQ_Receiver
    {
        #region Instances

        private CTR_Order ctr_order;
        private CTR_OrderDetail ctr_orderdetail;
        private CTR_Product ctr_product;
        private CustomUserController ctr_user;
        private CTR_UserProduct ctr_userproduct;

        private RMQ_Order rmq_order;
        private RMQ_OrderDetail rmq_orderdetail;
        private RMQ_Product rmq_product;
        private RMQ_SpecialCalls rmq_specialcalls;
        private RMQ_User rmq_user;
        private RMQ_UserProduct rmq_userproduct;

        private static volatile RMQ_Receiver _instance;
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
        private uint prefetch_size;
        private ushort prefetch_count;
        private bool global;
        private bool multiple;
        private bool auto_acknowledge;

        #endregion

        /// <summary>
        /// Comstructor for the class RMQ_Receiver
        /// </summary>
        private RMQ_Receiver()
        {
            this.host_name = "localhost";
            this.username = "guest";
            this.password = "guest";
            this.virtuel_host = "Task_Queue";
            this.requested_heartbeat = 30;
            this.automatic_recovery = true;
            this.network_recovery_interval = new TimeSpan(0, 0, 5);
            this.port = 5672;
            this.queue = "Order_Handler";
            this.durable = true;
            this.exclusive = false;
            this.auto_delete = false;
            this.arguments = null;
            this.prefetch_size = 0;
            this.prefetch_count = 1;
            this.global = false;
            this.multiple = false;
            this.auto_acknowledge = false;

            this.rmq_order = RMQ_Order.GetInstance();
            this.rmq_orderdetail = RMQ_OrderDetail.GetInstance();
            this.rmq_product = RMQ_Product.GetInstance();
            this.rmq_specialcalls = RMQ_SpecialCalls.GetInstance();
            this.rmq_user = RMQ_User.GetInstance();
            this.rmq_userproduct = RMQ_UserProduct.GetInstance();
        }

        #region Get Functions

        /// <summary>
        /// This is a multi threaded singleton for the class RMQ_Receiver.
        /// </summary>
        /// <returns>_instance</returns>
        public static RMQ_Receiver GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new RMQ_Receiver();
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
        /// <returns>IDictionary<string, object></returns>
        public IDictionary<string, object> GetArguments()
        {
            return arguments;
        }

        /// <summary>
        /// Returns the value of the instance prefetch_size.
        /// </summary>
        /// <returns>uint</returns>
        public uint GetPrefetchSize()
        {
            return prefetch_size;
        }

        /// <summary>
        /// Returns the value of the instance prefetch_count.
        /// </summary>
        /// <returns>ushort</returns>
        public ushort GetPrefetchCount()
        {
            return prefetch_count;
        }

        /// <summary>
        /// Returns the value of the instance global.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetGlobal()
        {
            return global;
        }

        /// <summary>
        /// Returns the value of the instance multiple.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetMultiple()
        {
            return multiple;
        }

        /// <summary>
        /// Returns the value of the instance auto_acknowledge.
        /// </summary>
        /// <returns>bool</returns>
        public bool GetAutoAcknowledge()
        {
            return auto_acknowledge;
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
        /// Changes the value of the instance prefetch_size.
        /// </summary>
        /// <param name="prefetch_size"></param>
        public void SetPrefetchSize(uint prefetch_size)
        {
            this.prefetch_size = prefetch_size;
        }

        /// <summary>
        /// Changes the value of the instance prefetch_count.
        /// </summary>
        /// <param name="prefetch_count"></param>
        public void SetPrefetchCount(ushort prefetch_count)
        {
            this.prefetch_count = prefetch_count;
        }

        /// <summary>
        /// Changes the value of the instance global.
        /// </summary>
        /// <param name="global"></param>
        public void SetGlobal(bool global)
        {
            this.global = global;
        }

        /// <summary>
        /// Changes the value of the instance multiple.
        /// </summary>
        /// <param name="multiple"></param>
        public void SetMultiple(bool multiple)
        {
            this.multiple = multiple;
        }

        /// <summary>
        /// Changes the value of the instance auto_acknowledge.
        /// </summary>
        /// <param name="auto_acknowledge"></param>
        public void SetAutoAcknowledge(bool auto_acknowledge)
        {
            this.auto_acknowledge = auto_acknowledge;
        }

        #endregion

        #region RabbitMQ Functions

        /// <summary>
        /// Receives messages from message queue.
        /// </summary>
        public void ReceiveMessage()
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
                channel.QueueDeclare(queue: GetQueue(), durable: GetDurable(), exclusive: GetExclusive(), autoDelete: GetAutoDelete(), arguments: GetArguments());
                channel.BasicQos(prefetchSize: GetPrefetchSize(), prefetchCount: GetPrefetchCount(), global: GetGlobal());

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    Managetasks(SplitMessage(message));

                    int dots = message.Split('.').Length - 1;

                    Thread.Sleep(dots * 1000);

                    Console.WriteLine(" [x] Done");

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: GetMultiple());
                };

                channel.BasicConsume(queue: GetQueue(), autoAck: GetAutoAcknowledge(), consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        #endregion

        #region Message Handling

        /// <summary>
        /// Checks which part of the program the information is intended to.
        /// </summary>
        /// <param name="message_parts"></param>
        public void Managetasks(List<string> message_parts)
        {
            if (message_parts.Count > 0)
            {
                switch (message_parts[0])
                {
                    case "Order":
                        rmq_order.OrderFunctions(message_parts);
                        break;

                    case "Order Detail":
                        rmq_orderdetail.OrderDetailFunctions(message_parts);
                        break;

                    case "Product":
                        rmq_product.ProductFunctions(message_parts);
                        break;

                    case "User":
                        rmq_user.UserFunctions(message_parts);
                        break;

                    case "User Product":
                        rmq_userproduct.UserProductFunctions(message_parts);
                        break;

                    case "Special Calls":
                        rmq_specialcalls.SpecialFunctions(message_parts);
                        break;

                    default:
                        Console.WriteLine("The given command area is invalid: " + message_parts[0]);
                        break;

                }
            }
        }

        /// <summary>
        /// Splits the message from the queue into parts and return it as a list.
        /// </summary>
        /// <param name="message"></param>
        /// <returns>List<string></returns>
        public List<string> SplitMessage(string message)
        {
            int index = 0;
            int dollar_number = 0;
            string letter = "";
            string part_result = "";
            List<string> result = new List<string>();

            while (index < message.Length)
            {
                letter = message[index].ToString();

                if (letter.Equals("$"))
                {
                    if (dollar_number >= 2)
                    {
                        dollar_number = 0;
                        result.Add(part_result);
                        part_result = "";
                    }

                    else
                    {
                        dollar_number++;
                    }
                }

                else
                {
                    part_result += letter;
                }

                index++;
            }

            return result;
        }

        #endregion
    }
}