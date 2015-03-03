int totalCount = 17;
int currentPage = 0;
decimal perPageCount = 50;
int i = 0;
while (i <= totalCount && totalCount != 0)
{
if (i % perPageCount == 0) // increase page by 1, this could have been handled in many ways
    {
        currentPage++;

        url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" + currentPage; 

        JsonString = getResponse(url);

        JsonObject = Decode(JsonString);
        
        for (page_number == 1 && page_number == total_event%50)
            current_event = JsonOject.event[0 -> total_event%50]; <-- Here is the place we have no common.

        for (page_number -> 2 to page_count - 1)
            current_event = JsonOject.event[0 -> 50]


        if (current_event.status == "alive")
            List.add(current_event)


        i++;
    }
return List
}
