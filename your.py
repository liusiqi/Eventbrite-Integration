event_totle = 147;

for i in range(0, 147):
    print(str(i//50 + 1) + ":" + str(i%50))
    #page_number = i//50
    #event_number = i%50

    #url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" + str(page_number);
    #        JsonString = getResponse(url);
    #        JsonObject = Decode(JsonString);
    #        current_event = JsonObject.events[event_number];
    #        if (current_event.status == "alive")
    #            List.add(current_event)
