#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using AutoMapper;
using Admin.ViewModels;
using Dapper;
using Newtonsoft.Json;

namespace Admin.Controllers
{
    public class PaymentHistoriesController : Controller
    {
        private readonly MovieTicketBookingContext _context;
        private readonly IMapper _mapper;

        public PaymentHistoriesController( MovieTicketBookingContext context, IMapper mapper ) {
            _context = context;
            _mapper = mapper;
        }

        // GET: Payment Histories
        public async Task<IActionResult> Index() 
        {
            var q = @"select q = JSON_QUERY((
                        select * from Payments p
                        cross apply(
	                        select u.UserName as Username from AspNetUsers u
	                        where u.Id = p.UserId
                        )u
                        cross apply(
	                        select m.Title as Movie from Movies m
	                        where m.Id = p.MovieId
                        )m
                        cross apply(
	                        select r.RoomNo from Rooms r
	                        where r.Id = p.RoomId
                        )r
                        cross apply(
	                        select dt.Date as ShowDate, dt.Time as ShowTime from ReleasedDateTimes dt
	                        where dt.Id = p.ReleasedDateTimeId
                        )dt
	                    for json path
                    ))";

            var dataJson = _context.Database.GetDbConnection().QueryFirst<string>( q );
            var resultList = JsonConvert.DeserializeObject<PaymentHistoryViewModel[]>( dataJson ?? "[]" );
            await Task.CompletedTask;
            return View( resultList );
        }
    }
}
