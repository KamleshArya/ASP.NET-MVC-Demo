using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace MvcAuction.Models
{
    public class Auction
    {
        [Required]
        public long Id {get; set;}

        [Required]
        [DataType(DataType.Text)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength:200,MinimumLength=5)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Start Price")]
        public decimal StartPrice { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Current Time")]
        public decimal? CurrentPrice { get; set; }

        //property virtual so that entity framewok can overiride it when retrive data from db
        //setter private becoz we dont want other than this class and entity framework to set the property
        public virtual Collection<Bid> Bids{get; private set;}

        public int BidCount
        {
            get { return Bids.Count; }
        }

        //Initialize bids collection to ensure that it never be null
        public Auction()
        {
            Bids = new Collection<Bid>();
        }
    }
}