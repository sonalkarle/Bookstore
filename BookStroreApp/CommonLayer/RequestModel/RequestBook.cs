using CommonLayer.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class RequestBook : BookModel
    {
        public IFormFile BookImage { get; set; }
    }
}
