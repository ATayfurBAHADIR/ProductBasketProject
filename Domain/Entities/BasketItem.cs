using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [DataContract(Name = "BasketItems")]
    public class BasketItem
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string ProductId { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public double UnitPrice { get; set; }
        [DataMember]
        public double OldUnitPrice { get; set; }
        [DataMember]
        public string PictureUrl { get; set; }

        // Quantity yani sepetteki ürün sayısı 1'den küçükse hata üreteceğiz.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (Quantity < 1)
            {
                results.Add(new ValidationResult("Invalid number of units", new[] { "Quantity" }));
            }

            return results;
        }

    }
}
