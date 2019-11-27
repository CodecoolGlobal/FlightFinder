namespace FlightFinder.Models
{
    public class BookingDetailsLink
    {
        public string Body { get; set; }
        public string Method { get; set; }
        public string Uri { get; set; }

        public BookingDetailsLink(string uri)
        {
            Uri = uri;
        }
    }
}