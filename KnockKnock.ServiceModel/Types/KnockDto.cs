using System;

namespace KnockKnock.ServiceModel.Types
{
    public interface IHasId
    {
        long Id { get; set; }
    }

    public class KnockDto : IHasId
    {
        public string FeedId { get; set; }

        public byte[] Content { get; set; }

        public string Message { get; set; }
        
        public LocationDto Location { get; set; }
        public virtual long Id { get; set; }
    }
}