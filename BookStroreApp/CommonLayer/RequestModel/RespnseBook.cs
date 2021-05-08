using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
   public  class RespnseBook: BookModel
    {
        
            public long BookID { get; set; }
            public string BookImage { get; set; }
            public bool InStock { get; set; }
            public bool InCart { get; set; }
        
    }
}
