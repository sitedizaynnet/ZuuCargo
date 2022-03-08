﻿using MVCProject.BLL.Services;
using MVCProject.Common.ViewModels;
using MVCProject.WebUI.PTTTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.WebUI.Controllers
{
    public class TrackController : BaseController
    {
        public ActionResult TrackShipment(string id)
        {
        

            var url = Url.RequestContext.RouteData.Values["id"];
            if (url != null)
            {
               
                ViewBag.TrackingInfo = TrackPTT(id.ToString());
                ViewBag.Id = url;
            }
            return View();
        }

        public TrackingInfo[] TrackPTT(string id)
        {
            TrackingRequest trackingRequest = new TrackingRequest();
            trackingRequest.inp = new Input();
            trackingRequest.inp.barcode = id;
            trackingRequest.inp.userName = "804440354";
            trackingRequest.inp.password = "S62Exy6V4NNi1iQqNZLVGA";
            PttTrackPortTypeClient pttTrackPortTypeClient = new PttTrackPortTypeClient();
            PTTTrack.Output output = pttTrackPortTypeClient.Tracking(trackingRequest.inp);
            
            return output.trackingInf;
        }
        // GET: Track
        public ActionResult Shipment(string id)
        {
            ZoneServices zoneServices = new ZoneServices();
            List<ZoneVM> zoneList = zoneServices.GetAll().ToList();
            ViewData["ZoneList"] = zoneList;


            ShipmentServices shipmentServices = new ShipmentServices();
            ShipmentVM shipmentVM = new ShipmentVM();
            shipmentVM = shipmentServices.GetAll().Where(x => x.TrackingNo == id).First();
            ShipmentMultiVM shipmentMultiVM = new ShipmentMultiVM();
            List<StatusUpdatesVM> statusUpdatesVMList = new List<StatusUpdatesVM>();
            StatusUpdatesServices statusUpdatesServices = new StatusUpdatesServices();

            statusUpdatesVMList = statusUpdatesServices.GetAll().Where(x => x.ShipmentId == shipmentVM.Id).ToList();
            shipmentMultiVM.StatusUpdatesVMList = statusUpdatesVMList;
            shipmentMultiVM.ShipmentVM = shipmentVM;
            ViewData["Shipment"] = shipmentMultiVM;

            return View();
        }

        public async  Task TrackDHLApiAsync(string trackingNumber)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                Headers =
    {
        { "DHL-API-Key", "Mtatsy0Rk1MIcQG3gGFkfZKpADKOHgD0" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
        }
    }
}