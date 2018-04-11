using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    //public class Message
    //{
    //    public string Text { get; set; }
    //}
    interface IMessage
    {
        string MessageText
        {
            get;
            set;
        }
    }
}
