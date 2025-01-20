
using System.ComponentModel.DataAnnotations;

namespace EventEase.Models

{

    public class EventModel
    {
        
        [Required]
        public required string EventName { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public required string Location { get; set; }

        [Required]
        public required string Description { get; set; }
    }

}
