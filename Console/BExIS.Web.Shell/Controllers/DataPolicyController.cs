﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vaiona.Web.Extensions;
using Vaiona.Web.Mvc.Data;
using Vaiona.Web.Mvc.Models;

namespace BExIS.Web.Shell.Controllers
{
    public class DataPolicyController : Controller
    {
        // GET: terms
        [DoesNotNeedDataAccess]
        public ActionResult Index()
        {
            ViewBag.Title = PresentationModel.GetViewTitleForTenant("Data Policy", this.Session.GetTenant());
            return View();
        }
    }
}