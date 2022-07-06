using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Courses.Services.Catalog.Models
{
    public class Category
    {
        //MongoDB üretecek Id yi gerçek bir id olarak algılaması için
        [BsonId]
        //tipini belirtmek için kullanıyoruz.
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
