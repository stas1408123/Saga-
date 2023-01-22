using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace WareHouse.OrderService.Domain.Serializers
{
    public class BsonDateOnlySerializer : StructSerializerBase<DateOnly>
    {
        public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            if (context.Reader.CurrentBsonType == BsonType.Null)
                return default(DateOnly);

            return base.Deserialize(context, args);
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            if (value is null)
                context.Writer.WriteNull();
            else
                base.Serialize(context, args, (DateOnly)value);
        }
    }
}
