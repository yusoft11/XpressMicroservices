using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XpressUserMgt.Web.Models;

namespace XpressUserMgt.Web.Controllers
{
    public class OptimizationController : Controller
    {
        private readonly ApplicationDBContext _context;
        public OptimizationController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        
        {
            // Without optimization

            var groups = _context.Groups.ToList();

            // With optimization (LINQ)

            var Optgrps = _context.Groups.Select(a => new { GroupName = a.GroupName, LeaderName = a.LeaderName }).ToList();

            // With optimization (SQL)

            string sql1 = "select GROUP_NAME GroupName, LEADER_NAME LeaderName from groups";

            var OptSqlGrps = _context.GroupItems.FromSqlRaw(sql1);

            // Without optimization
            var grp = _context.Groups.ToList();

            //With optimization (LINQ)
            var Optgrp = _context.Groups.OrderBy(y => y.Id).ToList();

            //With optimization (SQL)
            string sql2 = "select * from groups order by groupid";
            var _Itemgrps = _context.GroupItemAllRecords.FromSqlRaw(sql2);

            //With optimization (LINQ)
            var _Optgrp = _context.Groups.OrderBy(y => y.Id).Take(2).ToList();

            //With optimization (SQL)
            string sql3 = "select top 2 * from groups order by groupid";
            var Itemgrps = _context.GroupItemAllRecords.FromSqlRaw(sql3);
            return View();
        }
    }
}
