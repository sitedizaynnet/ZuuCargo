using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace MVCProject.BLL.Services
{
    public class ShipmentServices : IRepository<ShipmentVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Shipment> _shipmentRepository;

        public ShipmentServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _shipmentRepository = new Repository<Shipment>(context);
        }


        public IEnumerable<ShipmentVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ShipmentVM>>(_shipmentRepository.GetAll());
            return (IEnumerable<ShipmentVM>)data;
        }
      

        public ShipmentVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ShipmentVM>(_shipmentRepository.GetById(id));

        }


        public void Insert(ShipmentVM entity)
        {
            _shipmentRepository.Insert(ProjectMapper.ConvertToEntity<Shipment>(entity));
            uow.SaveChanges();

        }

        public void Update(ShipmentVM entity)
        {

            _shipmentRepository.Update(ProjectMapper.ConvertToEntity<Shipment>(entity));
            uow.SaveChanges();
        }

        public void Delete(ShipmentVM entity)
        {
            _shipmentRepository.Delete(context.Shipments.Find(entity.Id));
            uow.SaveChanges();
        }

        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }

        public string ExecutePushNotification(string title, string msg, string fcmToken, object data)
        {

            if (FirebaseApp.DefaultInstance == null)
            {
                var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "google-services.json");


                var defaultApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "zuucargo-firebase-adminsdk-498nd-a8e0938b79.json")),
                });
            }
            else
            {
                var defaultApp = FirebaseApp.DefaultInstance;
            }


            //   Console.WriteLine(defaultApp.Name); // "[DEFAULT]"
            var message = new FirebaseAdmin.Messaging.Message()
            {
                Data = new Dictionary<string, string>()
                {
                    ["Date"] = DateTime.Now.ToString(),
                    ["User"] = "ZUUCARGO"
                },
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = title,
                    Body = msg,
                    ImageUrl = "https://zuucargo.com/Theme/images/logo.png"
                },


                Token = fcmToken

            };
            var messaging = FirebaseMessaging.DefaultInstance;
            var result = messaging.SendAsync(message);
            //Console.WriteLine(result); //projects/myapp/messages/2492588335721724324
            return result.ToString();

        }

    }
}
