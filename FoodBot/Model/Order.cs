using Microsoft.Bot.Schema;
using System;

namespace FoodBot.Model {
    public class Order : IEquatable<Order> {

        public ChannelAccount Account { get; set; }

        public string OrderDetail { get; set; }

        public bool Equals(Order other) {
            return this.Account.Id == other.Account.Id;
        }

    }
}

// can haz/s
// i want

// clear
// get lunch

// kill