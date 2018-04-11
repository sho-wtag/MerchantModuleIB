using MerchantPortal.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MerchantPortal.WebApi.Controllers
{
    public class TransactionController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("api/transaction/selltransaction")]
        public IHttpActionResult GetSellTransaction([FromBody]JObject data)
        {
            RequestSellTransaction sellTransaction= data["RequestSellTransaction"].ToObject<RequestSellTransaction>();

            ResponseSellTransaction response = new ResponseSellTransaction();
            MerchantIBEntities proc_sell_trans = new MerchantIBEntities();

            response.SellTransaction = proc_sell_trans.Get_Sell_Transaction(sellTransaction.MerchantID, sellTransaction.TerminalID, sellTransaction.CurrencyCode, sellTransaction.MerTransactionID, sellTransaction.LanguageCode, sellTransaction.MerRefID, sellTransaction.MerVar1, sellTransaction.MerVar2, sellTransaction.MerVar3, sellTransaction.MerVar4, sellTransaction.MerVar5, sellTransaction.MerVar6, sellTransaction.MerVar7, sellTransaction.MerVar8, sellTransaction.ProductType, sellTransaction.ReturnURL, sellTransaction.TxnAmount);
            if (response.SellTransaction == null)
            {
                return NotFound();
            }
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/transaction/selltransactionverify")]
        public IHttpActionResult SellTransactionVerify([FromBody]JObject data)
        {
            RequestSellTrnxnVerify sellTransactionVerify = data["RequestSellTrnxnVerify"].ToObject<RequestSellTrnxnVerify>();

            ResponseSellTrnxnVerify response = new ResponseSellTrnxnVerify();
            MerchantIBEntities proc_sell_trans = new MerchantIBEntities();

            response.SellTransactionVerify = proc_sell_trans.Verify_Sell_Transaction(sellTransactionVerify.Action, sellTransactionVerify.MerchantID, sellTransactionVerify.MerRefID);
            if (response.SellTransactionVerify == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

    }
}
