  using MVCProject.BLL.Services;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using static MVCProject.WebUI.Controllers.BaseController;

namespace MVCProject.WebUI.Areas.Admin.Controllers
{
    public class AdminController : BaseController
    {

        public ActionResult GetUsers()
        {
            var users = Membership.GetAllUsers();
            return View(users);
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter, Driver")]

        public ActionResult Backup()
        {
            try
            {
                var context = new ZuuCargoEntities();
                Random rnd = new Random();
                rnd.Next(1, 9999);

                var query = @"BACKUP DATABASE ZuuCargo TO DISK = 'C:\\SQLServerBackups\\Backup-" + DateTime.Now.ToFileTime() + ".bak' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Backup-ZuuCargo.bak'";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);

            }
            catch (SqlException sqlException)
            {

                ViewBag.Error = sqlException.ToString();
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.ToString();
            }
            return View();
        }



        //BACKUP AND TRUNCATE ARRIVEDEXCHANGE 
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter, Driver")]
        public ActionResult BackupAndTruncateArrivedExchange()
        {
            try
            {
                var context = new ZuuCargoEntities();
                Random rnd = new Random();
                rnd.Next(1, 9999);

                var query = @"BACKUP DATABASE ZuuCargo TO DISK = 'C:\\SQLServerBackups\\Backup-" + DateTime.Now.ToFileTime() + ".bak' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Backup-ZuuCargo.bak'";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);



                rnd.Next(1, 9999);
                var query2 = @"TRUNCATE TABLE ArrivedExchange";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query2);

            }
            catch (SqlException sqlException)
            {

                ViewBag.Error = sqlException.ToString();
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.ToString();
            }
            return View();
        }


        //BACKUP AND TRUNCATE ARRIVEDEXCHANGE 
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter, Driver")]
        public ActionResult BackupAndTruncateArrivedMoney()
        {
            try
            {
                var context = new ZuuCargoEntities();
                Random rnd = new Random();
                rnd.Next(1, 9999);

                var query = @"BACKUP DATABASE ZuuCargo TO DISK = 'C:\\SQLServerBackups\\Backup-" + DateTime.Now.ToFileTime() + ".bak' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Backup-ZuuCargo.bak'";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);



                rnd.Next(1, 9999);
                var query2 = @"TRUNCATE TABLE ArrivedMoney";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query2);

            }
            catch (SqlException sqlException)
            {

                ViewBag.Error = sqlException.ToString();
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.ToString();
            }
            return View();
        }

        //BACKUP AND TRUNCATE ARRIVEDEXCHANGE 
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter, Driver")]
        public ActionResult BackupAndTruncateCargo()
        {
            try
            {
                var context = new ZuuCargoEntities();
                Random rnd = new Random();
                rnd.Next(1, 9999);

                var query = @"BACKUP DATABASE ZuuCargo TO DISK = 'C:\\SQLServerBackups\\Backup-" + DateTime.Now.ToFileTime() + ".bak' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Backup-ZuuCargo.bak'";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query);

              
              
                rnd.Next(1, 9999);
                var query2 = @"TRUNCATE TABLE TotalCargo";
                ViewBag.RowsAffected = context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, query2);

            }
            catch (SqlException sqlException)
            {

                ViewBag.Error = sqlException.ToString();
            }
            catch (Exception exception)
            {
                ViewBag.Error = exception.ToString();
            }
            return View();
        }

        // GET: Admin/Admin
        [CustomAuthorizeAttribute(Roles = "Admin, Accounter, Driver")]

        public ActionResult Index()
        {
            if (User.IsInRole("Driver"))
            {
                return RedirectToAction("Index", "Delivery");
            }
            UserServices userServices = new UserServices();
            ShipmentServices shipmentServices = new ShipmentServices();
            AccountingServices accountingServices = new AccountingServices();
            CargosService cargosService = new CargosService();
            RefundService refundService =  new RefundService();
            LostOrderServices lostOrderServices = new LostOrderServices();
            TotalCargoService totalCargoService = new TotalCargoService();
            KamuBorcServices kamuBorcServices = new KamuBorcServices();
            BoxsVarService boxsVarService = new BoxsVarService();
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();
            ArrivedMoneyServices arrivedMoneyServices = new ArrivedMoneyServices();
            MyBalanceServices myBalanceServices = new MyBalanceServices();
            ExpensiveServices expensiveServices = new ExpensiveServices();
            TaxiCostsServices taxiCostsServices = new TaxiCostsServices();
            ArrivedExchangeServices arrivedExchangeServices = new ArrivedExchangeServices();
            TurkishCargoServices turkishCargoServices = new TurkishCargoServices();
            KomerkServices komerkServices = new KomerkServices();
            RemainingCostServices remainingCostServices = new RemainingCostServices();

            ViewBag.userCount = userServices.GetAll().Count;
            ViewBag.ShipmentCount = shipmentServices.GetAll().Sum(x=>x.PackageCount);
            var totalAccount = accountingServices.GetAll().Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.TotalMyBalance = myBalanceServices.GetAll().Select(i => Convert.ToDouble(i.Amount)).Sum();

            ViewBag.Accounting = totalAccount - ViewBag.TotalMyBalance;
            ViewBag.Cargos = cargosService.GetAll().Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.Cargos2 = cargosService.GetAll().Select(i => Convert.ToDouble(i.TotalCargo)).Sum();
            ViewBag.Refunds = refundService.GetAll().Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.LostOrders = lostOrderServices.GetAll().Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.TotalCargo = totalCargoService.GetAll().Select(i => Convert.ToDouble(i.OrderTotal)).Sum();
            ViewBag.KamuBorcUK = kamuBorcServices.GetAll().Where(x => x.Category.Trim() == "Kamu Borc UK").Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.KamuBorcTurkey = kamuBorcServices.GetAll().Where(x=> x.Category.Trim() == "Kamu Borc Turkey").Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.BoxsVar = boxsVarService.GetAll().Select(i => Convert.ToDouble(i.OrderTotal)).Sum();
            ViewBag.TotalZuuCargo = ZuuCargoServices.GetAll().Select(i => Convert.ToDouble(i.Price)).Sum();
            ViewBag.TotalArrivedMoney = arrivedMoneyServices.GetAll().Select(i => Convert.ToDouble(i.Amount)).Sum();
            ViewBag.TotalExpensive = expensiveServices.GetAll().Select(i => Convert.ToDouble(i.Total)).Sum();
            ViewBag.TotalExchange = accountingServices.GetAll().Where(x => x.IsPaid == true).Select(i => Convert.ToDouble(i.Exchange)).Sum();
            ViewBag.TotalTaxi = taxiCostsServices.GetAll().Select(i => Convert.ToDouble(i.Total)).Sum();
            ViewBag.TotalArrivedExchange = arrivedExchangeServices.GetAll().Select(i => Convert.ToDouble(i.Total)).Sum();
            ViewBag.TurkishCargo = turkishCargoServices.GetAll().Select(i => Convert.ToDouble(i.Price)).Sum();
            ViewBag.TotalKomerk = komerkServices.GetAll().Select(i => Convert.ToDouble(i.Price)).Sum();
            ViewBag.TotalRemainingCost = remainingCostServices.GetAll().Select(x => x.RemainingCosts).Sum();

            return View();
        }

        public ActionResult AdminLogin()

        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Admin", "Admin");
            }
            else
            {
                return RedirectToAction("Login", "AdminAccount");

            }
           
        }

        [CustomAuthorizeAttribute(Roles = "Admin, Accounter")]
        public ActionResult Report(string[] function_param)
        {
            DateTime start = Convert.ToDateTime("01.01.0001");
            DateTime end = Convert.ToDateTime("01.01.3001");

            try
            {
                start = Convert.ToDateTime(function_param[0]);
                end = Convert.ToDateTime(function_param[1]);
            }
            catch (Exception)
            {


            }
            UserServices userServices = new UserServices();
            ShipmentServices shipmentServices = new ShipmentServices();
            AccountingServices accountingServices = new AccountingServices();
            CargosService cargosService = new CargosService();
            RefundService refundService = new RefundService();
            LostOrderServices lostOrderServices = new LostOrderServices();
           TotalCargoService totalCargoService = new TotalCargoService();
            KamuBorcServices kamuBorcServices = new KamuBorcServices();
            BoxsVarService boxsVarService = new BoxsVarService();
            ZuuCargoServices ZuuCargoServices = new ZuuCargoServices();

            ViewBag.userCount = userServices.GetAll().Count;
            ViewBag.ShipmentCount = shipmentServices.GetAll().Where(x => x.ShipmentDate >= start && x.ShipmentDate <= end).Count();
            ViewBag.Accounting = accountingServices.GetAll().Where(x => x.Date >= start && x.Date <= end).Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.Cargos = cargosService.GetAll().Where(x => x.Date >= start && x.Date <= end).Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.Cargos2 = cargosService.GetAll().Where(x => x.Date >= start && x.Date <= end).Select(i => Convert.ToDouble(i.TotalCargo)).Sum();
            ViewBag.Refunds = refundService.GetAll().Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.LostOrders = lostOrderServices.GetAll().Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.totalCargo = totalCargoService.GetAll().Where(x => x.Date >= start && x.Date <= end).Select(i => Convert.ToDouble(i.OrderTotal)).Sum();
            ViewBag.KamuBorcUK = kamuBorcServices.GetAll().Where(x => x.Date >= start && x.Date <= end).Where(x => x.Category.Trim() == "Kamu Borc UK").Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.KamuBorcTurkey = kamuBorcServices.GetAll().Where(x => x.Date >= start && x.Date <= end).Where(x => x.Category.Trim() == "Kamu Borc Turkey").Select(i => Convert.ToDouble(i.TotalDollar)).Sum();
            ViewBag.BoxsVar = boxsVarService.GetAll().Where(x => x.Date >= start && x.Date <= end).Select(i => Convert.ToDouble(i.OrderTotal)).Sum();
            ViewBag.TotalZuuCargo = ZuuCargoServices.GetAll().Where(x => x.Date >= start && x.Date <= end).Select(i => Convert.ToDouble(i.Price)).Sum();
            return View();
        }

        }
}