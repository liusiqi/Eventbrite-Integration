page_count = N;
event_total = M;

for page_number in range(1, page_count):
    url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" + currentPage;
    JsonString = getResponse(url);
    JsonObject = Decode(JsonString);
    if (page_count == 1)
        for event_number in range(0, event_total)
            current_event = JsonObject.events[event_number];
            if (current_event.status == "alive")
                List.add(current_event)
    else if (page_count >= 2)
        for event_number in range(0, 50)
            current_event = JsonObject.events[event_number];
            if (current_event.status == "alive")
                List.add(current_event)
        for page_number in range(2, page_count - 1)
            url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" + str(2);
            JsonString = getResponse(url);
            JsonObject = Decode(JsonString);
            for event_number in range(0,50)
                current_event = JsonObject.events[event_number];
                if (current_event.status == "alive")
                    List.add(current_event)
        url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" + str(page_count);
            JsonString = getResponse(url);
            JsonObject = Decode(JsonString);
            for event_number in range(0,event_total%50)
                current_event = JsonObject.events[event_number];
                if (current_event.status == "alive")
                    List.add(current_event)
