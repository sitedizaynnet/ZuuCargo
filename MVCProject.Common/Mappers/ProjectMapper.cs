using AutoMapper;
using MVCProject.Common.ViewModels;
using MVCProject.Entities;
using System;
using System.Collections.Generic;
using MVCProject.Common.Extentions;
using MVCProject.Models;

namespace MVCProject.Common.Mappers
{
    public static class ProjectMapper
    {
        static ProjectMapper()
        {
            #region Mappings
 

          

           

            Mapper.CreateMap<SubMenus, SubMenusVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<SubMenusVM, SubMenus>().IgnoreAllNonExisting();

            Mapper.CreateMap<Menus, MenusVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<MenusVM, Menus>().IgnoreAllNonExisting();

          
            Mapper.CreateMap<Category, CategoryVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<CategoryVM, Category>().IgnoreAllNonExisting();

         

            Mapper.CreateMap<Comment, CommentVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<CommentVM, Comment>().IgnoreAllNonExisting();

            Mapper.CreateMap<Order, OrderVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<OrderVM, Order>().IgnoreAllNonExisting();


            Mapper.CreateMap<Shipment, ShipmentVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ShipmentVM, Shipment>().IgnoreAllNonExisting();

            Mapper.CreateMap<Price, PriceVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<PriceVM, Price>().IgnoreAllNonExisting();


            //İL İLÇE VERİLER


            Mapper.CreateMap<Sehirler, SehirlerVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<SehirlerVM, Sehirler>().IgnoreAllNonExisting();

            Mapper.CreateMap<Ilceler, IlcelerVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<IlcelerVM, Ilceler>().IgnoreAllNonExisting();

            Mapper.CreateMap<Semt, SemtVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<SemtVM, Semt>().IgnoreAllNonExisting();

          

            Mapper.CreateMap<Message, MessageVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<MessageVM, Message>().IgnoreAllNonExisting();

            Mapper.CreateMap<Reply, ReplyVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ReplyVM, Reply>().IgnoreAllNonExisting();

            Mapper.CreateMap<Zone, ZoneVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ZoneVM, Zone>().IgnoreAllNonExisting();

            Mapper.CreateMap<StatusUpdates, StatusUpdatesVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<StatusUpdatesVM, StatusUpdates>().IgnoreAllNonExisting();

            Mapper.CreateMap<Accounting, AccountingVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<AccountingVM, Accounting>().IgnoreAllNonExisting();

            Mapper.CreateMap<AccountingProducts, AccountingProductsVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<AccountingProductsVM, AccountingProducts>().IgnoreAllNonExisting();

            Mapper.CreateMap<Refund, RefundVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<RefundVM, Refund>().IgnoreAllNonExisting();

            Mapper.CreateMap<LostOrder, LostOrderVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<LostOrderVM, LostOrder>().IgnoreAllNonExisting();
            Mapper.CreateMap<Cargos, CargosVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<CargosVM, Cargos>().IgnoreAllNonExisting();

            Mapper.CreateMap<TotalCargo , TotalCargoVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<TotalCargoVM, TotalCargo>().IgnoreAllNonExisting();

            Mapper.CreateMap<KamuBorc, KamuBorcVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<KamuBorcVM, KamuBorc>().IgnoreAllNonExisting();


            Mapper.CreateMap<BoxsVar, BoxsVarVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<BoxsVarVM, BoxsVar>().IgnoreAllNonExisting();

            Mapper.CreateMap<ZuuCargo, ZuuCargoVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ZuuCargoVM, ZuuCargo>().IgnoreAllNonExisting();

            Mapper.CreateMap<Deposit, DepositVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<DepositVM, Deposit>().IgnoreAllNonExisting();


            Mapper.CreateMap<GpsData, GpsDataVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<GpsDataVM, GpsData>().IgnoreAllNonExisting();


            Mapper.CreateMap<ArrivedMoney, ArrivedMoneyVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ArrivedMoneyVM, ArrivedMoney>().IgnoreAllNonExisting();

            Mapper.CreateMap<CustomerLocation, CustomerLocationVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<CustomerLocationVM, CustomerLocation>().IgnoreAllNonExisting();


            Mapper.CreateMap<MyBalance, MyBalanceVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<MyBalanceVM, MyBalance>().IgnoreAllNonExisting();


            Mapper.CreateMap<Expensive, ExpensiveVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ExpensiveVM, Expensive>().IgnoreAllNonExisting();

            Mapper.CreateMap<TaxiCosts, TaxiCostsVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<TaxiCostsVM, TaxiCosts>().IgnoreAllNonExisting();


            Mapper.CreateMap<ArrivedExchange, ArrivedExchangeVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ArrivedExchangeVM, ArrivedExchange>().IgnoreAllNonExisting();
            #endregion

            Mapper.CreateMap<TurkishCargo, TurkishCargoVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<TurkishCargoVM, TurkishCargo>().IgnoreAllNonExisting();


            Mapper.CreateMap<Komerk, KomerkVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<KomerkVM, Komerk>().IgnoreAllNonExisting();

            Mapper.CreateMap<ShipmentBarcodes, ShipmentBarcodesVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ShipmentBarcodesVM, ShipmentBarcodes>().IgnoreAllNonExisting();

            Mapper.CreateMap<ShipmentTurpexBarcodes, ShipmentTurpexBarcodesVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ShipmentTurpexBarcodesVM, ShipmentTurpexBarcodes>().IgnoreAllNonExisting();

            Mapper.CreateMap<Reklam, ReklamVM>().IgnoreAllNonExisting();
            Mapper.CreateMap<ReklamVM, Reklam>().IgnoreAllNonExisting();
        }



        /// <summary>
        /// Convert To VM
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TDestination ConvertToVM<TDestination>(object from) where TDestination : BaseVM
        {
            return Mapper.Map<TDestination>(from);
        }

        /// <summary>
        /// Convert To VM OR
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TDestination ConvertToVM<TSource, TDestination>(TSource from, TDestination to)
            where TSource : class
            where TDestination : BaseVM
        {
            if (to == null || to == default(TDestination))
                to = (TDestination)Activator.CreateInstance(typeof(TDestination));

            return Mapper.Map<TSource, TDestination>(from, to);
        }


        /// <summary>
        /// Convert To VM List
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TDestination ConvertToVMList<TDestination>(IEnumerable<object> from) where TDestination : IEnumerable<object>
        {
            return Mapper.Map<TDestination>(from);
        }


        /// <summary>
        /// Convert To Entity
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TDestination ConvertToEntity<TDestination>(BaseVM from)
            where TDestination : class
        {
            return Mapper.Map<TDestination>(from);
        }
        /// <summary>
        /// Convert To Entity OR
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>

        public static TDestination ConvertToEntity<TSource, TDestination>(TSource from, TDestination to)
            where TDestination : BaseVM
            where TSource : class
        {
            if (to == null || to == default(TDestination))
                to = (TDestination)Activator.CreateInstance(typeof(TDestination));

            return Mapper.Map<TSource, TDestination>(from, to);
        }

        /// <summary>
        /// Convert To Entity List
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TDestination ConvertToEntityList<TDestination>(IEnumerable<BaseVM> from)
            where TDestination : IEnumerable<object>
        {
            return Mapper.Map<TDestination>(from);
        }


        internal static TDestination ConvertTo<TDestination>(object from)
            where TDestination : class
        {
            return Mapper.Map<TDestination>(from);
        }

    }
}
