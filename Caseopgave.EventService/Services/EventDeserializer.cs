using Confluent.Kafka;
using System.Text;

namespace Caseopgave.EventService.Services;

public class EventDeserializer : IDeserializer<Event>
{
    public Event Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonConvert.DeserializeObject<Event>(Encoding.UTF8.GetString(data));
    }
}