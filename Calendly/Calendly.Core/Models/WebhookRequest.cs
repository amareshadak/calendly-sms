using System;
using System.Collections.Generic;

namespace Calendly.Core.Models
{
    /// <summary>
    /// Request sent by the webhook.  Leaving all classes in one file for the sake of time
    /// </summary>
    public class WebhookRequest
    {
        public string @event { get; set; }
        public DateTime time { get; set; }
        public Payload payload { get; set; }
    }

    public class Owner
    {
        public string type { get; set; }
        public string uuid { get; set; }
    }

    public class EventType
    {
        public string uuid { get; set; }
        public string kind { get; set; }
        public string slug { get; set; }
        public string name { get; set; }
        public int duration { get; set; }
        public Owner owner { get; set; }
    }

    public class ExtendedAssignedTo
    {
        public string name { get; set; }
        public string email { get; set; }
        public bool primary { get; set; }
    }

    public class Event
    {
        public string uuid { get; set; }
        public List<string> assigned_to { get; set; }
        public List<ExtendedAssignedTo> extended_assigned_to { get; set; }
        public DateTime start_time { get; set; }
        public string start_time_pretty { get; set; }
        public DateTime invitee_start_time { get; set; }
        public string invitee_start_time_pretty { get; set; }
        public DateTime end_time { get; set; }
        public string end_time_pretty { get; set; }
        public DateTime invitee_end_time { get; set; }
        public string invitee_end_time_pretty { get; set; }
        public DateTime created_at { get; set; }
        public string location { get; set; }
        public bool canceled { get; set; }
        public object canceler_name { get; set; }
        public object cancel_reason { get; set; }
        public object canceled_at { get; set; }
    }

    public class Payment
    {
        public string id { get; set; }
        public string provider { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string terms { get; set; }
        public bool successful { get; set; }
    }

    public class Invitee
    {
        public string uuid { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string text_reminder_number { get; set; }
        public string timezone { get; set; }
        public DateTime created_at { get; set; }
        public bool is_reschedule { get; set; }
        public List<Payment> payments { get; set; }
        public bool canceled { get; set; }
        public object canceler_name { get; set; }
        public object cancel_reason { get; set; }
        public object canceled_at { get; set; }
    }

    public class QuestionsAndAnswer
    {
        public string question { get; set; }
        public string answer { get; set; }
    }

    public class QuestionsAndResponses
    {
        public string __invalid_name__1_question { get; set; }
        public string __invalid_name__1_response { get; set; }
        public string __invalid_name__2_question { get; set; }
        public string __invalid_name__2_response { get; set; }
        public string __invalid_name__3_question { get; set; }
        public string __invalid_name__3_response { get; set; }
        public string __invalid_name__4_question { get; set; }
        public string __invalid_name__4_response { get; set; }
    }

    public class Tracking
    {
        public object utm_campaign { get; set; }
        public object utm_source { get; set; }
        public object utm_medium { get; set; }
        public object utm_content { get; set; }
        public object utm_term { get; set; }
        public object salesforce_uuid { get; set; }
    }

    public class Payload
    {
        public EventType event_type { get; set; }
        public Event @event { get; set; }
        public Invitee invitee { get; set; }
        public List<QuestionsAndAnswer> questions_and_answers { get; set; }
        public QuestionsAndResponses questions_and_responses { get; set; }
        public Tracking tracking { get; set; }
        public object old_event { get; set; }
        public object old_invitee { get; set; }
        public object new_event { get; set; }
        public object new_invitee { get; set; }
    }
}
