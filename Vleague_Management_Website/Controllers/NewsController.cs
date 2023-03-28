﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vleague_Management_Website.Models;
using X.PagedList;

namespace Vleague_Management_Website.Controllers
{
    public class NewsController : Controller
    {
        QlbongDaContext db = new QlbongDaContext();
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstnews = db.TinTucs.AsNoTracking().OrderBy(x => x.TieuDe);
            PagedList<TinTuc> lst = new PagedList<TinTuc>(lstnews, pageNumber, pageSize);

            return View(lst);
        }
        public IActionResult DetailNews (string Id)
        {
            var TinTuc = db.TinTucs.SingleOrDefault(x=>x.TinTucId==Id);
            
            return View(TinTuc);
        }
    }
}
