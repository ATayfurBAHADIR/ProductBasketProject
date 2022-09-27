using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        [BsonId]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public bool Active { get; set; }
        public string PictureUrl { get; set; }


//        {
//"Name": "Test Ürün Ad",
//"Description": "Test Ürün Açıklama",
//"Quantity":"32",
//"UnitPrice":"1905",
//"Active":"true",
//"PictureUrl":"Yol"
//}

    //[JsonConstructor]
    //public Product(string name, int? number, string description, double? price, bool active)
    //{
    //    Name = name;
    //    Price = price;
    //    Number = number;
    //    Active = active;
    //    Description = description;
    //    Id = Guid.NewGuid();
    //}
    //public Product(Guid id, int? number, string name, double? price, bool active, string description)
    //{
    //    Id = id;
    //    Name = name;
    //    Number = number;
    //    Price = price;
    //    Active = active;
    //    Description = description;
    //}

}
}
