using EnergyConsumptionBackendApp.API.Services.Interfaces;
using EnergyConsumptionBackendApp.API.ViewModels;
using EnergyConsumptionBackendApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Newtonsoft.Json;
using EnergyConsumptionBackendApp.Core.Constants;
using System.Text;
using System.Net.WebSockets;
using EnergyConsumptionBackendApp.Core.Models;
using EnergyConsumptionBackendApp.API.Services;

namespace EnergyConsumptionBackendApp.API.Controllers
{
    public class RabbitController : ControllerBase
    {
        private readonly DataContext dbContext;

        public RabbitController(DataContext dataContext)
        {
            dbContext = dataContext;
        }
        private void InitMessageConsumer()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@rabbitmq-integration:5672"),
                DispatchConsumersAsync = true
            };
            var channel = factory.CreateConnection().CreateModel();
            channel.QueueDeclare("energy-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (sender, e) =>
            {
                var message = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);

                var timestamp = DateTimeOffset.Parse(values["timestamp"]).UtcDateTime;
                var deviceId = Guid.Parse(values["device_id"]);
                var energyConsumption = (int)Double.Parse(values["measurement_value"]);

                Console.WriteLine("Timstamp: " + timestamp);
                Console.WriteLine("Device id: " + deviceId);
                Console.WriteLine("Energy value: " + energyConsumption);

                try
                {
                    var energy = await dbContext.Energies.Include(x => x.Device).ThenInclude(x => x.User).FirstOrDefaultAsync(x =>

                        x.Timestamp.Hour == timestamp.Hour &&
                        DateTime.Compare(x.Timestamp.Date, timestamp.Date) == 0 &&
                        x.DeviceId == deviceId
                    );
                    Console.WriteLine("baaaaaaaaaaaaa");
                    if (energy == null)
                    {
                        Console.WriteLine("energy = null ");
                        var device = await dbContext.Devices.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == deviceId);
                        if (device != null && energyConsumption > device.MaximumHourlyEnergyConsumption)
                        {
                            if (Connections.websocketConnections.ContainsKey(device.User.Username))
                            {
                                byte[] data = Encoding.ASCII.GetBytes("Energy consumption for " + device.Name + " exeeded maximum limit of " + device.MaximumHourlyEnergyConsumption + "KWh!\n Current consumption: " + energyConsumption + "KWh");
                                await Connections.websocketConnections[device.User.Username].SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else
                            {
                                Console.WriteLine("Socket not found!");
                            }
                        }
                        if (device != null)
                        {
                            Console.WriteLine("Device was found: " + device.Name);
                        }
                        else
                        {
                            Console.WriteLine("Could not find device with id: " + deviceId);
                        }
                        await dbContext.Energies.AddAsync(new Energy
                        {
                            Timestamp = timestamp,
                            DeviceId = deviceId,
                            EnergyConsumption = energyConsumption
                        });
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        Console.WriteLine("energy != null ");
                        energy.EnergyConsumption += energyConsumption;

                        if (energy.EnergyConsumption > energy.Device.MaximumHourlyEnergyConsumption)
                        {
                            if (Connections.websocketConnections.ContainsKey(energy.Device.User.Username))
                            {

                                byte[] data = Encoding.ASCII.GetBytes("Energy consumption for " + energy.Device.Name + " exeeded maximum limit of " + energy.Device.MaximumHourlyEnergyConsumption + "KWh!\n Current consumption: " + energy.EnergyConsumption + "KWh");
                                await Connections.websocketConnections[energy.Device.User.Username].SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
                            }
                            else
                            {
                                Console.WriteLine("Socket not found!");
                            }
                        }
                        dbContext.Energies.Update(energy);
                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception exeption)
                {
                    Console.WriteLine(exeption.Message);
                }

            };

            channel.BasicConsume("energy-queue", true, consumer);
            while(true)
            {

            }
            //Console.ReadLine();
        }

        //Task.Factory.StartNew(async () => await InitMessageConsumer());

        [HttpPost]
        [Route("InitRabbitConsumer")]
        public async Task<ActionResult> I()
        {
            try
            {
                InitMessageConsumer();
                return Ok();
            }
            catch (Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }

    }
}
