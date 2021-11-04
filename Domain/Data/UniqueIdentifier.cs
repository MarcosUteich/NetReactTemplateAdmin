using MongoDB.Bson;

namespace Domain.Data
{
    public static class UniqueIdentifier
    {
        public static string New => ObjectId.GenerateNewId().ToString();
    }
}
