namespace ICaNet.API
{
    public abstract class BaseResponse : BaseMessage
    {
        public BaseResponse(Guid correaltionId) : base()
        {
            base._correlationId = correaltionId;
        }

        protected BaseResponse()
        {
            
        }
    }
}
