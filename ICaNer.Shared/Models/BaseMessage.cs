namespace ICaNer.Shared.Models
{
    public abstract class BaseMessage
    {
        protected Guid _correlationId = Guid.NewGuid();
        public Guid CoorelationId() => _correlationId;
    }
}
