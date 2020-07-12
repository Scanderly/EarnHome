using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EarnHome.Models;
using Microsoft.AspNet.SignalR;

namespace EarnHome.Hubs
{
    public class SendRequestHub : Hub
    {
        EarnHomeEntities db = new EarnHomeEntities();
        User Users = new User();
        Request Request = new Request();
        public void Send(string userId,  string orderId)
        {
            // Call the addNewMessageToPage method to update clients.
           
            Clients.Client("orderUserId").addNewRequest(userId, orderId);
            
            Order order = db.Orders.Find(orderId);
            Request requestBase = new Request
            {
                UserId = Convert.ToInt32(userId),
                OrderId = Convert.ToInt32(orderId)
            };
            db.Requests.Add(requestBase);
            //ViewBag.Req = requestBase;
            db.SaveChanges();
            
            Notification notification = new Notification
            {
                Read = false,
                Text = order.User.Fullname + " size sorgu gonderib",
                UserId = order.UserId,
            };
            db.Notifications.Add(notification);
            db.SaveChanges();
            
        }
        public void Connect(int userId, int orderId)
        {
            var id = Request.ConnectionId;
           db.Requests.Add(new Request { ConnectionId = id, UserId = userId,OrderId=orderId });

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userId, orderId);

               
            
        }
    }
}