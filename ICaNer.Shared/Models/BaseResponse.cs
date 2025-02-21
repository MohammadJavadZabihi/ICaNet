namespace ICaNer.Shared.Models
{
    public abstract class BaseResponse : BaseMessage
    {
        public BaseResponse(Guid correaltionId) : base()
        {
            _correlationId = correaltionId;
        }

        protected BaseResponse()
        {

        }
    }
}
