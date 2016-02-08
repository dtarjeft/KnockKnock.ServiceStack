using System;
using System.Collections;
using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/knocks/{Id}", "GET")]
    public class KnockGetV1 : IReturn<KnockDto>
    {
        public long Id { get; set; }
    }

    [Route("/api/v1/knocks/latitude/{Latitude}/longitude/{Longitude}/radius/{Radius}", "GET")]
    public class ByLocationGetV1 : IReturn<ICollection<KnockDto>>
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
    }
}